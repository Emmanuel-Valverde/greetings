using System.Collections.Generic;
using System.Net.Mail;
using BirthdayGreetingsKata.Application;
using BirthdayGreetingsKata.Domain;
using BirthdayGreetingsKata.Infrastructure;
using NUnit.Framework;

namespace BirthdayGreetingsKata.Tests;

public class AcceptanceTest
{
    private const int SmtpPort = 25;
    private List<MailMessage> _messagesSent;
    private BirthdayService _service;

    private class BirthdayServiceForTesting : BirthdayService
    {
        private readonly List<MailMessage> _messages;

        public BirthdayServiceForTesting(List<MailMessage> messages) : base(new FileEmployeeRepository(), new SmtpClientWrapper("localhost", SmtpPort))
        {
            _messages = messages;
        }

        protected override void SendMessage(MailMessage msg, SmtpClient smtpClient)
        {
            _messages.Add(msg);
        }
    }

    [SetUp]
    public void SetUp()
    {
        _messagesSent = new List<MailMessage>();
        _service = new BirthdayServiceForTesting(_messagesSent);
    }

    [Test]
    public void Base_Scenario()
    {
        _service.SendGreetings(new OurDate("2008/10/08"));

        Assert.That(_messagesSent, Has.Exactly(1).Items);
        var message = _messagesSent[0];
        Assert.That(message.Body, Is.EqualTo("Happy Birthday, dear John!"));
        Assert.That(message.Subject, Is.EqualTo("Happy Birthday!"));
        Assert.That(message.To, Has.Exactly(1).Items);
        Assert.That(message.To[0].Address, Is.EqualTo("john.doe@foobar.com"));
    }

    [Test]
    public void Will_Not_Send_Emails_When_Nobodies_Birthday()
    {
        _service.SendGreetings(new OurDate("2008/01/01"));

        Assert.That(_messagesSent, Is.Empty);
    }
}