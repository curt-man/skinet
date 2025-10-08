using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skinet.API.Controllers
{
    public class FallbackController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}
