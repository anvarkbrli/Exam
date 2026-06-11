using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        [Authorize(Roles ="Admin,Moderator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
