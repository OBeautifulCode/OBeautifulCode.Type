// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    using static System.FormattableString;

    using TypeExtensions = OBeautifulCode.Type.Recipes.TypeExtensions;

    public static class TypeExtensionsTest
    {
        private const string MsCorLibNameAndVersion = "mscorlib (4.0.0.0)";

        private static readonly string ThisAssemblyNameAndVersion = "OBeautifulCode.Type.Recipes.Test" + " (" + Assembly.GetExecutingAssembly().GetName().Version + ")";

        [Fact]
        public static void TestTypes_ClosedTypes___Should_all_be_closed()
        {
            // Arrange
            var types = TestTypes.ClosedTypes;

            // Act
            var actual = types.Select(_ => _.ContainsGenericParameters).ToList();

            // Assert
            actual.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void TestTypes_OpenTypesWithoutGenericTypeDefinitionTypes___Should_all_be_open_but_not_generic_type_definitions()
        {
            // Arrange
            var types = TestTypes.OpenTypesWithoutGenericTypeDefinitionTypes;

            // Act
            var actual1 = types.Select(_ => _.ContainsGenericParameters).ToList();
            var actual2 = types.Select(_ => _.IsGenericTypeDefinition).ToList();

            // Assert
            actual1.Should().AllBeEquivalentTo(true);
            actual2.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void TestTypes_OpenTypes___Should_all_be_open()
        {
            // Arrange
            var types = TestTypes.OpenTypes;

            // Act
            var actual = types.Select(_ => _.ContainsGenericParameters).ToList();

            // Assert
            actual.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void TestTypes_GenericTypeDefinitions___Should_all_be_generic_type_definitions()
        {
            // Arrange
            var types = TestTypes.GenericTypeDefinitions;

            // Act
            var actual = types.Select(_ => _.IsGenericTypeDefinition).ToList();

            // Assert
            actual.Should().AllBeEquivalentTo(true);
        }
        [Fact]
        public static void GetEnumerableElementType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.GetEnumerableElementType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void GetEnumerableElementType___Should_throw_ArgumentException___When_parameter_type_is_not_assignable_to_IEnumerable()
        {
            // Arrange, Act
            var actual = Record.Exception(() => typeof(TestClass).GetEnumerableElementType());

            // Assert
            actual.Should().BeOfType<ArgumentException>();
            actual.Message.Should().Contain("Specified type is not assignable to IEnumerable: TestClass.");
        }

        [Fact]
        public static void GetEnumerableElementType___Should_return_element_type___When_called()
        {
            // Arrange
            var typesAndExpected = new[]
            {
                new { Type = typeof(IEnumerable), Expected = typeof(object) },
                new { Type = typeof(IEnumerable<string>), Expected = typeof(string) },
                new { Type = typeof(Collection<Guid>), Expected = typeof(Guid) },
                new { Type = typeof(ICollection<bool>), Expected = typeof(bool) },
                new { Type = typeof(ReadOnlyCollection<DateTime>), Expected = typeof(DateTime) },
                new { Type = typeof(IReadOnlyCollection<TimeSpan>), Expected = typeof(TimeSpan) },
                new { Type = typeof(List<TestClass>), Expected = typeof(TestClass) },
                new { Type = typeof(IList<int?>), Expected = typeof(int?) },
                new { Type = typeof(IReadOnlyList<int[]>), Expected = typeof(int[]) },

                new { Type = typeof(IDictionary<int, Guid>), Expected = typeof(KeyValuePair<int, Guid>) },
                new { Type = typeof(IReadOnlyDictionary<Guid, string>), Expected = typeof(KeyValuePair<Guid, string>) },
                new { Type = typeof(Dictionary<bool, int?>), Expected = typeof(KeyValuePair<bool, int?>) },
                new { Type = typeof(ReadOnlyDictionary<TestClass, bool?>), Expected = typeof(KeyValuePair<TestClass, bool?>) },
                new { Type = typeof(ConcurrentDictionary<string, DateTime>), Expected = typeof(KeyValuePair<string, DateTime>) },

                new { Type = typeof(BaseClassIList<string>), Expected = typeof(string) },
                new { Type = typeof(DerivedClassIList<DateTime?>), Expected = typeof(DateTime?) },
                new { Type = typeof(GenericClassList<Guid?>), Expected = typeof(Guid?) },
                new { Type = typeof(NonGenericClassCollection), Expected = typeof(string) },
                new { Type = typeof(IGenericIReadOnlyCollection<bool>), Expected = typeof(bool) },
                new { Type = typeof(INonGenericIReadOnlyCollection), Expected = typeof(string) },

                new { Type = typeof(BaseClassIDictionary<DateTime, string>), Expected = typeof(KeyValuePair<DateTime, string>) },
                new { Type = typeof(DerivedClassIDictionary<TestClass, int>), Expected = typeof(KeyValuePair<TestClass, int>) },
                new { Type = typeof(GenericClassDictionary<TimeSpan, bool?>), Expected = typeof(KeyValuePair<TimeSpan, bool?>) },
                new { Type = typeof(NonGenericDictionaryClass), Expected = typeof(KeyValuePair<string, int?>) },
                new { Type = typeof(IGenericIReadOnlyDictionary<string, TestClass>), Expected = typeof(KeyValuePair<string, TestClass>) },
                new { Type = typeof(INonGenericIReadOnlyDictionary), Expected = typeof(KeyValuePair<int, DateTime>) },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.GetEnumerableElementType()).ToList();

            // Assert
            actuals.Should().Equal(typesAndExpected.Select(_ => _.Expected));
        }

        [Fact]
        public static void GetDictionaryValueType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.GetDictionaryValueType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void GetDictionaryValueType___Should_throw_ArgumentException___When_parameter_type_is_not_assignable_to_a_dictionary_type()
        {
            // Arrange, Act
            var actual = Record.Exception(() => typeof(TestClass).GetDictionaryValueType());

            // Assert
            actual.Should().BeOfType<ArgumentException>();
            actual.Message.Should().Contain("Specified type is cannot be assigned to either IReadOnlyDictionary<T,K>, IDictionary<T,K>, or IDictionary: TestClass.");
        }

        [Fact]
        public static void GetDictionaryValueType___Should_return_value_type___When_called()
        {
            // Arrange
            var typesAndExpected = new[]
            {
                new { Type = typeof(IDictionary<int, Guid>), Expected = typeof(Guid) },
                new { Type = typeof(IReadOnlyDictionary<int, int?>), Expected = typeof(int?) },
                new { Type = typeof(Dictionary<int, string>), Expected = typeof(string) },
                new { Type = typeof(ReadOnlyDictionary<int, TestClass>), Expected = typeof(TestClass) },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.GetDictionaryValueType()).ToList();

            // Assert
            actuals.Should().Equal(typesAndExpected.Select(_ => _.Expected));
        }

        [Fact]
        public static void IsAnonymous___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsAnonymous(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsAnonymous___Should_return_true_for_anonymous_type()
        {
            // Arrange, Act, Assert
            new { SomeProp = "prop value" }.GetType().IsAnonymous().Should().BeTrue();
        }

        [Fact]
        public static void IsAnonymous___Should_return_false_for_concrete_type()
        {
            // Arrange, Act, Assert
            "string type".GetType().IsAnonymous().Should().BeFalse();
        }

        [Fact]
        public static void IsAnonymousFastCheck___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsAnonymousFastCheck(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsAnonymousFastCheck___Should_return_true_for_anonymous_type()
        {
            // Arrange, Act, Assert
            new { SomeProp = "prop value" }.GetType().IsAnonymousFastCheck().Should().BeTrue();
        }

        [Fact]
        public static void IsAnonymousFastCheck___Should_return_false_for_concrete_type()
        {
            // Arrange, Act, Assert
            "string type".GetType().IsAnonymousFastCheck().Should().BeFalse();
        }

        [Fact]
        public static void IsAssignableTo___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsAssignableTo(null, A.Dummy<Type>()));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsAssignableTo___Should_throw_ArgumentNullException___When_parameter_otherType_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => A.Dummy<Type>().IsAssignableTo(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("otherType");
        }

        [Fact]
        public static void IsAssignableTo___Should_throw_ArgumentException___When_parameter_type_is_an_unbound_generic_type()
        {
            // Arrange, Act
            var actual1 = Record.Exception(() => typeof(IEnumerable<>).IsAssignableTo(A.Dummy<Type>()));
            var actual2 = Record.Exception(() => typeof(List<>).IsAssignableTo(A.Dummy<Type>()));

            // Assert
            actual1.Should().BeOfType<ArgumentException>();
            actual1.Message.Should().Contain("type.IsGenericTypeDefinition");

            actual2.Should().BeOfType<ArgumentException>();
            actual2.Message.Should().Contain("type.IsGenericTypeDefinition");
        }

        [Fact]
        public static void IsAssignableTo___Should_return_true___When_type_is_equal_to_otherType()
        {
            // Arrange, Act
            var actual1 = typeof(string).IsAssignableTo(typeof(string));
            var actual2 = typeof(List<string>).IsAssignableTo(typeof(List<string>));
            var actual3 = typeof(IEnumerable<string>).IsAssignableTo(typeof(IEnumerable<string>));

            // Assert
            actual1.Should().BeTrue();
            actual2.Should().BeTrue();
            actual3.Should().BeTrue();
        }

        [Fact]
        public static void IsAssignableTo___Should_return_true___When_IsAssignableFrom_returns_true()
        {
            // Arrange, Act
            var actual1 = typeof(string).IsAssignableTo(typeof(object));
            var actual2 = typeof(List<string>).IsAssignableTo(typeof(IList));
            var actual3 = typeof(List<string>).IsAssignableTo(typeof(IList<string>));

            // Assert
            actual1.Should().BeTrue();
            actual2.Should().BeTrue();
            actual3.Should().BeTrue();
        }

        [Fact]
        public static void IsAssignableTo___Should_return_false___When_IsAssignableFrom_returns_false_and_otherType_is_not_unbound_generic_and_treatUnboundGenericAsAssignableTo_is_false()
        {
            // Arrange, Act
            var actual1 = typeof(List<string>).IsAssignableTo(typeof(List<object>));
            var actual2 = typeof(List<string>).IsAssignableTo(typeof(IList<object>));
            var actual3 = typeof(object).IsAssignableTo(typeof(string));
            var actual4 = typeof(BaseClassIList<string>).IsAssignableTo(typeof(DerivedClassIList<string>));
            var actual5 = typeof(IList<string>).IsAssignableTo(typeof(BaseClassIList<string>));

            // Assert
            actual1.Should().BeFalse();
            actual2.Should().BeFalse();
            actual3.Should().BeFalse();
            actual4.Should().BeFalse();
            actual5.Should().BeFalse();
        }

        [Fact]
        public static void IsAssignableTo___Should_return_false___When_IsAssignableFrom_returns_false_and_otherType_is_not_unbound_generic_and_treatUnboundGenericAsAssignableTo_is_true()
        {
            // Arrange, Act
            var actual1 = typeof(List<string>).IsAssignableTo(typeof(List<object>), treatUnboundGenericAsAssignableTo: true);
            var actual2 = typeof(List<string>).IsAssignableTo(typeof(IList<object>), treatUnboundGenericAsAssignableTo: true);
            var actual3 = typeof(object).IsAssignableTo(typeof(string), treatUnboundGenericAsAssignableTo: true);
            var actual4 = typeof(BaseClassIList<string>).IsAssignableTo(typeof(DerivedClassIList<string>), treatUnboundGenericAsAssignableTo: true);
            var actual5 = typeof(IList<string>).IsAssignableTo(typeof(BaseClassIList<string>), treatUnboundGenericAsAssignableTo: true);

            // Assert
            actual1.Should().BeFalse();
            actual2.Should().BeFalse();
            actual3.Should().BeFalse();
            actual4.Should().BeFalse();
            actual5.Should().BeFalse();
        }

        [Fact]
        public static void IsAssignableTo___Should_return_true___When_type_GenericTypeDefinition_is_equal_to_otherType_and_treatUnboundGenericAsAssignableTo_is_true()
        {
            // Arrange, Act
            var actual1 = typeof(List<string>).IsAssignableTo(typeof(List<>), treatUnboundGenericAsAssignableTo: true);
            var actual2 = typeof(IList<string>).IsAssignableTo(typeof(IList<>), treatUnboundGenericAsAssignableTo: true);
            var actual3 = typeof(int?).IsAssignableTo(typeof(Nullable<>), treatUnboundGenericAsAssignableTo: true);

            // Assert
            actual1.Should().BeTrue();
            actual2.Should().BeTrue();
            actual3.Should().BeTrue();
        }

        [Fact]
        public static void IsAssignableTo___Should_return_false___When_type_GenericTypeDefinition_is_equal_to_otherType_and_treatUnboundGenericAsAssignableTo_is_false()
        {
            // Arrange, Act
            var actual1 = typeof(List<string>).IsAssignableTo(typeof(List<>));
            var actual2 = typeof(IList<string>).IsAssignableTo(typeof(IList<>));
            var actual3 = typeof(int?).IsAssignableTo(typeof(Nullable<>));

            // Assert
            actual1.Should().BeFalse();
            actual2.Should().BeFalse();
            actual3.Should().BeFalse();
        }

        [Fact]
        public static void IsAssignableTo___Should_return_true___When_type_implements_or_inherits_an_interface_whose_generic_type_definition_equals_otherType_and_treatUnboundGenericAsAssignableTo_is_true()
        {
            // Arrange, Act
            var actual1 = typeof(List<string>).IsAssignableTo(typeof(IList<>), treatUnboundGenericAsAssignableTo: true);
            var actual2 = typeof(List<string>).IsAssignableTo(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true);
            var actual3 = typeof(IList<string>).IsAssignableTo(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true);
            var actual4 = typeof(DerivedClassIList<string>).IsAssignableTo(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true);
            var actual5 = typeof(DerivedClassIList<string>).IsAssignableTo(typeof(BaseClassIList<>), treatUnboundGenericAsAssignableTo: true);

            // Assert
            actual1.Should().BeTrue();
            actual2.Should().BeTrue();
            actual3.Should().BeTrue();
            actual4.Should().BeTrue();
            actual5.Should().BeTrue();
        }

        [Fact]
        public static void IsAssignableTo___Should_return_false___When_type_implements_or_inherits_an_interface_whose_generic_type_definition_equals_otherType_and_treatUnboundGenericAsAssignableTo_is_false()
        {
            // Arrange, Act
            var actual1 = typeof(List<string>).IsAssignableTo(typeof(IList<>));
            var actual2 = typeof(List<string>).IsAssignableTo(typeof(IEnumerable<>));
            var actual3 = typeof(IList<string>).IsAssignableTo(typeof(IEnumerable<>));
            var actual4 = typeof(DerivedClassIList<string>).IsAssignableTo(typeof(IEnumerable<>));
            var actual5 = typeof(DerivedClassIList<string>).IsAssignableTo(typeof(BaseClassIList<>));

            // Assert
            actual1.Should().BeFalse();
            actual2.Should().BeFalse();
            actual3.Should().BeFalse();
            actual4.Should().BeFalse();
            actual5.Should().BeFalse();
        }

        [Fact]
        public static void IsAssignableTo___Should_return_false___When_type_does_not_implement_nor_inherit_an_interface_whose_generic_type_definition_equals_otherType_and_treatUnboundGenericAsAssignableTo_is_true()
        {
            // Arrange, Act
            var actual1 = typeof(IList<string>).IsAssignableTo(typeof(List<>), treatUnboundGenericAsAssignableTo: true);
            var actual2 = typeof(IEnumerable<string>).IsAssignableTo(typeof(IList<>), treatUnboundGenericAsAssignableTo: true);
            var actual3 = typeof(IEnumerable<string>).IsAssignableTo(typeof(List<>), treatUnboundGenericAsAssignableTo: true);
            var actual4 = typeof(BaseClassIList<string>).IsAssignableTo(typeof(DerivedClassIList<>), treatUnboundGenericAsAssignableTo: true);
            var actual5 = typeof(IList<string>).IsAssignableTo(typeof(BaseClassIList<>), treatUnboundGenericAsAssignableTo: true);

            // Assert
            actual1.Should().BeFalse();
            actual2.Should().BeFalse();
            actual3.Should().BeFalse();
            actual4.Should().BeFalse();
            actual5.Should().BeFalse();
        }

        [Fact]
        public static void IsAssignableTo___Should_return_true___When_type_BaseType_implements_or_inherits_an_interface_whose_generic_type_definition_equals_otherType_and_treatUnboundGenericAsAssignableTo_is_true()
        {
            // Arrange, Act
            var actual = typeof(GenericClassList<string>).IsAssignableTo(typeof(List<>), treatUnboundGenericAsAssignableTo: true);

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public static void IsAssignableTo___Should_return_false___When_type_BaseType_implements_or_inherits_an_interface_whose_generic_type_definition_equals_otherType_and_treatUnboundGenericAsAssignableTo_is_false()
        {
            // Arrange, Act
            var actual = typeof(GenericClassList<string>).IsAssignableTo(typeof(List<>));

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public static void IsAssignableToNull___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsAssignableToNull(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsAssignableToNull___Should_return_false___When_parameter_type_is_not_assignable_to_null()
        {
            // Arrange
            var types = new[]
            {
                typeof(int),
                typeof(Guid),
                typeof(bool),
                typeof(DateTime),
            };

            // Act
            var actuals = types.Select(_ => _.IsAssignableToNull()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsAssignableToNull___Should_return_true___When_parameter_type_is_assignable_to_null()
        {
            // Arrange
            var types = new[]
            {
                typeof(int?),
                typeof(Guid?),
                typeof(bool?),
                typeof(DateTime?),
                typeof(string),
                typeof(List<string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsAssignableToNull()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsComparableType_type___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsComparableType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsComparableType_T___Should_return_false___When_parameter_type_is_not_comparable()
        {
            // Arrange, Act
            var actuals = new[]
            {
                TypeExtensions.IsComparableType<NonComparableClass>(),
            };

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsComparableType_T___Should_return_true___When_parameter_type_is_comparable()
        {
            // Arrange, Act
            var actuals = new[]
            {
                TypeExtensions.IsComparableType<int>(),
                TypeExtensions.IsComparableType<Guid>(),
                TypeExtensions.IsComparableType<bool>(),
                TypeExtensions.IsComparableType<DateTime>(),
                TypeExtensions.IsComparableType<int?>(),
                TypeExtensions.IsComparableType<Guid?>(),
                TypeExtensions.IsComparableType<bool?>(),
                TypeExtensions.IsComparableType<DateTime?>(),
                TypeExtensions.IsComparableType<string>(),
                TypeExtensions.IsComparableType<DayOfWeek>(),
                TypeExtensions.IsComparableType<DayOfWeek?>(),
                TypeExtensions.IsComparableType<ComparableClass>(),
            };

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsComparableType_type___Should_return_false___When_parameter_type_is_not_comparable()
        {
            // Arrange
            var types = new[]
            {
                typeof(NonComparableClass),
            };

            // Act
            var actuals = types.Select(_ => _.IsComparableType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsComparableType_type___Should_return_true___When_parameter_type_is_comparable()
        {
            // Arrange
            var types = new[]
            {
                typeof(int),
                typeof(Guid),
                typeof(bool),
                typeof(DateTime),
                typeof(int?),
                typeof(Guid?),
                typeof(bool?),
                typeof(DateTime?),
                typeof(string),
                typeof(DayOfWeek),
                typeof(DayOfWeek?),
                typeof(ComparableClass),
            };

            // Act
            var actuals = types.Select(_ => _.IsComparableType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsNonAnonymousClosedClassType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsNonAnonymousClosedClassType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsNonAnonymousClosedClassType___Should_return_false___When_parameter_type_is_an_interface_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(IList),
                typeof(IList<string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsNonAnonymousClosedClassType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsNonAnonymousClosedClassType___Should_return_false___When_parameter_type_is_an_open_generic_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(List<>),
                typeof(Dictionary<,>),
            };

            // Act
            var actuals = types.Select(_ => _.IsNonAnonymousClosedClassType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsNonAnonymousClosedClassType___Should_return_false___When_parameter_type_is_an_anonymous_type()
        {
            // Arrange
            var types = new[]
            {
                new { test = "test" }.GetType(),
            };

            // Act
            var actuals = types.Select(_ => _.IsNonAnonymousClosedClassType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsNonAnonymousClosedClassType___Should_return_true___When_parameter_type_is_a_not_anonymous_closed_class()
        {
            // Arrange
            var types = new[]
            {
                typeof(List<string>),
                typeof(Dictionary<string, string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsNonAnonymousClosedClassType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsNullableType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsNullableType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsNullableType___Should_return_false___When_parameter_type_is_not_Nullable()
        {
            // Arrange
            var types = new[]
            {
                typeof(string),
                typeof(int),
                typeof(Guid),
                typeof(bool),
                typeof(BaseClassIList<string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsNullableType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsNullableType___Should_return_true___When_parameter_type_is_Nullable()
        {
            // Arrange
            var types = new[]
            {
                typeof(int?),
                typeof(Guid?),
                typeof(bool?),
            };

            // Act
            var actuals = types.Select(_ => _.IsNullableType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsSystemCollectionType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsSystemCollectionType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsSystemCollectionType___Should_return_false___When_parameter_type_is_not_a_System_collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(Guid),
                typeof(Guid?),
                typeof(string),
                typeof(int),
                typeof(NonGenericClassCollection),
                typeof(KeyValuePair<,>),
                typeof(KeyValuePair<string, string>),
                typeof(IReadOnlyDictionary<string, string>),
                typeof(Dictionary<string, string>),
                typeof(bool[]),
                typeof(string[]),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsSystemCollectionType___Should_return_true___When_parameter_type_is_a_System_collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(Collection<>),
                typeof(ICollection<>),
                typeof(ReadOnlyCollection<>),
                typeof(IReadOnlyCollection<>),
                typeof(List<>),
                typeof(IList<>),
                typeof(IReadOnlyList<>),
                typeof(Collection<string>),
                typeof(ICollection<string>),
                typeof(ReadOnlyCollection<string>),
                typeof(IReadOnlyCollection<string>),
                typeof(List<string>),
                typeof(IList<string>),
                typeof(IReadOnlyList<string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsSystemDictionaryType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsSystemDictionaryType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsSystemDictionaryType___Should_return_false___When_parameter_type_is_not_a_System_dictionary_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(Guid),
                typeof(Guid?),
                typeof(string),
                typeof(int),
                typeof(NonGenericDictionaryClass),
                typeof(KeyValuePair<,>),
                typeof(KeyValuePair<string, string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemDictionaryType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsSystemDictionaryType___Should_return_true___When_parameter_type_is_a_System_dictionary_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(Dictionary<,>),
                typeof(IDictionary<,>),
                typeof(ReadOnlyDictionary<,>),
                typeof(IReadOnlyDictionary<,>),
                typeof(ConcurrentDictionary<,>),
                typeof(Dictionary<string, string>),
                typeof(IDictionary<string, string>),
                typeof(ReadOnlyDictionary<string, string>),
                typeof(IReadOnlyDictionary<string, string>),
                typeof(ConcurrentDictionary<string, string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemDictionaryType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsSystemOrderedCollectionType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsSystemOrderedCollectionType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsSystemOrderedCollectionType___Should_return_false___When_parameter_type_is_not_a_System_ordered_collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(Guid),
                typeof(Guid?),
                typeof(string),
                typeof(int),
                typeof(NonGenericClassCollection),
                typeof(KeyValuePair<,>),
                typeof(KeyValuePair<string, string>),
                typeof(IReadOnlyDictionary<string, string>),
                typeof(Dictionary<string, string>),
                typeof(ICollection<>),
                typeof(ICollection<string>),
                typeof(IReadOnlyCollection<>),
                typeof(IReadOnlyCollection<string>),
                typeof(bool[]),
                typeof(string[]),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemOrderedCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsSystemOrderedCollectionType___Should_return_true___When_parameter_type_is_a_System_ordered_collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(Collection<>),
                typeof(ReadOnlyCollection<>),
                typeof(List<>),
                typeof(IList<>),
                typeof(IReadOnlyList<>),
                typeof(Collection<string>),
                typeof(ReadOnlyCollection<string>),
                typeof(List<string>),
                typeof(IList<string>),
                typeof(IReadOnlyList<string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemOrderedCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsSystemUnorderedCollectionType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsSystemUnorderedCollectionType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsSystemUnorderedCollectionType___Should_return_false___When_parameter_type_is_not_a_System_unordered_collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(Guid),
                typeof(Guid?),
                typeof(string),
                typeof(int),
                typeof(NonGenericClassCollection),
                typeof(KeyValuePair<,>),
                typeof(KeyValuePair<string, string>),
                typeof(IReadOnlyDictionary<string, string>),
                typeof(Dictionary<string, string>),
                typeof(bool[]),
                typeof(string[]),
                typeof(Collection<>),
                typeof(ReadOnlyCollection<>),
                typeof(List<>),
                typeof(IList<>),
                typeof(IReadOnlyList<>),
                typeof(Collection<string>),
                typeof(ReadOnlyCollection<string>),
                typeof(List<string>),
                typeof(IList<string>),
                typeof(IReadOnlyList<string>),
                typeof(INonGenericIReadOnlyCollection),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemUnorderedCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsSystemUnorderedCollectionType___Should_return_true___When_parameter_type_is_a_System_unordered_collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(ICollection<>),
                typeof(ICollection<string>),
                typeof(IReadOnlyCollection<>),
                typeof(IReadOnlyCollection<string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemUnorderedCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void ToStringCompilable___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.ToStringCompilable(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void ToStringCompilable___Should_throw_NotSupportedException___When_parameter_throwIfNoCompilableStringExists_is_true_and_parameter_type_is_an_anonymous_type()
        {
            // Arrange
            var types = new[]
            {
                new { Anonymous = true }.GetType(),
            };

            // Act
            var actuals = types.Select(_ => Record.Exception(() => _.ToStringCompilable(throwIfNoCompilableStringExists: true))).ToList();

            // Assert
            actuals.Should().AllBeOfType<NotSupportedException>();
            actuals.Select(_ => _.Message.Should().Be("Anonymous types are not supported.")).ToList();
        }

        [Fact]
        public static void ToStringCompilable___Should_return_null___When_parameter_throwIfNoCompilableStringExists_is_false_and_parameter_type_is_an_anonymous_type()
        {
            // Arrange
            var types = new[]
            {
                new { Anonymous = true }.GetType(),
            };

            // Act
            var actuals = types.Select(_ => Record.Exception(() => _.ToStringCompilable(throwIfNoCompilableStringExists: false))).ToList();

            // Assert
            actuals.Select(_ => _.Should().BeNull()).ToList();
        }

        [Fact]
        public static void ToStringCompilable___Should_throw_NotSupportedException___When_parameter_throwIfNoCompilableStringExists_is_true_and_parameter_type_is_a_generic_open_constructed_type()
        {
            // Arrange
            var types = new[]
            {
                // IsGenericType: True
                // IsGenericTypeDefinition: False
                // ContainsGenericParameters: True
                // IsGenericParameter: False
                typeof(DerivedGenericClass<>).BaseType,

                // IsGenericType: True
                // IsGenericTypeDefinition: False
                // ContainsGenericParameters: True
                // IsGenericParameter: False
                typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType,
            };

            // Act
            var actuals = types.Select(_ => Record.Exception(() => _.ToStringCompilable(throwIfNoCompilableStringExists: true))).ToList();

            // Assert
            actuals.Should().AllBeOfType<NotSupportedException>();
            actuals.Select(_ => _.Message.Should().Be("Generic open constructed types are not supported.")).ToList();
        }

        [Fact]
        public static void ToStringCompilable___Should_return_null___When_parameter_throwIfNoCompilableStringExists_is_false_and_parameter_type_is_a_generic_open_constructed_type()
        {
            // Arrange
            var types = new[]
            {
                // IsGenericType: True
                // IsGenericTypeDefinition: False
                // ContainsGenericParameters: True
                // IsGenericParameter: False
                typeof(DerivedGenericClass<>).BaseType,

                // IsGenericType: True
                // IsGenericTypeDefinition: False
                // ContainsGenericParameters: True
                // IsGenericParameter: False
                typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType,
            };

            // Act
            var actuals = types.Select(_ => Record.Exception(() => _.ToStringCompilable(throwIfNoCompilableStringExists: false))).ToList();

            // Assert
            actuals.Select(_ => _.Should().BeNull()).ToList();
        }

        [Fact]
        public static void ToStringCompilable___Should_throw_NotSupportedException___When_parameter_throwIfNoCompilableStringExists_is_true_and_type_is_a_generic_parameter()
        {
            // Arrange
            var types = new[]
            {
                // IsGenericType: False
                // IsGenericTypeDefinition: False
                // ContainsGenericParameters: True
                // IsGenericParameter: True
                typeof(BaseGenericClass<,>).GetGenericArguments()[0],
            };

            // Act
            var actuals = types.Select(_ => Record.Exception(() => _.ToStringCompilable(throwIfNoCompilableStringExists: true))).ToList();

            // Assert
            actuals.Should().AllBeOfType<NotSupportedException>();
            actuals.Select(_ => _.Message.Should().Be("Generic parameters not supported.")).ToList();
        }

        [Fact]
        public static void ToStringCompilable___Should_return_null___When_parameter_throwIfNoCompilableStringExists_is_false_and_type_is_a_generic_parameter()
        {
            // Arrange
            var types = new[]
            {
                // IsGenericType: False
                // IsGenericTypeDefinition: False
                // ContainsGenericParameters: True
                // IsGenericParameter: True
                typeof(BaseGenericClass<,>).GetGenericArguments()[0],
            };

            // Act
            // ReSharper disable once ConvertClosureToMethodGroup
            var actuals = types.Select(_ => Record.Exception(() => _.ToStringCompilable(throwIfNoCompilableStringExists: false))).ToList();

            // Assert
            actuals.Select(_ => _.Should().BeNull()).ToList();
        }

        [Fact]
        public static void ToStringCompilable___Should_return_compilable_string_representation_of_the_specified_type___When_called()
        {
            // Arrange
            var typesAndExpected = new[]
            {
                new { Type = typeof(DerivedGenericClass<>), Expected = "DerivedGenericClass<>" },
                new { Type = new DerivedGenericClass<int>[0].GetType(), Expected = "DerivedGenericClass<int>[]" },
                new { Type = typeof(DerivedGenericClass<>.NestedInDerivedGeneric), Expected = "DerivedGenericClass<>.NestedInDerivedGeneric" },
                new { Type = typeof(string), Expected = "string" },
                new { Type = typeof(int), Expected = "int" },
                new { Type = typeof(int?), Expected = "int?" },
                new { Type = typeof(Guid), Expected = "Guid" },
                new { Type = typeof(Guid?), Expected = "Guid?" },
                new { Type = typeof(TestClass), Expected = "TestClass" },
                new { Type = typeof(TestClassWithNestedClass.NestedInTestClass), Expected = "TestClassWithNestedClass.NestedInTestClass" },
                new { Type = typeof(IReadOnlyDictionary<string, int?>), Expected = "IReadOnlyDictionary<string, int?>" },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = "IReadOnlyDictionary<string, Guid?>" },
                new { Type = typeof(string[]), Expected = "string[]" },
                new { Type = typeof(int?[]), Expected = "int?[]" },
                new { Type = typeof(TestClass[]), Expected = "TestClass[]" },
                new { Type = typeof(Guid?[]), Expected = "Guid?[]" },
                new { Type = typeof(IList<int?[]>), Expected = "IList<int?[]>" },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool?>[]), Expected = "IReadOnlyDictionary<TestClass, bool?>[]" },
                new { Type = typeof(IReadOnlyDictionary<bool[], TestClass>), Expected = "IReadOnlyDictionary<bool[], TestClass>" },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool[]>), Expected = "IReadOnlyDictionary<TestClass, bool[]>" },
                new { Type = typeof(IList<>), Expected = "IList<>" },
                new { Type = typeof(List<>), Expected = "List<>" },
                new { Type = typeof(IReadOnlyDictionary<,>), Expected = "IReadOnlyDictionary<,>" },
                new { Type = typeof(IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]>), Expected = "IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]>" },
                new { Type = (first: "one", second: 10).GetType(), Expected = "ValueTuple<string, int>" },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.ToStringCompilable()).ToList();

            // Assert
            typesAndExpected.Select(_ => _.Expected).Should().Equal(actuals);
        }

        [Fact]
        public static void ToStringReadable___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.ToStringReadable(null, A.Dummy<ToStringReadableOptions>()));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void ToStringReadable___Should_return_readable_string_representation_of_the_specified_type___When_parameter_options_is_None()
        {
            // Arrange
            var innerAnonymousObject = new { InnerAnonymous = 6 };
            var innerAnonymousTypeName = new Regex("AnonymousType\\d*").Match(innerAnonymousObject.GetType().Name);

            var anonymousObject = new { Anonymous = true, Inner = innerAnonymousObject };
            var anonymousTypeName = new Regex("AnonymousType\\d*").Match(anonymousObject.GetType().Name);

            var typesAndExpected = new[]
            {
                // value tuple:
                new { Type = (first: "one", second: 7).GetType(), Expected = "ValueTuple<string, int>" },

                // anonymous type:
                new { Type = anonymousObject.GetType(), Expected = Invariant($"{anonymousTypeName}<bool, {innerAnonymousTypeName}<int>>") },

                // anonymous type generic type definition:
                new { Type = anonymousObject.GetType().GetGenericTypeDefinition(), Expected = Invariant($"{anonymousTypeName}<T1, T2>") },

                // generic open constructed types:
                new { Type = typeof(DerivedGenericClass<>).BaseType, Expected = "BaseGenericClass<string, TDerived>" },
                new { Type = typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType, Expected = "OrphanedGenericClass<DerivedGenericClass<TDerived>>" },

                // generic parameter:
                new { Type = typeof(BaseGenericClass<,>).GetGenericArguments()[0], Expected = "TBase1" },

                // generic type definitions:
                new { Type = typeof(IList<>), Expected = "IList<T>" },
                new { Type = typeof(List<>), Expected = "List<T>" },
                new { Type = typeof(IReadOnlyDictionary<,>), Expected = "IReadOnlyDictionary<TKey, TValue>" },
                new { Type = typeof(DerivedGenericClass<>), Expected = "DerivedGenericClass<TDerived>" },

                // other types
                new { Type = new DerivedGenericClass<int>[0].GetType(), Expected = "DerivedGenericClass<int>[]" },
                new { Type = typeof(DerivedGenericClass<>.NestedInDerivedGeneric), Expected = "DerivedGenericClass<TDerived>.NestedInDerivedGeneric" },
                new { Type = typeof(string), Expected = "string" },
                new { Type = typeof(int), Expected = "int" },
                new { Type = typeof(int?), Expected = "int?" },
                new { Type = typeof(Guid), Expected = "Guid" },
                new { Type = typeof(Guid?), Expected = "Guid?" },
                new { Type = typeof(TestClass), Expected = "TestClass" },
                new { Type = typeof(TestClassWithNestedClass.NestedInTestClass), Expected = "TestClassWithNestedClass.NestedInTestClass" },
                new { Type = typeof(IReadOnlyDictionary<string, int?>), Expected = "IReadOnlyDictionary<string, int?>" },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = "IReadOnlyDictionary<string, Guid?>" },
                new { Type = typeof(string[]), Expected = "string[]" },
                new { Type = typeof(int?[]), Expected = "int?[]" },
                new { Type = typeof(TestClass[]), Expected = "TestClass[]" },
                new { Type = typeof(Guid?[]), Expected = "Guid?[]" },
                new { Type = typeof(IList<int?[]>), Expected = "IList<int?[]>" },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool?>[]), Expected = "IReadOnlyDictionary<TestClass, bool?>[]" },
                new { Type = typeof(IReadOnlyDictionary<bool[], TestClass>), Expected = "IReadOnlyDictionary<bool[], TestClass>" },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool[]>), Expected = "IReadOnlyDictionary<TestClass, bool[]>" },
                new { Type = typeof(IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]>), Expected = "IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]>" },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.ToStringReadable(ToStringReadableOptions.None)).ToList();

            // Assert
            typesAndExpected.Select(_ => _.Expected).Should().Equal(actuals);
        }

        [Fact]
        public static void ToStringReadable___Should_return_readable_string_representation_of_the_specified_type_with_namespaces_included___When_parameter_options_is_IncludeNamespace()
        {
            // Arrange
            var innerAnonymousObject = new { InnerAnonymous = 6 };
            var innerAnonymousTypeName = new Regex("AnonymousType\\d*").Match(innerAnonymousObject.GetType().Name);

            var anonymousObject = new { Anonymous = true, Inner = innerAnonymousObject };
            var anonymousTypeName = new Regex("AnonymousType\\d*").Match(anonymousObject.GetType().Name);

            var typesAndExpected = new[]
            {
                // value tuple:
                new { Type = (first: "one", second: 7).GetType(), Expected = "System.ValueTuple<string, int>" },

                // anonymous type:
                new { Type = anonymousObject.GetType(), Expected = anonymousTypeName + "<bool, " + innerAnonymousTypeName + "<int>>" },

                // anonymous type generic type definition:
                new { Type = anonymousObject.GetType().GetGenericTypeDefinition(), Expected = Invariant($"{anonymousTypeName}<T1, T2>") },

                // generic open constructed types:
                new { Type = typeof(DerivedGenericClass<>).BaseType, Expected = "OBeautifulCode.Type.Recipes.Test.BaseGenericClass<string, TDerived>" },
                new { Type = typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType, Expected = "OBeautifulCode.Type.Recipes.Test.OrphanedGenericClass<OBeautifulCode.Type.Recipes.Test.DerivedGenericClass<TDerived>>" },

                // generic parameter:
                new { Type = typeof(BaseGenericClass<,>).GetGenericArguments()[0], Expected = "TBase1" },

                // generic type definitions:
                new { Type = typeof(IList<>), Expected = "System.Collections.Generic.IList<T>" },
                new { Type = typeof(List<>), Expected = "System.Collections.Generic.List<T>" },
                new { Type = typeof(IReadOnlyDictionary<,>), Expected = "System.Collections.Generic.IReadOnlyDictionary<TKey, TValue>" },
                new { Type = typeof(DerivedGenericClass<>), Expected = "OBeautifulCode.Type.Recipes.Test.DerivedGenericClass<TDerived>" },

                // other types
                new { Type = new DerivedGenericClass<int>[0].GetType(), Expected = "OBeautifulCode.Type.Recipes.Test.DerivedGenericClass<int>[]" },
                new { Type = typeof(DerivedGenericClass<>.NestedInDerivedGeneric), Expected = "OBeautifulCode.Type.Recipes.Test.DerivedGenericClass<TDerived>.NestedInDerivedGeneric" },
                new { Type = typeof(string), Expected = "string" },
                new { Type = typeof(int), Expected = "int" },
                new { Type = typeof(int?), Expected = "int?" },
                new { Type = typeof(Guid), Expected = "System.Guid" },
                new { Type = typeof(Guid?), Expected = "System.Guid?" },
                new { Type = typeof(TestClass), Expected = "OBeautifulCode.Type.Recipes.Test.TestClass" },
                new { Type = typeof(TestClassWithNestedClass.NestedInTestClass), Expected = "OBeautifulCode.Type.Recipes.Test.TestClassWithNestedClass.NestedInTestClass" },
                new { Type = typeof(IReadOnlyDictionary<string, int?>), Expected = "System.Collections.Generic.IReadOnlyDictionary<string, int?>" },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = "System.Collections.Generic.IReadOnlyDictionary<string, System.Guid?>" },
                new { Type = typeof(string[]), Expected = "string[]" },
                new { Type = typeof(int?[]), Expected = "int?[]" },
                new { Type = typeof(TestClass[]), Expected = "OBeautifulCode.Type.Recipes.Test.TestClass[]" },
                new { Type = typeof(Guid?[]), Expected = "System.Guid?[]" },
                new { Type = typeof(IList<int?[]>), Expected = "System.Collections.Generic.IList<int?[]>" },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool?>[]), Expected = "System.Collections.Generic.IReadOnlyDictionary<OBeautifulCode.Type.Recipes.Test.TestClass, bool?>[]" },
                new { Type = typeof(IReadOnlyDictionary<bool[], TestClass>), Expected = "System.Collections.Generic.IReadOnlyDictionary<bool[], OBeautifulCode.Type.Recipes.Test.TestClass>" },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool[]>), Expected = "System.Collections.Generic.IReadOnlyDictionary<OBeautifulCode.Type.Recipes.Test.TestClass, bool[]>" },
                new { Type = typeof(IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]>), Expected = "System.Collections.Generic.IReadOnlyDictionary<System.Collections.Generic.IReadOnlyDictionary<System.Guid[], int?>, System.Collections.Generic.IList<System.Collections.Generic.IList<short>>[]>" },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.ToStringReadable(ToStringReadableOptions.IncludeNamespace)).ToList();

            // Assert
            typesAndExpected.Select(_ => _.Expected).Should().Equal(actuals);
        }

        [Fact]
        public static void ToStringReadable___Should_return_readable_string_representation_of_the_specified_type_with_assembly_details_included___When_parameter_options_is_IncludeAssemblyDetails()
        {
            // Arrange
            var innerAnonymousObject = new { InnerAnonymous = 6 };
            var innerAnonymousTypeName = new Regex("AnonymousType\\d*").Match(innerAnonymousObject.GetType().Name);

            var anonymousObject = new { Anonymous = true, Inner = innerAnonymousObject };
            var anonymousTypeName = new Regex("AnonymousType\\d*").Match(anonymousObject.GetType().Name);

            var typesAndExpected = new[]
            {
                // value tuple:
                new { Type = (first: "one", second: 7).GetType(), Expected = Invariant($"ValueTuple<string, int> || System.ValueTuple<T1, T2> => {MsCorLibNameAndVersion} | string => {MsCorLibNameAndVersion} | int => {MsCorLibNameAndVersion}") },

                // anonymous type:
                new { Type = anonymousObject.GetType(), Expected = Invariant($"{anonymousTypeName}<bool, {innerAnonymousTypeName}<int>> || {anonymousTypeName}<T1, T2> => {ThisAssemblyNameAndVersion} | bool => {MsCorLibNameAndVersion} | {innerAnonymousTypeName}<T1> => {ThisAssemblyNameAndVersion} | int => {MsCorLibNameAndVersion}") },

                // anonymous type generic type definition:
                new { Type = anonymousObject.GetType().GetGenericTypeDefinition(), Expected = Invariant($"{anonymousTypeName}<T1, T2> || {anonymousTypeName}<T1, T2> => {ThisAssemblyNameAndVersion}") },

                // generic open constructed types:
                new { Type = typeof(DerivedGenericClass<>).BaseType, Expected = Invariant($"BaseGenericClass<string, TDerived> || OBeautifulCode.Type.Recipes.Test.BaseGenericClass<TBase1, TBase2> => {ThisAssemblyNameAndVersion} | string => {MsCorLibNameAndVersion}") },
                new { Type = typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType, Expected = Invariant($"OrphanedGenericClass<DerivedGenericClass<TDerived>> || OBeautifulCode.Type.Recipes.Test.OrphanedGenericClass<TOrphaned> => {ThisAssemblyNameAndVersion} | OBeautifulCode.Type.Recipes.Test.DerivedGenericClass<TDerived> => {ThisAssemblyNameAndVersion}") },

                // generic parameter:
                new { Type = typeof(BaseGenericClass<,>).GetGenericArguments()[0], Expected = "TBase1" },

                // generic type definitions:
                new { Type = typeof(IList<>), Expected = Invariant($"IList<T> || System.Collections.Generic.IList<T> => {MsCorLibNameAndVersion}") },
                new { Type = typeof(List<>), Expected = Invariant($"List<T> || System.Collections.Generic.List<T> => {MsCorLibNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<,>), Expected = Invariant($"IReadOnlyDictionary<TKey, TValue> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion}") },
                new { Type = typeof(DerivedGenericClass<>), Expected = Invariant($"DerivedGenericClass<TDerived> || OBeautifulCode.Type.Recipes.Test.DerivedGenericClass<TDerived> => {ThisAssemblyNameAndVersion}") },

                // other types
                new { Type = new DerivedGenericClass<int>[0].GetType(), Expected = Invariant($"DerivedGenericClass<int>[] || OBeautifulCode.Type.Recipes.Test.DerivedGenericClass<TDerived> => {ThisAssemblyNameAndVersion} | int => {MsCorLibNameAndVersion}") },
                new { Type = typeof(DerivedGenericClass<>.NestedInDerivedGeneric), Expected = Invariant($"DerivedGenericClass<TDerived>.NestedInDerivedGeneric || OBeautifulCode.Type.Recipes.Test.DerivedGenericClass<TDerived>.NestedInDerivedGeneric => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(string), Expected = Invariant($"string || string => {MsCorLibNameAndVersion}") },
                new { Type = typeof(int), Expected = Invariant($"int || int => {MsCorLibNameAndVersion}") },
                new { Type = typeof(int?), Expected = Invariant($"int? || int => {MsCorLibNameAndVersion}") },
                new { Type = typeof(Guid), Expected = Invariant($"Guid || System.Guid => {MsCorLibNameAndVersion}") },
                new { Type = typeof(Guid?), Expected = Invariant($"Guid? || System.Guid => {MsCorLibNameAndVersion}") },
                new { Type = typeof(TestClass), Expected = Invariant($"TestClass || OBeautifulCode.Type.Recipes.Test.TestClass => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(TestClassWithNestedClass.NestedInTestClass), Expected = Invariant($"TestClassWithNestedClass.NestedInTestClass || OBeautifulCode.Type.Recipes.Test.TestClassWithNestedClass.NestedInTestClass => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<string, int?>), Expected = Invariant($"IReadOnlyDictionary<string, int?> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | string => {MsCorLibNameAndVersion} | int => {MsCorLibNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = Invariant($"IReadOnlyDictionary<string, Guid?> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | string => {MsCorLibNameAndVersion} | System.Guid => {MsCorLibNameAndVersion}") },
                new { Type = typeof(string[]), Expected = Invariant($"string[] || string => {MsCorLibNameAndVersion}") },
                new { Type = typeof(int?[]), Expected = Invariant($"int?[] || int => {MsCorLibNameAndVersion}") },
                new { Type = typeof(TestClass[]), Expected = Invariant($"TestClass[] || OBeautifulCode.Type.Recipes.Test.TestClass => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(Guid?[]), Expected = Invariant($"Guid?[] || System.Guid => {MsCorLibNameAndVersion}") },
                new { Type = typeof(IList<int?[]>), Expected = Invariant($"IList<int?[]> || System.Collections.Generic.IList<T> => {MsCorLibNameAndVersion} | int => {MsCorLibNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool?>[]), Expected = Invariant($"IReadOnlyDictionary<TestClass, bool?>[] || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | OBeautifulCode.Type.Recipes.Test.TestClass => {ThisAssemblyNameAndVersion} | bool => {MsCorLibNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<bool[], TestClass>), Expected = Invariant($"IReadOnlyDictionary<bool[], TestClass> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | bool => {MsCorLibNameAndVersion} | OBeautifulCode.Type.Recipes.Test.TestClass => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool[]>), Expected = Invariant($"IReadOnlyDictionary<TestClass, bool[]> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | OBeautifulCode.Type.Recipes.Test.TestClass => {ThisAssemblyNameAndVersion} | bool => {MsCorLibNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]>), Expected = Invariant($"IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | System.Guid => {MsCorLibNameAndVersion} | int => {MsCorLibNameAndVersion} | System.Collections.Generic.IList<T> => {MsCorLibNameAndVersion} | System.Collections.Generic.IList<T> => {MsCorLibNameAndVersion} | short => {MsCorLibNameAndVersion}") },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.ToStringReadable(ToStringReadableOptions.IncludeAssemblyDetails)).ToList();

            // Assert
            typesAndExpected.Select(_ => _.Expected).Should().Equal(actuals);
        }
    }
}
