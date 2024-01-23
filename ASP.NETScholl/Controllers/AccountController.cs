using ASP.NETScholl.Models;
using ASP.NETScholl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETScholl.Controllers {
  public class AccountController : Controller {
    private UserManager<AppUser> userManager;
    private SignInManager<AppUser> signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) {
      this.userManager = userManager;
      this.signInManager = signInManager;
    }
    [AllowAnonymous]                                            //abychom se bez přihlášení dostali k příhlášení
    public IActionResult Login(string returnUrl) {
      LoginViewModel login = new LoginViewModel();
      login.ReturnUrl = returnUrl;
      return View();
    }
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]                       //omezuje určité typy útoků
    public async Task<IActionResult> Login(LoginViewModel login) {
      if(ModelState.IsValid) {                                                                   //kontrola, zda je vše vyplněná, jak má být
        var appUser = await userManager.FindByNameAsync(login.UserName);
        if(appUser != null) {
          var signInResult = await signInManager.PasswordSignInAsync(appUser, login.Password, login.Remember, false);
          if (signInResult.Succeeded) {
            return Redirect(login.ReturnUrl ?? "/");
          }
        }
        ModelState.AddModelError("", "Login failed: Invalid username or password");
      }
      return View(login);
    }
    public async Task<IActionResult> Logout() {
      await signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }
    public ActionResult AccessDenied() {
      return View();
    }
  }
}
