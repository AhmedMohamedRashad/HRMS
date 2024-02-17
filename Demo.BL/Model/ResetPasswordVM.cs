using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Model
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Password Required")]
        [RegularExpression("^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?\\d)(?=.*?[\\W_]).{6,}$", ErrorMessage = " password must contain at least eight characters, at least one number and both lower and uppercase letters and special characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Required")]
        [RegularExpression("^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?\\d)(?=.*?[\\W_]).{6,}$", ErrorMessage = " password must contain at least eight characters, at least one number and both lower and uppercase letters and special characters")]
        [Compare("Password", ErrorMessage = "password not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }

    }
}
