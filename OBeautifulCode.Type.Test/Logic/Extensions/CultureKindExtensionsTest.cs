// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CultureKindExtensionsTest.cs" company="OBeautifulCode">
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

    public static class CultureKindExtensionsTest
    {
        [Fact]
        public static void ToCultureName___Should_throw_ArgumentOutOfRangeException___When_parameter_cultureKind_is_Unknown()
        {
            // Arrange, Act
            var actual = Record.Exception(() => CultureKind.Unknown.ToCultureName());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
            actual.Message.AsTest().Must().ContainString("cultureKind is CultureKind.Unknown");
        }

        [Fact]
        public static void ToCultureInfo___Should_throw_ArgumentOutOfRangeException___When_parameter_cultureKind_is_Unknown()
        {
            // Arrange, Act
            var actual = Record.Exception(() => CultureKind.Unknown.ToCultureInfo());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
            actual.Message.AsTest().Must().ContainString("cultureKind is CultureKind.Unknown");
        }

        [Fact]
        public static void ToCultureInfo___Should_return_object_whose_Name_is_equal_to_the_result_of_ToCultureName___When_called_using_same_cultureKind()
        {
            // Arrange
            var cultureKinds = EnumExtensions.GetDefinedEnumValues<CultureKind>().Except(new[] { CultureKind.Unknown, CultureKind.Invariant }).ToList();

            var expected = cultureKinds.Select(_ => _.ToCultureName()).ToList();

            // Act
            var actual = cultureKinds.Select(_ => _.ToCultureInfo().Name).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void ToCultureInfo___Should_return_CultureInfo_Invariant___When_cultureKind_is_Invariant()
        {
            // Arrange
            var cultureKinds = EnumExtensions.GetDefinedEnumValues<CultureKind>().Except(new[] { CultureKind.Unknown, CultureKind.Invariant }).ToList();

            var expected = cultureKinds.Select(_ => _.ToCultureName()).ToList();

            // Act
            var actual = cultureKinds.Select(_ => _.ToCultureInfo().Name).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }
    }
}
