using Microsoft.AspNetCore.Identity;

namespace AuthticatorApp.Models.Domain
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
