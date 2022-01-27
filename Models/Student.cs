namespace InstituteAdministration.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Teacher Teacher { get; set; } = null!;

        public int TeacherId { get; set; }

        public List<StudentCourse> StudentCourses { get; set; } = null!;

    }
}
