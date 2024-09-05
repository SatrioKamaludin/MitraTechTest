using MitraTechTest.Models;

namespace MitraTechTest.Services
{
    public class EmployeeService : IEmployeeService
    {

        // Initialize 3 Existing Employee datas
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { EmployeeId = 1001, FullName = "Adit", BirthDate = new DateTime(2054, 8, 17) },
            new Employee { EmployeeId = 1002, FullName = "Anton", BirthDate = new DateTime(2054, 8, 18) },
            new Employee { EmployeeId = 1003, FullName = "Amir", BirthDate = new DateTime(2054, 8, 19) },
        };

        //Obtain the list of employees
        public IEnumerable<Employee> GetEmployees()
        {
            return employees;
        }

        //Obtain an employee by ID
        public Employee? GetEmployeeById(int id)
        {
            return employees.FirstOrDefault(e => e.EmployeeId == id);
        }

        //Add an employee
        public Employee AddEmployee(Employee employee)
        {
            employee.EmployeeId = employees.Max(e => e.EmployeeId) + 1; //Get the next ID by finding the max ID and adding 1
            employees.Add(employee);
            return employee;
        }

        //Check if existing employee already in data
        public bool EmployeeExists(string fullName, DateTime birthDate)
        {
            return employees.Any(e => e.FullName == fullName && e.BirthDate == birthDate);
        }

        //Update an employee by ID
        public bool UpdateEmployee(int id, Employee employee)
        {
            var existingEmployee = employees.FirstOrDefault(e => e.EmployeeId == id);
            if (existingEmployee == null)
            {
                return false;
            }
            existingEmployee.FullName = employee.FullName;
            existingEmployee.BirthDate = employee.BirthDate;
            return true;
        }

        //Delete an employee by ID
        public bool DeleteEmployee(int id)
        {
            var employee = employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return false;
            }
            employees.Remove(employee);
            return true;
        }
    }
}