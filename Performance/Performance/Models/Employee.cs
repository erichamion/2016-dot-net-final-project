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

        //public List<Employee> GetChainOfCommand(bool includeSelf)
        //{
        //    List<Employee> result = new List<Employee>();

        //    Employee currentEmployee = includeSelf ? this : Manager;
        //    while (currentEmployee != null)
        //    {
        //        result.Add(currentEmployee);
        //        currentEmployee = Manager;
        //    }

        //    return result;
        //}

        public bool IsIndirectManagerOf(Employee target, int minSeparation = 0)
        {
            Employee currentEmployee = target;
            int separation = 0;
            
            while (currentEmployee != null)
            {
                if (currentEmployee.Id == this.Id)
                {
                    return (separation >= minSeparation);
                }
                separation++;
                currentEmployee = currentEmployee.Manager;
            }

            return false;
        }

        
    }
}