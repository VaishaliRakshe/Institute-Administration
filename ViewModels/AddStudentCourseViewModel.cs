using InstituteAdministration.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InstituteAdministration.ViewModels
{
    public class AddStudentCourseViewModel
    {
        [Required(ErrorMessage = "Student is required")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public int CourseId { get; set; }

        public Student Student { get; set; }

        public List<SelectListItem> Courses { get; set; }

        public AddStudentCourseViewModel(Student theStudent, List<Course> possibleCourses)
        {
            Courses = new List<SelectListItem>();

            foreach (var course in possibleCourses)
            {
                Courses.Add(new SelectListItem
                {
                    Value = course.Id.ToString(),
                    Text = course.Name
                });
            }

            Student = theStudent;
        }

        public AddStudentCourseViewModel()
        {
        }
    }
}
