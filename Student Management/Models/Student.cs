using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models {
    /// <summary>
    /// 学生模型
    /// </summary>
    public class Student {
        public int Id { get; set; }
        [Display(Name = "名字")]
        [Required(ErrorMessage = "请输入名字")]
        //[MaxLength(10, ErrorMessage = "名字的长度不能超过50个字符")]
        [StringLength(10,MinimumLength =1,ErrorMessage ="1-10个字之间")]
        public string Name { get; set; }
        [Display(Name = "班级")]
        [Required]
        public ClassNameEnum? ClassName { get; set; }
        [Required(AllowEmptyStrings =true)]
        [Display(Name = "邮箱地址")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage ="邮箱的格式不正确")]
        public string Email { get; set; }

        public string PhotoPath { get; set; }

    }
}
