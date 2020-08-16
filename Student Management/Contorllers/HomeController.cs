using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentManagement.Models;
using StudentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace StudentManagement.Contorllers {

    public class HomeController : Controller { //家庭控制器
        private readonly IStudentRepository _studentRepository;//仓储接口
        private readonly HostingEnvironment hostingEnvironment;

        //使用构造函数注入的方式注入IstudentRepository
        public HomeController(IStudentRepository studentRepository,HostingEnvironment hostingEnvironment) {
            _studentRepository = studentRepository;
            this.hostingEnvironment = hostingEnvironment;//用来获取wwwroot目录
        }

        public IActionResult Index() {
            IEnumerable<Student> students = _studentRepository.GetAllStudents();
            return View(students);
        }
        public IActionResult Details(int? id) {//详情//JsonResult
            //return $"id={id},名字为666";

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
        //当_studentRepository注入到控制器之中，在添加学生的时候从from表单提交POST请求，Create操作方法响应
        //post请求的时候将student实例对象添加到_studentRepository仓储服务（默认是MockStudentRepositroy）之中
        //所有将内容默认添加到了内存当中。
        public IActionResult Create(StudentCreateViewModel model) {//点击创建按钮会调用Contorllers控制器，。net会通过模型绑定器来接收（post）表单的当中的Create操作方法将student对象发送的前端。
            if (ModelState.IsValid) {//模型验证         上传的照片保存到wwwroot                     
                //Student newStudent = _studentRepository.Add(student);
                //return RedirectToAction("Details", new { id = newStudent.Id });
                string uniqueFileName = null;
                if (model.Photo!=null) {
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder , uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Student newStudent = new Student {
                    Name = model.Name,
                    Email = model.Email,
                    ClassName = model.ClassName,
                    PhotoPath = uniqueFileName
                };
                _studentRepository.Add(newStudent);
                return RedirectToAction("Details", new { id = newStudent.Id });
            }
            return View();//Create视图
        }
    }
}
