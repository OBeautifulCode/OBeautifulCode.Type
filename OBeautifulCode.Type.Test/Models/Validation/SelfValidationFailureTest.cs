// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelfValidationFailureTest.cs" company="OBeautifulCode">
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

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class SelfValidationFailureTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static SelfValidationFailureTest()
        {
            StringRepresentationTestScenarios
                .AddScenario(
                    new StringRepresentationTestScenario<SelfValidationFailure>
                    {
                        Name = "Single property name scenario",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                        {
                            var systemUnderTest = new SelfValidationFailure("MyProperty", "My message here.");

                            var result =
                                new SystemUnderTestExpectedStringRepresentation<SelfValidationFailure>
                                {
                                    SystemUnderTest = systemUnderTest,
                                    ExpectedStringRepresentation = "MyProperty : My message here.",
                                };

                            return result;
                        },
                    })
                .AddScenario(
                    new StringRepresentationTestScenario<SelfValidationFailure>
                    {
                        Name = "Two property names scenario",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                        {
                            var systemUnderTest = new SelfValidationFailure(
                                new[]
                                {
                                    "MyProperty1",
                                    "MyProperty2",
                                },
                                "My message here.");

                            var result =
                                new SystemUnderTestExpectedStringRepresentation<SelfValidationFailure>
                                {
                                    SystemUnderTest = systemUnderTest,
                                    ExpectedStringRepresentation = "MyProperty1|MyProperty2 : My message here.",
                                };

                            return result;
                        },
                    })
                .AddScenario(
                    new StringRepresentationTestScenario<SelfValidationFailure>
                    {
                        Name = "Multiple property names scenario",
                        SystemUnderTestExpectedStringRepresentationFunc = () =>
                        {
                            var systemUnderTest = new SelfValidationFailure(
                                new[]
                                {
                                    "MyProperty1",
                                    "MyProperty2",
                                    "MyProperty3",
                                },
                                "My message here.");

                            var result =
                                new SystemUnderTestExpectedStringRepresentation<SelfValidationFailure>
                                {
                                    SystemUnderTest = systemUnderTest,
                                    ExpectedStringRepresentation = "MyProperty1|MyProperty2|MyProperty3 : My message here.",
                                };

                            return result;
                        },
                    });
        }
    }
}