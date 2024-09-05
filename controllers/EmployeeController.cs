using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using MitraTechTest.Models;
using MitraTechTest.Services;

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
                return Ok(_employeeService.GetEmployees());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                var newEmployee = _employeeService.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                if (!employeeUpdatePassed)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                if (!employeeDeletePassed)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
