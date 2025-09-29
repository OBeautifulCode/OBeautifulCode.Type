// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationExceptionTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using OBeautifulCode.Assertion.Recipes;
    using Xunit;

    public static class ValidationExceptionTest
    {
        [Fact]
        public static void Constructor___Should_not_throw___When_parameter_failures_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() =>
            {
                new ValidationException(null);
                new ValidationException(A.Dummy<string>(), null);
                new ValidationException(A.Dummy<string>(), new ArgumentException(A.Dummy<string>()), null);
            });

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void Failures___Should_return_failures_passed_to_constructor___When_called()
        {
            // Arrange
            var expected = A.Dummy<IReadOnlyList<ValidationFailure>>();

            var subjectsUnderTest = new[]
            {
                new ValidationException(expected),
                new ValidationException(A.Dummy<string>(), expected),
                new ValidationException(A.Dummy<string>(), new ArgumentException(A.Dummy<string>()), expected),
            };

            // Act
            var actual = subjectsUnderTest.Select(_ => _.Failures).ToList();

            // Assert
            actual.AsTest().Must().Each().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void Message___Should_return_message_passed_to_constructor___When_called()
        {
            // Arrange
            var expected = A.Dummy<string>();

            var subjectsUnderTest = new ValidationException(expected, A.Dummy<IReadOnlyList<ValidationFailure>>());

            // Act
            var actual = subjectsUnderTest.Message;

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void InnerException___Should_return_message_passed_to_constructor___When_called()
        {
            // Arrange
            var expected = new ArgumentException(A.Dummy<string>());

            var subjectsUnderTest = new ValidationException(A.Dummy<string>(), expected, A.Dummy<IReadOnlyList<ValidationFailure>>());

            // Act
            var actual = subjectsUnderTest.InnerException;

            // Assert
            actual.AsTest().Must().BeSameReferenceAs(expected);
        }

        [Fact]
        public static void ToString___Should_return_friendly_string_representation___When_called()
        {
            // Arrange
            var subjectUnderTest1 = new ValidationException(null);

            var subjectUnderTest2 = new ValidationException(
                new[]
                {
                    new ValidationFailure("Type1", "Path1", "Message1"),
                    null,
                    new ValidationFailure("Type2", "Path2", "Message2"),
                });

            // Act
            var actual1 = subjectUnderTest1.ToString();
            var actual2 = subjectUnderTest2.ToString();

            // Assert
            actual1.AsTest().Must().BeEqualTo("OBeautifulCode.Type.ValidationException: Exception of type 'OBeautifulCode.Type.ValidationException' was thrown.");
            actual2.AsTest().Must().BeEqualTo("Validation failures: \r\n- Type1 : Path1 => Message1\r\n- Type2 : Path2 => Message2\r\nOBeautifulCode.Type.ValidationException: Exception of type 'OBeautifulCode.Type.ValidationException' was thrown.");
        }
    }
}
