using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Performance.Models
{
    public class Employee
    {
        [Key]
        public String Id { get; set; }

        public String managerId { get; set; }

        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        [ForeignKey("managerId")]
        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Subordinates { get; set; }
    }
}