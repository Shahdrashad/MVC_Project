using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAl.Contexts
{
	public class MvcDbContext:IdentityDbContext<ApplicationUser>
	{
		public MvcDbContext(DbContextOptions<MvcDbContext> options) : base(options)
		{


		}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("Server=.;Database=MvcAppDb;Trusted_connection=true");
		//}
		public DbSet<Department>Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }
		

	}
}
