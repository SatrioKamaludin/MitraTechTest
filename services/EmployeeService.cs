using MitraTechTest.Dtos;
using MitraTechTest.Models;

namespace MitraTechTest.Services
{
    public class EmployeeService : IEmployeeService
    {

        // Initialize 3 Existing Employee datas
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { EmployeeId = 1001, FullName = "Adit", BirthDate = new DateOnly(2054, 8, 17) },
            new Employee { EmployeeId = 1002, FullName = "Anton", BirthDate = new DateOnly(2054, 8, 18) },
            new Employee { EmployeeId = 1003, FullName = "Amir", BirthDate = new DateOnly(2054, 8, 19) },
        };

        //Obtain the list of employees
        public Response GetEmployees()
        {
            return new Response
            {
                Success = true,
                Message = "Employees retrieved successfully",
                Data = employees
            };
        }

        //Obtain an employee by ID
        public Response GetEmployeeById(int id)
        {
            if (!employees.Any(e => e.EmployeeId == id))
            {
                return new Response { Success = false, Message = "Employee " + id + " not found" };
            }
            return new Response
            {
                Success = true,
                Message = "Employee " + id + " retrieved successfully",
                Data = employees.FirstOrDefault(e => e.EmployeeId == id)
            };
        }

        //Add an employee
        public Employee AddEmployee(Employee employee)
        {
            employee.EmployeeId = employees.Max(e => e.EmployeeId) + 1; //Get the next ID by finding the max ID and adding 1
            employees.Add(employee);
            return employee;
        }

        //Check if existing employee already in data
        public bool EmployeeExists(string fullName, DateOnly birthDate)
        {
            return employees.Any(e => e.FullName == fullName && e.BirthDate == birthDate);
        }

        //Update an employee by ID
        public Response UpdateEmployee(int id, Employee employee)
        {
            var existingEmployee = employees.FirstOrDefault(e => e.EmployeeId == id);
            if (existingEmployee == null)
            {
                return new Response { Success = false, Message = "Employee " + id + " not found" };
            }
            existingEmployee.FullName = employee.FullName;
            existingEmployee.BirthDate = employee.BirthDate;
            return new Response { Success = true, Message = "Employee " + id + " updated successfully" };
        }

        //Delete an employee by ID
        public Response DeleteEmployee(int id)
        {
            var employee = employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return new Response { Success = false, Message = "Employee " + id + " not found" };
            }
            employees.Remove(employee);
            return new Response { Success = true, Message = "Employee " + id + " deleted successfully" };
        }
    }
}