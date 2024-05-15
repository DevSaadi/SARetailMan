using Microsoft.AspNetCore.Mvc;
using RepositoryProject.Areas.Student.Data;
using RepositoryProject.Areas.Student.Models;
using RepositoryProject.Service.Interface;

namespace RepositoryProject.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentController : Controller
    {
        private readonly IStudent _student;

        public StudentController(IStudent student)
        {
            _student = student;
        }

        // GET: 
        public async Task<IActionResult> Index()
        {
            StudentViewModels model = new()
            {
                students = await _student.GetStudent(),
            };


            return View(model);
        }

        // POST:
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] StudentViewModels model)
        {
            Students data = new Students
            {
                Id = model.studentId,
                studentCode = model.studentCode,
                studentName = model.studentName,
                studentDepartment = model.studentDepartment,
                studentFee = model.studentFee,
            };

            await _student.SaveStudent(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CheckStuCode(string stuCode)
        {
            var result = await _student.CheckStuCode(stuCode);
            return Json(result);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _student.DeleteStudentById(Id);
            return RedirectToAction(nameof(Index));
        }


    }
}