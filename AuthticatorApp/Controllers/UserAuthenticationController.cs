using AuthticatorApp.Models.DTO;
using AuthticatorApp.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthticatorApp.Controllers
{
	public class UserAuthentication : Controller
	{
		private readonly IUserAuthenticationService _service; 
        public UserAuthentication(IUserAuthenticationService service)
        {
			this._service = service;
        }

		public IActionResult Registration()
		{
			return View();
		}

		[HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
			if (!ModelState.IsValid)
				return View(model);
			model.Role = "user";
			var result = await _service.RegistrationAsync(model);
			TempData["msg"] = result.Message;
			return RedirectToAction(nameof(Registration));
        }



        public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result =await _service.LoginAsync(model);
			if (result.StatusCode == 1)
			{
				return RedirectToAction("Display", "Dashboard");
			}
			else
			{
				TempData["msg"] = result.Message;
				return RedirectToAction(nameof(Login));
			}
		}

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await _service.LogoutAsync();
			return RedirectToAction(nameof(Login));
		}

		//public async Task<IActionResult> Reg()
		//{
			//var model = new RegistrationModel
			//{
				//Username = "admin1",
				//Name = "Arif Uddin",
				//Email = "admin1@gmail.com",
				//Password = "Admin12345#",
			//};
            //model.Role = "admin";
			//var result = await _service.RegistrationAsync(model);
			//return Ok(result);
       // }
	}
}
