using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.ViewModels;
using System.Collections.Generic;

namespace StudentManagement.Contorllers {

    public class HomeController : Controller {
        private readonly IStudentRepository _studentRepository;//仓储接口
        public HomeController(IStudentRepository studentRepository) {
            _studentRepository = studentRepository;
        }

        public IActionResult Index() {
            IEnumerable<Student> students = _studentRepository.GetAllStudents();
            return View(students);
        }
        public IActionResult Details(int? id) {//详情//JsonResult
            //return $"id={id},名字为{Name}";

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel() {
                student = _studentRepository.GetStudent(id ?? 1),//判断id为空则id=1
                PageTitle = "学生详细信息"
            };
            return View(homeDetailsViewModel);
        }
        [HttpGet]
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student) {//点击创建按钮会调用Contorllers控制器，。net会通过模型绑定器来接收（post）表单的当中的Create操作方法将student对象发送的前端。
            if (ModelState.IsValid) {//模型验证
                Student newStudent = _studentRepository.Add(student);
                return RedirectToAction("Details", new { id = newStudent.Id });
            }
            return View();
        }
    }
}
