using System;
using System.Net.Mail;
using BirthdayGreetingsKata.Application;
using BirthdayGreetingsKata.Domain;
using BirthdayGreetingsKata.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace BirthdayGreetingsKata.Tests;

public class AcceptanceTest
{
    private const int SmtpPort = 25;
    private BirthdayService _service;
    private SmtpClientWrapper _smtpClientWrapperSpy;
    
    [SetUp]
    public void SetUp()
    {
        _smtpClientWrapperSpy = Substitute.For<SmtpClientWrapper>("localhost", SmtpPort);
       
        _service = new BirthdayService(new FileEmployeeRepository(), _smtpClientWrapperSpy);
    }

    [Test]
    public void Base_Scenario()
    {
        _service.SendGreetings(new OurDate("2008/10/08"));
         _smtpClientWrapperSpy.Received(1).SendMessage("Happy Birthday, dear John!", "john.doe@foobar.com");
    }

    [Test]
    public void Will_Not_Send_Emails_When_Nobodies_Birthday()
    {
        _smtpClientWrapperSpy.DidNotReceive().SendMessage(Arg.Any<string>(), Arg.Any<string>());
        
        _service.SendGreetings(new OurDate("2008/01/01"));
    }
}