using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ServerNetCore
{
    public class AuthOptions
    {
        /// <summary> 
        /// Издатель токена 
        /// </summary> 
        public const string Issuer = "MyAuthServer";

        /// <summary> 
        /// Потребитель токена 
        /// </summary> 
        public const string Audience = "http://localhost:50338/chat";

        /// <summary> 
        /// Ключ для шифрации 
        /// </summary> 
        private const string Key = "mysuppersectet_secretkey!123";

        public const int LifeTime = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}