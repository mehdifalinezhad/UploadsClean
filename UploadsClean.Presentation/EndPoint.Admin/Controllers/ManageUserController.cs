using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using UploadsClean.Domain.Entities.Users;

namespace EndPoint.Admin.Controllers
{
	public class ManageUserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public ManageUserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Search(string username)
		{
			if (string.IsNullOrWhiteSpace(username))
				return RedirectToAction("Index");
		



			return View();
		}

		public async Task<IActionResult> AddUser()
		{

			return View();

		}
		[HttpPost]
		public async Task<IActionResult> AddUser(ApplicationUser UsertoAdd)
		{
			var result=_userManager.CreateAsync(UsertoAdd, UsertoAdd.Password);
			if (!result.IsCompletedSuccessfully==true)
			{ 
			  
				return RedirectToAction("AddUser");	

			}

			return RedirectToAction("Index","Home");

		}



	}
}