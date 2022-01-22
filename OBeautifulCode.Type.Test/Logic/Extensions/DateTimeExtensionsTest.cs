// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;
    using System.Linq;
    using FakeItEasy;
    using OBeautifulCode.Assertion.Recipes;
    using Xunit;

    public static class DateTimeExtensionsTest
    {
        [Fact]
        public static void ToString___Should_throw_ArgumentOutOfRangeException___When_parameter_formatKind_is_Unknown()
        {
            // Arrange
            var value = A.Dummy<DateTime>();

            // Act
            var actual = Record.Exception(() => value.ToString(DateTimeFormatKind.Unknown, A.Dummy<CultureKind>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
            actual.Message.AsTest().Must().ContainString("formatKind is DateTimeFormatKind.Unknown");
        }

        [Fact]
        public static void ToString___Should_throw_ArgumentOutOfRangeException___When_parameter_cultureKind_is_Unknown()
        {
            // Arrange
            var value = A.Dummy<DateTime>();

            // Act
            var actual = Record.Exception(() => value.ToString(A.Dummy<DateTimeFormatKind>(), CultureKind.Unknown));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
            actual.Message.AsTest().Must().ContainString("cultureKind is CultureKind.Unknown");
        }

        [Fact]
        public static void ToString___Should_return_formatted_DateTime___When_called()
        {
            // Arrange
            var value = new DateTime(2008, 10, 31, 17, 4, 32);

            var formatKindAndExpected = new[]
            {
                new { FormatKind = DateTimeFormatKind.ShortDatePattern, Expected = "10/31/2008" },
                new { FormatKind = DateTimeFormatKind.LongDatePattern, Expected = "Friday, October 31, 2008" },
                new { FormatKind = DateTimeFormatKind.FullDateTimePatternShortTime, Expected = "Friday, October 31, 2008 5:04 PM" },
                new { FormatKind = DateTimeFormatKind.FullDateTimePatternLongTime, Expected = "Friday, October 31, 2008 5:04:32 PM" },
                new { FormatKind = DateTimeFormatKind.GeneralDateTimePatternShortTime, Expected = "10/31/2008 5:04 PM" },
                new { FormatKind = DateTimeFormatKind.GeneralDateTimePatternLongTime, Expected = "10/31/2008 5:04:32 PM" },
                new { FormatKind = DateTimeFormatKind.MonthDayPattern, Expected = "October 31" },
                new { FormatKind = DateTimeFormatKind.RoundtripDateTimePattern, Expected = "2008-10-31T17:04:32.0000000" },
                new { FormatKind = DateTimeFormatKind.Rfc1123Pattern, Expected = "Fri, 31 Oct 2008 17:04:32 GMT" },
                new { FormatKind = DateTimeFormatKind.SortableDateTimePattern, Expected = "2008-10-31T17:04:32" },
                new { FormatKind = DateTimeFormatKind.ShortTimePattern, Expected = "5:04 PM" },
                new { FormatKind = DateTimeFormatKind.LongTimePattern, Expected = "5:04:32 PM" },
                new { FormatKind = DateTimeFormatKind.UniversalSortableDateTimePattern, Expected = "2008-10-31 17:04:32Z" },

                // This one depends on the timezone where the test is run.
                // In AppVeyor its 5:04:32, in ET on 1/22/2020 it's 9:04:32
                // new { FormatKind = DateTimeFormatKind.UniversalFullDateTimePattern, Expected = "Friday, October 31, 2008 5:04:32 PM" },
                new { FormatKind = DateTimeFormatKind.YearMonthPattern, Expected = "October 2008" },
            };

            var expected = formatKindAndExpected.Select(_ => _.Expected).ToList();

            // Act
            var actual = formatKindAndExpected.Select(_ => value.ToString(_.FormatKind, CultureKind.EnglishUnitedStates)).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }
    }
}
