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
    using System.Diagnostics.CodeAnalysis;
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
            var actual = Record.Exception(() => typeof(MyNonNestedClass).GetEnumerableElementType());

            // Assert
            actual.Should().BeOfType<ArgumentException>();
            actual.Message.Should().Contain("Specified type is not assignable to IEnumerable: MyNonNestedClass.");
        }

        [Fact]
        public static void GetEnumerableElementType___Should_return_element_type___When_called()
        {
            // Arrange
            var typesAndExpected = new[]
            {
                new { Type = typeof(IEnumerable), Expected = typeof(object) },
                new { Type = typeof(IEnumerable<string>), Expected = typeof(string) },
                new { Type = typeof(List<Guid>), Expected = typeof(Guid) },
                new { Type = typeof(IReadOnlyList<int?>), Expected = typeof(int?) },
                new { Type = typeof(IDictionary<int, Guid>), Expected = typeof(KeyValuePair<int, Guid>) },
                new { Type = typeof(IReadOnlyDictionary<int, Guid>), Expected = typeof(KeyValuePair<int, Guid>) },
                new { Type = typeof(Dictionary<int, Guid>), Expected = typeof(KeyValuePair<int, Guid>) },
                new { Type = typeof(ReadOnlyDictionary<int, Guid>), Expected = typeof(KeyValuePair<int, Guid>) },
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
            var actual = Record.Exception(() => typeof(MyNonNestedClass).GetDictionaryValueType());

            // Assert
            actual.Should().BeOfType<ArgumentException>();
            actual.Message.Should().Contain("Specified type is cannot be assigned to either IReadOnlyDictionary<T,K>, IDictionary<T,K>, or IDictionary: MyNonNestedClass.");
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
                new { Type = typeof(ReadOnlyDictionary<int, MyNonNestedClass>), Expected = typeof(MyNonNestedClass) },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.GetDictionaryValueType()).ToList();

            // Assert
            actuals.Should().Equal(typesAndExpected.Select(_ => _.Expected));
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
            var actual4 = typeof(BaseCollection<string>).IsAssignableTo(typeof(SubclassCollection<string>));
            var actual5 = typeof(IList<string>).IsAssignableTo(typeof(BaseCollection<string>));

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
            var actual4 = typeof(BaseCollection<string>).IsAssignableTo(typeof(SubclassCollection<string>), treatUnboundGenericAsAssignableTo: true);
            var actual5 = typeof(IList<string>).IsAssignableTo(typeof(BaseCollection<string>), treatUnboundGenericAsAssignableTo: true);

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
            var actual4 = typeof(SubclassCollection<string>).IsAssignableTo(typeof(IEnumerable<>), treatUnboundGenericAsAssignableTo: true);
            var actual5 = typeof(SubclassCollection<string>).IsAssignableTo(typeof(BaseCollection<>), treatUnboundGenericAsAssignableTo: true);

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
            var actual4 = typeof(SubclassCollection<string>).IsAssignableTo(typeof(IEnumerable<>));
            var actual5 = typeof(SubclassCollection<string>).IsAssignableTo(typeof(BaseCollection<>));

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
            var actual4 = typeof(BaseCollection<string>).IsAssignableTo(typeof(SubclassCollection<>), treatUnboundGenericAsAssignableTo: true);
            var actual5 = typeof(IList<string>).IsAssignableTo(typeof(BaseCollection<>), treatUnboundGenericAsAssignableTo: true);

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
            var actual = typeof(MyCollection<string>).IsAssignableTo(typeof(List<>), treatUnboundGenericAsAssignableTo: true);

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public static void IsAssignableTo___Should_return_false___When_type_BaseType_implements_or_inherits_an_interface_whose_generic_type_definition_equals_otherType_and_treatUnboundGenericAsAssignableTo_is_false()
        {
            // Arrange, Act
            var actual = typeof(MyCollection<string>).IsAssignableTo(typeof(List<>));

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
                typeof(BaseCollection<string>),
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
                typeof(CollectionDerivative),
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
                typeof(DictionaryDerivative),
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
        public static void IsSystemOrderedCollectionType___Should_return_false___When_parameter_type_is_not_a_System_collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(Guid),
                typeof(Guid?),
                typeof(string),
                typeof(int),
                typeof(CollectionDerivative),
                typeof(KeyValuePair<,>),
                typeof(KeyValuePair<string, string>),
                typeof(IReadOnlyDictionary<string, string>),
                typeof(Dictionary<string, string>),
                typeof(ICollection<>),
                typeof(ICollection<string>),
                typeof(IReadOnlyCollection<>),
                typeof(IReadOnlyCollection<string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemOrderedCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsSystemOrderedCollectionType___Should_return_true___When_parameter_type_is_a_System_collection_type()
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
                typeof(bool[]),
                typeof(string[]),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemOrderedCollectionType()).ToList();

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
                typeof(Derived<>).BaseType,

                // IsGenericType: True
                // IsGenericTypeDefinition: False
                // ContainsGenericParameters: True
                // IsGenericParameter: False
                typeof(Derived<>).GetField("F").FieldType,
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
                typeof(Derived<>).BaseType,

                // IsGenericType: True
                // IsGenericTypeDefinition: False
                // ContainsGenericParameters: True
                // IsGenericParameter: False
                typeof(Derived<>).GetField("F").FieldType,
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
                typeof(Base<,>).GetGenericArguments()[0],
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
                typeof(Base<,>).GetGenericArguments()[0],
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
                new { Type = typeof(Derived<>), Expected = "Derived<>" },
                new { Type = new Derived<int>[0].GetType(), Expected = "Derived<int>[]" },
                new { Type = typeof(Derived<>.Nested), Expected = "Derived<>.Nested" },
                new { Type = typeof(string), Expected = "string" },
                new { Type = typeof(int), Expected = "int" },
                new { Type = typeof(int?), Expected = "int?" },
                new { Type = typeof(Guid), Expected = "Guid" },
                new { Type = typeof(Guid?), Expected = "Guid?" },
                new { Type = typeof(MyNonNestedClass), Expected = "MyNonNestedClass" },
                new { Type = typeof(MyNestedClass), Expected = "TypeExtensionsTest.MyNestedClass" },
                new { Type = typeof(IReadOnlyDictionary<string, int?>), Expected = "IReadOnlyDictionary<string, int?>" },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = "IReadOnlyDictionary<string, Guid?>" },
                new { Type = typeof(string[]), Expected = "string[]" },
                new { Type = typeof(int?[]), Expected = "int?[]" },
                new { Type = typeof(MyNonNestedClass[]), Expected = "MyNonNestedClass[]" },
                new { Type = typeof(Guid?[]), Expected = "Guid?[]" },
                new { Type = typeof(IList<int?[]>), Expected = "IList<int?[]>" },
                new { Type = typeof(IReadOnlyDictionary<MyNonNestedClass, bool?>[]), Expected = "IReadOnlyDictionary<MyNonNestedClass, bool?>[]" },
                new { Type = typeof(IReadOnlyDictionary<bool[], MyNonNestedClass>), Expected = "IReadOnlyDictionary<bool[], MyNonNestedClass>" },
                new { Type = typeof(IReadOnlyDictionary<MyNonNestedClass, bool[]>), Expected = "IReadOnlyDictionary<MyNonNestedClass, bool[]>" },
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
                new { Type = typeof(Derived<>).BaseType, Expected = "Base<string, V>" },
                new { Type = typeof(Derived<>).GetField("F").FieldType, Expected = "G<Derived<V>>" },

                // generic parameter:
                new { Type = typeof(Base<,>).GetGenericArguments()[0], Expected = "T" },

                // generic type definitions:
                new { Type = typeof(IList<>), Expected = "IList<T>" },
                new { Type = typeof(List<>), Expected = "List<T>" },
                new { Type = typeof(IReadOnlyDictionary<,>), Expected = "IReadOnlyDictionary<TKey, TValue>" },
                new { Type = typeof(Derived<>), Expected = "Derived<V>" },

                // other types
                new { Type = new Derived<int>[0].GetType(), Expected = "Derived<int>[]" },
                new { Type = typeof(Derived<>.Nested), Expected = "Derived<V>.Nested" },
                new { Type = typeof(string), Expected = "string" },
                new { Type = typeof(int), Expected = "int" },
                new { Type = typeof(int?), Expected = "int?" },
                new { Type = typeof(Guid), Expected = "Guid" },
                new { Type = typeof(Guid?), Expected = "Guid?" },
                new { Type = typeof(MyNonNestedClass), Expected = "MyNonNestedClass" },
                new { Type = typeof(MyNestedClass), Expected = "TypeExtensionsTest.MyNestedClass" },
                new { Type = typeof(IReadOnlyDictionary<string, int?>), Expected = "IReadOnlyDictionary<string, int?>" },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = "IReadOnlyDictionary<string, Guid?>" },
                new { Type = typeof(string[]), Expected = "string[]" },
                new { Type = typeof(int?[]), Expected = "int?[]" },
                new { Type = typeof(MyNonNestedClass[]), Expected = "MyNonNestedClass[]" },
                new { Type = typeof(Guid?[]), Expected = "Guid?[]" },
                new { Type = typeof(IList<int?[]>), Expected = "IList<int?[]>" },
                new { Type = typeof(IReadOnlyDictionary<MyNonNestedClass, bool?>[]), Expected = "IReadOnlyDictionary<MyNonNestedClass, bool?>[]" },
                new { Type = typeof(IReadOnlyDictionary<bool[], MyNonNestedClass>), Expected = "IReadOnlyDictionary<bool[], MyNonNestedClass>" },
                new { Type = typeof(IReadOnlyDictionary<MyNonNestedClass, bool[]>), Expected = "IReadOnlyDictionary<MyNonNestedClass, bool[]>" },
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
                new { Type = typeof(Derived<>).BaseType, Expected = "OBeautifulCode.Type.Recipes.Test.Base<string, V>" },
                new { Type = typeof(Derived<>).GetField("F").FieldType, Expected = "OBeautifulCode.Type.Recipes.Test.G<OBeautifulCode.Type.Recipes.Test.Derived<V>>" },

                // generic parameter:
                new { Type = typeof(Base<,>).GetGenericArguments()[0], Expected = "T" },

                // generic type definitions:
                new { Type = typeof(IList<>), Expected = "System.Collections.Generic.IList<T>" },
                new { Type = typeof(List<>), Expected = "System.Collections.Generic.List<T>" },
                new { Type = typeof(IReadOnlyDictionary<,>), Expected = "System.Collections.Generic.IReadOnlyDictionary<TKey, TValue>" },
                new { Type = typeof(Derived<>), Expected = "OBeautifulCode.Type.Recipes.Test.Derived<V>" },

                // other types
                new { Type = new Derived<int>[0].GetType(), Expected = "OBeautifulCode.Type.Recipes.Test.Derived<int>[]" },
                new { Type = typeof(Derived<>.Nested), Expected = "OBeautifulCode.Type.Recipes.Test.Derived<V>.Nested" },
                new { Type = typeof(string), Expected = "string" },
                new { Type = typeof(int), Expected = "int" },
                new { Type = typeof(int?), Expected = "int?" },
                new { Type = typeof(Guid), Expected = "System.Guid" },
                new { Type = typeof(Guid?), Expected = "System.Guid?" },
                new { Type = typeof(MyNonNestedClass), Expected = "OBeautifulCode.Type.Recipes.Test.MyNonNestedClass" },
                new { Type = typeof(MyNestedClass), Expected = "OBeautifulCode.Type.Recipes.Test.TypeExtensionsTest.MyNestedClass" },
                new { Type = typeof(IReadOnlyDictionary<string, int?>), Expected = "System.Collections.Generic.IReadOnlyDictionary<string, int?>" },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = "System.Collections.Generic.IReadOnlyDictionary<string, System.Guid?>" },
                new { Type = typeof(string[]), Expected = "string[]" },
                new { Type = typeof(int?[]), Expected = "int?[]" },
                new { Type = typeof(MyNonNestedClass[]), Expected = "OBeautifulCode.Type.Recipes.Test.MyNonNestedClass[]" },
                new { Type = typeof(Guid?[]), Expected = "System.Guid?[]" },
                new { Type = typeof(IList<int?[]>), Expected = "System.Collections.Generic.IList<int?[]>" },
                new { Type = typeof(IReadOnlyDictionary<MyNonNestedClass, bool?>[]), Expected = "System.Collections.Generic.IReadOnlyDictionary<OBeautifulCode.Type.Recipes.Test.MyNonNestedClass, bool?>[]" },
                new { Type = typeof(IReadOnlyDictionary<bool[], MyNonNestedClass>), Expected = "System.Collections.Generic.IReadOnlyDictionary<bool[], OBeautifulCode.Type.Recipes.Test.MyNonNestedClass>" },
                new { Type = typeof(IReadOnlyDictionary<MyNonNestedClass, bool[]>), Expected = "System.Collections.Generic.IReadOnlyDictionary<OBeautifulCode.Type.Recipes.Test.MyNonNestedClass, bool[]>" },
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
                new { Type = typeof(Derived<>).BaseType, Expected = Invariant($"Base<string, V> || OBeautifulCode.Type.Recipes.Test.Base<T, U> => {ThisAssemblyNameAndVersion} | string => {MsCorLibNameAndVersion}") },
                new { Type = typeof(Derived<>).GetField("F").FieldType, Expected = Invariant($"G<Derived<V>> || OBeautifulCode.Type.Recipes.Test.G<T> => {ThisAssemblyNameAndVersion} | OBeautifulCode.Type.Recipes.Test.Derived<V> => {ThisAssemblyNameAndVersion}") },

                // generic parameter:
                new { Type = typeof(Base<,>).GetGenericArguments()[0], Expected = "T" },

                // generic type definitions:
                new { Type = typeof(IList<>), Expected = Invariant($"IList<T> || System.Collections.Generic.IList<T> => {MsCorLibNameAndVersion}") },
                new { Type = typeof(List<>), Expected = Invariant($"List<T> || System.Collections.Generic.List<T> => {MsCorLibNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<,>), Expected = Invariant($"IReadOnlyDictionary<TKey, TValue> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion}") },
                new { Type = typeof(Derived<>), Expected = Invariant($"Derived<V> || OBeautifulCode.Type.Recipes.Test.Derived<V> => {ThisAssemblyNameAndVersion}") },

                // other types
                new { Type = new Derived<int>[0].GetType(), Expected = Invariant($"Derived<int>[] || OBeautifulCode.Type.Recipes.Test.Derived<V> => {ThisAssemblyNameAndVersion} | int => {MsCorLibNameAndVersion}") },
                new { Type = typeof(Derived<>.Nested), Expected = Invariant($"Derived<V>.Nested || OBeautifulCode.Type.Recipes.Test.Derived<V>.Nested => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(string), Expected = Invariant($"string || string => {MsCorLibNameAndVersion}") },
                new { Type = typeof(int), Expected = Invariant($"int || int => {MsCorLibNameAndVersion}") },
                new { Type = typeof(int?), Expected = Invariant($"int? || int => {MsCorLibNameAndVersion}") },
                new { Type = typeof(Guid), Expected = Invariant($"Guid || System.Guid => {MsCorLibNameAndVersion}") },
                new { Type = typeof(Guid?), Expected = Invariant($"Guid? || System.Guid => {MsCorLibNameAndVersion}") },
                new { Type = typeof(MyNonNestedClass), Expected = Invariant($"MyNonNestedClass || OBeautifulCode.Type.Recipes.Test.MyNonNestedClass => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(MyNestedClass), Expected = Invariant($"TypeExtensionsTest.MyNestedClass || OBeautifulCode.Type.Recipes.Test.TypeExtensionsTest.MyNestedClass => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<string, int?>), Expected = Invariant($"IReadOnlyDictionary<string, int?> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | string => {MsCorLibNameAndVersion} | int => {MsCorLibNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = Invariant($"IReadOnlyDictionary<string, Guid?> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | string => {MsCorLibNameAndVersion} | System.Guid => {MsCorLibNameAndVersion}") },
                new { Type = typeof(string[]), Expected = Invariant($"string[] || string => {MsCorLibNameAndVersion}") },
                new { Type = typeof(int?[]), Expected = Invariant($"int?[] || int => {MsCorLibNameAndVersion}") },
                new { Type = typeof(MyNonNestedClass[]), Expected = Invariant($"MyNonNestedClass[] || OBeautifulCode.Type.Recipes.Test.MyNonNestedClass => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(Guid?[]), Expected = Invariant($"Guid?[] || System.Guid => {MsCorLibNameAndVersion}") },
                new { Type = typeof(IList<int?[]>), Expected = Invariant($"IList<int?[]> || System.Collections.Generic.IList<T> => {MsCorLibNameAndVersion} | int => {MsCorLibNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<MyNonNestedClass, bool?>[]), Expected = Invariant($"IReadOnlyDictionary<MyNonNestedClass, bool?>[] || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | OBeautifulCode.Type.Recipes.Test.MyNonNestedClass => {ThisAssemblyNameAndVersion} | bool => {MsCorLibNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<bool[], MyNonNestedClass>), Expected = Invariant($"IReadOnlyDictionary<bool[], MyNonNestedClass> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | bool => {MsCorLibNameAndVersion} | OBeautifulCode.Type.Recipes.Test.MyNonNestedClass => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<MyNonNestedClass, bool[]>), Expected = Invariant($"IReadOnlyDictionary<MyNonNestedClass, bool[]> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | OBeautifulCode.Type.Recipes.Test.MyNonNestedClass => {ThisAssemblyNameAndVersion} | bool => {MsCorLibNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]>), Expected = Invariant($"IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibNameAndVersion} | System.Guid => {MsCorLibNameAndVersion} | int => {MsCorLibNameAndVersion} | System.Collections.Generic.IList<T> => {MsCorLibNameAndVersion} | System.Collections.Generic.IList<T> => {MsCorLibNameAndVersion} | short => {MsCorLibNameAndVersion}") },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.ToStringReadable(ToStringReadableOptions.IncludeAssemblyDetails)).ToList();

            // Assert
            typesAndExpected.Select(_ => _.Expected).Should().Equal(actuals);
        }

        private class MyNestedClass
        {
        }

        private class BaseCollection<T> : IList<T>
        {
            public int Count { get; }

            public bool IsReadOnly { get; }

            public T this[int index]
            {
                get => throw new NotImplementedException();
                set => throw new NotImplementedException();
            }

            public IEnumerator<T> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            public void Add(T item)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(T item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public bool Remove(T item)
            {
                throw new NotImplementedException();
            }

            public int IndexOf(T item)
            {
                throw new NotImplementedException();
            }

            public void Insert(int index, T item)
            {
                throw new NotImplementedException();
            }

            public void RemoveAt(int index)
            {
                throw new NotImplementedException();
            }
        }

        private class SubclassCollection<T> : BaseCollection<T>
        {
        }

        private class MyCollection<T> : List<T>
        {
        }

        private class CollectionDerivative : Collection<string>
        {
        }

        private class DictionaryDerivative : Dictionary<string, string>
        {
        }
    }

    public class MyNonNestedClass
    {
    }

#pragma warning disable SA1314 // Type parameter names should begin with T
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "U", Justification = "For testing purposes.")]
    [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", MessageId = "T", Justification = "For testing purposes.")]
    public class Base<T, U>
#pragma warning restore SA1314 // Type parameter names should begin with T
    {
    }

#pragma warning disable SA1314 // Type parameter names should begin with T
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "V", Justification = "For testing purposes.")]
    [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", MessageId = "T", Justification = "For testing purposes.")]
    public class Derived<V> : Base<string, V>
#pragma warning restore SA1314 // Type parameter names should begin with T
    {
#pragma warning disable SA1401 // Fields should be private
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "F", Justification = "For testing purposes.")]
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "For testing purposes.")]
        public G<Derived<V>> F;
#pragma warning restore SA1401 // Fields should be private

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "For testing purposes.")]
        public class Nested
        {
        }
    }

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "G", Justification = "For testing purposes.")]
    public class G<T>
    {
    }
}
