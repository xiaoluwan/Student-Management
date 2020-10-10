using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.SqlServer.Server;
using StudentManagement.Models;
using StudentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace StudentManagement.Contorllers {

    public class HomeController : Controller { //主控制器
        private readonly IStudentRepository _studentRepository;//仓储接口
        private readonly HostingEnvironment hostingEnvironment;

        //使用构造函数注入的方式注入IstudentRepository
        public HomeController(IStudentRepository studentRepository, HostingEnvironment hostingEnvironment) {
            _studentRepository = studentRepository;
            this.hostingEnvironment = hostingEnvironment;//用来获取wwwroot目录
        }

        public IActionResult Index() {
            IEnumerable<Student> students = _studentRepository.GetAllStudents();
            return View(students);
        }
        public IActionResult Details(int id) {//详情//JsonResult
            throw new Exception("此异常发生在Details视图中");
            //return $"id={id},名字为666";
            Student student = _studentRepository.GetStudent(id);
            if (student==null) {
                Response.StatusCode = 404;//回复状态码
                return View("StudentNotFound", id);//未找到
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel() {
                student = student,//判断id为空则id=1
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
                if (model.Photos != null && model.Photos.Count > 0) {
                    uniqueFileName= ProcessUploadedFile(model);
                }
                Student newStudent = new Student {
                    Name = model.Name,
                    Email = model.Email,
                    ClassName = model.ClassName,
                    PhotoPath = uniqueFileName,
                };
                _studentRepository.Add(newStudent);
                return RedirectToAction("Details", new { id = newStudent.Id });
            }
            return View();//Create视图
        }

        [HttpGet]//读取数据库数据
        public IActionResult Edit(int id) { //通过Edit操作方法找到Edit视图   
            Student student = _studentRepository.GetStudent(id);//查询是否在数据库里
            StudentEditVideModel studentEditVidew = new StudentEditVideModel {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                ClassName = student.ClassName,
                ExistingPhotoPath = student.PhotoPath
            };

            return View(studentEditVidew);
        }
        [HttpPost]
        public IActionResult Edit(StudentEditVideModel model) {//创建StudentEditVideModel用于接收post表单数据
            if (ModelState.IsValid) {
                Student student = _studentRepository.GetStudent(model.Id);
                student.Email = model.Email;//视图模型赋值给领域模型
                student.Name = model.Name;
                student.ClassName = model.ClassName;
                if (model.Photos != null && model.Photos.Count > 0) {//用户有无选择新头像
                    if (model.ExistingPhotoPath != null) {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);//删除数据库旧头像  //Delete没有继承所以不是这里的问题,问题在FileStream流处理
                    }
                    student.PhotoPath = ProcessUploadedFile(model);
                }

                Student updataStudent = _studentRepository.Update(student);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //提炼共享的私有方法,处理文件信息
        /// <summary>
        /// 将图片保存指定的路径中,并返回唯一的文件名
        /// </summary>
        /// <returns></returns>
        private string ProcessUploadedFile(StudentCreateViewModel model) {

            string uniqueFileName = null;
            if (model.Photos != null&&model.Photos.Count>0) {
                foreach (var Photo in model.Photos) {
                    //必须将图像上传到WWWroot中的images文件夹
                    //而要获取wwwroot文件夹的路径，我们需要注入ASP.NET Core提供的HostingEnvironment服务
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    //为了确保文件名是唯一的，我们在文件名后附加一个新的GUID值和一个下划线
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;//添加全局唯一标识符
                    string filePath2 = Path.Combine(uploadFolder, uniqueFileName);
                    //使用IFormFile接口提供的CopyTo()方法将文件复制到wwwroot/images文件夹
                    //因为使用的非托管资源,所以需要手动释放.非托管资源垃圾回收器无法自动释放.
                    using (var fileStrem=new FileStream(filePath2,FileMode.Create)) {//这里使用
                        Photo.CopyTo(fileStrem);//这里释放
                    }
                    //Photo.CopyTo(new FileStream(filePath2, FileMode.Create));//流处理FileMode继承与IDisposable
                }
            }

            return uniqueFileName;
        }

        public IActionResult Delete(int id) {

           Student student=_studentRepository.Delete(id);
            if (student.PhotoPath != null) {
                string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", student.PhotoPath);
                System.IO.File.Delete(filePath);//删除数据库旧头像  //Delete没有继承所以不是这里的问题,问题在FileStream流处理
            }
          //return RedirectToAction("Index");
            var student2 = _studentRepository.GetAllStudents();
            return View("Index",student2);
        }
    }
}
