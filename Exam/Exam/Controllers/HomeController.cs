using System.Diagnostics;
using System.Threading.Tasks;
using Exam.Data;
using Exam.Models;
using Exam.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;

        public HomeController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Collections> collections = await _context.Collections.ToListAsync();
            HomeVM homeVM = new()
            {
                Collections = collections,
            };
            return View(homeVM);
        }

       
    }
}
