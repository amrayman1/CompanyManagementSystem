using Company.DAL.Entities;
using Company.PL.Helper;
using Company.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Common;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, 
                                    SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Sign up
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0],
                    IsAgree = model.IsAgree
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                    return RedirectToAction("SignIn");

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        #endregion

        #region SignIn
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user == null)
                    ModelState.AddModelError("", "Invalid Email");

                var password = await userManager.CheckPasswordAsync(user,model.Password);

                if (password)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password,model.RememberMe, false);
                    if (result.Succeeded)
                        return RedirectToAction("Index","Home");
                }
            }
            return View(model);
        }

        #endregion

        #region SignOut
        public async new Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var resetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                    var email = new Email()
                    {
                        Title = "Reset Password",
                        Body = resetPasswordLink,
                        To = model.Email
                    };

                    EmailSettings.SendEmail(email);

                    return RedirectToAction(nameof(CompleteForgetPassword));
                }
                ModelState.AddModelError("", "Invalid Email");
            }
            return View(model);
        }
        #endregion

        #region CompleteForgetPassword
        public IActionResult CompleteForgetPassword()
        {
            return View();
        }
        #endregion

        #region ResetPassword
        public IActionResult ResetPassword(string email, string token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if(user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token,model.Password);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(ResetPasswordDone));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        #endregion

        #region ResetPasswordDone
        public IActionResult ResetPasswordDone()
        {
            return View();
        }
        #endregion
    }
}
