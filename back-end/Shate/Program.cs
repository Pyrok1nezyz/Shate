using Shate.DAL;
using Shate.DAL.EF;
using Shate.DAL.Interfaces;
using Shate.DAL.Services;

namespace Back_end_mvc
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var db = new PostgreDbContext();
			db.SaveChanges();

			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllers();
			builder.Services.AddDbContext<PostgreDbContext>();
			builder.Services.AddSingleton<IUnitOfWork, UnitOfWorkService>();


			var app = builder.Build();

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
				pattern: "{controller=Items}/{action=Index}/{id?}");

			app.MapControllerRoute(
				name: "ComputerDuter",
				pattern: "{controller=Computers}/{action=Index}/{id?}");

			app.Run();
		}
	}
}