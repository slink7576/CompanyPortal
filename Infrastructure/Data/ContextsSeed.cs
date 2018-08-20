using ApplicationCore.Entities;
using Infrastructure.Identity;
using Infrastructure.IntermediateEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class ContextsSeed
    {
        public static void SeedEmployers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDTO>().HasData(
                new EmployeeDTO { Name = "Isaac", Surname = "Cooke", Position = Position.Accountant, Id = 1, Email = "exampleemailn1@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Sebastian", Surname = "Saunders", Position = Position.Accountant, Id = 2, Email = "exampleemailn2@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Leon", Surname = "John", Position = Position.Accountant, Id = 3, Email = "exampleemailn3@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Jayden", Surname = "Duncan", Position = Position.Accountant, Id = 4, Email = "exampleemailn4@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Luca", Surname = "Lee", Position = Position.Architect, Id = 5, Email = "exampleemailn5@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Milo", Surname = "Turner", Position = Position.Architect, Id = 6, Email = "exampleemailn6@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Albert", Surname = "Montgomery", Position = Position.Architect, Id = 7, Email = "exampleemailn7@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Terrell", Surname = "Medina", Position = Position.Architect, Id = 8, Email = "exampleemailn8@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Abel", Surname = "Carroll", Position = Position.Manager, Id = 9, Email = "exampleemailn9@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Rocco", Surname = "Walters", Position = Position.Manager, Id = 10, Email = "exampleemailn10@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Leo", Surname = "Henry", Position = Position.Manager, Id = 11, Email = "exampleemailn11@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Zak", Surname = "Wilson", Position = Position.Manager, Id = 12, Email = "exampleemailn12@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Arthur", Surname = "Willis", Position = Position.Secretary, Id = 13, Email = "exampleemailn13@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "David", Surname = "Nicholson", Position = Position.Secretary, Id = 14, Email = "exampleemailn14@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Henry", Surname = "Riley", Position = Position.Secretary, Id = 15, Email = "exampleemailn15@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Darren", Surname = "Watson", Position = Position.Secretary, Id = 16, Email = "exampleemailn16@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Nickolas", Surname = "Everett", Position = Position.SoftwareEngineer, Id = 17, Email = "exampleemailn17@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Landyn", Surname = "Dotson", Position = Position.SoftwareEngineer, Id = 18, Email = "exampleemailn18@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Kohen", Surname = "Riley", Position = Position.SoftwareEngineer, Id = 19, Email = "exampleemailn19@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Jesse", Surname = "Gardner", Position = Position.SoftwareEngineer, Id = 20, Email = "exampleemailn20@example.com", PhoneNumber = "+380675020926" },
                new EmployeeDTO { Name = "Jackson", Surname = "Snow", Position = Position.SoftwareEngineer, Id = 21, Email = "exampleemailn21@example.com", PhoneNumber = "+380675020926" }
                );
              
        }
        public static void SeedNewsCharts(ModelBuilder modelBuilder)
        {
           
                modelBuilder.Entity<ChartItem>().HasData(
                     new { Likes = 10, Date = DateTime.Parse("06.06.2018"), Id = 1, NewsItemId = 1 },
                new { Likes = 3, Date = DateTime.Parse("08.06.2018"), Id = 2, NewsItemId = 1 },
                new { Likes = 8, Date = DateTime.Parse("09.06.2018"), Id = 3, NewsItemId = 1 },
                new { Likes = 9, Date = DateTime.Parse("12.06.2018"), Id = 4, NewsItemId = 1 },

                new { Likes = 8, Date = DateTime.Parse("06.06.2018"), Id = 5, NewsItemId = 2 },
                new { Likes = 3, Date = DateTime.Parse("08.06.2018"), Id = 6, NewsItemId = 2 },
                new { Likes = 10, Date = DateTime.Parse("09.06.2018"), Id = 7, NewsItemId = 2 },
                new { Likes = 9, Date = DateTime.Parse("12.06.2018"), Id = 8, NewsItemId = 2 },

                new { Likes = 10, Date = DateTime.Parse("06.06.2018"), Id = 9, NewsItemId = 3 },
                new { Likes = 9, Date = DateTime.Parse("08.06.2018"), Id = 10, NewsItemId = 3 },
                new { Likes = 8, Date = DateTime.Parse("09.06.2018"), Id = 11, NewsItemId = 3 },
                new { Likes = 3, Date = DateTime.Parse("12.06.2018"), Id = 12, NewsItemId = 3 },

                  new { Likes = 10, Date = DateTime.Parse("06.06.2018"), Id = 13, NewsItemId = 4 },
                new { Likes = 3, Date = DateTime.Parse("08.06.2018"), Id = 14, NewsItemId = 4 },
                new { Likes = 1, Date = DateTime.Parse("09.06.2018"), Id = 15, NewsItemId = 4 },
                new { Likes = 16, Date = DateTime.Parse("12.06.2018"), Id = 16, NewsItemId = 4 },

                  new { Likes = 7, Date = DateTime.Parse("06.06.2018"), Id = 17, NewsItemId = 5 },
                new { Likes = 3, Date = DateTime.Parse("08.06.2018"), Id = 18, NewsItemId = 5 },
                new { Likes = 8, Date = DateTime.Parse("09.06.2018"), Id = 19, NewsItemId = 5 },
                new { Likes = 12, Date = DateTime.Parse("12.06.2018"), Id = 20, NewsItemId = 5 },

                  new { Likes = 13, Date = DateTime.Parse("06.06.2018"), Id = 21, NewsItemId = 6 },
                new { Likes = 0, Date = DateTime.Parse("08.06.2018"), Id = 22, NewsItemId = 6 },
                new { Likes = 8, Date = DateTime.Parse("09.06.2018"), Id = 23, NewsItemId = 6 },
                new { Likes = 9, Date = DateTime.Parse("12.06.2018"), Id = 24, NewsItemId = 6 },

                  new { Likes = 10, Date = DateTime.Parse("06.06.2018"), Id = 25, NewsItemId = 7 },
                new { Likes = 12, Date = DateTime.Parse("08.06.2018"), Id = 26, NewsItemId = 7 },
                new { Likes = 3, Date = DateTime.Parse("09.06.2018"), Id = 27, NewsItemId = 7 },
                new { Likes = 5, Date = DateTime.Parse("12.06.2018"), Id = 28, NewsItemId = 7 },

                  new { Likes = 3, Date = DateTime.Parse("06.06.2018"), Id = 29, NewsItemId = 8 },
                new { Likes = 7, Date = DateTime.Parse("08.06.2018"), Id = 30, NewsItemId = 8 },
                new { Likes = 11, Date = DateTime.Parse("09.06.2018"), Id = 31, NewsItemId = 8 },
                new { Likes = 9, Date = DateTime.Parse("12.06.2018"), Id = 32, NewsItemId = 8 },

                  new { Likes = 9, Date = DateTime.Parse("06.06.2018"), Id = 33, NewsItemId = 9 },
                new { Likes = 4, Date = DateTime.Parse("08.06.2018"), Id = 34, NewsItemId = 9 },
                new { Likes = 5, Date = DateTime.Parse("09.06.2018"), Id = 35, NewsItemId = 9 },
                new { Likes = 12, Date = DateTime.Parse("12.06.2018"), Id = 36, NewsItemId = 9 },

                  new { Likes = 10, Date = DateTime.Parse("06.06.2018"), Id = 37, NewsItemId = 10 },
                new { Likes = 5, Date = DateTime.Parse("08.06.2018"), Id = 38, NewsItemId = 10 },
                new { Likes = 6, Date = DateTime.Parse("09.06.2018"), Id = 39, NewsItemId = 10 },
                new { Likes = 9, Date = DateTime.Parse("12.06.2018"), Id = 40, NewsItemId = 10 }
                );
            string fish = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";


            modelBuilder.Entity<NewsItemDTO>().HasData(
                      new NewsItem { Author = "Admin", Body = fish, Header = "Title", TotalLikes = 30, Created = DateTime.Parse("05.06.2018"), Id = 1, },
                new NewsItem { Author = "Admin", Body = fish, Header = "Title", TotalLikes = 30, Created = DateTime.Parse("05.06.2018"), Id = 2 },
                new NewsItem { Author = "Admin", Body = fish, Header = "Title", TotalLikes = 30, Created = DateTime.Parse("05.06.2018"), Id = 3 },
                new NewsItem { Author = "Admin", Body = fish, Header = "Title", TotalLikes = 30, Created = DateTime.Parse("05.06.2018"), Id = 4 },
                new NewsItem { Author = "Admin", Body = fish, Header = "Title", TotalLikes = 30, Created = DateTime.Parse("05.06.2018"), Id = 5 },
                new NewsItem { Author = "Admin", Body = fish, Header = "Title", TotalLikes = 30, Created = DateTime.Parse("05.06.2018"), Id = 6 },
                new NewsItem { Author = "Admin", Body = fish, Header = "Title", TotalLikes = 30, Created = DateTime.Parse("05.06.2018"), Id = 7 },
                new NewsItem { Author = "Admin", Body = fish, Header = "Title", TotalLikes = 30, Created = DateTime.Parse("05.06.2018"), Id = 8 },
                new NewsItem { Author = "Admin", Body = fish, Header = "Title", TotalLikes = 30, Created = DateTime.Parse("05.06.2018"), Id = 9 },
                new NewsItem { Author = "Admin", Body = fish, Header = "Title", TotalLikes = 30, Created = DateTime.Parse("05.06.2018"), Id = 10 }
                );
            


        }
     

        public static IEnumerable<Employee> PreConfiguredEmployees()
        {
            return new List<EmployeeDTO>
            {
                new EmployeeDTO { Name = "Isaac", Surname = "Cooke", Position = Position.Accountant, Id = 1   },
                new EmployeeDTO { Name = "Sebastian", Surname = "Saunders", Position = Position.Accountant, Id = 2  },
                new EmployeeDTO { Name = "Leon", Surname = "John", Position = Position.Accountant,Id = 3 },
                new EmployeeDTO { Name = "Jayden", Surname = "Duncan", Position = Position.Accountant,  Id = 4},
                new EmployeeDTO { Name = "Luca", Surname = "Lee", Position = Position.Architect, Id = 5 },
                new EmployeeDTO { Name = "Milo", Surname = "Turner", Position = Position.Architect,Id = 6  },
                new EmployeeDTO { Name = "Albert", Surname = "Montgomery", Position = Position.Architect,Id = 7 },
                new EmployeeDTO { Name = "Terrell", Surname = "Medina", Position = Position.Architect,Id = 8  },
                new EmployeeDTO { Name = "Abel", Surname = "Carroll", Position = Position.Manager,  Id = 9 },
                new EmployeeDTO { Name = "Rocco", Surname = "Walters", Position = Position.Manager,Id = 10  },
                new EmployeeDTO { Name = "Leo", Surname = "Henry", Position = Position.Manager,  Id = 11 },
                new EmployeeDTO { Name = "Zak", Surname = "Wilson", Position = Position.Manager,  Id = 12 },
                new EmployeeDTO { Name = "Arthur", Surname = "Willis", Position = Position.Secretary, Id = 13 },
                new EmployeeDTO { Name = "David", Surname = "Nicholson", Position = Position.Secretary, Id = 14  },
                new EmployeeDTO { Name = "Henry", Surname = "Riley", Position = Position.Secretary, Id = 15 },
                new EmployeeDTO { Name = "Darren", Surname = "Watson", Position = Position.Secretary,Id = 16 },
                new EmployeeDTO { Name = "Nickolas", Surname = "Everett", Position = Position.SoftwareEngineer,  Id = 17 },
                new EmployeeDTO { Name = "Landyn", Surname = "Dotson", Position = Position.SoftwareEngineer, Id = 18  },
                new EmployeeDTO { Name = "Kohen", Surname = "Riley", Position = Position.SoftwareEngineer,   Id = 19},
                new EmployeeDTO { Name = "Jesse", Surname = "Gardner", Position = Position.SoftwareEngineer, Id = 20  },
                new EmployeeDTO { Name = "Jackson", Surname = "Snow", Position = Position.SoftwareEngineer,  Id = 21 }
            };

        }


        public static async Task SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            IdentityResult resultRole = await roleManager.CreateAsync(new IdentityRole("Admin"));


            if (userManager.Users.Count() == 0)
            {
            
               
                
                int i = 1;
                foreach (EmployeeDTO c in PreConfiguredEmployees())
                {
                    
                    string email = "exampleemailn" + i + "@example.com";

                    var appUser = new ApplicationUser { Email = email, UserName = email, EmployerId = i };
                    var result = await userManager.CreateAsync(appUser, "Password123*");
                    if (i == 1)
                    {
                        var rolePermResult = await userManager.AddToRoleAsync(appUser, "Admin");
                    }
                    if (!result.Succeeded)
                    {
                        throw new Exception("Error while seeding user data");
                    }
                    i++;



                }
            }


        }
    }
}