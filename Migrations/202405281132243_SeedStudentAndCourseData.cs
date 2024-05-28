namespace StudentManagementSystems.Migrations
{
    using Microsoft.Owin.BuilderProperties;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedStudentAndCourseData : DbMigration
    {
        public override void Up()
        {
            //InsertStudents
            Sql($"INSERT INTO Students (Name, DateOfBirth, StudentEmail, PhoneNumber, Address) VALUES ('Tran Van A', '2000-01-01', 'trana@vnu.edu.vn', '0987654321', 'Ha Noi');");
            Sql($"INSERT INTO Students (Name, DateOfBirth, StudentEmail, PhoneNumber, Address) VALUES ('Nguyen Van B', '2000-01-02', 'nguyenb@vnu.edu.vn', '0987654322', 'Quang Ninh');");
            Sql($"INSERT INTO Students (Name, DateOfBirth, StudentEmail, PhoneNumber, Address) VALUES ('Le Thi C', '2000-01-03', 'lec@vnu.edu.vn', '0987654323', 'Hai Phong');");
            Sql($"INSERT INTO Students (Name, DateOfBirth, StudentEmail, PhoneNumber, Address) VALUES ('Hoang Minh D', '2000-01-04', 'hoangd@vnu.edu.vn', '0987654324', 'Ha Noi');");
            Sql($"INSERT INTO Students (Name, DateOfBirth, StudentEmail, PhoneNumber, Address) VALUES ('Pham Thi E', '2000-01-05', 'phame@vnu.edu.vn', '0987654325', 'Quang Ninh');");
            Sql($"INSERT INTO Students (Name, DateOfBirth, StudentEmail, PhoneNumber, Address) VALUES ('Tran Van F', '2000-01-06', 'tranf@vnu.edu.vn', '0987654326', 'Hai Phong');");
            Sql($"INSERT INTO Students (Name, DateOfBirth, StudentEmail, PhoneNumber, Address) VALUES ('Nguyen Van G', '2000-01-07', 'nguyeng@vnu.edu.vn', '0987654327', 'Ha Noi');");
            Sql($"INSERT INTO Students (Name, DateOfBirth, StudentEmail, PhoneNumber, Address) VALUES ('Le Thi H', '2000-01-08', 'leh@vnu.edu.vn', '0987654328', 'Quang Ninh');");
            Sql($"INSERT INTO Students (Name, DateOfBirth, StudentEmail, PhoneNumber, Address) VALUES ('Hoang Minh I', '2000-01-09', 'hoangi@vnu.edu.vn', '0987654329', 'Hai Phong');");
            Sql($"INSERT INTO Students (Name, DateOfBirth, StudentEmail, PhoneNumber, Address) VALUES ('Pham Thi K', '2000-01-10', 'phamk@vnu.edu.vn', '0987654330', 'Ha Noi');");

            //Insert Course
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Programming Technology', 'Technology', 10, 'ThS Nguyen Tran Dinh Long', 'This course covers various programming technologies and languages.', '2024-06-02');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Entrepreneurship', 'Business', 10, 'TS Pham Ngoc Thanh', 'This course explores the fundamentals of entrepreneurship and startup.', '2024-06-01');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Computer Systems', 'Technology', 10, 'TS Pham Thi Viet Huong', 'This course delves into computer systems architecture and design.', '2024-06-04');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Embedded Systems', 'Technology', 10, 'ThS Nguyen Tran Dinh Long', 'This course focuses on embedded systems development and applications.', '2024-07-01');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Web Development', 'Technology', 10, 'TS Pham Ngoc Thanh', 'This course covers web development techniques and frameworks.', '2024-09-01');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Business Management', 'Business', 10, 'TS Pham Thi Viet Huongg', 'This course explores the principles of business management and leadership.', '2024-06-09');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Data Science', 'Technology', 10, 'ThS Nguyễn Đình Trần Long', 'This course introduces data science concepts and tools for analysis.', '2024-06-06');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Marketing Strategy', 'Business', 10, 'TS Pham Ngoc Thanh', 'This course covers marketing strategies and techniques for businesses.', '2024-06-08');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Network Security', 'Technology', 10, 'TS Pham Thi Viet Huongg', 'This course focuses on network security principles and practices.', '2024-06-09');");
            Sql($"INSERT INTO Courses (CourseName, Categories, Quantity, Instructor, Description, StartDate) VALUES ('Financial Management', 'Business', 10, 'ThS Nguyen Tran Dinh Longg', 'This course covers financial management principles and strategies.', '2024-07-01');");
        }

        public override void Down()
        {
        }
    }
}
