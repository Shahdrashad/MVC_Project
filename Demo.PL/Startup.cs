using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAl.Contexts;
using Demo.DAl.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Demo.PL
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		} 

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews() 
				.AddRazorRuntimeCompilation();
			services.AddDbContext<MvcDbContext>(Options =>Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); // allow Dependecy Injection
			services.AddScoped<IDepartmentRepository,DepartmentRepository>();
			services.AddScoped<IEmployeeRepository,EmployeeRepository>();
			//services.AddScoped<UserManager<ApplicationUser>>();

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				//P@ssw0rd
				//pa$$w0rd
			}).AddEntityFrameworkStores<MvcDbContext>()
			.AddDefaultTokenProviders();
		
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(Options=>
				{
					Options.LoginPath = "Account/Login";
					Options.AccessDeniedPath = "Home/Error";

				});

		}
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
