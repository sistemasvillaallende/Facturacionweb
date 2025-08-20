// Decompiled with JetBrains decompiler
// Type: BLL.Date
// Assembly: BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34EC4AF4-6E2A-4DA8-8439-6E4212AC4B99
// Assembly location: H:\NET 2018\Facturacion\bin\BLL.dll

using System;

namespace BLL
{
  public class Date
  {
    private static int[] daysInMonth = new int[12]
    {
      31,
      28,
      31,
      30,
      31,
      30,
      31,
      31,
      30,
      31,
      30,
      31
    };
    private static int[] daysIntoYear = new int[12]
    {
      0,
      31,
      59,
      90,
      120,
      151,
      181,
      212,
      243,
      273,
      304,
      334
    };
    private static int[] daysIntoLeapYear = new int[12]
    {
      0,
      31,
      60,
      91,
      121,
      152,
      182,
      213,
      244,
      274,
      305,
      335
    };
    private int year;
    private int month;
    private int day;

    public int Year
    {
      get
      {
        return this.year;
      }
      private set
      {
        if (value > 3000)
        {
          Console.WriteLine("Year too late, using '3000' instead");
          this.year = 3000;
        }
        else if (value < 1900)
        {
          Console.WriteLine("Year too early, using '1900' instead");
          this.year = 1900;
        }
        else
          this.year = value;
      }
    }

    public int Month
    {
      get
      {
        return this.month;
      }
      private set
      {
        if (value > 12)
        {
          Console.WriteLine("Month too big, using 12 instead");
          this.month = 12;
        }
        else if (value < 1)
        {
          Console.WriteLine("Month too small, using '1' instead");
          this.month = 1;
        }
        else
          this.month = value;
      }
    }

    public int Day
    {
      get
      {
        return this.day;
      }
      private set
      {
        int num1 = 0;
        if (this.month == 2 && Date.IsLeapYear(this.year))
          num1 = 1;
        int num2 = Date.daysInMonth[this.month - 1] + num1;
        if (value > num2)
        {
          Console.WriteLine("Day too big, using '{0}' instead", (object) num2);
          this.day = num2;
        }
        else if (value < 1)
        {
          Console.WriteLine("Day too small, using '1' instead");
          this.day = 1;
        }
        else
          this.day = value;
      }
    }

    public int DaysIntoYear
    {
      get
      {
        if (Date.IsLeapYear(this.year))
          return Date.daysIntoLeapYear[this.month - 1] + this.day;
        return Date.daysIntoYear[this.month - 1] + this.day;
      }
    }

    public Date(int year, int month, int day)
    {
      this.Year = year;
      this.Month = month;
      this.Day = day;
    }

    public static bool IsLeapYear(int year)
    {
      if (year % 4 != 0)
        return false;
      if (year % 100 == 0)
        return year % 400 == 0;
      return true;
    }

    private static int GetLeapYearsSince1900(int year)
    {
      int num1 = (year - 1900) / 4;
      int num2 = (year - 1900) / 100;
      int num3 = (year - 1600) / 400;
      int num4 = 0;
      if (Date.IsLeapYear(year))
        num4 = 1;
      int num5 = num2;
      return num1 - num5 + num3 - num4;
    }

    private static int GetDaysSince1900(Date dt)
    {
      return (dt.Year - 1900) * 365 + Date.GetLeapYearsSince1900(dt.Year) + dt.DaysIntoYear;
    }

    public static int GetDaysBetween(Date dt1, Date dt2)
    {
      return Math.Abs(Date.GetDaysSince1900(dt1) - Date.GetDaysSince1900(dt2));
    }

    public static int GetDaysBetween(string date1, string date2)
    {
      DateTime result1;
      DateTime.TryParse(date1, out result1);
      Date dt1 = new Date(result1.Year, result1.Month, result1.Day);
      DateTime result2;
      DateTime.TryParse(date2, out result2);
      Date dt2 = new Date(result2.Year, result2.Month, result2.Day);
      return Math.Abs(Date.GetDaysSince1900(dt1) - Date.GetDaysSince1900(dt2));
    }

    public static string GetDateTicks()
    {
      long ticks = DateTime.Now.Ticks - new DateTime(2001, 1, 1).Ticks;
      TimeSpan timeSpan = new TimeSpan(ticks);
      return string.Format("{0:N0}", (object) ticks);
    }
  }
}
