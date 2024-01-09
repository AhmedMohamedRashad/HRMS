using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BL.Model
{
    public class EmployeeVM
    {
        public EmployeeVM()
        {
            CreationDate= DateTime.Now;
            IsDeleted= false;
            IsUpdated= false;
        }
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Required")]
        [MinLength(3,ErrorMessage ="Min length is 3")]
        [MaxLength(50, ErrorMessage = "Max length is 50")]
        public string Name { get; set; }
        public string? Notes { get; set; }
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string? Email { get; set; }
        [RegularExpression("[0-9]{2,5}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}")]
        public string Address { get; set; }
        [Range(3000,50000,ErrorMessage ="Salary btw 3K : 50k")]
        public double Salary { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsUpdated { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
        public int? DistrictId { get; set; }

        public District? District { get; set; }
        public string? CvName { get; set; }
        public string? ImageName { get; set; }

        public IFormFile? Cv { get; set; }
        public IFormFile? Image { get; set; }

    }
}
