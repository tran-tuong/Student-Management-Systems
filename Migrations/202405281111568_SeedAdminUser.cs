namespace StudentManagementSystems.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using StudentManagementSystems.Models;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdminUser : DbMigration
    {
        public override void Up()
        {
            // Create an instance of the ApplicationDbContext
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // Create "Admin" role if it doesn't exist
                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new IdentityRole { Name = "Admin" };
                    roleManager.Create(role);
                }

                // Create admin user if it doesn't exist
                var adminEmail = "admin@123.com";
                var adminPassword = "Ab12345!";
                var adminUser = userManager.FindByEmail(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail
                    };

                    userManager.Create(adminUser, adminPassword);
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }
        }
        
        public override void Down()
        {
        }
    }
}
