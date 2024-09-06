using System.ComponentModel.DataAnnotations;

namespace MitraTechTest.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "The FullName field is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "The BirthDate field is required.")]
        public DateOnly BirthDate { get; set; }
    }
}