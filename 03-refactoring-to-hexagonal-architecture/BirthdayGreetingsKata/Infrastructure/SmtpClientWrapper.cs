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

    public virtual void SendMessage(MessageDto messageDto)
    {
        // Construct the message
        var msg = new MailMessage
        {
            From = new MailAddress(Sender),
            Subject = Subject,
            Body = messageDto.To
        };
        msg.To.Add(messageDto.What);
        SmtpClient.Send(msg);
    }
}