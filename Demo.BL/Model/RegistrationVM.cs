using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Model
{
    public class RegistrationVM
    {
        [Required(ErrorMessage ="Email Required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [MinLength(6,ErrorMessage ="Min length password is 6")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Required")]
        [MinLength(6, ErrorMessage = "Min length password is 6")]
        [Compare("Password",ErrorMessage ="password not match")]
        public string ConfirmPassword { get; set; }
        
        public bool IsAgree { get; set; }

    }
}
