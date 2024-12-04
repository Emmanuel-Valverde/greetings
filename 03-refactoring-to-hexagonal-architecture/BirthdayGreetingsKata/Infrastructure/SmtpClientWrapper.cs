using System.Net.Mail;
using BirthdayGreetingsKata.Application;
using BirthdayGreetingsKata.Domain;

namespace BirthdayGreetingsKata.Infrastructure;

public class SmtpClientWrapper : IMessageSender
{
    public SmtpClient SmtpClient { get; }
    public SmtpClientWrapper (string smtpHost, int smtpPort)
    {
        // Create a mail session
        SmtpClient = new SmtpClient(smtpHost)
        {
            Port = smtpPort
        };
    }

    public static MailMessage GetMessage(string sender, string subject, string body, string recipient)
    {
        // Construct the message
        var msg = new MailMessage
        {
            From = new MailAddress(sender),
            Subject = subject,
            Body = body
        };
        msg.To.Add(recipient);
        return msg;
    }

    public static (string, string) ComposeMessage(Employee employee, string bodyTemplate)
    {
        var recipient = employee.Email;
        var body = bodyTemplate.Replace("%NAME%",
            employee.FirstName);
        return (recipient, body);
    }
}