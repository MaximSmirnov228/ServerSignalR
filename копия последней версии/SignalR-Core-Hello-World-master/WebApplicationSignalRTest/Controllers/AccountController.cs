﻿using Microsoft.AspNetCore.Http;
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

namespace ServerNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;

        public AccountController(UserManager<Person> userManager, SignInManager<Person> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Person user = new Person { Email = model.Email, UserName = model.Email, Year = model.Year };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // установка token

                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
    }

    //[Route("api/[controller]")]
    //[ApiController]
    //public class AccountController : ControllerBase
    //{
    //    private List<Person> people = new List<Person>()
    //    {
    //        new Person{Login = "admin@gmail.com",Password = "12345", Role = "admin"},
    //        new Person{Login = "qwerty",Password = "55555", Role = "user"},
    //        new Person{Login = "jon",Password = "1", Role = "user"},
    //    };

    //    [HttpPost("/token")]
    //    public async Task Token()
    //    {
    //        var userName = Request.Form["username"];
    //        var password = Request.Form["password"];

    //        var identity = GetIdentity(userName, password);

    //        if (identity == null)
    //        {
    //            Response.StatusCode = 400;
    //            await Response.WriteAsync("Invalid username or password");
    //            return;
    //        }

    //        var now = DateTime.UtcNow;

    //        // создаем JWT токен
    //        var jwt = new JwtSecurityToken(
    //            AuthOptions.Issuer,
    //            AuthOptions.Audience,
    //            notBefore: now,
    //            claims: identity.Claims,
    //            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LifeTime)),
    //            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

    //        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

    //        var response = new
    //        {
    //            access_token = encodedJwt,
    //            username = identity.Name
    //        };

    //        // Сериализация ответа

    //        Response.ContentType = "application/json";
    //        await Response.WriteAsync(JsonConvert.SerializeObject(response,
    //            new JsonSerializerSettings { Formatting = Formatting.Indented }));
    //    }

    //    private ClaimsIdentity GetIdentity(string userName, string password)
    //    {
    //        Person person = people.FirstOrDefault(x => x.Login == userName && x.Password == password);

    //        if (person != null)
    //        {
    //            var claims = new List<Claim>
    //            {
    //                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
    //                new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
    //            };
    //            ClaimsIdentity claimsIdentity =
    //                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
    //                    ClaimsIdentity.DefaultRoleClaimType);
    //            return claimsIdentity;
    //        }

    //        // если пользователя не найдено
    //        return null;
    //    }
    //}
}