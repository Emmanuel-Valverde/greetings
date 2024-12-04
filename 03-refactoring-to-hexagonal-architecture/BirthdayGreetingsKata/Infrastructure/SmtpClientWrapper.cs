using System.Net.Mail;
using BirthdayGreetingsKata.Application;

namespace BirthdayGreetingsKata.Infrastructure;

public class SmtpClientWrapper : IMessageSender
{
    public SmtpClient SmtpClient { get; }
    const string Sender = "sender@here.com";
    const string Subject = "Happy Birthday!";
    
    public SmtpClientWrapper (string smtpHost, int smtpPort)
    {
        // Create a mail session
        SmtpClient = new SmtpClient(smtpHost)
        {
            Port = smtpPort
        };
    }

    public void SendMessage(string body, string recipient)
    {
        // Construct the message
        var msg = new MailMessage
        {
            From = new MailAddress(Sender),
            Subject = Subject,
            Body = body
        };
        msg.To.Add(recipient);
        SmtpClient.Send(msg);
    }
}