using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Pri.Ee.Core.Data;
using Pri.Ee.Core.Entities;

namespace Pri.Ee.Core.Seed
{
    public class DataSeeder
    {
        public static async Task SeedAsync(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            //Rollen seeden
            if(!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            //Users Seeden
            var adminUser = await userManager.FindByNameAsync("admin");

            if (adminUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };

                await userManager.CreateAsync(user, "Admin123!");
                await userManager.AddToRoleAsync(user, "Admin");

            }

            var normalUser = await userManager.FindByNameAsync("user");

            if(normalUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = "user",
                    Email = "user@test.com"
                };

                await userManager.CreateAsync(user, "User123!");
                await userManager.AddToRoleAsync(user, "User");

            }

            //Data seeden
            if (!context.Authors.Any())
            {
                var author1 = new Author { Name = "Axie Oh" };
                var author2 = new Author { Name = "Lauren Roberts" };
                var author3 = new Author { Name = "Rebecca Yarros" };

                context.Authors.AddRange(author1, author2, author3);
                await context.SaveChangesAsync();

                var books = new List<Book>
                {
                    new Book { Title = "The Girl Who Fell Beneath the Sea", AuthorId = author1.Id },
                    new Book { Title = "XOXO", AuthorId = author1.Id },
                    new Book { Title = "ASAP", AuthorId = author1.Id },
                    new Book { Title = "The Floating World", AuthorId = author1.Id },
                    new Book { Title = "Powerless", AuthorId = author2.Id },
                    new Book { Title = "Reckless", AuthorId = author2.Id },
                    new Book { Title = "Fearless", AuthorId = author2.Id },
                    new Book { Title = "Fourth Wing", AuthorId = author3.Id },
                    new Book { Title = "Iron Flame", AuthorId = author3.Id },
                    new Book { Title = "Onyx Storm", AuthorId = author3.Id },
                };

                context.Books.AddRange(books);
                await context.SaveChangesAsync();
            }
        }
    }
}
