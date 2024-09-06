//Interface class
using MitraTechTest.Dtos;
using MitraTechTest.Models;

namespace MitraTechTest.Services
{
    public interface IEmployeeService
    {
        Response GetEmployees();
        Response GetEmployeeById(int id);
        Response AddEmployee(EmployeeForm employeeForm);
        bool EmployeeExists(string fullName, DateOnly birthDate);
        Response UpdateEmployee(int id, EmployeeForm employeeForm);
        Response DeleteEmployee(int id);
    }
}