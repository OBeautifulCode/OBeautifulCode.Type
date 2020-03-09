// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UtcDateTimeRangeInclusiveTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Equality.Recipes;

    using Xunit;

    public static partial class UtcDateTimeRangeInclusiveTest
    {
        static UtcDateTimeRangeInclusiveTest()
        {
            EquatableTestScenarios.RemoveAllScenarios();

            EquatableTestScenarios.AddScenario(
                new EquatableTestScenario<UtcDateTimeRangeInclusive>
                {
                    ReferenceObject = ReferenceObjectForEquatableTestScenarios,
                    ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new UtcDateTimeRangeInclusive[]
                    {
                        new UtcDateTimeRangeInclusive(
                            ReferenceObjectForEquatableTestScenarios.StartDateTimeInUtc,
                            ReferenceObjectForEquatableTestScenarios.EndDateTimeInUtc),
                    },
                    ObjectsThatAreNotEqualToReferenceObject = new UtcDateTimeRangeInclusive[]
                    {
                        new UtcDateTimeRangeInclusive(
                            A.Dummy<UtcDateTimeRangeInclusive>().Whose(_ => (!_.StartDateTimeInUtc.IsEqualTo(ReferenceObjectForEquatableTestScenarios.StartDateTimeInUtc)) && (_.StartDateTimeInUtc <= ReferenceObjectForEquatableTestScenarios.EndDateTimeInUtc)).StartDateTimeInUtc,
                            ReferenceObjectForEquatableTestScenarios.EndDateTimeInUtc),
                        new UtcDateTimeRangeInclusive(
                            ReferenceObjectForEquatableTestScenarios.StartDateTimeInUtc,
                            A.Dummy<UtcDateTimeRangeInclusive>().Whose(_ => (!_.EndDateTimeInUtc.IsEqualTo(ReferenceObjectForEquatableTestScenarios.EndDateTimeInUtc)) && (_.EndDateTimeInUtc >= ReferenceObjectForEquatableTestScenarios.StartDateTimeInUtc)).EndDateTimeInUtc),
                    },
                    ObjectsThatAreNotOfTheSameTypeAsReferenceObject = new object[]
                    {
                        A.Dummy<object>(),
                        A.Dummy<string>(),
                        A.Dummy<int>(),
                        A.Dummy<int?>(),
                        A.Dummy<Guid>(),
                    },
                });
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_startDateTimeInUtc_is_not_DateTimeKind_Utc()
        {
            // Arrange
            var startDateTime1 = new DateTime(1, DateTimeKind.Local);
            var startDateTime2 = new DateTime(1, DateTimeKind.Unspecified);

            // Act
            var ex1 = Record.Exception(() => new UtcDateTimeRangeInclusive(startDateTime1, DateTime.UtcNow));
            var ex2 = Record.Exception(() => new UtcDateTimeRangeInclusive(startDateTime2, DateTime.UtcNow));

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
            var ex1 = Record.Exception(() => new UtcDateTimeRangeInclusive(DateTime.UtcNow, endDateTime1));
            var ex2 = Record.Exception(() => new UtcDateTimeRangeInclusive(DateTime.UtcNow, endDateTime2));

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
            var ex = Record.Exception(() => new UtcDateTimeRangeInclusive(startDateTimeInUtc, endDateTimeInUtc));

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
            var ex = Record.Exception(() => new UtcDateTimeRangeInclusive(startDateTimeInUtc, endDateTimeInUtc));

            // Assert
            ex.Should().BeNull();
        }

        [Fact]
        public static void ToString___Should_return_string_representation_of_date_time_range___When_called()
        {
            // Arrange
            var startDateTime = new DateTime(2019, 3, 10, 14, 4, 59, DateTimeKind.Utc);
            var endDateTime = new DateTime(2020, 11, 22, 9, 43, 4, DateTimeKind.Utc);

            var systemUnderTest = new UtcDateTimeRangeInclusive(startDateTime, endDateTime);

            // Act
            var actual = systemUnderTest.ToString();

            // Assert
            actual.Should().Be("03/10/2019 14:04:59 to 11/22/2020 09:43:04");
        }
    }
}
