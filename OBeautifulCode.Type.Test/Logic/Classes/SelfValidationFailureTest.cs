// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelfValidationFailureTest.cs" company="OBeautifulCode">
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
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Equality.Recipes;
    using Xunit;

    public static class SelfValidationFailureTest
    {
        private static readonly SelfValidationFailure ReferenceObjectForEquatableTestScenarios = A.Dummy<SelfValidationFailure>();

        private static readonly EquatableTestScenarios<SelfValidationFailure> EquatableTestScenarios = new EquatableTestScenarios<SelfValidationFailure>()
            .AddScenario(() =>
                new EquatableTestScenario<SelfValidationFailure>
                {
                    Name = "Default Code Generated Scenario",
                    ReferenceObject = ReferenceObjectForEquatableTestScenarios,
                    ObjectsThatAreEqualToButNotTheSameAsReferenceObject = new[]
                    {
                        new SelfValidationFailure(
                                ReferenceObjectForEquatableTestScenarios.PropertyNames,
                                ReferenceObjectForEquatableTestScenarios.Message),
                    },
                    ObjectsThatAreNotEqualToReferenceObject = new[]
                    {
                        new SelfValidationFailure(
                                A.Dummy<SelfValidationFailure>().Whose(_ => !_.PropertyNames.IsEqualTo(ReferenceObjectForEquatableTestScenarios.PropertyNames)).PropertyNames,
                                ReferenceObjectForEquatableTestScenarios.Message),
                        new SelfValidationFailure(
                                ReferenceObjectForEquatableTestScenarios.PropertyNames,
                                A.Dummy<SelfValidationFailure>().Whose(_ => !_.Message.IsEqualTo(ReferenceObjectForEquatableTestScenarios.Message)).Message),
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

        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            SelfValidationFailure systemUnderTest1 = null;
            SelfValidationFailure systemUnderTest2 = null;

            // Act
            var actual = systemUnderTest1 == systemUnderTest2;

            // Assert
            actual.AsTest().Must().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange
                SelfValidationFailure systemUnderTest = null;

                // Act
                var actual1 = systemUnderTest == scenario.ReferenceObject;
                var actual2 = scenario.ReferenceObject == systemUnderTest;

                // Assert
                actual1.AsTest().Must().BeFalse(because: scenario.Id);
                actual2.AsTest().Must().BeFalse(because: scenario.Id);
            }
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_same_object_is_on_both_sides_of_operator()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                #pragma warning disable CS1718 // Comparison made to same variable
                var actual = scenario.ReferenceObject == scenario.ReferenceObject;
                #pragma warning restore CS1718 // Comparison made to same variable

                // Assert
                actual.AsTest().Must().BeTrue(because: scenario.Id);
            }
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_objects_being_compared_derive_from_the_same_type_but_are_not_of_the_same_type()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actuals1 = scenario.ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject.Select(_ => scenario.ReferenceObject == _).ToList();
                var actuals2 = scenario.ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject.Select(_ => _ == scenario.ReferenceObject).ToList();

                // Assert
                actuals1.AsTest().Must().Each().BeFalse(because: scenario.Id);
                actuals2.AsTest().Must().Each().BeFalse(because: scenario.Id);
            }
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actuals1 = scenario.ObjectsThatAreNotEqualToReferenceObject.Select(_ => scenario.ReferenceObject == _).ToList();
                var actuals2 = scenario.ObjectsThatAreNotEqualToReferenceObject.Select(_ => _ == scenario.ReferenceObject).ToList();

                // Assert
                actuals1.AsTest().Must().Each().BeFalse(because: scenario.Id);
                actuals2.AsTest().Must().Each().BeFalse(because: scenario.Id);
            }
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actuals1 = scenario.ObjectsThatAreEqualToButNotTheSameAsReferenceObject.Select(_ => scenario.ReferenceObject == _).ToList();
                var actuals2 = scenario.ObjectsThatAreEqualToButNotTheSameAsReferenceObject.Select(_ => _ == scenario.ReferenceObject).ToList();

                // Assert
                actuals1.AsTest().Must().Each().BeTrue(because: scenario.Id);
                actuals2.AsTest().Must().Each().BeTrue(because: scenario.Id);
            }
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            SelfValidationFailure systemUnderTest1 = null;
            SelfValidationFailure systemUnderTest2 = null;

            // Act
            var actual = systemUnderTest1 != systemUnderTest2;

            // Assert
            actual.AsTest().Must().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange
                SelfValidationFailure systemUnderTest = null;

                // Act
                var actual1 = systemUnderTest != scenario.ReferenceObject;
                var actual2 = scenario.ReferenceObject != systemUnderTest;

                // Assert
                actual1.AsTest().Must().BeTrue(because: scenario.Id);
                actual2.AsTest().Must().BeTrue(because: scenario.Id);
            }
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_same_object_is_on_both_sides_of_operator()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
#pragma warning disable CS1718 // Comparison made to same variable
                var actual = scenario.ReferenceObject != scenario.ReferenceObject;
#pragma warning restore CS1718 // Comparison made to same variable

                // Assert
                actual.AsTest().Must().BeFalse(because: scenario.Id);
            }
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_objects_being_compared_derive_from_the_same_type_but_are_not_of_the_same_type()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actuals1 = scenario.ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject.Select(_ => scenario.ReferenceObject != _).ToList();
                var actuals2 = scenario.ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject.Select(_ => _ != scenario.ReferenceObject).ToList();

                // Assert
                actuals1.AsTest().Must().Each().BeTrue(because: scenario.Id);
                actuals2.AsTest().Must().Each().BeTrue(because: scenario.Id);
            }
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_objects_being_compared_have_different_property_values()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actuals1 = scenario.ObjectsThatAreNotEqualToReferenceObject.Select(_ => scenario.ReferenceObject != _).ToList();
                var actuals2 = scenario.ObjectsThatAreNotEqualToReferenceObject.Select(_ => _ != scenario.ReferenceObject).ToList();

                // Assert
                actuals1.AsTest().Must().Each().BeTrue(because: scenario.Id);
                actuals2.AsTest().Must().Each().BeTrue(because: scenario.Id);
            }
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_objects_being_compared_have_same_property_values()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actuals1 = scenario.ObjectsThatAreEqualToButNotTheSameAsReferenceObject.Select(_ => scenario.ReferenceObject != _).ToList();
                var actuals2 = scenario.ObjectsThatAreEqualToButNotTheSameAsReferenceObject.Select(_ => _ != scenario.ReferenceObject).ToList();

                // Assert
                actuals1.AsTest().Must().Each().BeFalse(because: scenario.Id);
                actuals2.AsTest().Must().Each().BeFalse(because: scenario.Id);
            }
        }

        [Fact]
        public static void Equals_with_SelfValidationFailure___Should_return_false___When_parameter_other_is_null()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange
                SelfValidationFailure systemUnderTest = null;

                // Act
                var actual = scenario.ReferenceObject.Equals(systemUnderTest);

                // Assert
                actual.AsTest().Must().BeFalse(because: scenario.Id);
            }
        }

        [Fact]
        public static void Equals_with_SelfValidationFailure___Should_return_true___When_parameter_other_is_same_object()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actual = scenario.ReferenceObject.Equals(scenario.ReferenceObject);

                // Assert
                actual.AsTest().Must().BeTrue(because: scenario.Id);
            }
        }

        [Fact]
        public static void Equals_with_SelfValidationFailure___Should_return_false___When_parameter_other_is_derived_from_the_same_type_but_is_not_of_the_same_type_as_this_object()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actuals = scenario.ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject.Select(_ => scenario.ReferenceObject.Equals(_)).ToList();

                // Assert
                actuals.AsTest().Must().Each().BeFalse(because: scenario.Id);
            }
        }

        [Fact]
        public static void Equals_with_SelfValidationFailure___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actuals = scenario.ObjectsThatAreNotEqualToReferenceObject.Select(_ => scenario.ReferenceObject.Equals(_)).ToList();

                // Assert
                actuals.AsTest().Must().Each().BeFalse(because: scenario.Id);
            }
        }

        [Fact]
        public static void Equals_with_SelfValidationFailure___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actuals = scenario.ObjectsThatAreEqualToButNotTheSameAsReferenceObject.Select(_ => scenario.ReferenceObject.Equals(_)).ToList();

                // Assert
                actuals.AsTest().Must().Each().BeTrue(because: scenario.Id);
            }
        }

        [Fact]
        public static void Equals_with_Object___Should_return_false___When_parameter_other_is_null()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actual = scenario.ReferenceObject.Equals((object)null);

                // Assert
                actual.AsTest().Must().BeFalse(because: scenario.Id);
            }
        }

        [Fact]
        public static void Equals_with_Object___Should_return_false___When_parameter_other_is_not_of_the_same_type()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actuals1 = scenario.ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject.Select(_ => scenario.ReferenceObject.Equals((object)_)).ToList();
                var actuals2 = scenario.ObjectsThatAreNotOfTheSameTypeAsReferenceObject.Select(_ => scenario.ReferenceObject.Equals((object)_)).ToList();

                // Assert
                actuals1.AsTest().Must().Each().BeFalse(because: scenario.Id);
                actuals2.AsTest().Must().Each().BeFalse(because: scenario.Id);
            }
        }

        [Fact]
        public static void Equals_with_Object___Should_return_true___When_parameter_other_is_same_object()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actual = scenario.ReferenceObject.Equals((object)scenario.ReferenceObject);

                // Assert
                actual.AsTest().Must().BeTrue(because: scenario.Id);
            }
        }

        [Fact]
        public static void Equals_with_Object___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actuals = scenario.ObjectsThatAreNotEqualToReferenceObject.Select(_ => scenario.ReferenceObject.Equals((object)_)).ToList();

                // Assert
                actuals.AsTest().Must().Each().BeFalse(because: scenario.Id);
            }
        }

        [Fact]
        public static void Equals_with_Object___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var actuals = scenario.ObjectsThatAreEqualToButNotTheSameAsReferenceObject.Select(_ => scenario.ReferenceObject.Equals((object)_)).ToList();

                // Assert
                actuals.AsTest().Must().Each().BeTrue(because: scenario.Id);
            }
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal_for_two_objects___When_objects_have_different_property_values()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var unexpected = scenario.ReferenceObject.GetHashCode();

                var actuals = scenario.ObjectsThatAreNotEqualToReferenceObject.Select(_ => _.GetHashCode()).ToList();

                // Assert
                actuals.AsTest().Must().NotContainElement(unexpected, because: scenario.Id);
            }
        }

        [Fact]
        public static void GetHashCode___Should_be_equal_for_two_objects___When_objects_have_the_same_property_values()
        {
            var scenarios = EquatableTestScenarios.ValidateAndPrepareForTesting();

            foreach (var scenario in scenarios)
            {
                // Arrange, Act
                var expected = scenario.ReferenceObject.GetHashCode();

                var actuals = scenario.ObjectsThatAreEqualToButNotTheSameAsReferenceObject.Select(_ => _.GetHashCode()).ToList();

                // Assert
                actuals.AsTest().Must().Each().BeEqualTo(expected, because: scenario.Id);
            }
        }
    }
}