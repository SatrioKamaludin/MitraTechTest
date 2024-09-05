//Class for Swagger POST and PUT requestBody Example

using Swashbuckle.AspNetCore.Filters;
using MitraTechTest.Models;

public class EmployeeExample : IExamplesProvider<Employee>
{
    public Employee GetExamples()
    {
        return new Employee
        {
            FullName = "John Doe",
            BirthDate = DateTime.Parse("1990, 1, 1")
        };
    }
}
