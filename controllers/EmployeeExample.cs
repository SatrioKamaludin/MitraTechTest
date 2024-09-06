//Class for Swagger POST and PUT requestBody Example

using Swashbuckle.AspNetCore.Filters;
using MitraTechTest.Dtos;

public class EmployeeExample : IExamplesProvider<EmployeeForm>
{
    public EmployeeForm GetExamples()
    {
        return new EmployeeForm
        {
            FullName = "John Doe",
            BirthDate = DateOnly.Parse("1990, 1, 1")
        };
    }
}
