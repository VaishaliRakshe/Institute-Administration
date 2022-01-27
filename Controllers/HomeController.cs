using InstituteAdministration.Data;
using InstituteAdministration.Models;
using InstituteAdministration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InstituteAdministration.Controllers
{
    public class HomeController : Controller
    {
        private StudentDbContext context;

        public HomeController(StudentDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
             //List<Student> students = context.Students.ToList();
            List<Student> students = context.Students.Include(j => j.Teacher).ToList();
           

            return View();
        }



        public IActionResult AddStudent()
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context.Teachers.ToList(), context.Courses.ToList());
            return View(addStudentViewModel);
        }

        [HttpPost]

        public IActionResult ProcessAddStudentForm(AddStudentViewModel addStudentViewModel, string[] selectedCourses)
        {
            if (ModelState.IsValid)
            {
                Student newStudent = new Student
                {
                    Name = addStudentViewModel.StudentName,
                    TeacherId = addStudentViewModel.TeacherID
                };

                foreach (string course in selectedCourses)
                {
                    StudentCourse studentCourse = new StudentCourse
                    {
                        StudentId = newStudent.Id,
                        CourseId = Int32.Parse(course),
                        Student = newStudent



                    };
                    context.StudentCourses.Add(studentCourse);
                }

                context.Students.Add(newStudent);
                context.SaveChanges();

                return Redirect("Index");
            }

            return View("AddStudent", addStudentViewModel);
        }

        public IActionResult Detail(int id)
        {
            Student theStudent = context.Students
                .Include(j => j.Teacher)
                .Single(j => j.Id == id);

            List<StudentCourse> studentCourses = context.StudentCourses
                .Where(js => js.StudentId == id)
                .Include(js => js.Course)
                .ToList();

            StudentDetailViewModel viewModel = new StudentDetailViewModel(theStudent, studentCourses);
            return View(viewModel);
        }
    }
}