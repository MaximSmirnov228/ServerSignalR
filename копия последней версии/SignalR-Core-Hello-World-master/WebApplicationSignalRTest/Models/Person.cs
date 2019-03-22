using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ServerNetCore.Models
{
    public class Person : IdentityUser
    {
        //public string Login { get; set; }
        public string Password { get; set; }

        //public string Role { get; set; }
        public string Name { get; set; }

        public string Fam { get; set; }
        public string Pol { get; set; }
    }
}