using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagementSystems.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        [StringLength(100)]
        public string Categories { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(100)]
        public string Instructor { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}