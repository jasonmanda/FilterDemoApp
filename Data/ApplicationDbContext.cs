using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FilterDemoApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public void InitData(string userName, string password, string[] listRoles, UserManager<IdentityUser> userManager)
        {

            try
            {
                var _ = this.Database.BeginTransactionAsync().Result;

                foreach (var item in listRoles)
                {
                    var test = this.Roles.Any(role => role.Name == item);
                    if (!test)
                    {
                        this.Roles.Add(new IdentityRole { Name = item, NormalizedName = item.Trim().ToUpper() });
                    }


                }
                this.SaveChanges();

                if (!this.Users.Any(u => u.UserName == userName))
                {
                    var currentUserSa = new IdentityUser { UserName = userName.Trim(), NormalizedUserName = userName.Trim().ToUpper(),EmailConfirmed=true };
                    var result = userManager.CreateAsync(currentUserSa, password).Result;

                    var currentRole = this.Roles.Where(x => x.Name == "Super Admin").FirstOrDefault() as IdentityRole;
                    var currentUserRole = new IdentityUserRole<string> { RoleId = currentRole.Id, UserId = currentUserSa.Id };
                    this.UserRoles.Add(currentUserRole);
                    this.SaveChanges();
                }
                this.Database.CommitTransaction();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);

                try
                {
                    this.Database.RollbackTransaction();

                }
                catch (Exception exp1)
                {
                    Console.WriteLine(exp1.Message);
                }
            }

        }
    }
}
