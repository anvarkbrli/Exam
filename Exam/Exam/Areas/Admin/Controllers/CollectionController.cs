using Exam.Areas.Admin.ViewModels;
using Exam.Data;
using Exam.Models;
using Exam.Utilities.Enums;
using Exam.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CollectionController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        public CollectionController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Index()
        {
            List<GetVM> getVM = await _context.Collections.Select(c => new GetVM
            {
                Id = c.Id,
                Name = c.Name,
                Stock = c.Stock,
                Image = c.Image,
                Category = c.Category
            }).ToListAsync();
            return View(getVM);
        }
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateVM createVM)
        {
            if (!ModelState.IsValid) return View(createVM);

            bool existName = await _context.Collections.AnyAsync(n => n.Name == createVM.Name.Trim());
            if (existName)
            {
                ModelState.AddModelError(nameof(createVM.Name), "This name already exists!");
                return View(createVM);
            }

            if (!createVM.Photo.CheckType("image/"))
            {
                ModelState.AddModelError(nameof(createVM.Photo), "Type of this file is incorrect, you can only upload image!");
                return View(createVM);
            }
            if (createVM.Photo.CheckSize(5, FileSize.MB))
            {
                ModelState.AddModelError(nameof(createVM.Photo), "Size of this image is too large, maximum size is 5 MB!");
                return View(createVM);
            }

            Collections collections = new()
            {
                Name = createVM.Name,
                Stock = createVM.Stock,
                Category = createVM.Category,
                Image = await createVM.Photo.CreateFile(_env.WebRootPath, "assets", "images")
            };

            _context.Collections.Add(collections);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();

            Collections? collections = await _context.Collections.FirstOrDefaultAsync(c => c.Id == id);
            if (collections is null) return NotFound();

            UpdateVM updateVM = new()
            {
                Name = collections.Name,
                Image = collections.Image,
                Category = collections.Category,
                Stock = collections.Stock
            };
            return View(updateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateVM updateVM)
        {
            if (id is null) return BadRequest();

            Collections? collections = await _context.Collections.FirstOrDefaultAsync(c => c.Id == id);
            if (collections is null) return NotFound();

            if (!ModelState.IsValid) return View(updateVM);

            if(updateVM.Photo != null)
            {
                if (!updateVM.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError(nameof(updateVM.Photo), "Type of this file is incorrect, you can only upload image!");
                    return View(updateVM);
                }
                if (updateVM.Photo.CheckSize(5, FileSize.MB))
                {
                    ModelState.AddModelError(nameof(updateVM.Photo), "Size of this image is too large, maximum size is 5 MB!");
                    return View(updateVM);
                }
                collections.Image.DeleteFile(_env.WebRootPath, "assets", "images");
                collections.Image = await updateVM.Photo.CreateFile(_env.WebRootPath, "assets", "images");
            }

            collections.Name = updateVM.Name;
            collections.Stock = updateVM.Stock;
            collections.Category = updateVM.Category;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Collections? collections = await _context.Collections.FirstOrDefaultAsync(c=>c.Id == id);
            if (collections is null) return NotFound();

            collections.Image.DeleteFile(_env.WebRootPath, "assets", "images");
             _context.Collections.Remove(collections);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();

            Collections? collections = await _context.Collections.FirstOrDefaultAsync(c => c.Id == id);
            if (collections is null) return NotFound();

            DetailsVM detailsVM = new()
            {
                Name = collections.Name,
                Category = collections.Category,
                Stock = collections.Stock,
                Image = collections.Image
            };
            return View(detailsVM);
        }
    }
}
