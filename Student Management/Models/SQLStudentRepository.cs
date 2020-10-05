using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models {
    public class SQLStudentRepository : IStudentRepository {
        private readonly AppDbContext context;

        public SQLStudentRepository(AppDbContext context) {
            this.context = context;
        }
        public Student Add(Student student) {
  
            context.Students.Add(student);
            context.SaveChanges();
            return student;
        }

        public Student Delete(int id) {
            Student student = context.Students.Find(id);
            if (student!=null) {
                context.Students.Remove(student);
                context.SaveChanges();
            }
            return student;
        }

        public IEnumerable<Student> GetAllStudents() {
            return context.Students;
        }

        public Student GetStudent(int id) {
            return context.Students.Find(id);
        }

        public Student Update(Student updateStudent) {
            var student = context.Students.Attach(updateStudent);//给updateStudent类打上标签，EFCroe才能把它放到Context里面 进行操作
            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;// 可以被修改的
            context.SaveChanges();
            return updateStudent;
        }
    }
}
