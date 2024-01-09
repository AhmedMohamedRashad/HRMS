using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entity
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(50)]
        public string Name { get; set; }
        public string? Notes { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }
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
    }
}
