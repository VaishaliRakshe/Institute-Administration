using System.ComponentModel.DataAnnotations;
namespace InstituteAdministration.ViewModels
{
    public class AddTeacherViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
