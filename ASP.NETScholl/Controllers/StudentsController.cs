using ASP.NETScholl.Services;
using Microsoft.AspNetCore.Mvc;
using ASP.NETScholl.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NETScholl.Controllers {
  [Authorize(Roles = "Teacher, Admin")]
  public class StudentsController : Controller {
    private StudentService service;

    public StudentsController(StudentService studentService) {
      this.service = studentService;
    }

    public async Task<IActionResult> IndexAsync() {
      var allStudents = await service.GetAllAsync();
      return View(allStudents);
    }
    public IActionResult Create() {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(Student student) {
      await service.CreateStudentAsync(student);
      return RedirectToAction("Index");                         //přesměruje řízení na metodu, která se jmenuje index
    }
    public async Task<IActionResult> Edit(int id) {
      var studentToEdit = await service.GetById(id);
      if (studentToEdit == null) {
        return View("NotFound");
      }
      return View(studentToEdit);
    }
    [HttpPost]
    public async Task<IActionResult> EditAsync(int id, [Bind("Id, FirstName, LastName, DateOfBirth")] Student student) {
      await service.UpdateAsync(student);
      return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id) {
      var studentToDelete = await service.GetById(id);
      if (studentToDelete == null) {
        return View("NotFound");
      }
      service.DeleteAsync(studentToDelete);
      return RedirectToAction("Index");
    }
  }
}
