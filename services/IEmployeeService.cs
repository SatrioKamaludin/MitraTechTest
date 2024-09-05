//Interface class
using MitraTechTest.Models;

namespace MitraTechTest.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();
        Employee? GetEmployeeById(int id);
        Employee AddEmployee(Employee employee);
        bool EmployeeExists(string fullName, DateTime birthDate);
        bool UpdateEmployee(int id, Employee employee);
        bool DeleteEmployee(int id);
    }
}