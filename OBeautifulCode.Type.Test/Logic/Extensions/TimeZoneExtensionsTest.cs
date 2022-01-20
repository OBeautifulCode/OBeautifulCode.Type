// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeZoneExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;
    using System.Linq;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Enum.Recipes;
    using Xunit;

    public static class TimeZoneExtensionsTest
    {
        [Fact]
        public static void ToTimeZoneInfo___Should_throw_ArgumentOutOfRangeException___When_parameter_timeZone_is_Unknown()
        {
            // Arrange, Act
            var actual = Record.Exception(() => StandardTimeZone.Unknown.ToTimeZoneInfo());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
            actual.Message.AsTest().Must().ContainString("timeZone is StandardTimeZone.Unknown");
        }

        [Fact]
        public static void ToTimeZoneInfo___Should_return_correct_TimeZoneInfo___When_called()
        {
            // Arrange
            var timeZones = EnumExtensions.GetDefinedEnumValues<StandardTimeZone>()
                .Except(new[] { StandardTimeZone.Unknown })
                .ToList();

            // Act
            timeZones.ToDictionary(_ => _, _ => _.ToTimeZoneInfo());

            // Assert
            // HOW CAN WE ASSERT THIS?
        }
    }
}
