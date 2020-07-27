using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.Models {
    public class MockStudentRepositroy : IStudentRepository {//模拟学生资料库
        private List<Student> _studentsList;

        public MockStudentRepositroy() {
            _studentsList = new List<Student>() {
                new Student(){Id=1,Name="张三",ClassName=ClassNameEnum.FirstGrade,Email="111.com"},
                new Student(){Id=2,Name="李四",ClassName=ClassNameEnum.SecondGrade,Email="222.com"},
                new Student(){Id=3,Name="王二",ClassName=ClassNameEnum.GradeThree,Email="333.com"},

            };

        }

        public Student Add(Student student) {
            student.Id = _studentsList.Max(s => s.Id) + 1;
            _studentsList.Add(student);
            return student;
        }

        public IEnumerable<Student> GetAllStudents() {
            return _studentsList;
        }

        public Student GetStudent(int id) {
            return _studentsList.FirstOrDefault(a => a.Id == id);
        }
    }
}
