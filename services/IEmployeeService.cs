//Interface class
using MitraTechTest.Dtos;
using MitraTechTest.Models;

namespace MitraTechTest.Services
{
    public interface IEmployeeService
    {
        Response GetEmployees();
        Response GetEmployeeById(int id);
        Employee AddEmployee(Employee employee);
        bool EmployeeExists(string fullName, DateOnly birthDate);
        Response UpdateEmployee(int id, Employee employee);
        Response DeleteEmployee(int id);
    }
}