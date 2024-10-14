using System.Threading.Tasks;
using Demo.DAl.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
			_signInManager = signInManager;
		}
        #region register
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                    FirstName = model.FirstName,
                    LastName = model.LastName,


                };
                var Result = _userManager.CreateAsync(User, model.Password).Result;

                if (Result.Succeeded)
                {

                    //login
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }


            }

            return View(model);

        }

        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(model.Email).Result;
                if (user != null)
                {
                    var Result = _userManager.CheckPasswordAsync(user, model.Password).Result;

                    if (Result)
                    {
                        //Login
                      var LoginResult=  await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe,false);
                        if (LoginResult.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Password is Incorrect");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "email is not Exists");
                }
            }
            return View(model);
        }

		#endregion

		#region SignOut

        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

		#endregion

		#region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>   SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User =  _userManager.FindByNameAsync(model.Email).Result;
                if (User != null) 
                {
                    var token = _userManager.GeneratePasswordResetTokenAsync(User).Result;
                    var url = Url.Action(nameof(ResetPassword),nameof(AccountController).Replace("Controller",string.Empty),
                        new {email=model.Email,Token = token},Request.Scheme);
                    var email = new Email
                    {
                        Subject="Reset Password",
                        Body=url,
                        Recipient=model.Email
                        

                    };
                    MailSetting.SendEmail(email);

                    return RedirectToAction(nameof(CheckYourInBox));

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is not Exists");

                }

            }
            else
            {
                return View("ForgetPassword",model);
            }

            return View();


        }
        public IActionResult CheckYourInBox()
        {
            return View();
        }


		public IActionResult ResetPassword ()
        {
            return View();
        }
		#endregion







	}
}
