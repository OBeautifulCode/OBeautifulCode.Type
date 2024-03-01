// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Range{T}Test.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;
    using FluentAssertions;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class RangeTTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static RangeTTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(
                    new ConstructorArgumentValidationTestScenario<Range<Version>>
                    {
                        Name = "constructor should throw ArgumentOutOfRangeException when 'start' > 'end'",
                        ConstructionFunc = () =>
                        {
                            var start = A.Dummy<Version>();

                            var end = A.Dummy<Version>().ThatIs(_ => _ < start);

                            return new Range<Version>(start, end);
                        },
                        ExpectedExceptionType = typeof(ArgumentOutOfRangeException),
                        ExpectedExceptionMessageContains = new[] { "start is > end" },
                    });

            StringRepresentationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(
                    new StringRepresentationTestScenario<Range<Version>>
                    {
                        Name = "ToString() should return string representation of range",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                        {
                            var startDateTime = new DateTime(2019, 3, 10, 14, 4, 59, DateTimeKind.Utc);
                            var endDateTime = new DateTime(2020, 11, 22, 9, 43, 4, DateTimeKind.Utc);

                            return new SystemUnderTestExpectedStringRepresentation<Range<Version>>
                            {
                                SystemUnderTest = new Range<Version>(new Version(1, 2), new Version(3, 4)),
                                ExpectedStringRepresentation = "1.2 to 3.4",
                            };
                        },
                    });

            EquatableTestScenarios
                .RemoveAllScenarios()
                .AddScenario(
                    new EquatableTestScenario<Range<Version>>
                    {
                        ReferenceObject = ReferenceObjectForEquatableTestScenarios,
                        ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new Range<Version>[]
                        {
                            new Range<Version>(
                                ReferenceObjectForEquatableTestScenarios.Start,
                                ReferenceObjectForEquatableTestScenarios.End),
                        },
                        ObjectsThatAreNotEqualToReferenceObject = new Range<Version>[]
                        {
                            new Range<Version>(
                                A.Dummy<Range<Version>>().Whose(_ => (!_.Start.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Start)) && (_.Start <= ReferenceObjectForEquatableTestScenarios.End)).Start,
                                ReferenceObjectForEquatableTestScenarios.End),
                            new Range<Version>(
                                ReferenceObjectForEquatableTestScenarios.Start,
                                A.Dummy<Range<Version>>().Whose(_ => (!_.End.IsEqualTo(ReferenceObjectForEquatableTestScenarios.End)) && (_.End >= ReferenceObjectForEquatableTestScenarios.Start)).End),
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
                    new DeepCloneWithTestScenario<Range<Version>>
                    {
                        Name = "DeepCloneWithStart should deep clone object and replace Start with the provided start",
                        WithPropertyName = "Start",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Range<Version>>();

                            var referenceObject = A.Dummy<Range<Version>>().ThatIs(_ => (!systemUnderTest.Start.IsEqualTo(_.Start)) && (_.Start <= systemUnderTest.Start));

                            var result = new SystemUnderTestDeepCloneWithValue<Range<Version>>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.Start,
                            };

                            return result;
                        },
                    })
                .AddScenario(() =>
                    new DeepCloneWithTestScenario<Range<Version>>
                    {
                        Name = "DeepCloneWithEnd should deep clone object and replace End with the provided end",
                        WithPropertyName = "End",
                        SystemUnderTestDeepCloneWithValueFunc = () =>
                        {
                            var systemUnderTest = A.Dummy<Range<Version>>();

                            var referenceObject = A.Dummy<Range<Version>>().ThatIs(_ => (!systemUnderTest.End.IsEqualTo(_.End)) && (_.End >= systemUnderTest.End));

                            var result = new SystemUnderTestDeepCloneWithValue<Range<Version>>
                            {
                                SystemUnderTest = systemUnderTest,
                                DeepCloneWithValue = referenceObject.End,
                            };

                            return result;
                        },
                    });
        }

        [Fact]
        public static void Constructor___Should_not_throw___When_parameter_start_is_equal_to_end()
        {
            // Arrange
            var startDateTimeInUtc = DateTime.UtcNow;
            var endDateTimeInUtc = startDateTimeInUtc;

            // Act
            var ex = Record.Exception(() => new Range<DateTime>(startDateTimeInUtc, endDateTimeInUtc));

            // Assert
            ex.Should().BeNull();
        }
    }
}