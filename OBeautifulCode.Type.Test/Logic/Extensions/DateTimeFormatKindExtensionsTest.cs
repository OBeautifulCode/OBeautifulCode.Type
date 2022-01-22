// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeFormatKindExtensionsTest.cs" company="OBeautifulCode">
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

    public static class DateTimeFormatKindExtensionsTest
    {
        [Fact]
        public static void ToFormatString___Should_throw_ArgumentOutOfRangeException___When_parameter_formatKind_is_Unknown()
        {
            // Arrange, Act
            var actual = Record.Exception(() => DateTimeFormatKind.Unknown.ToFormatString());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
            actual.Message.AsTest().Must().ContainString("formatKind is DateTimeFormatKind.Unknown");
        }

        [Fact]
        public static void ToFormatString___Should_return_correct_format_string___When_called()
        {
            // Arrange
            var formatKinds = EnumExtensions.GetDefinedEnumValues<DateTimeFormatKind>()
                .Except(new[] { DateTimeFormatKind.Unknown })
                .ToList();

            // Act
            formatKinds.ToDictionary(_ => _, _ => _.ToFormatString());

            // Assert
            // HOW CAN WE ASSERT THIS?
        }
    }
}
