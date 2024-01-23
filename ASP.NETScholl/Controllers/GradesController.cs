using ASP.NETScholl.Models;
using ASP.NETScholl.Services;
using ASP.NETScholl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace ASP.NETScholl.Controllers {
  [Authorize(Roles ="Teacher, Admin")]
  public class GradesController : Controller {    
    private GradeService service;
    public GradesController(GradeService gradeService) {
      this.service = gradeService;

    }
    public async Task<IActionResult> Index() {
      var allGrades = await service.GetAllGrades();
      return View(allGrades);
    }
    public async Task<IActionResult> Create() {
      var gradesDropdownsData = await service.GetDropdownValuesAsync();
      ViewBag.Students = new SelectList(gradesDropdownsData.Students, "Id", "LastName");
      ViewBag.Subjects = new SelectList(gradesDropdownsData.Subjects, "Id", "Name");
      return View();
    }
    [HttpPost]
    public async Task <IActionResult> CreateAsync(GradeViewModel gradeModel) {
      await service.CreateAsync(gradeModel);
      return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> EditAsync(int id) {
      var gradesDropdownsData = await service.GetDropdownValuesAsync();
      ViewBag.Students = new SelectList(gradesDropdownsData.Students, "Id", "LastName");
      ViewBag.Subjects = new SelectList(gradesDropdownsData.Subjects, "Id", "Name");
      var gradeToEdit = service.GetById(id);
      if (gradeToEdit == null) {
        return View("NotFound");
      }
      else return View(gradeToEdit);      
    }
    [HttpPost]
    public async Task<IActionResult> EditAsync(int id, GradeViewModel updateGrade) {
      await service.Update(updateGrade);
      return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id) {
      await service.DeleteAsync(id);
      return RedirectToAction("Index");
    }
  }
}