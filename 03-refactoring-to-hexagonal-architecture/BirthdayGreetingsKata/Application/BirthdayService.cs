using System.Net.Mail;
using BirthdayGreetingsKata.Domain;
using BirthdayGreetingsKata.Infrastructure;

namespace BirthdayGreetingsKata.Application;

public class BirthdayService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly SmtpClientWrapper _smtpClientWrapper;
    private readonly MessageDto _messageDto;

    const string BodyTemplate = "Happy Birthday, dear %NAME%!";
    
    

    public BirthdayService(IEmployeeRepository employeeRepository, SmtpClientWrapper smtpClientWrapper)
    {
        _employeeRepository = employeeRepository;
        _smtpClientWrapper = smtpClientWrapper;
    }

    public void SendGreetings(OurDate ourDate)
    {
        var employees = _employeeRepository.GetAllEmployees();
        foreach (var employee in employees)
        {
            if (employee.IsBirthday(ourDate))
            {
                MessageDto messageDto = new MessageDto( employee.Email, BodyTemplate.Replace("%NAME%",employee.FirstName));
                _smtpClientWrapper.SendMessage(null);
            }
        }
    }
}