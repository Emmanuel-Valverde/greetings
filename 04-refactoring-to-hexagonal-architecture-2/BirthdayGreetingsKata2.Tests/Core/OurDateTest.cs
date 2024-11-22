using NUnit.Framework;
using static BirthdayGreetingsKata2.Tests.helpers.OurDateFactory;

namespace BirthdayGreetingsKata2.Tests.Core;

public class OurDateTest
{
    [Test]
    public void Identifies_If_Two_Dates_Were_In_The_Same_Day()
    {
        var ourDate = OurDate("1789/01/24");
        var sameDay = OurDate("2001/01/24");
        var notSameDay = OurDate("1789/01/25");
        var notSameMonth = OurDate("1789/02/24");

        Assert.That(ourDate.IsSameDay(sameDay), Is.True, "same");
        Assert.That(ourDate.IsSameDay(notSameDay), Is.False, "not same day");
        Assert.That(ourDate.IsSameDay(notSameMonth), Is.False, "not same month");
    }
}