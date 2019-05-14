using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ServerNetCore.Areas.Identity.Pages.Account;
using ServerNetCore.Models;
using RegisterModel = ServerNetCore.ViewModels.RegisterModel;

namespace ServerNetCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ДОДЕЛАТЬ НАДО
        //public ApplicationDbContext()
        //{
        //    using (ApplicationDbContext dbContext = new ApplicationDbContext())
        //    {
        //    }
        //}

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    // Customize the ASP.NET Core Identity model and override the defaults if needed.
        //    // For example, you can rename the ASP.NET Core Identity table names and more.
        //    // Add your customizations after calling base.OnModelCreating(builder);
        //}
    }
}