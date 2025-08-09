using Inventorium.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Inventorium.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginModel(SignInManager<IdentityUser> signInManager,
                          ILogger<LoginModel> logger,
                          UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/Inventory");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);

                if (user != null)
                {
                    // Optional: Uncomment if you want to enforce email confirmation
                    // if (!await _userManager.IsEmailConfirmedAsync(user))
                    // {
                    //     ModelState.AddModelError(string.Empty, "Email not confirmed yet.");
                    //     return Page();
                    // }

                    var passwordValid = await _userManager.CheckPasswordAsync(user, Input.Password);

                    if (passwordValid)
                    {
                        await _signInManager.SignInAsync(user, Input.RememberMe);
                        _logger.LogInformation("User logged in.");
                        return RedirectToAction("Index", "Inventory");
                    }
                }

                // If we reached here, something failed.
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // Redisplay the form if something went wrong
            return Page();
        }
    }
}

























//public async Task<IActionResult> OnPostAsync(string returnUrl = null)
//{
//    ReturnUrl = returnUrl ?? Url.Content("~/Inventory");

//    if (ModelState.IsValid)
//    {
//        var user = await _userManager.FindByEmailAsync(Input.Email);

//        if (user != null)
//        {
//            if (!await _userManager.IsEmailConfirmedAsync(user))
//            {
//                ModelState.AddModelError(string.Empty, "Email not confirmed yet.");
//                return Page();
//            }
//        }

//        var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

//        if (result.Succeeded)
//        {
//            // ✅ Redirect directly to Inventory after successful login
//            return RedirectToAction("Index", "Inventory");
//        }

//        //if (result.RequiresTwoFactor)
//        //{
//        //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
//        //}

//        if (result.IsLockedOut)
//        {
//            _logger.LogWarning("User account locked out.");
//            return RedirectToPage("./Lockout");
//        }
//        else
//        {
//            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//            return Page();
//        }
//    }

// If we got this far, something failed; redisplay form.
