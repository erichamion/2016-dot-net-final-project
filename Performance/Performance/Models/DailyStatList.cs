using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Performance.Models
{
    public class IndividualStatListDTO
    {
        public String FirstName { get; }
        public String LastName { get; }
        public double? AverageHandleTime { get; } = null;
        public double? AverageHoldTime { get; } = null;
        public double? AverageWorkTime { get; } = null;
        public double AttendancePoints { get; }

        public static IndividualStatListDTO FromDailyStatLists(IList<DailyStatList> statLists)
        {
            if (statLists == null || statLists.Count() == 0) return null;
            return new IndividualStatListDTO(statLists);
        }

        private IndividualStatListDTO(IList<DailyStatList> statLists)
        {
            int totalCalls = 0;
            // We can keep these as ints, don't need longs. An int holds about 68 years in seconds.
            int totalHandleTime = 0;
            int totalHoldTime = 0;
            int totalWorkTime = 0;
            this.AttendancePoints = 0;

            foreach (DailyStatList statList in statLists)
            {
                if (this.FirstName == null && this.LastName == null)
                {
                    this.FirstName = statList.Employee.FirstName;
                    this.LastName = statList.Employee.LastName;
                }

                totalCalls += statList.Calls;
                totalHandleTime += statList.HandleTime;
                totalWorkTime += statList.WorkTime;
                totalHoldTime += statList.HoldTime;

                this.AttendancePoints += statList.AttendancePoints;
            }

            if (totalCalls > 0) { 
                double calls = totalCalls;
            
                this.AverageHandleTime = totalHandleTime / calls;
                this.AverageHoldTime = totalHoldTime / calls;
                this.AverageWorkTime = totalWorkTime / calls;
            }
        }

        
    }

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
        public virtual Employee Employee { get; set; }
    }
}