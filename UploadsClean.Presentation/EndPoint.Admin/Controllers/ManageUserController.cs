using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Identity.Client;
using NToastNotify;
using System.Diagnostics.CodeAnalysis;
using UploadsClean.Common;
using UploadsClean.Domain.Entities.Users;

namespace EndPoint.Admin.Controllers
{
	public class ManageUserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IToastNotification notishow;

		public ManageUserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IToastNotification notishow, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			this.notishow = notishow;
			_signInManager = signInManager;
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
		[HttpGet]
		public async Task<IActionResult> AddUser()
		{

			return View();

		}
		[HttpPost]
		public async Task<IActionResult> AddUser(UserModel UsertoAdd)
		{
			ApplicationUser userdto = ModelToDto.UserModelToDto(UsertoAdd);

			var userModel = await _userManager.FindByNameAsync(userdto.FarsiFirstName);
			if (userModel == null)
			{
				var result = await _userManager.CreateAsync(userdto, userdto.Password);
				var ThisRole = await _userManager.AddToRoleAsync(userdto, userdto.Role);
				ModelState.Clear();
			}
			else
			{
				var UserUpdated = await _userManager.UpdateAsync(userdto);
				notishow.AddSuccessToastMessage("کاربر ویرایش شد");
				return View();
			}
			notishow.AddSuccessToastMessage("کاربر اضاف شد");
			
			return View();
	
		}

		public async Task<IActionResult> Login()
		{


			return View(new SignInmodelv());

		}

		[HttpPost]
		public async Task<IActionResult> Login(SignInmodelv sign)
		{
			if (!ModelState.IsValid)
			{
				return View(sign); ;
			}
			var signUser = await _userManager.FindByNameAsync(sign.Username);

			if (signUser == null)
			{
				notishow.AddErrorToastMessage("شما اول باید ثبت نام کنید");

				return RedirectToAction(nameof(SignIn));
			}
			ApplicationUser _user = ModelToDto.UserSignInModelToDto(sign);
			var checkOut = _signInManager.PasswordSignInAsync(_user, _user.Password, true, true);


			return RedirectToAction("Index"); ;

		}




	}
}