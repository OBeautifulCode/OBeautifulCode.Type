// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelfValidationFailureTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using OBeautifulCode.Assertion.Recipes;
    using Xunit;

    public static class SelfValidationFailureTest
    {
        [Fact]
        public static void PropertyNames___Should_return_list_with_propertyName_passed_to_constructor__When_called()
        {
            // Arrange
            var expected = (IReadOnlyList<string>)new[] { A.Dummy<string>() };

            var subjectUnderTest = new SelfValidationFailure(expected.Single(), A.Dummy<string>());

            // Act
            var actual = subjectUnderTest.PropertyNames;

            // Assert
            actual.AsTest().Must().BeUnorderedEqualTo(expected);
        }

        [Fact]
        public static void PropertyNames___Should_return_propertyNames_passed_to_constructor__When_called()
        {
            // Arrange
            var expected = A.Dummy<IReadOnlyList<string>>();

            var subjectUnderTest = new SelfValidationFailure(expected, A.Dummy<string>());

            // Act
            var actual = subjectUnderTest.PropertyNames;

            // Assert
            actual.AsTest().Must().BeUnorderedEqualTo(expected);
        }

        [Fact]
        public static void Message___Should_return_message_passed_to_constructor__When_called()
        {
            // Arrange
            var expected = A.Dummy<string>();

            var subjectUnderTest1 = new SelfValidationFailure(A.Dummy<string>(), expected);
            var subjectUnderTest2 = new SelfValidationFailure(A.Dummy<IReadOnlyList<string>>(), expected);

            // Act
            var actual1 = subjectUnderTest1.Message;
            var actual2 = subjectUnderTest2.Message;

            // Assert
            actual1.AsTest().Must().BeEqualTo(expected);
            actual2.AsTest().Must().BeEqualTo(expected);
        }
    }
}