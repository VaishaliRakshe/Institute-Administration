using InstituteAdministration.Models;
using Microsoft.EntityFrameworkCore;




namespace InstituteAdministration.Data
{
    public class StudentDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<StudentCourse> StudentCourses { get; set; } = null!;

        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(j => new { j.StudentId, j.CourseId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
