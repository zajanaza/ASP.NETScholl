using ASP.NETScholl.Models;
using ASP.NETScholl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ASP.NETScholl.Controllers {
  [Authorize(Roles = "Admin")]
  public class RolesController : Controller {
    private RoleManager<IdentityRole> roleManager;
    private UserManager<AppUser> userManager;

    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager) {
      this.roleManager = roleManager;
      this.userManager = userManager;
    }

    public IActionResult Index() {
      return View(roleManager.Roles);
    }
    public IActionResult Create() {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(string name) {
      if (ModelState.IsValid) {
        IdentityResult createResult = await roleManager.CreateAsync(new IdentityRole { Name = name });
        if (createResult.Succeeded) {
          return RedirectToAction("Index");
        }
        else {
          foreach (var error in createResult.Errors) {
            ModelState.AddModelError("", error.Description);
          }
        }
      }
      return View(name);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(string id) {
      var roleToDelete = await roleManager.FindByIdAsync(id);
      if (roleToDelete != null) {
        var deleteResult = await roleManager.DeleteAsync(roleToDelete);
        if (deleteResult.Succeeded) {
          foreach (var error in deleteResult.Errors) {
            ModelState.AddModelError("", error.Description);
          }
        }
      }
      else {
        ModelState.AddModelError("", "Role not found");
      }
      return RedirectToAction("Index");
    }
    public async Task<IActionResult> Edit(string id) {
      var role = await roleManager.FindByIdAsync(id);
      List<AppUser> members = new List<AppUser>();
      List<AppUser> nonMembers = new List<AppUser>();
      if (role != null) {
        foreach (var user in userManager.Users) {
          var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
          list.Add(user);
        }
        return View(new RoleEditViewModel {
          Members = members,
          NonMembers = nonMembers,
          Role = role
        });
      }
      return View("NotFount");
    }
    [HttpPost]
    public async Task<IActionResult> Edit(RoleModificationViewModel model) {
      IdentityResult result;
      if (ModelState.IsValid) {
        foreach (string userId in model.IdsToAdd ?? new string[] { }) {
          var user = await userManager.FindByIdAsync(userId);
          if (user != null) {
            result = await userManager.AddToRoleAsync(user, model.RoleName);
          }
        }
        foreach (string userId in model.IdsToDelete ?? new string[] { }) {
          var user = await userManager.FindByIdAsync(userId);
          if (userId != null) {
            result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
          }
        }
        return RedirectToAction("Index");
      }
      else {
        return RedirectToAction("edit", "roles", model.RoleId);
      }
    }
  }
}
