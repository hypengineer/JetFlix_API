using System;
using JetFlix_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JetFlix_API.Data
{
	public class DbInitializer
	{
        public DbInitializer(ApplicationDbContext? context, RoleManager<JetFlixRole>? roleManager, UserManager<JetFlixUser> userManager)
        {

            JetFlixRole identityRole;
            Restriction restriction;
            JetFlixUser user;
            Category? category = null;
            if (context != null)
            {
                context.Database.Migrate();


                if (roleManager != null)
                {
                    if (roleManager.Roles.Count() == 0)
                    {
                        identityRole = new JetFlixRole("Administrator");
                        roleManager.CreateAsync(identityRole).Wait();
                        identityRole = new JetFlixRole("Admin");
                        roleManager.CreateAsync(identityRole).Wait();
                        identityRole = new JetFlixRole("Company Admin");
                        roleManager.CreateAsync(identityRole).Wait();
                        identityRole = new JetFlixRole("Manager");
                        roleManager.CreateAsync(identityRole).Wait();
                    }
                }

                if (context.Categories.Count() == 0)
                {
                    category = new Category();
                    category.Name = "Bilim-Kurgu";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Aksiyon";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Gerilim";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Korku";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Komedi";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Romantik";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Animasyon";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Fantastik";
                    context.Categories.Add(category);
                    context.SaveChanges();
                }

                if (!context.Restrictions.Any())
                {
                    restriction = new Restriction();
                    restriction.Name = "Genel Izleyici";
                    restriction.Id = 0;
                    context.Restrictions.Add(restriction);
                    restriction = new Restriction();
                    restriction.Name = "7";
                    restriction.Id = 7;
                    context.Restrictions.Add(restriction);
                    restriction = new Restriction();
                    restriction.Name = "13";
                    restriction.Id = 13;
                    context.Restrictions.Add(restriction);
                    restriction = new Restriction();
                    restriction.Name = "18";
                    restriction.Id = 18;
                    context.Restrictions.Add(restriction);
                    restriction = new Restriction();
                    restriction.Name = "Korku ve Şiddet";
                    restriction.Id = 19;
                    context.Restrictions.Add(restriction);
                    restriction = new Restriction();
                    restriction.Name = "Olumsuz Örnek";
                    restriction.Id = 20;
                    context.Restrictions.Add(restriction);
                    context.SaveChanges();

                }


                if (userManager != null)
                {
                    if (userManager.Users.Count() == 0)
                    {

                        user = new JetFlixUser();
                        user.UserName = "Administrator";
                        user.Name = "Administrator";
                        user.Email = "abc@def.com";
                        user.PhoneNumber = "1112223344";
                        user.Passive = false;
                        userManager.CreateAsync(user, "Admin123!").Wait();
                        userManager.AddToRoleAsync(user, "Administrator").Wait();

                    }
                }
            }
        }
    }
}


