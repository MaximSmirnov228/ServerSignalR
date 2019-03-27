using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServerNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return Content("hello");
        }

        [AllowAnonymous]
        public IActionResult IndexAnon()
        {
            return Content("hello");
        }
    }
}