using StudentManagementSystems.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentManagementSystems.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminEnrollmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminEnrollments
        public async Task<ActionResult> Index(string searchString, string studentSearchString, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CourseFilter = searchString;
            ViewBag.StudentFilter = studentSearchString;

            var enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Student);

            if (!String.IsNullOrEmpty(searchString))
            {
                enrollments = enrollments.Where(e => e.Course.CourseName.Contains(searchString) || e.Course.CourseID.ToString().Contains(searchString));
            }

            if (!String.IsNullOrEmpty(studentSearchString))
            {
                enrollments = enrollments.Where(e => e.Student.Name.Contains(studentSearchString) ||e.Student.StudentEmail.Contains(studentSearchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    enrollments = enrollments.OrderByDescending(e => e.Student.Name);
                    break;
                case "Date":
                    enrollments = enrollments.OrderBy(e => e.EnrollmentDate);
                    break;
                case "date_desc":
                    enrollments = enrollments.OrderByDescending(e => e.EnrollmentDate);
                    break;
                default:
                    enrollments = enrollments.OrderBy(e => e.Student.Name);
                    break;
            }

            return View(await enrollments.ToListAsync());
        }

        // GET: AdminEnrollments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enrollment enrollment = await db.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .SingleOrDefaultAsync(e => e.EnrollmentID == id);

            if (enrollment == null)
            {
                return HttpNotFound();
            }

            return View(enrollment);
        }

        // POST: AdminEnrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                db.Enrollments.Remove(enrollment);

                var course = await db.Courses.FindAsync(enrollment.CourseID);
                if (course != null)
                {
                    course.Quantity++;
                    db.Entry(course).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
