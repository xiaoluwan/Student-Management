using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models {
    public static class ModelBuilderExtensions {
        public static void Seed(this ModelBuilder modelBuilder) {//扩展方法 ，解耦
            modelBuilder.Entity<Student>().HasData(
    new Student {
        Id = 1,
        Name = "yang",
        ClassName = ClassNameEnum.FirstGrade,
        Email = "1136083913@qq.com",
    },
        new Student {
            Id = 2,
            Name = "李四",
            ClassName = ClassNameEnum.GradeThree,
            Email = "999@qq.com",
        }
    );
        }
    }
}
