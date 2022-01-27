using InstituteAdministration.Data;
using InstituteAdministration.Models;
using InstituteAdministration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstituteAdministration.Controllers
{
    public class ListController : Controller
    {
        internal static Dictionary<string, string> ColumnChoices = new Dictionary<string, string>()
        {
            {"all", "All"},
            {"teacher", "Teacher"},
            {"course", "Course"}
        };

        internal static List<string> TableChoices = new List<string>()
        {
            "teacher",
            "course"
        };

        private StudentDbContext context;

        public ListController(StudentDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.columns = ColumnChoices;
            ViewBag.tablechoices = TableChoices;
            ViewBag.teachers = context.Teachers.ToList();
            ViewBag.courses = context.Courses.ToList();
            return View();
        }

        // list jobs by column and value
        public IActionResult Students(string column, string value)
        {
            List<Student> students = new List<Student>();
            List<StudentDetailViewModel> displayStudents = new List<StudentDetailViewModel>();

            if (column.ToLower().Equals("all"))
            {
                students = context.Students
                    .Include(j => j.Teacher)
                    .ToList();

                foreach (var student in students)
                {
                    List<StudentCourse> studentCourses = context.StudentCourses
                        .Where(js => js.StudentId == student.Id)
                        .Include(js => js.Course)
                        .ToList();

                    StudentDetailViewModel newDisplayStudent = new StudentDetailViewModel(student, studentCourses);
                    displayStudents.Add(newDisplayStudent);
                }

                ViewBag.title = "All Students";
            }
            else
            {
                if (column == "teacher")
                {
                    students = context.Students
                        .Include(j => j.Teacher)
                        .Where(j => j.Teacher.Name == value)
                        .ToList();

                    foreach (Student student in students)
                    {
                        List<StudentCourse> studentCourses = context.StudentCourses
                        .Where(js => js.StudentId == student.Id)
                        .Include(js => js.Course)
                        .ToList();

                        StudentDetailViewModel newDisplayStudent = new StudentDetailViewModel(student, studentCourses);
                        displayStudents.Add(newDisplayStudent);
                    }
                }
                else if (column == "course")
                {
                    List<StudentCourse> studentCourses = context.StudentCourses
                        .Where(j => j.Course.Name == value)
                        .Include(j => j.Student)
                        .ToList();

                    foreach (var student in studentCourses)
                    {
                        Student foundStudent = context.Students
                            .Include(j => j.Teacher)
                            .Single(j => j.Id == student.StudentId);

                        List<StudentCourse> displayCourses = context.StudentCourses
                            .Where(js => js.StudentId == foundStudent.Id)
                            .Include(js => js.Course)
                            .ToList();

                        StudentDetailViewModel newDisplayStudent = new StudentDetailViewModel(foundStudent, displayCourses);
                        displayStudents.Add(newDisplayStudent);
                    }
                }
                ViewBag.title = "Students with " + ColumnChoices[column] + ": " + value;
            }
            ViewBag.students = displayStudents;

            return View();
        }
    }
}
