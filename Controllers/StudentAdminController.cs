using StudentManagementSystems.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementSystems.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentAdmin
        public async Task<ActionResult> Index(string searchString, string sortOrder)
        {
            var students = from s in db.Students
                           select s;

            // Search
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString)
                                       || s.StudentEmail.Contains(searchString)
                                       || s.StudentID.ToString().Contains(searchString));
            }

            // Sort
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParm = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "ID":
                    students = students.OrderBy(s => s.StudentID);
                    break;
                case "ID_desc":
                    students = students.OrderByDescending(s => s.StudentID);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.DateOfBirth);
                    break;
                case "Date_desc":
                    students = students.OrderByDescending(s => s.DateOfBirth);
                    break;
                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }

            return View(await students.ToListAsync());
        }

        // GET: StudentAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StudentID,Name,DateOfBirth,StudentEmail,PhoneNumber,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: StudentAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: StudentAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StudentID,Name,DateOfBirth,StudentEmail,PhoneNumber,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: StudentAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: StudentAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Student student = await db.Students.FindAsync(id);
            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
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
