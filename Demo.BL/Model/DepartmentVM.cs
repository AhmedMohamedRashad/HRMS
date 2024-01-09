using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Model
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        [MinLength(3,ErrorMessage ="Min Length is 3")]
        [MaxLength(50,ErrorMessage ="Max Length is 50")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Code Required")]
        public string? Code { get; set; }
    }
}
