using System;
using System.Collections.Generic;

namespace PCLActivitySet.Recurrence
{
    [Flags]
    public enum EDaysOfWeekFlags
    {
        None = 0,
        Sunday = 0x1,
        Monday = 0x2,
        Tuesday = 0x4,
        Wednesday = 0x8,
        Thursday = 0x10,
        Friday = 0x20,
        Saturday = 0x40,
    }

    public static class DaysOfWeekFlags
    {
        public static bool DateMatches(DateTime date, EDaysOfWeekFlags dowFlags)
        {
            return (ConvertFrom(date) & dowFlags) != 0;
        }

        public static HashSet<EDaysOfWeekFlags> AsSeperatedValues(EDaysOfWeekFlags daysOfWeek)
        {
            HashSet<EDaysOfWeekFlags> retVal = new HashSet<EDaysOfWeekFlags>();

            foreach (EDaysOfWeekFlags d in Enum.GetValues(typeof(EDaysOfWeekFlags)))
            {
                // Note: This naturally excludes the "None" value by design.
                if ((daysOfWeek & d) != 0)
                    retVal.Add(d);
            }

            return retVal;
        }

        public static bool HasWeekDays(EDaysOfWeekFlags daysOfWeek)
        {
            return HasWeekDays(daysOfWeek, false);
        }

        public static bool HasWeekDays(EDaysOfWeekFlags daysOfWeek, bool allowNone)
        {
            if (allowNone && daysOfWeek == EDaysOfWeekFlags.None)
                return true;
            return (WeekDays & daysOfWeek) != 0;
        }

        public static bool HasWeekendDays(EDaysOfWeekFlags daysOfWeek)
        {
            return HasWeekendDays(daysOfWeek, false);
        }

        public static bool HasWeekendDays(EDaysOfWeekFlags daysOfWeek, bool allowNone)
        {
            if (allowNone && daysOfWeek == EDaysOfWeekFlags.None)
                return true;
            return (WeekendDays & daysOfWeek) != 0;
        }

        public static bool HasOnlyWeekDays(EDaysOfWeekFlags daysOfWeek)
        {
            return HasOnlyWeekDays(daysOfWeek, false);
        }

        public static bool HasOnlyWeekDays(EDaysOfWeekFlags daysOfWeek, bool allowNone)
        {
            if (allowNone && daysOfWeek == EDaysOfWeekFlags.None)
                return true;
            return (WeekDays & daysOfWeek) != 0 && (WeekendDays & daysOfWeek) == 0;
        }

        public static bool HasOnlyWeekendDays(EDaysOfWeekFlags daysOfWeek)
        {
            return HasOnlyWeekendDays(daysOfWeek, false);
        }

        public static bool HasOnlyWeekendDays(EDaysOfWeekFlags daysOfWeek, bool allowNone)
        {
            if (allowNone && daysOfWeek == EDaysOfWeekFlags.None)
                return true;
            return (WeekendDays & daysOfWeek) != 0 && (WeekDays & daysOfWeek) == 0;
        }

        public static EDaysOfWeekFlags ConvertFrom(EDaysOfWeek dayOfWeek)
        {
            if (Enum.IsDefined(typeof(EDaysOfWeek), dayOfWeek))
                return (EDaysOfWeekFlags)Enum.Parse(typeof(EDaysOfWeekFlags), Enum.GetName(typeof(EDaysOfWeek), dayOfWeek));
            else
                throw new ArgumentException($"Undefined {typeof(EDaysOfWeek)} value ({(int) dayOfWeek}).");
        }

        public static EDaysOfWeekFlags ConvertFrom(IEnumerable<EDaysOfWeek> daysOfWeek)
        {
            EDaysOfWeekFlags retVal = EDaysOfWeekFlags.None;
            foreach (EDaysOfWeek day in daysOfWeek)
                retVal |= ConvertFrom(day);

            return retVal;
        }

        public static EDaysOfWeekFlags ConvertFrom(EDaysOfWeekExt dayOfWeek)
        {
            if (DaysOfWeekExt.IsDayGroupClassifier(dayOfWeek))
                throw new ArgumentException($"Day Group Classifiers of the type {typeof(EDaysOfWeekExt)} cannnot be converted to the type {typeof(EDaysOfWeekFlags)}.");

            if (Enum.IsDefined(typeof(EDaysOfWeekExt), dayOfWeek))
                return (EDaysOfWeekFlags)Enum.Parse(typeof(EDaysOfWeekFlags), Enum.GetName(typeof(EDaysOfWeekExt), dayOfWeek));
            else
                throw new ArgumentException($"Undefined {typeof(EDaysOfWeekExt)} value ({(int) dayOfWeek}).");}

        public static EDaysOfWeekFlags ConvertFrom(IEnumerable<EDaysOfWeekExt> daysOfWeek)
        {
            EDaysOfWeekFlags retVal = EDaysOfWeekFlags.None;
            foreach (EDaysOfWeekExt day in daysOfWeek)
                retVal |= ConvertFrom(day);

            return retVal;
        }

        public static EDaysOfWeekFlags ConvertFrom(DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return EDaysOfWeekFlags.Sunday;
                case DayOfWeek.Monday:
                    return EDaysOfWeekFlags.Monday;
                case DayOfWeek.Tuesday:
                    return EDaysOfWeekFlags.Tuesday;
                case DayOfWeek.Wednesday:
                    return EDaysOfWeekFlags.Wednesday;
                case DayOfWeek.Thursday:
                    return EDaysOfWeekFlags.Thursday;
                case DayOfWeek.Friday:
                    return EDaysOfWeekFlags.Friday;
                case DayOfWeek.Saturday:
                    return EDaysOfWeekFlags.Saturday;
                default:
                    throw new InvalidOperationException("Invalid Day Of Week");
            }
        }

        public const EDaysOfWeekFlags WeekDays = EDaysOfWeekFlags.Monday | EDaysOfWeekFlags.Tuesday | EDaysOfWeekFlags.Wednesday | EDaysOfWeekFlags.Thursday | EDaysOfWeekFlags.Friday;
        public const EDaysOfWeekFlags WeekendDays = EDaysOfWeekFlags.Saturday | EDaysOfWeekFlags.Sunday;
        public const EDaysOfWeekFlags EveryDay = DaysOfWeekFlags.WeekDays | DaysOfWeekFlags.WeekendDays;
    }

}
