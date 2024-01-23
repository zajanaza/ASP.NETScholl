using ASP.NETScholl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP.NETScholl.Controllers {
  public class HomeController : Controller {
    private UserManager<AppUser> userManager;

    public HomeController(UserManager<AppUser> userManager) {
      this.userManager = userManager;
    }
    [Authorize]                                             //zajistí zobrazení pouze autorizovaným uživatelům
    public async Task <IActionResult> Index() {
      var user = await userManager.GetUserAsync(HttpContext.User);                   //slouží ke zjištění aktuálně příhlášeného usera
      string message = $"Hello, {user.UserName}";
      return View("Index", message);
    }                                   

    public IActionResult Privacy() {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}