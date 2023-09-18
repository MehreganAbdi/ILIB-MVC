using ILIb1._1.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;
using ZendeskApi_v2.Models.Constants;

namespace ILIb1._1.Data
{
	public class Seed
	{
		public static void SeedData(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>();

				context.Database.EnsureCreated();

				if (!context.Books.Any())
				{
					context.Books.AddRange(new List<Book>
					{
						new Book
						{
							Title= "Holiday1",
							BookCategory= Enum.BookCategory.MainRefrences,
							Description = "One of the most popular refrences for basic physics .",
							BookCount = 10,
							Edition = 9,
							Year = 2012,
							Author = new Author
							{
								FullName = "Robert Holiday",

							}
						},
						new Book
						{

							Title= "Holiday ElectroMagnetism",
							BookCategory= Enum.BookCategory.MainRefrences,
							Description = "One of the most popular refrences for basic physics .",
							BookCount = 10,
							Edition = 7,
							Year = 2009,
							Author = new Author
							{
								FullName = "j.Kenedy"
							}
						},
						new Book
						{
							Title= "Arfken Mathematics in physics",
							BookCategory= Enum.BookCategory.Math,
							Description = "The Best book for highlevel math in Theorical physics ",
							BookCount = 1,
							Edition = 5,
							Year = 2010,
							Author = new Author
							{
								FullName = "Arfken"
							}
						},
						new Book
						{
							Title= "Modern physics",
							BookCategory= Enum.BookCategory.ModernPhysics,
							Description = "Moder physics basic topics",
							BookCount = 5,
							Edition = 4,
							Year = 2012,
							Author = new Author
							{
								FullName = "Ican't remember"
							}
						}
					});

					context.SaveChanges();

					if (!context.Authors.Any())
					{
						context.Authors.AddRange(new List<Author>()
						{
							new Author
							{
								FullName="Ican't remember"
							},
							new Author
							{
								FullName="Rober Holiday"
							},
							new Author
							{
								FullName="Arfken"
							},
							new Author
							{
								FullName= "j.Kenedy"
							}
						});
						context.SaveChanges();
					}

				}

			}
		}
		public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				//Roles
				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
				if (!await roleManager.RoleExistsAsync(UserRoles.User))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

				//Users
				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
				string adminUserEmail = "mehreganabdix@gmail.com";

				var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
				if (adminUser == null)
				{
					var newAdminUser = new AppUser()
					{
						UserName = "MehreganA",
						Email = adminUserEmail,
						EmailConfirmed = true,

					};
					await userManager.CreateAsync(newAdminUser, "Coding@1234?");
					
					await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
				}

				//string appUserEmail = "user@etickets.com";

				//var appUser = await userManager.FindByEmailAsync(appUserEmail);
				//if (appUser == null)
				//{
				//	var newAppUser = new AppUser()
				//	{
				//		UserName = "app-user",
				//		Email = appUserEmail,
				//		EmailConfirmed = true,
				//	};
				//	await userManager.CreateAsync(newAppUser, "Coding@1234?");
				//	await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
				//}
			}
		}
	}
}
