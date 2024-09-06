using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using MitraTechTest.Models;
using MitraTechTest.Services;
using MitraTechTest.Dtos;

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
                return Ok(new { getEmployeeesResult.Success, getEmployeeesResult.Message, getEmployeeesResult.Data });
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
                if (!employee.Success)
                {
                    return NotFound(new Response { Success = employee.Success, Message = employee.Message });
                }
                return Ok(new { employee.Success, employee.Message, employee.Data });
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
        public ActionResult<Employee> AddEmployee(Employee employee)
        {
            try
            {
                if (_employeeService.EmployeeExists(employee.FullName, employee.BirthDate)){
                    return BadRequest("Employee with the same name and birthdate already exists");
                }
                var newEmployee = _employeeService.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
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
        public IActionResult updateEmployee(int id, Employee employee)
        {
            try
            {
                var employeeUpdatePassed = _employeeService.UpdateEmployee(id, employee);
                if (!employeeUpdatePassed.Success)
                {
                    return NotFound(new Response { Success = false, Message = employeeUpdatePassed.Message });
                }
                return Ok(new Response { Success = true, Message = employeeUpdatePassed.Message } );
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
                if (!employeeDeletePassed.Success)
                {
                    return NotFound(new Response { Success = false, Message = employeeDeletePassed.Message });
                }
                return Ok(new Response { Success = true, Message = employeeDeletePassed.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred: " + ex.Message);
            }
        }
    }
}
