using AuthticatorApp.Models.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthticatorApp.Models.Domain
{
	public class DatabaseContext : IdentityDbContext<ApplicationUser>
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{

		}


		public DbSet<RegistrationModel> registrationModels { get; set; }
	}
}
