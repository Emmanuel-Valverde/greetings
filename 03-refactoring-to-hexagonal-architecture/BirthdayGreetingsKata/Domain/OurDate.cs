using System;

namespace BirthdayGreetingsKata.Domain;

public class OurDate
{
    private readonly DateTime _date;

    public OurDate(string yyyyMMdd)
    {
        _date = DateTime.ParseExact(yyyyMMdd, "yyyy/MM/dd", null);
    }

    private int GetDay()
    {
        return _date.Day;
    }

    private int GetMonth()
    {
        return _date.Month;
    }

    public bool IsSameDay(OurDate anotherDate)
    {
        return anotherDate.GetDay() == GetDay()
               && anotherDate.GetMonth() == GetMonth();
    }

    private bool Equals(OurDate other)
    {
        return _date.Equals(other._date);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((OurDate)obj);
    }

    public override int GetHashCode()
    {
        return _date.GetHashCode();
    }
}