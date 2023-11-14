using AuthticatorApp.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthticatorApp.Controllers
{
    [Authorize]
    public class UsersTableController : Controller
    {
        public IActionResult Display()
        {
            return View();
        }



    }
}
