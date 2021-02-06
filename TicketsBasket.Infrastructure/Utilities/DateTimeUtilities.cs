using System;

namespace TicketsBasket.Infrastructure.Utilities
{
  public  class DateTimeUtilities
  {
    public static string GetPassedTime(DateTime currentDate, DateTime originalDate)
    {
      var timeSpan = currentDate.Subtract(originalDate);
      var difference = (int)timeSpan.TotalSeconds;

      var second = 1;
      var minute = 60 * second;
      var hour = minute * 60;
      var day = hour * 24;
      var month = day * 30;
      var year = month * 12;

      if (difference < minute)
        return difference == 1 ? "one second ago" : $"{difference} seconds ago";

      if (difference < hour)
        return timeSpan.Minutes == minute ? "a minute ago" : $"{timeSpan.Minutes} minutes ago";

      if (difference < day)
        return timeSpan.Hours == hour ? "one hour ago" : $"{timeSpan.Hours} hours ago";

      if (difference < month)
        return timeSpan.Days == day ? "one day ago" : $"{timeSpan.Days} days ago";

      if (difference < year)
      {
        var months = ((int)(timeSpan.Days / 30));
        return months == month ? "one month ago" : $"{months} months ago";
      }

      var years = (int)(timeSpan.TotalDays / 365);
      return years == 1 ? "one year ago" : $"{years} ago";


    }

  }
}