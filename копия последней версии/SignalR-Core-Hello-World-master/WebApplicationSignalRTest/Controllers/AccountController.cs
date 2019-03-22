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
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Person> _userManager;

        public AccountController(UserManager<Person> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        //[Route("api/[controller]/GetToken")]
        public async Task<IActionResult> GetTokenAuth()
        {
            if (HttpContext.Request.Headers.TryGetValue("Authorization", out var authorization))
            {
                var authHeader = authorization.ToString();
                if (authHeader?.StartsWith("basic", StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    var token = authHeader.Substring("Basic ".Length).Trim();
                    System.Console.WriteLine(token);
                    var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                    var credentials = credentialstring.Split(':');
                    //Authorization
                    // basic asdasdasdcdcasgfdlgkdsjgkdfjgb
                    var name = await _userManager.FindByNameAsync(credentials[0]);
                    if (name == null)
                    {
                        return StatusCode(401, "Не верное имя");
                    }
                    var Fam = await _userManager.FindByNameAsync(credentials[1]);
                    if (Fam == null)
                    {
                        return StatusCode(401, "Не верное Фамилия");
                    }
                    var Pol = await _userManager.FindByNameAsync(credentials[2]);
                    if (Pol == null)
                    {
                        return StatusCode(401, "Не верно указан пол");
                    }
                    var user = await _userManager.FindByEmailAsync(credentials[3]);
                    if (user == null)
                    {
                        return StatusCode(401, "Не верный логин или пароль1");
                    }

                    var res = await _userManager.CheckPasswordAsync(user, credentials[4]);
                    if (res)
                    {
                        var now = DateTime.UtcNow;
                        var jwt = new JwtSecurityToken(
                            AuthOptions.Issuer,
                            AuthOptions.Audience,
                            notBefore: now,
                            claims: User.Claims,
                            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LifeTime)),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                                SecurityAlgorithms.HmacSha256));

                        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                        return Content(encodedJwt);
                    }
                    else
                    {
                        return StatusCode(401, "Не верный логин или пароль2");
                    }
                }
                else
                {
                    return StatusCode(400, "Не верная структура токена");
                }
            }
            else
            {
                return StatusCode(400, "Не обноружен заголовок Body ");
            }
        }
    }
}

//private List<Person> people = new List<Person>()
//{
//    //new Person{UserName = "admin@gmail.com",PasswordHash = "12345"},
//    //new Person{UserName = "qwerty",PasswordHash = "55555"},
//    //new Person{UserName  = "jon",PasswordHash = "1"},
//};
//    [HttpPost]
//    [AllowAnonymous]
//    public async Task<IActionResult> Register()
//    {
//        var client = new HttpClient();
//        var customer = new Person { Email = "admin@gmail.com", Password = "Qwerty12345&" };

//        var customerJson = JsonConvert.SerializeObject(customer);
//        var response = await client.PostAsync(
//            "http://localhost:4815/api/Customer",
//            new StringContent(customerJson, Encoding.UTF8, "application/json"));

//        //just some template output to test which I'm getting back.
//        string resultJson = "{ 'Name':'adam'}";

//        if (response.StatusCode == HttpStatusCode.OK) resultJson = await response.Content.ReadAsStringAsync();
//        var updatedCustomer = JsonConvert.DeserializeObject(resultJson);
//    }

//    return resultJson;
//}

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