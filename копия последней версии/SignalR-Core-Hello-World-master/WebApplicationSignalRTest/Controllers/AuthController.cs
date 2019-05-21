using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServerNetCore.Models;

namespace ServerNetCore.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetToken()
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
                    var user = await _userManager.FindByEmailAsync(credentials[0]);
                    if (user == null)
                    {
                        return StatusCode(401, "Не верный логин");
                    }

                    var identity = await GetIdentity(user);
                    var res = await _userManager.CheckPasswordAsync(user, credentials[1]);
                    if (res)
                    {
                        var now = DateTime.UtcNow;
                        var jwt = new JwtSecurityToken(
                            AuthOptions.Issuer,
                            AuthOptions.Audience,
                            notBefore: now,
                            claims: identity.Claims,
                            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LifeTime)),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                                SecurityAlgorithms.HmacSha256));

                        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                        return Content(encodedJwt);
                    }
                    else
                    {
                        return StatusCode(401, "Не верный пароль");
                    }
                }
                else
                {
                    return StatusCode(400, "Не верная структура токена");
                }
            }
            else
            {
                return StatusCode(400, "Не обноружен заголовок Authorization ");
            }
        }

        [NonAction]
        private async Task<ClaimsIdentity> GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "user"),
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}