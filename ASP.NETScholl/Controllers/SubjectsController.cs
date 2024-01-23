using ASP.NETScholl.Services;
using Microsoft.AspNetCore.Mvc;
using ASP.NETScholl.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NETScholl.Controllers {  
  public class SubjectsController : Controller {
    private SubjectService service;
    public SubjectsController(SubjectService subjectService) {
      this.service = subjectService;
    }

    public async Task<IActionResult> IndexAsync() {
      var allSubjects = await service.GetAllAsync();
      return View(allSubjects);
    }
    [Authorize(Roles = "Admin, Teacher")]
    public IActionResult Create() {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(Subject subject) {
      await service.CreateSubjectAsync(subject);
      return RedirectToAction("Index");                         //přesměruje řízení na metodu, která se jmenuje index
    }
    [Authorize(Roles = "Admin, Teacher")]
    public async Task<IActionResult> Edit(int id) {
      var subjectToEdit = await service.GetById(id);
      if (subjectToEdit == null) {
        return View("NotFound");
      }
      return View(subjectToEdit);
    }
    [HttpPost]
    public async Task<IActionResult> EditAsync(int id, [Bind("Id, Name")] Subject subject) {
      await service.UpdateAsync(subject);
      return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin, Teacher")]
    public async Task<IActionResult> DeleteAsync(int id) {
      var subjectToDelete = await service.GetById(id);
      if (subjectToDelete == null) {
        return View("NotFound");
      }
      service.DeleteAsync(subjectToDelete);
      return RedirectToAction("Index");
    }
  }
}
