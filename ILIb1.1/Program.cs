using ILIb1._1.Data;
using ILIb1._1.InterFaces;
using ILIb1._1.Repository;
using Microsoft.EntityFrameworkCore;

namespace ILIb1._1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddScoped<IBookRepository, BookRepository>();
			builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
			builder.Services.AddDbContext<ApplicationDBContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			var app = builder.Build();

			if(args.Length==1 && args[0].ToLower() == "seeddata")
			{
				//
				//Seed.SeedData(app);
				//Seed.SeedUsersAndRolesAsync(app);			
			}


			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}