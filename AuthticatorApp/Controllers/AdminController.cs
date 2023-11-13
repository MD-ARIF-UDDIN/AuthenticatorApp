using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthticatorApp.Controllers
{
	public class AdminController : Controller
	{
		[Authorize(Roles="admin")]
		public IActionResult Display()
		{
			return View();
		}
	}
}
