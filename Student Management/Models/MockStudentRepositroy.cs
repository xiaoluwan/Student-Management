using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.Models {
    public class MockStudentRepositroy66 : IStudentRepository {//模拟学生资料库
        private List<Student> _studentsList;

        public MockStudentRepositroy66() {
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

        public Student Delete(int id) {
            Student student = _studentsList.FirstOrDefault(s => s.Id == id);
            if (student!=null) {
                _studentsList.Remove(student);
            }
            return student;
        }

        public IEnumerable<Student> GetAllStudents() {
            return _studentsList;
        }

        public Student GetStudent(int id) {
            return _studentsList.FirstOrDefault(a => a.Id == id);
        }

        public Student Update(Student updateStudent) {
            Student student = _studentsList.FirstOrDefault(a => a.Id == updateStudent.Id);
            if (student!=null) {
                student.Name = updateStudent.Name;
                student.Email = updateStudent.Email;
                student.ClassName = updateStudent.ClassName;
            }
            return student;
        }
    }
}
