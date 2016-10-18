using Performance.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;

namespace Performance.Controllers
{
    public abstract class AbstractStatsController : ApiController
    {
        public abstract Task<IHttpActionResult> GetDailyStatList([FromUri(Name = "id")] string employeeId, DateTime? startDate = default(DateTime?), DateTime? endDate = default(DateTime?));

        protected ApplicationDbContext db = new ApplicationDbContext();

        protected async Task<bool> CanAccessEmployeeAsync(string targetEmployeeId)
        {
            Employee targetEmployee = db.Employees.Find(targetEmployeeId);
            return (targetEmployee != null && await CanAccessEmployeeAsync(targetEmployee));
        }

        protected async Task<bool> CanAccessEmployeeAsync(Employee targetEmployee)
        {
            ApplicationUserManager um = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser userInfo = await um.FindByIdAsync(User.Identity.GetUserId());
            return userInfo.Employee.IsIndirectManagerOf(targetEmployee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        protected bool DailyStatListExists(string id)
        {
            return db.DailyStatLists.Count(e => e.EmployeeId == id) > 0;
        }
    }
}