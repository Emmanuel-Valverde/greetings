using System.Collections.Generic;

namespace BirthdayGreetingsKata;

public interface IEmployeeRepository
{
    public List<Employee> GetAllEmployees();
}
