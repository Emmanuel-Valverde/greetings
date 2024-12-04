using System.Collections.Generic;
using System.IO;

namespace BirthdayGreetingsKata;


public class FileEmployeeRepository : IEmployeeRepository
{
    private const string FileName = "employee_data.txt";

    public List<Employee> GetAllEmployees()
    {
        var employees = new List<Employee>();
        using var reader = new StreamReader(FileName);
        var str = "";
        str = reader.ReadLine(); // skip header
        while ((str = reader.ReadLine()) != null)
        {
            var employeeData = str.Split(", ");
            var firstName = employeeData[1];
            var lastName = employeeData[0];
            var birthDate = employeeData[2];
            var email = employeeData[3];
            employees.Add(new Employee(firstName, lastName,
                birthDate, email));
        }

        return employees;
    }
}