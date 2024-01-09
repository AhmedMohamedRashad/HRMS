using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entity
{
   
    [Table("District")]
    public class District
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string? Name { get; set; }

        public int CityId { get; set; }

        public City? City { get; set; }

    }
}
