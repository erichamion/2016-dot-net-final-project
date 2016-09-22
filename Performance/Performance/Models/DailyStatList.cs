using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Performance.Models
{
    public class DailyStatList
    {
        [Required]
        public String EmployeeId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Calls { get; set; }

        [Required]
        public int HandleTime { get; set; }

        [Required]
        public int TalkTime { get; set; }

        [Required]
        public int HoldTime { get; set; }

        [Required]
        public int WorkTime { get; set; }

        [Required]
        public double AttendancePoints { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}