using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Performance.Models;

namespace Performance.Controllers
{
    [Authorize]
    [RoutePrefix("api/TeamStats")]
    public class TeamStatsController : AbstractStatsController
    {
        // GET: api/Stats/5/2016-09-01/2016-09-
        [Route("{managerId}/{startDate:DateTime?}/{endDate:DateTime?}")]
        [ResponseType(typeof(TeamStatListDTO))]
        public override async Task<IHttpActionResult> GetDailyStatList(string managerId, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (!await CanAccessEmployeeAsync(managerId)) return NotFound();

            DateTime effStartDate = startDate.GetValueOrDefault(DateTime.Today);
            DateTime effEndDate = endDate.GetValueOrDefault(DateTime.Today);
            var managerQuery = await db.Employees.FindAsync(managerId);
            TeamStatListDTO result = await TeamStatListDTO.FromManagerAsync(db, managerQuery, effStartDate, effEndDate);
            
            return (result != null) ? (IHttpActionResult)Ok(result) : NotFound();
        }
        
        
    }
}