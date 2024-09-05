using System.ComponentModel.DataAnnotations;

namespace MitraTechTest.Models
{
    public class Employee {
        public int EmployeeId { get; set; }
        
        [Required]
        public required string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public required DateTime BirthDate { get; set; }
    }
}