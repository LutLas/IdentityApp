using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityApp.Pages.Identity
{
    [AllowAnonymous]
    public class SignInModel : UserPageModel
    {
        public SignInModel(SignInManager<IdentityUser> signMgr,
                UserManager<IdentityUser> usrMgr)
        {
            SignInManager = signMgr;
            UserManager = usrMgr;
        }
        public SignInManager<IdentityUser> SignInManager { get; set; }
        public UserManager<IdentityUser> UserManager { get; set; }
        [Required]
        [EmailAddress]
        [BindProperty]
        public string Email { get; set; }
        [Required]
        [BindProperty]
        public string Password { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.PasswordSignInAsync(Email,
                Password, true, true);
                if (result.Succeeded)
                {
                    return Redirect(ReturnUrl ?? "/");
                }
                else if (result.IsLockedOut)
                {
                    TempData["message"] = "Account Locked";
                }
                else if (result.IsNotAllowed)
                {
                    IdentityUser user = await UserManager.FindByEmailAsync(Email);
                    if (user != null &&
                    !await UserManager.IsEmailConfirmedAsync(user))
                    {
                        return RedirectToPage("SignUpConfirm");
                    }
                    TempData["message"] = "Sign In Not Allowed";
                }
                else if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("SignInTwoFactor", new { ReturnUrl });
                }
                else
                {
                    TempData["message"] = "Sign In Failed";
                }
            }
            return Page();
        }
    }
}