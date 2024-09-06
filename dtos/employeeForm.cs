// Data Transfer Object for Adding and Updating employee

// dtos\employeeFrom.cs
namespace MitraTechTest.Dtos
{
    public class EmployeeForm
    {
        public string FullName { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}