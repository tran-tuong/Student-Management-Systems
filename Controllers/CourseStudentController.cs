using Microsoft.AspNet.Identity;
using StudentManagementSystems.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentManagementSystems.Controllers
{
    [Authorize]
    public class StudentCoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index(string searchString, string sortOrder)
        {
            var courses = from c in db.Courses select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.CourseName.Contains(searchString) || c.CourseID.ToString().Contains(searchString));
            }

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewBag.StartDateSortParm = sortOrder == "StartDate" ? "startDate_desc" : "StartDate";

            switch (sortOrder)
            {
                case "name_desc":
                    courses = courses.OrderByDescending(c => c.CourseName);
                    break;
                case "Quantity":
                    courses = courses.OrderBy(c => c.Quantity);
                    break;
                case "quantity_desc":
                    courses = courses.OrderByDescending(c => c.Quantity);
                    break;
                case "StartDate":
                    courses = courses.OrderBy(c => c.StartDate);
                    break;
                case "startDate_desc":
                    courses = courses.OrderByDescending(c => c.StartDate);
                    break;
                default:
                    courses = courses.OrderBy(c => c.CourseName);
                    break;
            }

            return View(await courses.ToListAsync());
        }

        // GET: StudentCourses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        // GET: Course/Enroll/5
        public async Task<ActionResult> Enroll(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            var userEmail = User.Identity.GetUserName();
            var student = db.Students.SingleOrDefault(s => s.StudentEmail == userEmail);
            if (student == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Check if the student is already enrolled in the course
            var existingEnrollment = db.Enrollments
                .Any(e => e.CourseID == course.CourseID && e.StudentID == student.StudentID);
            if (existingEnrollment)
            {
                ModelState.AddModelError("", "You are already enrolled in this course.");
                return View(course);
            }

            var enrollment = new Enrollment
            {
                CourseID = course.CourseID,
                StudentID = student.StudentID,
                EnrollmentDate = DateTime.Now
            };

            if (course.Quantity > 0)
            {
                course.Quantity--;
                db.Entry(course).State = EntityState.Modified;
            }
            else
            {
                ModelState.AddModelError("", "The course is fully booked.");
                return View(course);
            }

            db.Enrollments.Add(enrollment);
            await db.SaveChangesAsync();
            return RedirectToAction("EnrolledCourses");
        }

        // GET: Course/EnrolledCourses
        public async Task<ActionResult> EnrolledCourses()
        {
            var userEmail = User.Identity.GetUserName();
            var student = await db.Students.SingleOrDefaultAsync(s => s.StudentEmail == userEmail);
            if (student == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var enrollments = await db.Enrollments
                .Where(e => e.StudentID == student.StudentID)
                .Include(e => e.Course)
                .ToListAsync();

            return View(enrollments);
        }

        // POST: Course/Unenroll/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Unenroll(int id)
        {
            var userEmail = User.Identity.GetUserName();
            var student = await db.Students.SingleOrDefaultAsync(s => s.StudentEmail == userEmail);
            if (student == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var enrollment = await db.Enrollments
                .Where(e => e.CourseID == id && e.StudentID == student.StudentID)
                .FirstOrDefaultAsync();

            if (enrollment != null)
            {
                db.Enrollments.Remove(enrollment);

                var course = await db.Courses.FindAsync(id);
                if (course != null)
                {
                    course.Quantity++;
                    db.Entry(course).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();
            }

            return RedirectToAction("EnrolledCourses");
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
