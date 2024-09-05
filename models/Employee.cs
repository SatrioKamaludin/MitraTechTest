using System.ComponentModel.DataAnnotations;

namespace MitraTechTest.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "The FullName field is required.")]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "The BirthDate field is required.")]
        [DataType(DataType.Date)]
        //[RegularExpression(@"\d{4}-\d{2}-\d{2}", ErrorMessage = "The BirthDate field must be valid (YYYY-MM-DD).")]
        public required DateTime BirthDate { get; set; }
    }
}