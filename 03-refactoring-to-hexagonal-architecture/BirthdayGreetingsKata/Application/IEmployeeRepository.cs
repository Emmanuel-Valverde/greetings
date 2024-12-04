using System.Collections.Generic;
using BirthdayGreetingsKata.Domain;

namespace BirthdayGreetingsKata.Application;

public interface IEmployeeRepository
{
    public List<Employee> GetAllEmployees();
}
