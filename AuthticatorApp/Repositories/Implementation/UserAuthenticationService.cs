using AuthticatorApp.Models;
using AuthticatorApp.Models.Domain;
using AuthticatorApp.Models.DTO;
using AuthticatorApp.Repositories.Abstract;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace AuthticatorApp.Repositories.Implementation
{
	public class UserAuthenticationService : IUserAuthenticationService
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		public UserAuthenticationService(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.signInManager = signInManager;

		}
		public async Task<Status> LoginAsync(LoginModel model)
		{
			var status = new Status();
			var user = await userManager.FindByNameAsync(model.Username);
			if (user == null)
			{
				status.StatusCode = 0;
				status.Message = "Invalid username";
				return status;
			}

			if (!await userManager.CheckPasswordAsync(user, model.Password))
			{
				status.StatusCode = 0;
				status.Message = "Invalid Password";
				return status;
			}
			var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
			if (signInResult.Succeeded)
			{
				var userRoles = await userManager.GetRolesAsync(user);
				var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.Name),
				};

				foreach (var userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				}
				status.StatusCode = 1;
				status.Message = "Logged in successfully";
				return status;
			}
			else if (signInResult.IsLockedOut)
			{
				status.StatusCode = 0;
				status.Message = "User lockedd out";
				return status;
			}
			else
			{
				status.StatusCode = 0;
				status.Message = "Error on login";
				return status;
			}

		}

		public async Task LogoutAsync()
		{
			await signInManager.SignOutAsync();
		}


		public async Task<Status> RegistrationAsync(RegistrationModel model)
		{
			var status = new Status();
			var userExists = await userManager.FindByNameAsync(model.Username);
			if (userExists != null)
			{
				status.StatusCode = 0;
				status.Message = "User already exists";
				return status;
			}

			ApplicationUser user = new ApplicationUser
			{
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
				Name = model.Name,
				PhoneNumber=model.Phonenumber,
				EmailConfirmed = true,
			};

			var result = await userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
			{
				status.StatusCode = 0;
				status.Message = "User creation failed";
				return status;
			}

			if (!await roleManager.RoleExistsAsync(model.Role))
				await roleManager.CreateAsync(new IdentityRole(model.Role));

			if(await roleManager.RoleExistsAsync(model.Role))
			{
				await userManager.AddToRoleAsync(user, model.Role);
			}

			status.StatusCode = 1;
			status.Message = "user registered successfully";
			return status;
		}

		
	}
}





