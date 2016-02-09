using System;

namespace EventMaker3000.Converter
{
    public class DateTimeConverter
    {
        // the job here is to get some input from datepicker and timepicker. so we can click in the gui so the input there should go here so it can be converted
        // It makes a DateTime object
        public static DateTime DateTimeOffsetAndTimeSetToDateTime(DateTimeOffset date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0);
        }

    }
}