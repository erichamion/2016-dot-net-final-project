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
    [RoutePrefix("api/Stats")]
    public class StatsController : AbstractStatsController
    {
        
        
        // GET: api/Stats/5/2016-09-01/2016-09-05
        [Route("{id}/{startDate:DateTime?}/{endDate:DateTime?}")]
        [ResponseType(typeof(IndividualStatListDTO))]
        public override async Task<IHttpActionResult> GetDailyStatList([FromUri(Name="id")] string employeeId, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (!await CanAccessEmployeeAsync(employeeId))
            {
                return NotFound();
            }

            DateTime effStartDate = startDate.GetValueOrDefault(DateTime.Today);
            DateTime effEndDate = endDate.GetValueOrDefault(DateTime.Today);
            IndividualStatListDTO result = await IndividualStatListDTO.FromEmployeeIdAsync(db.DailyStatLists, employeeId, effStartDate, effEndDate);

            return (result != null) ? (IHttpActionResult)Ok(result) : NotFound();
        }
        
        
    }
}