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
    public class TeamStatListDTO : IStatListDTO
    {
        public double? AverageHandleTime { get; }
        public double? AverageHoldTime { get; }
        public double? AverageWorkTime { get; }
        public double? AttendancePoints { get; }

        public static async Task<TeamStatListDTO> FromManagerAsync(ApplicationDbContext context, Employee managerQuery, DateTime startDate, DateTime endDate)
        {
            DbSet<DailyStatList> dbSet = context.DailyStatLists;
            context.Entry(managerQuery).Collection(x => x.Subordinates);

            // Get all subordinates' stats
            List <IStatListDTO> subordinateStatLists = new List<IStatListDTO>();
            foreach (Employee subordinate in managerQuery.Subordinates)
            {
                subordinateStatLists.Add(await FromManagerAsync(context, subordinate, startDate, endDate));
            }

            //IEnumerable<IStatListDTO> subordinateStatLists = await Task.WhenAll(manager.Subordinates.Select(subordinate => FromManagerAsync(dbSet, subordinate, startDate, endDate)));

            // Get the manager's own stats if the manager has performed agent tasks
            IStatListDTO ownStatList = await IndividualStatListDTO.FromEmployeeIdAsync(dbSet, managerQuery.Id, startDate, endDate);
            if (ownStatList != null)
            {
                subordinateStatLists.Add(ownStatList);
            }

            // Average everything together, as long as there is something to average
            return (subordinateStatLists.Count() > 0) ? 
                new TeamStatListDTO(subordinateStatLists) :
                null;
        }

        private TeamStatListDTO(IEnumerable<IStatListDTO> memberStatLists)
        {
            var membersWithAHT = memberStatLists.Where(x => x.AverageHandleTime != null);
            var membersWithHoldTime = memberStatLists.Where(x => x.AverageHoldTime != null);
            var membersWithWorkTime = memberStatLists.Where(x => x.AverageWorkTime != null);
            var membersWithAttendance = memberStatLists.Where(x => x.AttendancePoints != null);

            if (membersWithAHT.Count() > 0)
            {
                AverageHandleTime = membersWithAHT.Average(x => x.AverageHandleTime);
            }
            if (membersWithHoldTime.Count() > 0)
            {
                AverageHoldTime = membersWithHoldTime.Average(x => x.AverageHoldTime);
            }
            if (membersWithWorkTime.Count() > 0)
            {
                AverageWorkTime = membersWithWorkTime.Average(x => x.AverageWorkTime);
            }
            if (membersWithAttendance.Count() > 0)
            {
                AttendancePoints = membersWithAttendance.Average(x => x.AttendancePoints);
            }
        }
    }

    public class IndividualStatListDTO : IStatListDTO
    {
        public String FirstName { get; }
        public String LastName { get; }
        public double? AverageHandleTime { get; }
        public double? AverageHoldTime { get; }
        public double? AverageWorkTime { get; }
        public double? AttendancePoints { get; }

        
        public static async Task<IndividualStatListDTO> FromEmployeeIdAsync(DbSet<DailyStatList> dbSet, String employeeId, DateTime startDate, DateTime endDate)
        {
            var dailyStatLists = await dbSet.Where(x => x.EmployeeId.Equals(employeeId) && x.Date >= startDate && x.Date <= endDate).ToListAsync();
            return FromDailyStatLists(dailyStatLists);
        }

        private static IndividualStatListDTO FromDailyStatLists(IList<DailyStatList> statLists)
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
        public int HoldTime { get; set; }

        [Required]
        public int WorkTime { get; set; }

        [Required]
        public double AttendancePoints { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}