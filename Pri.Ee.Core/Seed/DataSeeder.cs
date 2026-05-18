using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            try 
            {

                if (!context.Authors.Any())
                {

                    var authors = new List<Author>
                    {
                        new Author { Name = "Axie Oh"},
                        new Author { Name = "Lauren Roberts"},
                        new Author { Name = "Rebecca Yarros"}

                    };


                    context.Authors.AddRange(authors);
                    await context.SaveChangesAsync();

                }



                if (!context.Books.Any())
                {

                    var authors = await context.Authors.ToListAsync();

                    var author1 = authors.FirstOrDefault(a => a.Name == "Axie Oh");
                    var author2 = authors.FirstOrDefault(a => a.Name == "Lauren Roberts");
                    var author3 = authors.FirstOrDefault(a => a.Name == "Rebecca Yarros");

                    if (author1 == null || author2 == null || author3 == null)
                    {
                        throw new Exception("OneOne or more authors not found");
                    }

                    var books = new List<Book>
                {
                    new Book 
                    {
                        Title = "The Girl Who Fell Beneath the Sea",
                        Description = "he Girl Who Fell Beneath the Sea by Axie Oh is a YA fantasy novel inspired by Korean folklore. It follows Mina, who sacrifices herself to the sea to save her brother’s love and ends up in the Spirit Realm. There, she tries to break a deadly curse on her homeland while uncovering secrets about fate, gods, and herself.",
                        AuthorId = author1.Id 
                    },
                    new Book 
                    {
                        Title = "XOXO",
                        Description = "XOXO by Axie Oh is a YA romance about Jenny, a gifted cellist who meets a mysterious boy in LA and later finds out he is a famous K-pop idol at her school in South Korea. As they grow closer, they must navigate fame, secrets, and the pressure of his celebrity life.",
                        AuthorId = author1.Id 
                    },
                    new Book 
                    { 
                        Title = "ASAP", 
                        Description= "ASAP by Axie Oh is a YA romance and sequel to XOXO. It follows Sori and her ex, K-pop idol Nathaniel, who reconnect after years apart. As they deal with fame, family pressure, and past heartbreak, they get a second chance at love.",
                        AuthorId = author1.Id },
                    new Book 
                    {
                        Title = "The Floating World", 
                        Description = "The Floating World by Axie Oh is a YA fantasy about Sunho, a boy with a forgotten past, and Ren, a performer with magical powers. When they meet, they uncover secrets about their identities and a divided world of light and darkness.",
                        AuthorId = author1.Id },
                    new Book 
                    { 
                        Title = "Powerless", 
                        Description = "Powerless by Lauren Roberts is a YA fantasy romance set in the kingdom of Ilya, where people with powers (Elites) rule over powerless people (Ordinaries). Paedyn, an Ordinary pretending to have a gift, is forced into deadly trials where she meets Prince Kai, her enemy and maybe her biggest threat.",
                        AuthorId = author2.Id 
                    },
                    new Book 
                    { 
                        Title = "Reckless", 
                        Description = "Reckless by Lauren Roberts is the sequel to Powerless. It follows Paedyn and Kai as they are forced apart and must face new dangers, political conflict, and difficult choices. As their feelings grow stronger, they also have to decide what they’re willing to sacrifice for love and survival.",
                        AuthorId = author2.Id 
                    },
                    new Book 
                    {
                        Title = "Fearless", 
                        Description = "Fearless by Lauren Roberts is the final book in the Powerless trilogy. It follows Paedyn and Kai as they face the consequences of past choices and the ongoing conflict in Ilya. With war looming and loyalties tested, they must decide what they are willing to risk for love, freedom, and the future of their world.",
                        AuthorId = author2.Id 
                    },
                    new Book 
                    { 
                        Title = "Fourth Wing", 
                        Description = "Fourth Wing by Rebecca Yarros is a fantasy novel about Violet Sorrengail, who is forced to become a dragon rider at a deadly war college. She must survive brutal trials, bond with a dragon, and navigate dangerous rivalries especially with Xaden Riorson.",
                        AuthorId = author3.Id },
                    new Book 
                    {
                        Title = "Iron Flame", 
                        Description = "Iron Flame by Rebecca Yarros is the sequel to Fourth Wing. It follows Violet Sorrengail as she continues training at Basgiath War College, facing harsher challenges, political secrets, and growing war tensions. As danger rises, she must fight to survive while dealing with her complicated bond with Xaden.",
                        AuthorId = author3.Id 
                    },
                    new Book 
                    { 
                        Title = "Onyx Storm",
                        Description = "Onyx Storm by Rebecca Yarros is the third book in the Fourth Wing series. It follows Violet as the war intensifies and secrets about dragons, power, and leadership are revealed. She must face new enemies and difficult choices while trying to protect the people she loves and survive the growing conflict.",
                        AuthorId = author3.Id 
                    },
                };

                    context.Books.AddRange(books);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
