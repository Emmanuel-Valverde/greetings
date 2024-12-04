using System;
using BirthdayGreetingsKata.Application;
using BirthdayGreetingsKata.Domain;
using BirthdayGreetingsKata.Infrastructure;

namespace BirthdayGreetingsKata;

public class App
{
    private const string SmtpHost = "localhost";
    private const int SmtpPort = 25;
    private static readonly OurDate OurDate = new OurDate("2008/10/08");
    private static readonly IEmployeeRepository EmployeeRepository = new FileEmployeeRepository();
    private static readonly SmtpClientWrapper SmtpClientWrapper = new(SmtpHost, SmtpPort);
    private static readonly BirthdayService BirthdayService  = new(EmployeeRepository, SmtpClientWrapper);

    private static void Main(string[] args)
    {
        try
        {
            BirthdayService.SendGreetings(OurDate);
        }
        catch (Exception e)
        {
            Console.Write(e.StackTrace);
        }
    }
}