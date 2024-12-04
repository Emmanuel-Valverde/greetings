using System.Net.Mail;
using BirthdayGreetingsKata.Domain;
using BirthdayGreetingsKata.Infrastructure;

namespace BirthdayGreetingsKata.Application;

public class BirthdayService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly SmtpClientWrapper _smtpClientWrapper;

    const string BodyTemplate = "Happy Birthday, dear %NAME%!";
    const string Subject = "Happy Birthday!";
    const string Sender = "sender@here.com";

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
                var (recipient, body) = SmtpClientWrapper.ComposeMessage(employee, BodyTemplate);
                SendMessage(body, recipient);
            }
        }
    }

    private void SendMessage(string body, string recipient)
    {
        var msg = SmtpClientWrapper.GetMessage(Sender, Subject, body, recipient);

        // Send the message
        SendMessage(msg, _smtpClientWrapper.SmtpClient);
    }

    // made protected for testing :-(
    protected virtual void SendMessage(MailMessage msg, SmtpClient smtpClient)
    {
        smtpClient.Send(msg);
    }
}