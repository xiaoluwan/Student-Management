using System.Collections.Generic;

namespace StudentManagement.Models {
    public interface IStudentRepository {//学生管理
        Student GetStudent(int id);
        IEnumerable<Student> GetAllStudents();//在内存中的数据
        Student Add(Student student);
    }
}
