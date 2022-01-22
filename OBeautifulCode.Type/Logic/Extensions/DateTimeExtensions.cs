// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts the value of the current <see cref="DateTime"/> object to its equivalent
        /// string representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatKind">The kind of formatting to apply.</param>
        /// <param name="cultureKind">OPTIONAL kind of culture to use.  DEFAULT is to use the invariant culture.</param>
        /// <returns>
        /// A string representation of the specified <see cref="DateTime"/> value as specified by format and culture.
        /// </returns>
        public static string ToString(
            this DateTime value,
            DateTimeFormatKind formatKind,
            CultureKind cultureKind = CultureKind.Invariant)
        {
            if (formatKind == DateTimeFormatKind.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(formatKind)} is {nameof(DateTimeFormatKind)}.{nameof(DateTimeFormatKind.Unknown)}"));
            }

            if (cultureKind == CultureKind.Unknown)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(cultureKind)} is {nameof(CultureKind)}.{nameof(CultureKind.Unknown)}"));
            }

            var formatString = formatKind.ToFormatString();

            var cultureInfo = cultureKind.ToCultureInfo();

            var result = value.ToString(formatString, cultureInfo);

            return result;
        }
    }
}
