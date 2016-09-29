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
    public class TeamStatsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: api/Stats
        //public IQueryable<DailyStatList> GetDailyStatLists()
        //{
        //    return db.DailyStatLists;
        //}

        // GET: api/Stats/5/2016-09-01/2016-09-
        [Route("{managerId}/{startDate:DateTime?}/{endDate:DateTime?}")]
        [ResponseType(typeof(TeamStatListDTO))]
        public async Task<IHttpActionResult> GetDailyStatList(string managerId, DateTime? startDate = null, DateTime? endDate = null)
        {
            DateTime effStartDate = startDate.GetValueOrDefault(DateTime.Today);
            DateTime effEndDate = endDate.GetValueOrDefault(DateTime.Today);
            var managerQuery = await db.Employees.FindAsync(managerId);
            TeamStatListDTO result = await TeamStatListDTO.FromManagerAsync(db, managerQuery, effStartDate, effEndDate);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }



        //// PUT: api/Stats/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutDailyStatList(string id, DailyStatList dailyStatList)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != dailyStatList.EmployeeId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(dailyStatList).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DailyStatListExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Stats
        //[ResponseType(typeof(DailyStatList))]
        //public async Task<IHttpActionResult> PostDailyStatList(DailyStatList dailyStatList)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.DailyStatLists.Add(dailyStatList);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (DailyStatListExists(dailyStatList.EmployeeId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = dailyStatList.EmployeeId }, dailyStatList);
        //}

        //// DELETE: api/Stats/5
        //[ResponseType(typeof(DailyStatList))]
        //public async Task<IHttpActionResult> DeleteDailyStatList(string id)
        //{
        //    DailyStatList dailyStatList = await db.DailyStatLists.FindAsync(id);
        //    if (dailyStatList == null)
        //    {
        //        return NotFound();
        //    }

        //    db.DailyStatLists.Remove(dailyStatList);
        //    await db.SaveChangesAsync();

        //    return Ok(dailyStatList);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DailyStatListExists(string id)
        {
            return db.DailyStatLists.Count(e => e.EmployeeId == id) > 0;
        }
    }
}