using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using ServerNetCore.Models;
using Microsoft.AspNetCore.Identity;
using ServerNetCore.ViewModels;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ServerNetCore.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Content("Успешно");
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var error in result.Errors)
                    {
                        sb.AppendLine(error.Description);
                    }

                    return Content(sb.ToString());
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        sb.AppendLine(error.ErrorMessage);
                    }
                }
                return Content(sb.ToString());
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList().Select(x => x.Email);
            return Content(JsonConvert.SerializeObject(users));
        }
    }
}