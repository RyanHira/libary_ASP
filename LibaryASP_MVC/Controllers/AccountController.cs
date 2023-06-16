using LibaryASP_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibaryASP_MVC.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
            };
            
			var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (identityResult.Succeeded)
            {
                //Role asign met Userrole
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                if (roleIdentityResult.Succeeded) 
                {
                    //succes note
                    return RedirectToAction("Register");
                }
            }

            //show error note
            return View("Register");

		}

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginVieModel 
            { 
                ReturnUrl   = ReturnUrl
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVieModel loginVieModel)
        {
            var signInResult = await signInManager.PasswordSignInAsync(loginVieModel.Username, 
                loginVieModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginVieModel.ReturnUrl))
                {
                    return Redirect(loginVieModel.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            //show errors
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied() 
        { 
            return View();
        }
    }
}
