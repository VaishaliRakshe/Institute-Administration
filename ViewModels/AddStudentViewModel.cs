using InstituteAdministration.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InstituteAdministration.ViewModels
{
    public class AddStudentViewModel
    {
        [Required(ErrorMessage = "Name is not Null")]
        public string StudentName { get; set; }

        public int TeacherID { get; set; }
        public List<SelectListItem> AllTeachers { get; set; }
        public List<int> CourseId { get; set; }
        public List<Course> AllCourses { get; set; }



        public AddStudentViewModel(List<Teacher> teachers, List<Course> courses)
        {

            AllTeachers = new List<SelectListItem>();
            foreach (Teacher tea in teachers)
            {
                AllTeachers.Add(new SelectListItem
                {
                    Text = tea.Name,
                    Value = tea.Id.ToString()
                });
            }
            AllCourses = courses;

        }
        public AddStudentViewModel()
        {

        }
    }
}
