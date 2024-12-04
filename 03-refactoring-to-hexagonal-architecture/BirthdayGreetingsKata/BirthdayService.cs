using System;
using System.Net.Mail;

namespace BirthdayGreetingsKata;

public class BirthdayService
{
    public void SendGreetings(OurDate ourDate,
        string smtpHost, int smtpPort, IEmployeeRepository employeeRepository)
    {
        var employees = employeeRepository.GetAllEmployees();

        const string bodyTemplate = "Happy Birthday, dear %NAME%!";
        const string subject = "Happy Birthday!";
        const string sender = "sender@here.com";
        
        foreach (var employee in employees)
        {
            if (employee.IsBirthday(ourDate))
            {
                var (recipient, body) = composeMessage(employee, bodyTemplate);
                SendMessage(smtpHost, smtpPort, sender, subject,
                    body, recipient);
            }
        }
    }

    private static (string, string) composeMessage(Employee employee, string bodyTemplate)
    {
        var recipient = employee.Email;
        var body = bodyTemplate.Replace("%NAME%",
            employee.FirstName);
        return (recipient, body);
    }

    private void SendMessage(string smtpHost, int smtpPort, string sender,
        string subject, string body, string recipient)
    {
        // Create a mail session
        var smtpClient = new SmtpClient(smtpHost)
        {
            Port = smtpPort
        };

        // Construct the message
        var msg = new MailMessage
        {
            From = new MailAddress(sender),
            Subject = subject,
            Body = body
        };
        msg.To.Add(recipient);

        // Send the message
        SendMessage(msg, smtpClient);
    }

    // made protected for testing :-(
    protected virtual void SendMessage(MailMessage msg, SmtpClient smtpClient)
    {
        smtpClient.Send(msg);
    }

    static void Main(string[] args)
    {
        var service = new BirthdayService();
        try
        {
            service.SendGreetings(new OurDate("2008/10/08"), "localhost", 25, new FileEmployeeRepository());
        }
        catch (Exception e)
        {
            Console.Write(e.StackTrace);
        }
    }
}