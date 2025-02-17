using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
