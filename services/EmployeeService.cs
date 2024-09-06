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
                StatusCode = 200,
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
                return new Response { StatusCode = 404, Success = false, Message = "Employee " + id + " not found" };
            }
            return new Response
            {
                StatusCode = 200,
                Success = true,
                Message = "Employee " + id + " retrieved successfully",
                Data = employees.FirstOrDefault(e => e.EmployeeId == id)
            };
        }

        //Add an employee
        public Response AddEmployee(EmployeeForm employeeForm)
        {
            var validation = new List<string>();  // Store validation list

            if (string.IsNullOrEmpty(employeeForm.FullName))
            {
                validation.Add("The FullName field is required."); // Add error list
            }

            if (employeeForm.BirthDate == default)
            {
                validation.Add("The BirthDate field is required."); // Add error list
            }

            if (validation.Count > 0)
            {
                return new Response // Return validation errors
                {
                    StatusCode = 400,
                    Success = false,
                    Message = string.Join(", ", validation)
                };
            }

            if (EmployeeExists(employeeForm.FullName, employeeForm.BirthDate))
            {
                return new Response
                {
                    StatusCode = 409,
                    Success = false,
                    Message = "Employee already exists"
                };
            }
            Employee employee = new Employee
            {
                EmployeeId = employees.Max(e => e.EmployeeId) + 1,
                FullName = employeeForm.FullName,
                BirthDate = employeeForm.BirthDate //Get the next ID by finding the max ID and adding 1
            };

            employees.Add(employee);
            return new Response // Return successfully created employee
            {
                StatusCode = 201,
                Success = true,
                Message = "Employee " + employee.EmployeeId + " created successfully",
                Data = employee
            };
        }

        //Check if existing employee already in data
        public bool EmployeeExists(string fullName, DateOnly birthDate)
        {
            return employees.Any(e => e.FullName == fullName && e.BirthDate == birthDate);
        }

        //Update an employee by ID
        public Response UpdateEmployee(int id, EmployeeForm employeeForm)
        {
            var existingEmployee = employees.FirstOrDefault(e => e.EmployeeId == id);
            // Return error if employee not found
            if (existingEmployee == null)
            {
                return new Response { StatusCode = 404, Success = false, Message = "Employee " + id + " not found" };
            }

            // No changes if forms left blank
            if (string.IsNullOrEmpty(employeeForm.FullName) && employeeForm.BirthDate == default)
            {
                return new Response { StatusCode = 400, Success = true, Message = "No changes made" };
            }

            if(!string.IsNullOrEmpty(employeeForm.FullName))
            {
                existingEmployee.FullName = employeeForm.FullName;
            }

            if (employeeForm.BirthDate != default)
            {
                existingEmployee.BirthDate = employeeForm.BirthDate;
            }
            return new Response { StatusCode = 200, Success = true, Message = "Employee " + id + " updated successfully" };
        }

        //Delete an employee by ID
        public Response DeleteEmployee(int id)
        {
            var employee = employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return new Response { StatusCode = 404, Success = false, Message = "Employee " + id + " not found" };
            }
            employees.Remove(employee);
            return new Response { StatusCode = 200, Success = true, Message = "Employee " + id + " deleted successfully" };
        }
    }
}