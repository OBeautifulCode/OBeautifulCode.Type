// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleValue{TValue}Test.cs" company="OBeautifulCode">
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
    public static partial class SimpleValueTValueTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static SimpleValueTValueTest()
        {
            ConstructorArgumentValidationTestScenarios
                .RemoveAllScenarios()
                .AddScenario(ConstructorArgumentValidationTestScenario<SimpleValue<Version>>.ConstructorCannotThrowScenario);
        }

        [Fact]
        public static void GetValue___Should_return_Value___When_called()
        {
            // Arrange
            var expected = A.Dummy<Version>();

            var systemUnderTest = new SimpleValue<Version>(expected);

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
            var systemUnderTest = new SimpleValue<Version>(A.Dummy<Version>());

            // Act
            var actual = systemUnderTest.GetValueType();

            // Assert
            actual.AsTest().Must().BeEqualTo(typeof(Version));
        }
    }
}