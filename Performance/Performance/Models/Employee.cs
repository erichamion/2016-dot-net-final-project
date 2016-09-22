using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Performance.Models
{
    public class Employee
    {
        [Required, Key]
        public String Id { get; set; }

        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        public virtual Employee Manager { get; set; }
    }
}