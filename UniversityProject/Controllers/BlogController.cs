using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UniversityProject.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
