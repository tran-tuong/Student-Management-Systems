namespace StudentManagementSystems.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.BuilderProperties;
    using StudentManagementSystems.Models;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedStudentAndCourseData : DbMigration
    {
        public override void Up()
        {
            //InsertStudents
            // Tạo các bản ghi trong bảng Students và người dùng tương ứng
            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var students = new[]
            {
                new { Name = "Tran Van A", DateOfBirth = "2000-01-01", StudentEmail = "trana@vnu.edu.vn", PhoneNumber = "0987654321", Address = "Ha Noi" },
                new { Name = "Nguyen Van B", DateOfBirth = "2000-01-02", StudentEmail = "nguyenb@vnu.edu.vn", PhoneNumber = "0987654322", Address = "Quang Ninh" },
                new { Name = "Le Thi C", DateOfBirth = "2000-01-03", StudentEmail = "lec@vnu.edu.vn", PhoneNumber = "0987654323", Address = "Hai Phong" },
                new { Name = "Hoang Minh D", DateOfBirth = "2000-01-04", StudentEmail = "hoangd@vnu.edu.vn", PhoneNumber = "0987654324", Address = "Ha Noi" },
                new { Name = "Pham Thi E", DateOfBirth = "2000-01-05", StudentEmail = "phame@vnu.edu.vn", PhoneNumber = "0987654325", Address = "Quang Ninh" },
                new { Name = "Tran Van F", DateOfBirth = "2000-01-06", StudentEmail = "tranf@vnu.edu.vn", PhoneNumber = "0987654326", Address = "Hai Phong" },
                new { Name = "Nguyen Van G", DateOfBirth = "2000-01-07", StudentEmail = "nguyeng@vnu.edu.vn", PhoneNumber = "0987654327", Address = "Ha Noi" },
                new { Name = "Le Thi H", DateOfBirth = "2000-01-08", StudentEmail = "leh@vnu.edu.vn", PhoneNumber = "0987654328", Address = "Quang Ninh" },
                new { Name = "Hoang Minh I", DateOfBirth = "2000-01-09", StudentEmail = "hoangi@vnu.edu.vn", PhoneNumber = "0987654329", Address = "Hai Phong" },
                new { Name = "Pham Thi K", DateOfBirth = "2000-01-10", StudentEmail = "phamk@vnu.edu.vn", PhoneNumber = "0987654330", Address = "Ha Noi" }
            };

            foreach (var student in students)
            {
                Sql($"INSERT INTO Students (Name, DateOfBirth, StudentEmail, PhoneNumber, Address) VALUES ('{student.Name}', '{student.DateOfBirth}', '{student.StudentEmail}', '{student.PhoneNumber}', '{student.Address}');");

                var user = new ApplicationUser
                {
                    UserName = student.StudentEmail,
                    Email = student.StudentEmail,
                    PhoneNumber = student.PhoneNumber
                };

                var result = userManager.Create(user, "Ab12345!");
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create user {student.StudentEmail}: {string.Join(", ", result.Errors)}");
                }
            }

            //Insert Course
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Programming Technology', 'Technology', 1, 'ThS Nguyen Tran Dinh Long', 'This course covers various programming technologies and languages.', '2024-06-02');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Entrepreneurship', 'Business', 10, 'TS Pham Ngoc Thanh', 'This course explores the fundamentals of entrepreneurship and startup.', '2024-06-01');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Computer Systems', 'Technology', 10, 'TS Pham Thi Viet Huong', 'This course delves into computer systems architecture and design.', '2024-06-04');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Embedded Systems', 'Technology', 8, 'ThS Nguyen Tran Dinh Long', 'This course focuses on embedded systems development and applications.', '2024-07-01');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Web Development', 'Technology', 40, 'TS Pham Ngoc Thanh', 'This course covers web development techniques and frameworks.', '2024-09-01');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Business Management', 'Business', 30, 'TS Pham Thi Viet Huongg', 'This course explores the principles of business management and leadership.', '2024-06-09');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Data Science', 'Technology', 10, 'ThS Nguyễn Đình Trần Long', 'This course introduces data science concepts and tools for analysis.', '2024-06-06');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Marketing Strategy', 'Business', 3, 'TS Pham Ngoc Thanh', 'This course covers marketing strategies and techniques for businesses.', '2024-06-08');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Network Security', 'Technology', 98, 'TS Pham Thi Viet Huongg', 'This course focuses on network security principles and practices.', '2024-06-09');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Financial Management', 'Business', 20, 'ThS Nguyen Tran Dinh Longg', 'This course covers financial management principles and strategies.', '2024-07-01');");
        }

        public override void Down()
        {
            var studentEmails = new[]
            {
                "trana@vnu.edu.vn", "nguyenb@vnu.edu.vn", "lec@vnu.edu.vn", "hoangd@vnu.edu.vn",
                "phame@vnu.edu.vn", "tranf@vnu.edu.vn", "nguyeng@vnu.edu.vn", "leh@vnu.edu.vn",
                "hoangi@vnu.edu.vn", "phamk@vnu.edu.vn"
            };

            foreach (var email in studentEmails)
            {
                Sql($"DELETE FROM Students WHERE StudentEmail = '{email}';");

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = userManager.FindByEmail(email);
                if (user != null)
                {
                    userManager.Delete(user);
                }
            }
        }
    }
}
