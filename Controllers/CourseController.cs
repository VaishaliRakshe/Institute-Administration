using InstituteAdministration.Data;
using InstituteAdministration.Models;
using InstituteAdministration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstituteAdministration.Controllers
{
    public class CourseController : Controller
    {
        private StudentDbContext context;

        public CourseController(StudentDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Course> courses = context.Courses.ToList();
            return View(courses);
        }

        public IActionResult Add()
        {
            Course course = new Course();
            return View(course);
        }

        [HttpPost]
        public IActionResult Add(Course course)
        {
            if (ModelState.IsValid)
            {
                context.Courses.Add(course);
                context.SaveChanges();
                return Redirect("/Course/");
            }

            return View("Add", course);
        }

        public IActionResult AddStudent(int id)
        {
            Student? theStudent = context.Students.Find(id);
            List<Course> possibleCourses = context.Courses.ToList();
            AddStudentCourseViewModel viewModel = new AddStudentCourseViewModel(theStudent, possibleCourses);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddStudent(AddStudentCourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                int studentId = viewModel.StudentId;
                int courseId = viewModel.CourseId;

                List<StudentCourse> existingItems = context.StudentCourses
                    .Where(js => js.StudentId == studentId)
                    .Where(js => js.CourseId == courseId)
                    .ToList();

                if (existingItems.Count == 0)
                {
                    StudentCourse studentCourse = new StudentCourse
                    {
                        StudentId = studentId,
                        CourseId = courseId
                    };
                    context.StudentCourses.Add(studentCourse);
                    context.SaveChanges();
                }

                return Redirect("/Home/Detail/" + studentId);
            }

            return View(viewModel);
        }

        public IActionResult About(int id)
        {
            List<StudentCourse> studentCourses = context.StudentCourses
                .Where(js => js.CourseId == id)
                .Include(js => js.Student)
                .Include(js => js.Course)
                .ToList();

            return View(studentCourses);
        }
    }
}
