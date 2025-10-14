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
    using OBeautifulCode.Type.Recipes;
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

            var expectedValidationFailure = GetExpectedValidationFailure(-5);

            // Act
            var actual = subjectUnderTest.IsValid(out var actualFailures, TestValidationOptions);

            // Assert
            actual.AsTest().Must().BeFalse();
            actualFailures.AsTest().Must().HaveCount(1).And().Each().BeEqualTo(expectedValidationFailure);
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

            var expectedValidationFailure = GetExpectedValidationFailure(-5);

            // Act
            var actual = Record.Exception(() => subjectUnderTest.ThrowIfInvalid(TestValidationOptions));

            // Assert
            actual.AsTest().Must().BeOfType<ValidationException>();
            ((ValidationException)actual).Failures.AsTest().Must().HaveCount(1).And().Each().BeEqualTo(expectedValidationFailure);
        }

        [Fact]
        public static void GetValidationFailures___Should_throw_ArgumentNullException___When_parameter_options_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ValidatableExtensions.GetValidationFailures(
                A.Dummy<object>(),
                null,
                new PropertyPathTracker(),
                A.Dummy<string>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("options");
        }

        [Fact]
        public static void GetValidationFailures___Should_throw_ArgumentNullException___When_parameter_propertyPathTracker_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ValidatableExtensions.GetValidationFailures(
                A.Dummy<object>(),
                new ValidationOptions(),
                null,
                A.Dummy<string>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("propertyPathTracker");
        }

        [Fact]
        public static void GetValidationFailures___Should_throw_ArgumentNullException___When_parameter_segmentName_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ValidatableExtensions.GetValidationFailures(
                A.Dummy<object>(),
                new ValidationOptions(),
                new PropertyPathTracker(),
                null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("segmentName");
        }

        [Fact]
        public static void GetValidationFailures___Should_throw_ArgumentException___When_parameter_segmentName_is_white_space()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ValidatableExtensions.GetValidationFailures(
                A.Dummy<object>(),
                new ValidationOptions(),
                new PropertyPathTracker(),
                "  \r\n  "));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("segmentName");
            actual.Message.AsTest().Must().ContainString("white space");
        }

        [Fact]
        public static void GetValidationFailures___Should_return_empty_list___When_parameter_objectToValidate_is_null()
        {
            // Arrange, Act
            var actual = ValidatableExtensions.GetValidationFailures(
                null,
                new ValidationOptions(),
                new PropertyPathTracker(),
                A.Dummy<string>());

            // Assert
            actual.AsTest().Must().BeEmptyEnumerable();
        }

        [Fact]
        public static void GetValidationFailures___Should_return_empty_list___When_parameter_objectToValidate_is_IValidatable_and_valid()
        {
            // Arrange
            var objectToValidate = new ValidatableTestObject();

            // Act
            var actual = ValidatableExtensions.GetValidationFailures(
                objectToValidate,
                TestValidationOptions,
                new PropertyPathTracker(),
                A.Dummy<string>());

            // Assert
            actual.AsTest().Must().BeEmptyEnumerable();
        }

        [Fact]
        public static void GetValidationFailures___Should_return_validation_failure___When_parameter_objectToValidate_is_IValidatable_and_invalid()
        {
            // Arrange
            var objectToValidate = new ValidatableTestObject
            {
                IntProperty = -1,
            };

            var segmentName = A.Dummy<string>();

            var expected = GetExpectedValidationFailure(-1, segmentName);

            // Act
            var actual = ValidatableExtensions.GetValidationFailures(
                objectToValidate,
                TestValidationOptions,
                new PropertyPathTracker(),
                segmentName);

            // Assert
            actual.AsTest().Must().HaveCount(1).And().Each().BeEqualTo(expected);
        }

        [Fact]
        public static void GetValidationFailures___Should_return_empty_list___When_parameter_objectToValidate_is_type_that_cannot_contain_validatable_object()
        {
            // Arrange
            var objectsToValidate = new object[]
            {
                A.Dummy<char>(),
                A.Dummy<bool>(),
                A.Dummy<double>(),
                A.Dummy<RelativeSortOrder>(),
                A.Dummy<string>(),
                A.Dummy<decimal>(),
                A.Dummy<DateTime>(),
                A.Dummy<DateTimeOffset>(),
                A.Dummy<TimeSpan>(),
                A.Dummy<Guid>(),
                A.Dummy<Version>(),
                A.Dummy<Uri>(),
                A.Dummy<bool?>(),
                A.Dummy<decimal?>(),
                A.Dummy<Guid?>(),
            };

            // Act
            var actual = objectsToValidate.Select(_ => ValidatableExtensions.GetValidationFailures(
                _,
                TestValidationOptions,
                new PropertyPathTracker(),
                A.Dummy<string>()));

            // Assert
            actual.AsTest().Must().Each().BeEmptyEnumerable();
        }

        [Fact]
        public static void GetValidationFailures___Should_return_empty_list___When_there_are_no_validation_failures()
        {
            // Arrange
            var objectsToValidate = new object[]
            {
                // array
                new ValidatableTestObject[] { new ValidatableTestObject(), new ValidatableTestObject() },

                // list
                new List<ValidatableTestObject> { new ValidatableTestObject(), new ValidatableTestObject() },

                // dictionary
                new Dictionary<ValidatableTestObject, ValidatableTestObject>
                {
                    { new ValidatableTestObject(), new ValidatableTestObject() },
                    { new ValidatableTestObject(), new ValidatableTestObject() },
                    { new ValidatableTestObject(), new ValidatableTestObject() },
                },

                // kvp
                new KeyValuePair<ValidatableTestObject, ValidatableTestObject>(new ValidatableTestObject(), new ValidatableTestObject()),

                // tuple
                new Tuple<ValidatableTestObject, ValidatableTestObject>(new ValidatableTestObject(), new ValidatableTestObject()),

                // combo
                new Dictionary<List<ValidatableTestObject>, Tuple<KeyValuePair<ValidatableTestObject, ValidatableTestObject>[], Dictionary<ValidatableTestObject, ValidatableTestObject>>>()
                {
                    {
                        new List<ValidatableTestObject> { new ValidatableTestObject(), new ValidatableTestObject() },
                        new Tuple<KeyValuePair<ValidatableTestObject, ValidatableTestObject>[], Dictionary<ValidatableTestObject, ValidatableTestObject>>(
                            new[]
                            {
                                new KeyValuePair<ValidatableTestObject, ValidatableTestObject>(
                                    new ValidatableTestObject(),
                                    new ValidatableTestObject()),
                                new KeyValuePair<ValidatableTestObject, ValidatableTestObject>(
                                    new ValidatableTestObject(),
                                    new ValidatableTestObject()),
                            },
                            new Dictionary<ValidatableTestObject, ValidatableTestObject>
                            {
                                { new ValidatableTestObject(), new ValidatableTestObject() },
                                { new ValidatableTestObject(), new ValidatableTestObject() },
                                { new ValidatableTestObject(), new ValidatableTestObject() },
                            })
                    },
                },
            };

            // Act
            var actual = objectsToValidate.Select(_ => ValidatableExtensions.GetValidationFailures(
                _,
                TestValidationOptions,
                new PropertyPathTracker(),
                A.Dummy<string>()));

            // Assert
            actual.AsTest().Must().Each().BeEmptyEnumerable();
        }

        [Fact]
        public static void GetValidationFailures___Should_return_validation_failure___When_there_is_one_validation_failure()
        {
            // Arrange
            var segmentName = "SomeProperty";

            var objectsToValidateAndExpected = new[]
            {
                new
                {
                    // array
                    ObjectToValidate = (object)new ValidatableTestObject[]
                    {
                        new ValidatableTestObject() { IntProperty = -1 },
                        new ValidatableTestObject(),
                    },
                    Expected = GetExpectedValidationFailure(-1, Invariant($"{segmentName}[0]")),
                },
                new
                {
                    // array
                    ObjectToValidate = (object)new ValidatableTestObject[]
                    {
                        new ValidatableTestObject(),
                        new ValidatableTestObject { IntProperty = -2 },
                    },
                    Expected = GetExpectedValidationFailure(-2, Invariant($"{segmentName}[1]")),
                },
                new
                {
                    // list
                    ObjectToValidate = (object)new List<ValidatableTestObject>
                    {
                        new ValidatableTestObject(),
                        new ValidatableTestObject { IntProperty = -3 },
                    },
                    Expected = GetExpectedValidationFailure(-3, Invariant($"{segmentName}[1]")),
                },
                new
                {
                    // dictionary key
                    ObjectToValidate = (object)new Dictionary<ValidatableTestObject, ValidatableTestObject>
                    {
                        { new ValidatableTestObject(), new ValidatableTestObject() },
                        { new ValidatableTestObject { IntProperty = -4 }, new ValidatableTestObject() },
                        { new ValidatableTestObject(), new ValidatableTestObject() },
                    },
                    Expected = GetExpectedValidationFailure(-4, Invariant($"{segmentName}[\"K-4\"]:Key")),
                },
                new
                {
                    // dictionary value
                    ObjectToValidate = (object)new Dictionary<ValidatableTestObject, ValidatableTestObject>
                    {
                        { new ValidatableTestObject(), new ValidatableTestObject() },
                        { new ValidatableTestObject { IntProperty = 5 }, new ValidatableTestObject() { IntProperty = -6 } },
                        { new ValidatableTestObject(), new ValidatableTestObject() },
                    },
                    Expected = GetExpectedValidationFailure(-6, Invariant($"{segmentName}[\"K5\"]")),
                },
                new
                {
                    // kvp key
                    ObjectToValidate = (object)new KeyValuePair<ValidatableTestObject, ValidatableTestObject>(new ValidatableTestObject { IntProperty = -7 }, new ValidatableTestObject()),
                    Expected = GetExpectedValidationFailure(-7, Invariant($"{segmentName}.Key")),
                },
                new
                {
                    // kvp value
                    ObjectToValidate = (object)new KeyValuePair<ValidatableTestObject, ValidatableTestObject>(new ValidatableTestObject(), new ValidatableTestObject() { IntProperty = -8 }),
                    Expected = GetExpectedValidationFailure(-8, Invariant($"{segmentName}.Value")),
                },
                new
                {
                    // tuple item1
                    ObjectToValidate = (object)new Tuple<ValidatableTestObject, ValidatableTestObject>(new ValidatableTestObject() { IntProperty = -9 }, new ValidatableTestObject()),
                    Expected = GetExpectedValidationFailure(-9, Invariant($"{segmentName}.Item1")),
                },
                new
                {
                    // tuple item2
                    ObjectToValidate = (object)new Tuple<ValidatableTestObject, ValidatableTestObject>(new ValidatableTestObject(), new ValidatableTestObject() { IntProperty = -10 }),
                    Expected = GetExpectedValidationFailure(-10, Invariant($"{segmentName}.Item2")),
                },
                new
                {
                    // combo
                    ObjectToValidate = (object)new Dictionary<ValidatableTestObject, Tuple<KeyValuePair<ValidatableTestObject, ValidatableTestObject>[], Dictionary<ValidatableTestObject, ValidatableTestObject>>>()
                    {
                        {
                            new ValidatableTestObject() { IntProperty = 11 },
                            new Tuple<KeyValuePair<ValidatableTestObject, ValidatableTestObject>[], Dictionary<ValidatableTestObject, ValidatableTestObject>>(
                                new[]
                                {
                                    new KeyValuePair<ValidatableTestObject, ValidatableTestObject>(
                                        new ValidatableTestObject(),
                                        new ValidatableTestObject()),
                                    new KeyValuePair<ValidatableTestObject, ValidatableTestObject>(
                                        new ValidatableTestObject(),
                                        new ValidatableTestObject() { IntProperty = -12 }),
                                },
                                new Dictionary<ValidatableTestObject, ValidatableTestObject>
                                {
                                    { new ValidatableTestObject(), new ValidatableTestObject() },
                                    { new ValidatableTestObject(), new ValidatableTestObject() },
                                    { new ValidatableTestObject(), new ValidatableTestObject() },
                                })
                        },
                    },
                    Expected = GetExpectedValidationFailure(-12, Invariant($"{segmentName}[\"K11\"].Item1[1].Value")),
                },
                new
                {
                    // combo
                    ObjectToValidate = (object)new Dictionary<ValidatableTestObject, Tuple<KeyValuePair<ValidatableTestObject, ValidatableTestObject>[], Dictionary<ValidatableTestObject, ValidatableTestObject>>>()
                    {
                        {
                            new ValidatableTestObject() { IntProperty = 13 },
                            new Tuple<KeyValuePair<ValidatableTestObject, ValidatableTestObject>[], Dictionary<ValidatableTestObject, ValidatableTestObject>>(
                                new[]
                                {
                                    new KeyValuePair<ValidatableTestObject, ValidatableTestObject>(
                                        new ValidatableTestObject(),
                                        new ValidatableTestObject()),
                                    new KeyValuePair<ValidatableTestObject, ValidatableTestObject>(
                                        new ValidatableTestObject(),
                                        new ValidatableTestObject()),
                                },
                                new Dictionary<ValidatableTestObject, ValidatableTestObject>
                                {
                                    { new ValidatableTestObject(), new ValidatableTestObject() },
                                    { new ValidatableTestObject() { IntProperty = 14 }, new ValidatableTestObject() { IntProperty = -15 } },
                                    { new ValidatableTestObject(), new ValidatableTestObject() },
                                })
                        },
                    },
                    Expected = GetExpectedValidationFailure(-15, Invariant($"{segmentName}[\"K13\"].Item2[\"K14\"]")),
                },
            };

            var objectsToValidate = objectsToValidateAndExpected.Select(_ => _.ObjectToValidate).ToList();

            var expected = objectsToValidateAndExpected.Select(_ => (IReadOnlyList<ValidationFailure>)new List<ValidationFailure> { _.Expected }).ToList();

            // Act
            var actual = objectsToValidate.Select(_ => ValidatableExtensions.GetValidationFailures(
                    _,
                    TestValidationOptions,
                    new PropertyPathTracker(),
                    segmentName))
                .ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetValidationFailures___Should_return_all_validation_failures___When_there_are_multiple_validation_failures_and_ValidateUntil_is_FullyTraversed()
        {
            // Arrange
            var segmentName = "SomeProperty";

            // combo
            var objectToValidate = new Dictionary<ValidatableTestObject2, Tuple<KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>[], Dictionary<ValidatableTestObject2, ValidatableTestObject2>>>()
            {
                {
                    new ValidatableTestObject2() { IntProperty = -1 },
                    new Tuple<KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>[], Dictionary<ValidatableTestObject2, ValidatableTestObject2>>(
                        new[]
                        {
                            new KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>(
                                new ValidatableTestObject2() { IntProperty = -2 },
                                new ValidatableTestObject2() { IntProperty = -3 }),
                            new KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>(
                                new ValidatableTestObject2() { IntProperty = -4 },
                                new ValidatableTestObject2() { IntProperty = -5 }),
                        },
                        new Dictionary<ValidatableTestObject2, ValidatableTestObject2>
                        {
                            { new ValidatableTestObject2() { IntProperty = -6 }, new ValidatableTestObject2() { IntProperty = -7 } },
                            { new ValidatableTestObject2() { IntProperty = -8 }, new ValidatableTestObject2() { IntProperty = -9 } },
                        })
                },
            };

            IReadOnlyList<ValidationFailure> expected = new[]
            {
                GetExpectedValidationFailure2(-1, Invariant($"{segmentName}[\"K-1\"]:Key")),
                GetExpectedValidationFailure2(-2, Invariant($"{segmentName}[\"K-1\"].Item1[0].Key")),
                GetExpectedValidationFailure2(-3, Invariant($"{segmentName}[\"K-1\"].Item1[0].Value")),
                GetExpectedValidationFailure2(-4, Invariant($"{segmentName}[\"K-1\"].Item1[1].Key")),
                GetExpectedValidationFailure2(-5, Invariant($"{segmentName}[\"K-1\"].Item1[1].Value")),
                GetExpectedValidationFailure2(-6, Invariant($"{segmentName}[\"K-1\"].Item2[\"K-6\"]:Key")),
                GetExpectedValidationFailure2(-7, Invariant($"{segmentName}[\"K-1\"].Item2[\"K-6\"]")),
                GetExpectedValidationFailure2(-8, Invariant($"{segmentName}[\"K-1\"].Item2[\"K-8\"]:Key")),
                GetExpectedValidationFailure2(-9, Invariant($"{segmentName}[\"K-1\"].Item2[\"K-8\"]")),
            };

            // Act
            var actual = ValidatableExtensions.GetValidationFailures(
                objectToValidate,
                new ValidationOptions { ValidateUntil = ValidateUntil.FullyTraversed },
                new PropertyPathTracker(),
                segmentName);

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void GetValidationFailures___Should_return_first_failure___When_there_is_are_multiple_validation_failures_and_ValidateUntil_is_FirstInvalidObject()
        {
            // Arrange
            var segmentName = "SomeProperty";

            var objectsToValidateAndExpected = new[]
            {
                new
                {
                    // array
                    ObjectToValidate = (object)new ValidatableTestObject2[]
                    {
                        new ValidatableTestObject2 { IntProperty = -1 },
                        new ValidatableTestObject2 { IntProperty = int.MinValue },
                    },
                    Expected = GetExpectedValidationFailure2(-1, Invariant($"{segmentName}[0]")),
                },
                new
                {
                    // list
                    ObjectToValidate = (object)new List<ValidatableTestObject2>
                    {
                        new ValidatableTestObject2 { IntProperty = -3 },
                        new ValidatableTestObject2 { IntProperty = int.MinValue },
                    },
                    Expected = GetExpectedValidationFailure2(-3, Invariant($"{segmentName}[0]")),
                },
                new
                {
                    // dictionary key
                    ObjectToValidate = (object)new Dictionary<ValidatableTestObject2, ValidatableTestObject2>
                    {
                        { new ValidatableTestObject2(), new ValidatableTestObject2() },
                        { new ValidatableTestObject2 { IntProperty = -4 }, new ValidatableTestObject2() { IntProperty = int.MinValue } },
                        { new ValidatableTestObject2() { IntProperty = int.MinValue }, new ValidatableTestObject2() { IntProperty = int.MinValue } },
                    },
                    Expected = GetExpectedValidationFailure2(-4, Invariant($"{segmentName}[\"K-4\"]:Key")),
                },
                new
                {
                    // dictionary value
                    ObjectToValidate = (object)new Dictionary<ValidatableTestObject2, ValidatableTestObject2>
                    {
                        { new ValidatableTestObject2(), new ValidatableTestObject2() },
                        { new ValidatableTestObject2 { IntProperty = 5 }, new ValidatableTestObject2() { IntProperty = -6 } },
                        { new ValidatableTestObject2() { IntProperty = int.MinValue }, new ValidatableTestObject2() { IntProperty = int.MinValue } },
                    },
                    Expected = GetExpectedValidationFailure2(-6, Invariant($"{segmentName}[\"K5\"]")),
                },
                new
                {
                    // kvp key
                    ObjectToValidate = (object)new KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>(new ValidatableTestObject2 { IntProperty = -7 }, new ValidatableTestObject2() { IntProperty = int.MinValue }),
                    Expected = GetExpectedValidationFailure2(-7, Invariant($"{segmentName}.Key")),
                },
                new
                {
                    // kvp value
                    ObjectToValidate = (object)new KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>(new ValidatableTestObject2(), new ValidatableTestObject2() { IntProperty = -8 }),
                    Expected = GetExpectedValidationFailure2(-8, Invariant($"{segmentName}.Value")),
                },
                new
                {
                    // tuple item1
                    ObjectToValidate = (object)new Tuple<ValidatableTestObject2, ValidatableTestObject2>(new ValidatableTestObject2() { IntProperty = -9 }, new ValidatableTestObject2() { IntProperty = int.MinValue }),
                    Expected = GetExpectedValidationFailure2(-9, Invariant($"{segmentName}.Item1")),
                },
                new
                {
                    // tuple item2
                    ObjectToValidate = (object)new Tuple<ValidatableTestObject2, ValidatableTestObject2, ValidatableTestObject2>(new ValidatableTestObject2(), new ValidatableTestObject2() { IntProperty = -10 }, new ValidatableTestObject2() { IntProperty = int.MinValue }),
                    Expected = GetExpectedValidationFailure2(-10, Invariant($"{segmentName}.Item2")),
                },
                new
                {
                    // combo
                    ObjectToValidate = (object)new Dictionary<ValidatableTestObject2, Tuple<KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>[], Dictionary<ValidatableTestObject2, ValidatableTestObject2>>>()
                    {
                        {
                            new ValidatableTestObject2() { IntProperty = 11 },
                            new Tuple<KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>[], Dictionary<ValidatableTestObject2, ValidatableTestObject2>>(
                                new[]
                                {
                                    new KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>(
                                        new ValidatableTestObject2(),
                                        new ValidatableTestObject2()),
                                    new KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>(
                                        new ValidatableTestObject2(),
                                        new ValidatableTestObject2() { IntProperty = -12 }),
                                },
                                new Dictionary<ValidatableTestObject2, ValidatableTestObject2>
                                {
                                    { new ValidatableTestObject2() { IntProperty = int.MinValue }, new ValidatableTestObject2() { IntProperty = int.MinValue } },
                                    { new ValidatableTestObject2() { IntProperty = int.MinValue }, new ValidatableTestObject2() { IntProperty = int.MinValue } },
                                    { new ValidatableTestObject2(), new ValidatableTestObject2() },
                                })
                        },
                    },
                    Expected = GetExpectedValidationFailure2(-12, Invariant($"{segmentName}[\"K11\"].Item1[1].Value")),
                },
                new
                {
                    // combo
                    ObjectToValidate = (object)new Dictionary<ValidatableTestObject2, Tuple<KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>[], Dictionary<ValidatableTestObject2, ValidatableTestObject2>>>()
                    {
                        {
                            new ValidatableTestObject2() { IntProperty = 13 },
                            new Tuple<KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>[], Dictionary<ValidatableTestObject2, ValidatableTestObject2>>(
                                new[]
                                {
                                    new KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>(
                                        new ValidatableTestObject2(),
                                        new ValidatableTestObject2()),
                                    new KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>(
                                        new ValidatableTestObject2(),
                                        new ValidatableTestObject2()),
                                },
                                new Dictionary<ValidatableTestObject2, ValidatableTestObject2>
                                {
                                    { new ValidatableTestObject2(), new ValidatableTestObject2() },
                                    { new ValidatableTestObject2() { IntProperty = 14 }, new ValidatableTestObject2() { IntProperty = -15 } },
                                    { new ValidatableTestObject2() { IntProperty = int.MinValue }, new ValidatableTestObject2() { IntProperty = int.MinValue } },
                                })
                        },
                    },
                    Expected = GetExpectedValidationFailure2(-15, Invariant($"{segmentName}[\"K13\"].Item2[\"K14\"]")),
                },
                new
                {
                    // combo
                    ObjectToValidate = (object)new Dictionary<ValidatableTestObject2, Tuple<KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>[], Dictionary<ValidatableTestObject2, ValidatableTestObject2>>>()
                    {
                        {
                            new ValidatableTestObject2() { IntProperty = -13 },
                            new Tuple<KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>[], Dictionary<ValidatableTestObject2, ValidatableTestObject2>>(
                                new[]
                                {
                                    new KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>(
                                        new ValidatableTestObject2() { IntProperty = int.MinValue },
                                        new ValidatableTestObject2()),
                                    new KeyValuePair<ValidatableTestObject2, ValidatableTestObject2>(
                                        new ValidatableTestObject2(),
                                        new ValidatableTestObject2() { IntProperty = int.MinValue }),
                                },
                                new Dictionary<ValidatableTestObject2, ValidatableTestObject2>
                                {
                                    { new ValidatableTestObject2() { IntProperty = int.MinValue }, new ValidatableTestObject2() { IntProperty = int.MinValue } },
                                    { new ValidatableTestObject2() { IntProperty = int.MinValue }, new ValidatableTestObject2() { IntProperty = int.MinValue } },
                                    { new ValidatableTestObject2(), new ValidatableTestObject2() },
                                })
                        },
                    },
                    Expected = GetExpectedValidationFailure2(-13, Invariant($"{segmentName}[\"K-13\"]:Key")),
                },
            };

            var objectsToValidate = objectsToValidateAndExpected.Select(_ => _.ObjectToValidate).ToList();

            var expected = objectsToValidateAndExpected.Select(_ => (IReadOnlyList<ValidationFailure>)new List<ValidationFailure> { _.Expected }).ToList();

            // Act
            var actual = objectsToValidate.Select(_ => ValidatableExtensions.GetValidationFailures(
                    _,
                    new ValidationOptions { ValidateUntil = ValidateUntil.FirstInvalidObject },
                    new PropertyPathTracker(),
                    segmentName))
                .ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        private static ValidationFailure GetExpectedValidationFailure(
            int invalidValue,
            string segmentPrefix = null)
        {
            if (segmentPrefix != null)
            {
                segmentPrefix += ".";
            }

            var result = new ValidationFailure(
                typeof(ValidatableTestObject).ToStringReadable(),
                Invariant($"{segmentPrefix}{nameof(ValidatableTestObject.IntProperty)}"),
                Invariant($"{nameof(ValidatableTestObject.IntProperty)} < 0.  It is {invalidValue}."));

            return result;
        }

        private static ValidationFailure GetExpectedValidationFailure2(
            int invalidValue,
            string segmentPrefix = null)
        {
            if (segmentPrefix != null)
            {
                segmentPrefix += ".";
            }

            var result = new ValidationFailure(
                typeof(ValidatableTestObject2).ToStringReadable(),
                Invariant($"{segmentPrefix}{nameof(ValidatableTestObject2.IntProperty)}"),
                Invariant($"{nameof(ValidatableTestObject2.IntProperty)} < 0.  It is {invalidValue}."));

            return result;
        }

        private class ValidatableTestObject : IValidatable
        {
            public int IntProperty { get; set; }

            public IReadOnlyList<SelfValidationFailure> GetSelfValidationFailures()
            {
                var result = new List<SelfValidationFailure>();

                if (this.IntProperty < 0)
                {
                    result.Add(new SelfValidationFailure(
                        nameof(this.IntProperty),
                        Invariant($"{nameof(this.IntProperty)} < 0.  It is {this.IntProperty}.")));
                }

                return result;
            }

            public IReadOnlyList<ValidationFailure> GetValidationFailures(
                ValidationOptions options = null,
                PropertyPathTracker propertyPathTracker = null)
            {
                options.AsOp().Must().BeEqualTo(TestValidationOptions);
                propertyPathTracker = propertyPathTracker ?? new PropertyPathTracker();

                var segmentSeparator = propertyPathTracker.HasSegments ? propertyPathTracker.SegmentSeparator : string.Empty;

                var result = (this.GetSelfValidationFailures() ?? new List<SelfValidationFailure>())
                    .Where(_ => _ != null)
                    .Select(_ =>
                    {
                        var propertyNames = _.PropertyNames.Count > 1
                            ? Invariant($"({string.Join("|", _.PropertyNames)})")
                            : _.PropertyNames.Single();

                        return new ValidationFailure(
                            this.GetType().ToStringReadable(),
                            Invariant($"{propertyPathTracker.FullPath}{segmentSeparator}{propertyNames}"),
                            _.Message);
                    })
                    .ToList();

                return result;
            }

            public override string ToString()
            {
                var result = Invariant($"K{this.IntProperty}");

                return result;
            }
        }

        // Copy of ValidatableTestObject, but options are not asserted.
        private class ValidatableTestObject2 : IValidatable
        {
            public int IntProperty { get; set; }

            public IReadOnlyList<SelfValidationFailure> GetSelfValidationFailures()
            {
                var result = new List<SelfValidationFailure>();

                if (this.IntProperty < 0)
                {
                    result.Add(new SelfValidationFailure(
                        nameof(this.IntProperty),
                        Invariant($"{nameof(this.IntProperty)} < 0.  It is {this.IntProperty}.")));
                }

                return result;
            }

            public IReadOnlyList<ValidationFailure> GetValidationFailures(
                ValidationOptions options = null,
                PropertyPathTracker propertyPathTracker = null)
            {
                propertyPathTracker = propertyPathTracker ?? new PropertyPathTracker();

                var segmentSeparator = propertyPathTracker.HasSegments ? propertyPathTracker.SegmentSeparator : string.Empty;

                var result = (this.GetSelfValidationFailures() ?? new List<SelfValidationFailure>())
                    .Where(_ => _ != null)
                    .Select(_ =>
                    {
                        var propertyNames = _.PropertyNames.Count > 1
                            ? Invariant($"({string.Join("|", _.PropertyNames)})")
                            : _.PropertyNames.Single();

                        return new ValidationFailure(
                            this.GetType().ToStringReadable(),
                            Invariant($"{propertyPathTracker.FullPath}{segmentSeparator}{propertyNames}"),
                            _.Message);
                    })
                    .ToList();

                return result;
            }

            public override string ToString()
            {
                var result = Invariant($"K{this.IntProperty}");

                return result;
            }
        }
    }
}
