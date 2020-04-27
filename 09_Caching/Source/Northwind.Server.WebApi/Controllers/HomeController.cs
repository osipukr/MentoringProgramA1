using Microsoft.AspNetCore.Mvc;

namespace Northwind.Server.WebApi.Controllers
{
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => RedirectPermanent("/swagger");
    }
}