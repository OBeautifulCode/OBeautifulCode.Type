// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatableExtensionsTest.cs" company="OBeautifulCode">
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
    using static System.FormattableString;

    public static class ValidatableExtensionsTest
    {
        private static readonly ValidationOptions TestValidationOptions = A.Dummy<ValidationOptions>();

        [Fact]
        public static void IsValid___Should_throw_ArgumentNullException___When_parameter_validatable_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ValidatableExtensions.IsValid(null, out var failures));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("validatable");
        }

        [Fact]
        public static void IsValid___Should_return_false_with_empty_failures___When_object_is_valid()
        {
            // Arrange
            var subjectUnderTest = new ValidatableTestObject
            {
                IntProperty = 5,
            };

            // Act
            var actual = subjectUnderTest.IsValid(out var actualFailures, TestValidationOptions);

            // Assert
            actual.AsTest().Must().BeTrue();
            actualFailures.AsTest().Must().BeEmptyEnumerable();
        }

        [Fact]
        public static void IsValid___Should_return_true_with_failures_populated___When_object_is_invalid()
        {
            // Arrange
            var subjectUnderTest = new ValidatableTestObject
            {
                IntProperty = -5,
            };

            var expectedFailure = new ValidationFailure(nameof(ValidatableTestObject), nameof(ValidatableTestObject.IntProperty), Invariant($"{nameof(ValidatableTestObject.IntProperty)} < 0"));

            // Act
            var actual = subjectUnderTest.IsValid(out var actualFailures, TestValidationOptions);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualFailures.AsTest().Must().HaveCount(1).And().Each().BeEqualTo(expectedFailure);
        }

        [Fact]
        public static void ThrowIfInvalid___Should_throw_ArgumentNullException___When_parameter_validatable_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ValidatableExtensions.ThrowIfInvalid(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("validatable");
        }

        [Fact]
        public static void ThrowIfInvalid___Should_not_throw___When_object_is_valid()
        {
            // Arrange
            var subjectUnderTest = new ValidatableTestObject
            {
                IntProperty = 5,
            };

            // Act
            var actual = Record.Exception(() => subjectUnderTest.ThrowIfInvalid(TestValidationOptions));

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void IsValid___Should_throw_ValidationException___When_object_is_invalid()
        {
            // Arrange
            var subjectUnderTest = new ValidatableTestObject
            {
                IntProperty = -5,
            };

            var expectedFailure = new ValidationFailure(nameof(ValidatableTestObject), nameof(ValidatableTestObject.IntProperty), Invariant($"{nameof(ValidatableTestObject.IntProperty)} < 0"));

            // Act
            var actual = Record.Exception(() => subjectUnderTest.ThrowIfInvalid(TestValidationOptions));

            // Assert
            actual.AsTest().Must().BeOfType<ValidationException>();
            ((ValidationException)actual).Failures.AsTest().Must().HaveCount(1).And().Each().BeEqualTo(expectedFailure);
        }

        private class ValidatableTestObject : IValidatable
        {
            public int IntProperty { get; set; }

            public IReadOnlyList<SelfValidationFailure> GetSelfValidationFailures()
            {
                var result = new List<SelfValidationFailure>();

                if (this.IntProperty < 0)
                {
                    result.Add(new SelfValidationFailure(nameof(this.IntProperty), Invariant($"{nameof(this.IntProperty)} < 0")));
                }

                return result;
            }

            public IReadOnlyList<ValidationFailure> GetValidationFailures(
                ValidationOptions options = null,
                PropertyPathTracker propertyPathTracker = null)
            {
                options.AsOp().Must().BeEqualTo(TestValidationOptions);

                var selfValidationFailures = this.GetSelfValidationFailures() ?? new List<SelfValidationFailure>();

                var result = selfValidationFailures.Select(_ =>
                    new ValidationFailure(
                        nameof(ValidatableTestObject),
                        string.Join("|", _.PropertyNames),
                        _.Message))
                    .ToList();

                return result;
            }
        }
    }
}
