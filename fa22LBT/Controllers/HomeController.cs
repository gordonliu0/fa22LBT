using Microsoft.AspNetCore.Mvc;

namespace fa22LBT.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}