using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models {
    /// <summary>
    /// 学生模型
    /// </summary>
    public class Student {
        public int Id { get; set; }
        [Display(Name="名字")]
        [Required(ErrorMessage ="请输入名字"),MaxLength(50,ErrorMessage ="名字的长度不能超过50个字符")]
        public string Name { get; set; }
        [Display(Name="班级")]
        [Required]
        public ClassNameEnum? ClassName { get; set; }
        [Required]
        [Display(Name="邮箱地址")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",ErrorMessage ="邮箱的格式不正确")]
        public string Email { get; set; }



    }
}
