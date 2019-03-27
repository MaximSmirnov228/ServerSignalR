using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ServerNetCore.Models;
using Microsoft.AspNetCore.Identity;
using ServerNetCore.ViewModels;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net;
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

        //[Route("api/[controller]/GetToken")]
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
                    sb.AppendLine("Ошибки");
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
                return Content("Не валидная модель" + sb.ToString());
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