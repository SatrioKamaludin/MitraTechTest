using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using MitraTechTest.Models;
using MitraTechTest.Services;
using MitraTechTest.Dtos;

// Controllers that call the Employee Service to get, add, update, and delete employees
namespace MitraTechTest.Controllers
{
    /// <summary>
    /// Handles CRUD operations for employees.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <returns>A list of employees.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Gets a list of all employees.")]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                var getEmployeeesResult = _employeeService.GetEmployees();
                return StatusCode(getEmployeeesResult.StatusCode, new { getEmployeeesResult.Success, getEmployeeesResult.Message, getEmployeeesResult.Data });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred: " + ex.Message);
            }
        }

        /// <param name="id">The ID of the employee.</param>
        /// <returns>The employee with the specified ID.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets an employee by ID.")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            try
            {
                var employee = _employeeService.GetEmployeeById(id);
                return StatusCode(employee.StatusCode, new { employee.Success, employee.Message, employee.Data });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred: " + ex.Message);
            }
        }

        /// <param name="employee">The employee to create.</param>
        /// <returns>The created employee.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new employee.")]
        [SwaggerRequestExample(typeof(Employee), typeof(EmployeeExample))]
        public ActionResult<Employee> AddEmployee(EmployeeForm employeeForm)
        {
            try
            {
                var newEmployee = _employeeService.AddEmployee(employeeForm);
                return StatusCode(newEmployee.StatusCode, new { newEmployee.Success, newEmployee.Message, newEmployee.Data });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred: " + ex.Message);
            }
        }

        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="employee">The updated employee data.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an existing employee.")]
        [SwaggerRequestExample(typeof(Employee), typeof(EmployeeExample))]
        public IActionResult updateEmployee(int id, EmployeeForm employeeForm)
        {
            try
            {
                var employeeUpdatePassed = _employeeService.UpdateEmployee(id, employeeForm);
                return StatusCode(employeeUpdatePassed.StatusCode, new { employeeUpdatePassed.Success, employeeUpdatePassed.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred: " + ex.Message);
            }
        }

        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes an employee by ID.")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var employeeDeletePassed = _employeeService.DeleteEmployee(id);
                return StatusCode(employeeDeletePassed.StatusCode, new { employeeDeletePassed.Success, employeeDeletePassed.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred: " + ex.Message);
            }
        }
    }
}
