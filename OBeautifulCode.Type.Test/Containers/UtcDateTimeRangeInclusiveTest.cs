// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UtcDateTimeRangeInclusiveTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Type.Test.Internal;

    using Xunit;

    public static partial class UtcDateTimeRangeInclusiveTest
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static UtcDateTimeRangeInclusiveTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(
                    new ConstructorArgumentValidationTestScenario<UtcDateTimeRangeInclusive>
                    {
                        Name = "constructor should throw ArgumentException when 'startDateTimeInUtc' is DateTimeKind.Local",
                        ConstructionFunc = () =>
                        {
                            var startDateTime = new DateTime(1, DateTimeKind.Local);

                            return new UtcDateTimeRangeInclusive(startDateTime, DateTime.UtcNow);
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "startDateTimeInUtc DateTimeKind is not Utc" },
                    })
                .AddScenario(
                    new ConstructorArgumentValidationTestScenario<UtcDateTimeRangeInclusive>
                    {
                        Name = "constructor should throw ArgumentException when 'startDateTimeInUtc' is DateTimeKind.Unspecified",
                        ConstructionFunc = () =>
                        {
                            var startDateTime = new DateTime(1, DateTimeKind.Unspecified);

                            return new UtcDateTimeRangeInclusive(startDateTime, DateTime.UtcNow);
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "startDateTimeInUtc DateTimeKind is not Utc" },
                    })
                .AddScenario(
                    new ConstructorArgumentValidationTestScenario<UtcDateTimeRangeInclusive>
                    {
                        Name = "constructor should throw ArgumentException when 'endDateTimeInUtc' is DateTimeKind.Local",
                        ConstructionFunc = () =>
                        {
                            var endDateTime = new DateTime(1, DateTimeKind.Local);

                            return new UtcDateTimeRangeInclusive(DateTime.UtcNow, endDateTime);
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "endDateTimeInUtc DateTimeKind is not Utc" },
                    })
                .AddScenario(
                    new ConstructorArgumentValidationTestScenario<UtcDateTimeRangeInclusive>
                    {
                        Name = "constructor should throw ArgumentException when 'endDateTimeInUtc' is DateTimeKind.Unspecified",
                        ConstructionFunc = () =>
                        {
                            var endDateTime = new DateTime(1, DateTimeKind.Unspecified);

                            return new UtcDateTimeRangeInclusive(DateTime.UtcNow, endDateTime);
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "endDateTimeInUtc DateTimeKind is not Utc" },
                    })
                .AddScenario(
                    new ConstructorArgumentValidationTestScenario<UtcDateTimeRangeInclusive>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when 'startDateTimeInUtc' > 'endDateTimeInUtc'",
                        ConstructionFunc = () =>
                        {
                            var startDateTimeInUtc = DateTime.UtcNow;

                            var endDateTimeInUtc = startDateTimeInUtc.AddMilliseconds(-1);

                            return new UtcDateTimeRangeInclusive(startDateTimeInUtc, endDateTimeInUtc);
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "startDateTimeInUtc is > endDateTimeInUtc" },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(
                    new StringRepresentationTestScenario<UtcDateTimeRangeInclusive>
                    {
                        Name = "ToString() should return string representation of date time range",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                        {
                            var startDateTime = new DateTime(2019, 3, 10, 14, 4, 59, DateTimeKind.Utc);
                            var endDateTime = new DateTime(2020, 11, 22, 9, 43, 4, DateTimeKind.Utc);

                            return new SystemUnderTestExpectedStringRepresentation<UtcDateTimeRangeInclusive>
                            {
                                SystemUnderTest = new UtcDateTimeRangeInclusive(startDateTime, endDateTime),
                                ExpectedStringRepresentation = "03/10/2019 14:04:59 to 11/22/2020 09:43:04",
                            };
                        },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(
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

            DeepCloneWithTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<UtcDateTimeRangeInclusive>
                    {
                        Name = "DeepCloneWithStartDateTimeInUtc should deep clone object and replace ParentBoolProperty with the provided parentBoolProperty",
                        WithPropertyName = "StartDateTimeInUtc",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<UtcDateTimeRangeInclusive>();

                            var referenceObject = A.Dummy<UtcDateTimeRangeInclusive>().ThatIs(_ => (!systemUnderTest.StartDateTimeInUtc.IsEqualTo(_.StartDateTimeInUtc)) && (_.StartDateTimeInUtc <= systemUnderTest.StartDateTimeInUtc));

                            var result = new SystemUnderTestDeepCloneWithValue<UtcDateTimeRangeInclusive>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.StartDateTimeInUtc,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<UtcDateTimeRangeInclusive>
                    {
                        Name = "DeepCloneWithEndDateTimeInUtc should deep clone object and replace ParentBoolProperty with the provided parentBoolProperty",
                        WithPropertyName = "EndDateTimeInUtc",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<UtcDateTimeRangeInclusive>();

                            var referenceObject = A.Dummy<UtcDateTimeRangeInclusive>().ThatIs(_ => (!systemUnderTest.EndDateTimeInUtc.IsEqualTo(_.EndDateTimeInUtc)) && (_.EndDateTimeInUtc >= systemUnderTest.EndDateTimeInUtc));

                            var result = new SystemUnderTestDeepCloneWithValue<UtcDateTimeRangeInclusive>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.EndDateTimeInUtc,
                            };

                            return result;
                        },
                    });
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
    }
}
