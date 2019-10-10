// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeRangeInclusiveTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;
    using System.Linq;
    using System.Threading;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AutoFakeItEasy;

    using Xunit;

    public static class DateTimeRangeInclusiveTest
    {
        private static readonly DateTimeRangeInclusive ObjectForEquatableTests = A.Dummy<DateTimeRangeInclusive>();

        private static readonly DateTimeRangeInclusive ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests =
            new DateTimeRangeInclusive(ObjectForEquatableTests.StartDateTimeInUtc, ObjectForEquatableTests.EndDateTimeInUtc);

        private static readonly DateTimeRangeInclusive[] ObjectsThatAreNotEqualToObjectForEquatableTests =
        {
            A.Dummy<DateTimeRangeInclusive>(),
            new DateTimeRangeInclusive(A.Dummy<DateTime>().ThatIs(_ => _.ToUniversalTime() <= ObjectForEquatableTests.EndDateTimeInUtc).ToUniversalTime(), ObjectForEquatableTests.EndDateTimeInUtc),
            new DateTimeRangeInclusive(ObjectForEquatableTests.StartDateTimeInUtc, A.Dummy<DateTime>().ThatIs(_ => _.ToUniversalTime() >= ObjectForEquatableTests.StartDateTimeInUtc).ToUniversalTime()),
        };

        private static readonly string ObjectThatIsNotTheSameTypeAsObjectForEquatableTests = A.Dummy<string>();

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_startDateTimeInUtc_is_not_DateTimeKind_Utc()
        {
            // Arrange
            var startDateTime1 = new DateTime(1, DateTimeKind.Local);
            var startDateTime2 = new DateTime(1, DateTimeKind.Unspecified);

            // Act
            var ex1 = Record.Exception(() => new DateTimeRangeInclusive(startDateTime1, DateTime.UtcNow));
            var ex2 = Record.Exception(() => new DateTimeRangeInclusive(startDateTime2, DateTime.UtcNow));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("startDateTimeInUtc DateTimeKind is not Utc");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("startDateTimeInUtc DateTimeKind is not Utc");
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_endDateTimeInUtc_is_not_DateTimeKind_Utc()
        {
            // Arrange
            var endDateTime1 = new DateTime(1, DateTimeKind.Local);
            var endDateTime2 = new DateTime(1, DateTimeKind.Unspecified);

            // Act
            var ex1 = Record.Exception(() => new DateTimeRangeInclusive(DateTime.UtcNow, endDateTime1));
            var ex2 = Record.Exception(() => new DateTimeRangeInclusive(DateTime.UtcNow, endDateTime2));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("endDateTimeInUtc DateTimeKind is not Utc");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("endDateTimeInUtc DateTimeKind is not Utc");
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_startDateTimeInUtc_is_greater_than_endDateTimeInUtc()
        {
            // Arrange
            var startDateTimeInUtc = DateTime.UtcNow;
            var endDateTimeInUtc = startDateTimeInUtc.AddMilliseconds(-1);

            // Act
            var ex = Record.Exception(() => new DateTimeRangeInclusive(startDateTimeInUtc, endDateTimeInUtc));

            // Assert
            ex.Should().BeOfType<ArgumentOutOfRangeException>();
            ex.Message.Should().Contain("startDateTimeInUtc is > endDateTimeInUtc");
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_parameter_startDateTimeInUtc_is_equal_to_endDateTimeInUtc()
        {
            // Arrange
            var startDateTimeInUtc = DateTime.UtcNow;
            var endDateTimeInUtc = startDateTimeInUtc;

            // Act
            var ex = Record.Exception(() => new DateTimeRangeInclusive(startDateTimeInUtc, endDateTimeInUtc));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void StartDateTimeInUtc___Should_return_same_startDateTimeInUtc_passed_to_constructor___When_getting()
        {
            // Arrange
            var expected = DateTime.UtcNow;
            Thread.Sleep(100);
            var endDateTimeInUtc = DateTime.UtcNow;
            var systemUnderTest = new DateTimeRangeInclusive(expected, endDateTimeInUtc);

            // Act
            var actual = systemUnderTest.StartDateTimeInUtc;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void EndDateTimeInUtc___Should_return_same_endDateTimeInUtc_passed_to_constructor___When_getting()
        {
            // Arrange
            var startDateTimeInUtc = DateTime.UtcNow;
            Thread.Sleep(100);
            var expected = DateTime.UtcNow;
            var systemUnderTest = new DateTimeRangeInclusive(startDateTimeInUtc, expected);

            // Act
            var actual = systemUnderTest.EndDateTimeInUtc;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            DateTimeRangeInclusive systemUnderTest1 = null;
            DateTimeRangeInclusive systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 == systemUnderTest2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            DateTimeRangeInclusive systemUnderTest = null;

            // Act
            var result1 = systemUnderTest == ObjectForEquatableTests;
            var result2 = ObjectForEquatableTests == systemUnderTest;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange, Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result = ObjectForEquatableTests == ObjectForEquatableTests;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange, Act
            var results = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests == _).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests == ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            DateTimeRangeInclusive systemUnderTest1 = null;
            DateTimeRangeInclusive systemUnderTest2 = null;

            // Act
            var result = systemUnderTest1 != systemUnderTest2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            DateTimeRangeInclusive systemUnderTest = null;

            // Act
            var result1 = systemUnderTest != ObjectForEquatableTests;
            var result2 = ObjectForEquatableTests != systemUnderTest;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange, Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result = ObjectForEquatableTests != ObjectForEquatableTests;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_objects_being_compared_have_different_property_values()
        {
            // Arrange, Act
            var results = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests != _).ToList();

            // sAssert
            results.ForEach(_ => _.Should().BeTrue());
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_objects_being_compared_have_same_property_values()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests != ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_DateTimeRangeInclusive___Should_return_false___When_parameter_other_is_null()
        {
            // Arrange
            DateTimeRangeInclusive systemUnderTest = null;

            // Act
            var result = ObjectForEquatableTests.Equals(systemUnderTest);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_DateTimeRangeInclusive___Should_return_true___When_parameter_other_is_same_object()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals(ObjectForEquatableTests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_DateTimeRangeInclusive___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange, Act
            var results = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests.Equals(_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Equals_with_DateTimeRangeInclusive___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals(ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_Object___Should_return_false___When_parameter_other_is_null()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_Object___Should_return_false___When_parameter_other_is_not_of_the_same_type()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals((object)ObjectThatIsNotTheSameTypeAsObjectForEquatableTests);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void Equals_with_Object___Should_return_true___When_parameter_other_is_same_object()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals((object)ObjectForEquatableTests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals_with_Object___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange, Act
            var results = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests.Equals((object)_)).ToList();

            // Assert
            results.ForEach(_ => _.Should().BeFalse());
        }

        [Fact]
        public static void Equals_with_Object___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange, Act
            var result = ObjectForEquatableTests.Equals((object)ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_objects___When_objects_have_different_property_values()
        {
            // Arrange, Act
            var hashCode1 = ObjectForEquatableTests.GetHashCode();
            var hashCode2 = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => _.GetHashCode()).ToList();

            // Assert
            hashCode2.ForEach(_ => _.Should().NotBe(hashCode1));
        }

        [Fact]
        public static void GetHashCode___Should_be_equal_for_two_objects___When_objects_have_the_same_property_values()
        {
            // Arrange, Act
            var hash1 = ObjectForEquatableTests.GetHashCode();
            var hash2 = ObjectThatIsEqualButNotTheSameAsObjectForEquatableTests.GetHashCode();

            // Assert
            hash1.Should().Be(hash2);
        }

        [Fact]
        public static void DeepClone___Should_clone_item___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<DateTimeRangeInclusive>();

            // Act
            var actual = systemUnderTest.DeepClone();

            // Assert
            actual.Should().Be(systemUnderTest);
            actual.Should().NotBeSameAs(systemUnderTest);
        }

        [Fact]
        public static void ToString___Should_return_string_representation_of_date_time_range___When_called()
        {
            // Arrange
            var startDateTime = new DateTime(2019, 3, 10, 14, 4, 59, DateTimeKind.Utc);
            var endDateTime = new DateTime(2020, 11, 22, 9, 43, 4, DateTimeKind.Utc);

            var systemUnderTest = new DateTimeRangeInclusive(startDateTime, endDateTime);

            // Act
            var actual = systemUnderTest.ToString();

            // Assert
            actual.Should().Be("03/10/2019 14:04:59 to 11/22/2020 09:43:04");
        }
    }
}
