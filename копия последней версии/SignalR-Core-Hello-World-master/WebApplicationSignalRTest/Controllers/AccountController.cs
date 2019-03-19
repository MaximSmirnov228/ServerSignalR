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

namespace ServerNetCore.Controllers
{
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Person> _userManager;

        public AccountController(UserManager<Person> userManager)
        {
            _userManager = userManager;
        }

        //private List<Person> people = new List<Person>()
        //{
        //    //new Person{UserName = "admin@gmail.com",PasswordHash = "12345"},
        //    //new Person{UserName = "qwerty",PasswordHash = "55555"},
        //    //new Person{UserName  = "jon",PasswordHash = "1"},
        //};
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var client = new HttpClient();
            var customer = new Person { Email = "admin@gmail.com", Password = "Qwerty12345&" };

            var customerJson = JsonConvert.SerializeObject(customer);
            var response = await client.PostAsync(
                "http://localhost:4815/api/Customer",
                new StringContent(customerJson, Encoding.UTF8, "application/json"));

            //just some template output to test which I'm getting back.
            string resultJson = "{ 'Name':'adam'}";

            if (response.StatusCode == HttpStatusCode.OK) resultJson = await response.Content.ReadAsStringAsync();
            var updatedCustomer = JsonConvert.DeserializeObject(resultJson);
        }

        return resultJson;
    }
}

//    var userName = Request.Form["username"];
//    var password = Request.Form["password"];

//    var identity = GetIdentity(userName, password);

//    if (identity == null)
//    {
//        Response.StatusCode = 400;
//        await Response.WriteAsync("Invalid username or password");
//        return;
//    }

//    var now = DateTime.UtcNow;

//    // создаем JWT токен
//    var jwt = new JwtSecurityToken(
//        AuthOptions.Issuer,
//        AuthOptions.Audience,
//        notBefore: now,
//        claims: identity.Claims,
//        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LifeTime)),
//        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

//    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

//    var response = new
//    {
//        access_token = encodedJwt,
//        username = identity.Name
//    };

//    // Сериализация ответа

//    Response.ContentType = "application/json";
//    await Response.WriteAsync(JsonConvert.SerializeObject(response,
//        new JsonSerializerSettings { Formatting = Formatting.Indented }));
//}

//private ClaimsIdentity GetIdentity(string userName, string password)
//{
//    Person person = people.FirstOrDefault(x => x.Email == userName && x.p == password);

//    if (person != null)
//    {
//        var claims = new List<Claim>
//            {
//                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),

//            };
//        ClaimsIdentity claimsIdentity =
//            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
//                ClaimsIdentity.DefaultRoleClaimType);
//        return claimsIdentity;
//    }

//    // если пользователя не найдено
//    return null;

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