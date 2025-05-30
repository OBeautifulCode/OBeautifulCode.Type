﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamedValue{TValue}Test.cs" company="OBeautifulCode">
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
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Math.Recipes;

    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class NamedValueTValueTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static NamedValueTValueTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<NamedValue<Version>>
                    {
                        Name = "constructor should throw ArgumentNullException when parameter 'name' is null scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<NamedValue<Version>>();

                            var result = new NamedValue<Version>(
                                                 null,
                                                 referenceObject.Value);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentNullException),
                        ExpectedExceptionMessageContains = new[] { "name", },
                    })
                .AddScenario(() =>
                    new ConstructorArgumentValidationTestScenario<NamedValue<Version>>
                    {
                        Name = "constructor should throw ArgumentException when parameter 'name' is white space scenario",
                        ConstructionFunc = () =>
                        {
                            var referenceObject = A.Dummy<NamedValue<Version>>();

                            var result = new NamedValue<Version>(
                                                 Invariant($"  {Environment.NewLine}  "),
                                                 referenceObject.Value);

                            return result;
                        },
                        ExpectedExceptionType = typeof(ArgumentException),
                        ExpectedExceptionMessageContains = new[] { "name", "white space", },
                    });
        }

        [Fact]
        public static void GetValue___Should_return_Value___When_called()
        {
            // Arrange
            var expected = A.Dummy<Version>();

            var systemUnderTest = new NamedValue<Version>(A.Dummy<string>(), expected);

            // Act
            var actual = systemUnderTest.GetValue();

            // Assert
            actual.AsTest().Must().BeOfType<Version>();
            ((Version)actual).AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetValueType___Should_return_type_of_Value___When_called()
        {
            // Arrange
            var systemUnderTest = new NamedValue<Version>(A.Dummy<string>(), A.Dummy<Version>());

            // Act
            var actual = systemUnderTest.GetValueType();

            // Assert
            actual.AsTest().Must().BeEqualTo(typeof(Version));
        }
    }
}