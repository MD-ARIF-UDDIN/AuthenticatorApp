using AuthticatorApp.Models.DTO;

namespace AuthticatorApp.Repositories.Abstract
{
	public interface IUserAuthenticationService
	{
		Task<Status> LoginAsync(LoginModel model);
		Task LogoutAsync();
        Task<Status> RegistrationAsync(RegistrationModel model);

    }
}
