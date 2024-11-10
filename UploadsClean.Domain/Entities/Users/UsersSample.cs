using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadsClean.Domain.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "نام الزامی است")]
        public string FarsiFirstName { set; get; }
        [Required(ErrorMessage = "نام خانوادگی الزامی است ")]
        public string FarsiLastName { get; set; }
        [Required(ErrorMessage = "ورود رمز غبور اجباری است ")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ورود  تکرار رمز غبور اجباری است ")]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور باید شبیه رمز عبور باشد")]
		public string ComfirmPassword { get; set; }
        public string Role { get; set; }


    }
}
