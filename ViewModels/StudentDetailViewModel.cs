using InstituteAdministration.Models;

namespace InstituteAdministration.ViewModels
{
    public class StudentDetailViewModel
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; }
        public string CourseText { get; set; }

        public StudentDetailViewModel(Student theStudent, List<StudentCourse> studentCourses)
        {
            StudentId = theStudent.Id;
            Name = theStudent.Name;
            TeacherName = theStudent.Teacher.Name;

            CourseText = "";
            for (int i = 0; i < studentCourses.Count; i++)
            {
                CourseText += studentCourses[i].Course.Name;
                if (i < studentCourses.Count - 1)
                {
                    CourseText += ", ";
                }
            }
        }
    }
}
