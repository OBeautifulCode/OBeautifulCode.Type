// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeFormatKindExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections.Generic;
    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="DateTimeFormatKind"/>.
    /// </summary>
    public static class DateTimeFormatKindExtensions
    {
        private static readonly Dictionary<DateTimeFormatKind, string> DateTimeFormatKindToFormatStringMap =
            new Dictionary<DateTimeFormatKind, string>
            {
                { DateTimeFormatKind.ShortDatePattern, "d" },
                { DateTimeFormatKind.LongDatePattern, "D" },
                { DateTimeFormatKind.FullDateTimePatternShortTime, "f" },
                { DateTimeFormatKind.FullDateTimePatternLongTime, "F" },
                { DateTimeFormatKind.GeneralDateTimePatternShortTime, "g" },
                { DateTimeFormatKind.GeneralDateTimePatternLongTime, "G" },
                { DateTimeFormatKind.MonthDayPattern, "M" },
                { DateTimeFormatKind.RoundtripDateTimePattern, "O" },
                { DateTimeFormatKind.Rfc1123Pattern, "R" },
                { DateTimeFormatKind.SortableDateTimePattern, "s" },
                { DateTimeFormatKind.ShortTimePattern, "t" },
                { DateTimeFormatKind.LongTimePattern, "T" },
                { DateTimeFormatKind.UniversalSortableDateTimePattern, "u" },
                { DateTimeFormatKind.UniversalFullDateTimePattern, "U" },
                { DateTimeFormatKind.YearMonthPattern, "Y" },
            };

        /// <summary>
        /// Gets the format string associated with a specified <see cref="DateTimeFormatKind"/>.
        /// </summary>
        /// <param name="formatKind">The format kind.</param>
        /// <returns>
        /// The format string associated with a specified <see cref="DateTimeFormatKind"/>.
        /// </returns>
        public static string ToFormatString(
            this DateTimeFormatKind formatKind)
        {
            if (formatKind == DateTimeFormatKind.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(formatKind)} is {nameof(DateTimeFormatKind)}.{nameof(DateTimeFormatKind.Unknown)}"));
            }

            if (!DateTimeFormatKindToFormatStringMap.ContainsKey(formatKind))
            {
                throw new NotSupportedException(Invariant($"This {nameof(DateTimeFormatKind)} is not supported: {formatKind}."));
            }

            var result = DateTimeFormatKindToFormatStringMap[formatKind];

            return result;
        }
    }
}
