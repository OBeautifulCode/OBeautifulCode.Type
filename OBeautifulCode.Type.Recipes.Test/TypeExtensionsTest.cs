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
        private const string MsCorLibAssemblyNameAndVersion = "mscorlib (4.0.0.0)";

        private const string ValueTupleAssemblyNameAndVersion = "System.ValueTuple (4.0.3.0)";

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
        public static void GetArrayKind___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.GetArrayKind(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void GetArrayKind___Should_return_ArrayKind_None___When_type_is_not_an_array()
        {
            // Arrange
            var types = new[]
            {
                typeof(object),
                typeof(string),
                typeof(Guid),
                typeof(DateTime),
                typeof(int),
                typeof(Guid?),
                typeof(DateTime?),
                typeof(int?),
                typeof(IReadOnlyCollection<object>),
                typeof(IReadOnlyCollection<string>),
                typeof(IReadOnlyCollection<Guid>),
                typeof(IReadOnlyCollection<DateTime>),
                typeof(IReadOnlyCollection<int>),
                typeof(List<object>),
                typeof(List<string>),
                typeof(List<Guid>),
                typeof(List<DateTime>),
                typeof(List<int>),
            };

            // Act
            var actuals = types.Select(_ => _.GetArrayKind()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(ArrayKind.None);
        }

        [Fact]
        public static void GetArrayKind___Should_return_ArrayKind_Vector___When_type_is_a_vector_array()
        {
            // Arrange
            var types = new[]
            {
                typeof(object[]),
                typeof(string[]),
                typeof(Guid[]),
                typeof(DateTime[]),
                typeof(int[]),
                typeof(Guid?[]),
                typeof(DateTime?[]),
                typeof(int?[]),
                typeof(IReadOnlyCollection<object>[]),
                typeof(IReadOnlyCollection<string>[]),
                typeof(IReadOnlyCollection<Guid>[]),
                typeof(IReadOnlyCollection<DateTime>[]),
                typeof(IReadOnlyCollection<int>[]),
                typeof(List<object>[]),
                typeof(List<string>[]),
                typeof(List<Guid>[]),
                typeof(List<DateTime>[]),
                typeof(List<int>[]),
                typeof(object[][]),
                typeof(string[][]),
                typeof(Guid[][]),
                typeof(DateTime[][]),
                typeof(int[][]),
                typeof(Guid?[][]),
                typeof(DateTime?[][]),
                typeof(int?[][]),
                typeof(IReadOnlyCollection<object>[][]),
                typeof(IReadOnlyCollection<string>[][]),
                typeof(IReadOnlyCollection<Guid>[][]),
                typeof(IReadOnlyCollection<DateTime>[][]),
                typeof(IReadOnlyCollection<int>[][]),
                typeof(List<object>[][]),
                typeof(List<string>[][]),
                typeof(List<Guid>[][]),
                typeof(List<DateTime>[][]),
                typeof(List<int>[][]),
                typeof(object[][,]),
                typeof(string[][,]),
                typeof(Guid[][,]),
                typeof(DateTime[][,]),
                typeof(int[][,]),
                typeof(Guid?[][,]),
                typeof(DateTime?[][,]),
                typeof(int?[][,]),
                typeof(IReadOnlyCollection<object>[][,]),
                typeof(IReadOnlyCollection<string>[][,]),
                typeof(IReadOnlyCollection<Guid>[][,]),
                typeof(IReadOnlyCollection<DateTime>[][,]),
                typeof(IReadOnlyCollection<int>[][,]),
                typeof(List<object>[][,]),
                typeof(List<string>[][,]),
                typeof(List<Guid>[][,]),
                typeof(List<DateTime>[][,]),
                typeof(List<int>[][,]),
            };

            // Act
            var actuals = types.Select(_ => _.GetArrayKind()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(ArrayKind.Vector);
        }

        [Fact]
        public static void GetArrayKind___Should_return_ArrayKind_Multidimensional___When_type_is_a_multidimensional_array()
        {
            // Arrange
            var types = new[]
            {
                typeof(object[,]),
                typeof(string[,]),
                typeof(Guid[,]),
                typeof(DateTime[,]),
                typeof(int[,]),
                typeof(Guid?[,]),
                typeof(DateTime?[,]),
                typeof(int?[,]),
                typeof(IReadOnlyCollection<object>[,]),
                typeof(IReadOnlyCollection<string>[,]),
                typeof(IReadOnlyCollection<Guid>[,]),
                typeof(IReadOnlyCollection<DateTime>[,]),
                typeof(IReadOnlyCollection<int>[,]),
                typeof(List<object>[,]),
                typeof(List<string>[,]),
                typeof(List<Guid>[,]),
                typeof(List<DateTime>[,]),
                typeof(List<int>[,]),
                typeof(object[,][]),
                typeof(string[,][]),
                typeof(Guid[,][]),
                typeof(DateTime[,][]),
                typeof(int[,][]),
                typeof(Guid?[,][]),
                typeof(DateTime?[,][]),
                typeof(int?[,][]),
                typeof(IReadOnlyCollection<object>[,][]),
                typeof(IReadOnlyCollection<string>[,][]),
                typeof(IReadOnlyCollection<Guid>[,][]),
                typeof(IReadOnlyCollection<DateTime>[,][]),
                typeof(IReadOnlyCollection<int>[,][]),
                typeof(List<object>[,][]),
                typeof(List<string>[,][]),
                typeof(List<Guid>[,][]),
                typeof(List<DateTime>[,][]),
                typeof(List<int>[,][]),
                typeof(object).MakeArrayType(1),
                typeof(string).MakeArrayType(1),
                typeof(Guid).MakeArrayType(1),
                typeof(DateTime).MakeArrayType(1),
                typeof(int).MakeArrayType(1),
                typeof(Guid?).MakeArrayType(1),
                typeof(DateTime?).MakeArrayType(1),
                typeof(int?).MakeArrayType(1),
                typeof(IReadOnlyCollection<object>).MakeArrayType(1),
                typeof(IReadOnlyCollection<string>).MakeArrayType(1),
                typeof(IReadOnlyCollection<Guid>).MakeArrayType(1),
                typeof(IReadOnlyCollection<DateTime>).MakeArrayType(1),
                typeof(IReadOnlyCollection<int>).MakeArrayType(1),
                typeof(List<object>).MakeArrayType(1),
                typeof(List<string>).MakeArrayType(1),
                typeof(List<Guid>).MakeArrayType(1),
                typeof(List<DateTime>).MakeArrayType(1),
                typeof(List<int>).MakeArrayType(1),
                typeof(object[]).MakeArrayType(1),
                typeof(string[]).MakeArrayType(1),
                typeof(Guid[]).MakeArrayType(1),
                typeof(DateTime[]).MakeArrayType(1),
                typeof(int[]).MakeArrayType(1),
                typeof(Guid?[]).MakeArrayType(1),
                typeof(DateTime?[]).MakeArrayType(1),
                typeof(int?[]).MakeArrayType(1),
                typeof(IReadOnlyCollection<object>[]).MakeArrayType(1),
                typeof(IReadOnlyCollection<string>[]).MakeArrayType(1),
                typeof(IReadOnlyCollection<Guid>[]).MakeArrayType(1),
                typeof(IReadOnlyCollection<DateTime>[]).MakeArrayType(1),
                typeof(IReadOnlyCollection<int>[]).MakeArrayType(1),
                typeof(List<object>[]).MakeArrayType(1),
                typeof(List<string>[]).MakeArrayType(1),
                typeof(List<Guid>[]).MakeArrayType(1),
                typeof(List<DateTime>[]).MakeArrayType(1),
                typeof(List<int>[]).MakeArrayType(1),
            };

            // Act
            var actuals = types.Select(_ => _.GetArrayKind()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(ArrayKind.Multidimensional);
        }

        [Fact]
        public static void GetClosedEnumerableElementType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.GetClosedEnumerableElementType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void GetClosedEnumerableElementType___Should_throw_ArgumentException___When_parameter_type_is_not_a_closed_Enumerable_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => Record.Exception(_.GetClosedEnumerableElementType));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ArgumentException>();
                actual.Message.Should().Contain("Specified type is not a closed Enumerable type");
            }
        }

        [Fact]
        public static void GetClosedEnumerableElementType___Should_return_element_type___When_called()
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
                new { Type = typeof(NonGenericClassDictionary), Expected = typeof(KeyValuePair<string, int?>) },
                new { Type = typeof(IGenericIReadOnlyDictionary<string, TestClass>), Expected = typeof(KeyValuePair<string, TestClass>) },
                new { Type = typeof(INonGenericIReadOnlyDictionary), Expected = typeof(KeyValuePair<int, DateTime>) },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.GetClosedEnumerableElementType()).ToList();

            // Assert
            actuals.Should().Equal(typesAndExpected.Select(_ => _.Expected));
        }

        [Fact]
        public static void GetClosedDictionaryKeyType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.GetClosedDictionaryKeyType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void GetClosedDictionaryKeyType___Should_throw_ArgumentException___When_parameter_type_is_not_a_closed_Dictionary_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(TestClass),
                    typeof(IEnumerable),
                    typeof(IEnumerable<string>),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => Record.Exception(_.GetClosedDictionaryKeyType));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ArgumentException>();
                actual.Message.Should().Contain("Specified type is not a closed Dictionary type");
            }
        }

        [Fact]
        public static void GetClosedDictionaryKeyType___Should_return_key_type___When_called()
        {
            // Arrange
            var typesAndExpected = new[]
            {
                new { Type = typeof(IDictionary<int, Guid>), Expected = typeof(int) },
                new { Type = typeof(IReadOnlyDictionary<Guid, string>), Expected = typeof(Guid) },
                new { Type = typeof(Dictionary<bool, int?>), Expected = typeof(bool) },
                new { Type = typeof(ReadOnlyDictionary<TestClass, bool?>), Expected = typeof(TestClass) },
                new { Type = typeof(ConcurrentDictionary<string, DateTime>), Expected = typeof(string) },

                new { Type = typeof(BaseClassIDictionary<DateTime, string>), Expected = typeof(DateTime) },
                new { Type = typeof(DerivedClassIDictionary<TestClass, int>), Expected = typeof(TestClass) },
                new { Type = typeof(GenericClassDictionary<TimeSpan, bool?>), Expected = typeof(TimeSpan) },
                new { Type = typeof(NonGenericClassDictionary), Expected = typeof(string) },
                new { Type = typeof(IGenericIReadOnlyDictionary<string, TestClass>), Expected = typeof(string) },
                new { Type = typeof(INonGenericIReadOnlyDictionary), Expected = typeof(int) },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.GetClosedDictionaryKeyType()).ToList();

            // Assert
            actuals.Should().Equal(typesAndExpected.Select(_ => _.Expected));
        }

        [Fact]
        public static void GetClosedDictionaryValueType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.GetClosedDictionaryValueType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void GetClosedDictionaryValueType___Should_throw_ArgumentException___When_parameter_type_is_not_a_closed_Dictionary_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(TestClass),
                    typeof(IEnumerable),
                    typeof(IEnumerable<string>),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => Record.Exception(_.GetClosedDictionaryValueType));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ArgumentException>();
                actual.Message.Should().Contain("Specified type is not a closed Dictionary type");
            }
        }

        [Fact]
        public static void GetClosedDictionaryValueType___Should_return_value_type___When_called()
        {
            // Arrange
            var typesAndExpected = new[]
            {
                new { Type = typeof(IDictionary<int, Guid>), Expected = typeof(Guid) },
                new { Type = typeof(IReadOnlyDictionary<Guid, string>), Expected = typeof(string) },
                new { Type = typeof(Dictionary<bool, int?>), Expected = typeof(int?) },
                new { Type = typeof(ReadOnlyDictionary<TestClass, bool?>), Expected = typeof(bool?) },
                new { Type = typeof(ConcurrentDictionary<string, DateTime>), Expected = typeof(DateTime) },

                new { Type = typeof(BaseClassIDictionary<DateTime, string>), Expected = typeof(string) },
                new { Type = typeof(DerivedClassIDictionary<TestClass, int>), Expected = typeof(int) },
                new { Type = typeof(GenericClassDictionary<TimeSpan, bool?>), Expected = typeof(bool?) },
                new { Type = typeof(NonGenericClassDictionary), Expected = typeof(int?) },
                new { Type = typeof(IGenericIReadOnlyDictionary<string, TestClass>), Expected = typeof(TestClass) },
                new { Type = typeof(INonGenericIReadOnlyDictionary), Expected = typeof(DateTime) },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.GetClosedDictionaryValueType()).ToList();

            // Assert
            actuals.Should().Equal(typesAndExpected.Select(_ => _.Expected));
        }

        [Fact]
        public static void GetClosedSystemCollectionElementType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.GetClosedSystemCollectionElementType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void GetClosedSystemCollectionElementType___Should_throw_ArgumentException___When_parameter_type_is_not_a_closed_System_Collection_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(Collection<>),
                    typeof(ICollection<>),
                    typeof(ReadOnlyCollection<>),
                    typeof(IReadOnlyCollection<>),
                    typeof(List<>),
                    typeof(IList<>),
                    typeof(IReadOnlyList<>),
                    typeof(Dictionary<,>),
                    typeof(IDictionary<,>),
                    typeof(ReadOnlyDictionary<,>),
                    typeof(IReadOnlyDictionary<,>),
                    typeof(ConcurrentDictionary<,>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IEnumerable<string>),
                    typeof(IDictionary<string, string>),
                    typeof(IReadOnlyDictionary<string, string>),
                    typeof(Dictionary<string, string>),
                    typeof(ReadOnlyDictionary<string, string>),
                    typeof(ConcurrentDictionary<string, string>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => Record.Exception(_.GetClosedSystemCollectionElementType));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ArgumentException>();
                actual.Message.Should().Contain("Specified type is not a closed System Collection type");
            }
        }

        [Fact]
        public static void GetClosedSystemCollectionElementType___Should_return_element_type___When_called()
        {
            // Arrange
            var typesAndExpected = new[]
            {
                new { Type = typeof(Collection<Guid>), Expected = typeof(Guid) },
                new { Type = typeof(ICollection<bool>), Expected = typeof(bool) },
                new { Type = typeof(ReadOnlyCollection<DateTime>), Expected = typeof(DateTime) },
                new { Type = typeof(IReadOnlyCollection<TimeSpan>), Expected = typeof(TimeSpan) },
                new { Type = typeof(List<TestClass>), Expected = typeof(TestClass) },
                new { Type = typeof(IList<int?>), Expected = typeof(int?) },
                new { Type = typeof(IReadOnlyList<int[]>), Expected = typeof(int[]) },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.GetClosedSystemCollectionElementType()).ToList();

            // Assert
            actuals.Should().Equal(typesAndExpected.Select(_ => _.Expected));
        }

        [Fact]
        public static void GetClosedSystemDictionaryKeyType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.GetClosedSystemDictionaryKeyType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void GetClosedSystemDictionaryKeyType___Should_throw_ArgumentException___When_parameter_type_is_not_a_closed_System_Dictionary_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(Collection<>),
                    typeof(ICollection<>),
                    typeof(ReadOnlyCollection<>),
                    typeof(IReadOnlyCollection<>),
                    typeof(List<>),
                    typeof(IList<>),
                    typeof(IReadOnlyList<>),
                    typeof(Dictionary<,>),
                    typeof(IDictionary<,>),
                    typeof(ReadOnlyDictionary<,>),
                    typeof(IReadOnlyDictionary<,>),
                    typeof(ConcurrentDictionary<,>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IEnumerable<string>),
                    typeof(IEnumerable<KeyValuePair<string, string>>),
                    typeof(Collection<Guid>),
                    typeof(ICollection<bool>),
                    typeof(ReadOnlyCollection<DateTime>),
                    typeof(IReadOnlyCollection<TimeSpan>),
                    typeof(List<TestClass>),
                    typeof(IList<int?>),
                    typeof(IReadOnlyList<int[]>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => Record.Exception(_.GetClosedSystemDictionaryKeyType));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ArgumentException>();
                actual.Message.Should().Contain("Specified type is not a closed System Dictionary type");
            }
        }

        [Fact]
        public static void GetClosedSystemDictionaryKeyType___Should_return_element_type___When_called()
        {
            // Arrange
            var typesAndExpected = new[]
            {
                new { Type = typeof(IDictionary<TestClass, string>), Expected = typeof(TestClass) },
                new { Type = typeof(IReadOnlyDictionary<Guid?, string>), Expected = typeof(Guid?) },
                new { Type = typeof(Dictionary<DateTime, string>), Expected = typeof(DateTime) },
                new { Type = typeof(ReadOnlyDictionary<IList<string>, string>), Expected = typeof(IList<string>) },
                new { Type = typeof(ConcurrentDictionary<List<bool>, string>), Expected = typeof(List<bool>) },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.GetClosedSystemDictionaryKeyType()).ToList();

            // Assert
            actuals.Should().Equal(typesAndExpected.Select(_ => _.Expected));
        }

        [Fact]
        public static void GetClosedSystemDictionaryValueType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.GetClosedSystemDictionaryValueType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void GetClosedSystemDictionaryValueType___Should_throw_ArgumentException___When_parameter_type_is_not_a_closed_System_Dictionary_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(Collection<>),
                    typeof(ICollection<>),
                    typeof(ReadOnlyCollection<>),
                    typeof(IReadOnlyCollection<>),
                    typeof(List<>),
                    typeof(IList<>),
                    typeof(IReadOnlyList<>),
                    typeof(Dictionary<,>),
                    typeof(IDictionary<,>),
                    typeof(ReadOnlyDictionary<,>),
                    typeof(IReadOnlyDictionary<,>),
                    typeof(ConcurrentDictionary<,>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IEnumerable<string>),
                    typeof(IEnumerable<KeyValuePair<string, string>>),
                    typeof(Collection<Guid>),
                    typeof(ICollection<bool>),
                    typeof(ReadOnlyCollection<DateTime>),
                    typeof(IReadOnlyCollection<TimeSpan>),
                    typeof(List<TestClass>),
                    typeof(IList<int?>),
                    typeof(IReadOnlyList<int[]>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => Record.Exception(_.GetClosedSystemDictionaryValueType));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<ArgumentException>();
                actual.Message.Should().Contain("Specified type is not a closed System Dictionary type");
            }
        }

        [Fact]
        public static void GetClosedSystemDictionaryValueType___Should_return_element_type___When_called()
        {
            // Arrange
            var typesAndExpected = new[]
            {
                new { Type = typeof(IDictionary<string, TestClass>), Expected = typeof(TestClass) },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = typeof(Guid?) },
                new { Type = typeof(Dictionary<string, DateTime>), Expected = typeof(DateTime) },
                new { Type = typeof(ReadOnlyDictionary<string, IList<string>>), Expected = typeof(IList<string>) },
                new { Type = typeof(ConcurrentDictionary<string, List<bool>>), Expected = typeof(List<bool>) },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.GetClosedSystemDictionaryValueType()).ToList();

            // Assert
            actuals.Should().Equal(typesAndExpected.Select(_ => _.Expected));
        }

        [Fact]
        public static void GetGenericTypeDefinitionOrSpecifiedType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.GetGenericTypeDefinitionOrSpecifiedType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void GetGenericTypeDefinitionOrSpecifiedType___Should_return_specified_type___When_type_is_not_generic()
        {
            // Arrange
            var expected = new Type[0]
                .Concat(TestTypes.ClosedTypes)
                .Concat(TestTypes.OpenTypes)
                .Where(_ => !_.IsGenericType)
                .ToList();

            // Act
            var actual = expected.Select(_ => _.GetGenericTypeDefinitionOrSpecifiedType());

            // Assert
            actual.Should().Equal(expected);
        }

        [Fact]
        public static void GetGenericTypeDefinitionOrSpecifiedType___Should_return_specified_type___When_type_is_a_generic_type_definition()
        {
            // Arrange
            var expected = TestTypes.GenericTypeDefinitions;

            // Act
            var actual = expected.Select(_ => _.GetGenericTypeDefinitionOrSpecifiedType());

            // Assert
            actual.Should().Equal(expected);
        }

        [Fact]
        public static void GetGenericTypeDefinitionOrSpecifiedType___Should_return_generic_type_definition___When_type_is_generic_but_not_generic_type_definition()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.ClosedTypes)
                .Concat(TestTypes.OpenTypes)
                .Where(_ => _.IsGenericType && (!_.IsGenericTypeDefinition))
                .ToList();

            var expected = types.Select(_ => _.GetGenericTypeDefinition()).ToList();

            // Act
            var actual = types.Select(_ => _.GetGenericTypeDefinitionOrSpecifiedType());

            // Assert
            actual.Should().Equal(expected);
        }

        [Fact]
        public static void GetInheritancePath___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.GetInheritancePath(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void GetInheritancePath___Should_return_the_inheritance_path_of_the_specified_type___When_called()
        {
            // Arrange
            var typesAndExpected = new[]
            {
                // object
                new { Type = typeof(object), Expected = new Type[0] },

                // value types
                new { Type = typeof(int), Expected = new[] { typeof(ValueType), typeof(object) } },
                new { Type = typeof(Guid), Expected = new[] { typeof(ValueType), typeof(object) } },

                // nullable types
                new { Type = typeof(int?), Expected = new[] { typeof(ValueType), typeof(object) } },
                new { Type = typeof(Guid?), Expected = new[] { typeof(ValueType), typeof(object) } },

                // arrays
                new { Type = typeof(int[]), Expected = new[] { typeof(Array), typeof(object) } },
                new { Type = typeof(object[]), Expected = new[] { typeof(Array), typeof(object) } },
                new { Type = typeof(Guid?[]), Expected = new[] { typeof(Array), typeof(object) } },

                // open and closed interfaces
                new { Type = typeof(IEnumerable), Expected = new Type[0] },
                new { Type = typeof(IReadOnlyList<string>), Expected = new Type[0] },
                new { Type = typeof(IReadOnlyList<>), Expected = new Type[0] },

                // open and closed classes
                new { Type = typeof(TestClass), Expected = new[] { typeof(object) } },
                new { Type = typeof(BaseClassIList<>), Expected = new[] { typeof(object) } },
                new { Type = typeof(BaseClassIList<string>), Expected = new[] { typeof(object) } },

                // the first BaseType is NOT the generic type definition:
                // https://stackoverflow.com/questions/59141721/why-is-the-basetype-of-a-generic-type-definition-not-itself-a-generic-type-defin
                new { Type = typeof(DerivedClassIList<>), Expected = new[] { typeof(DerivedClassIList<>).BaseType, typeof(object) } },

                // closed generic
                new { Type = typeof(DerivedClassIList<string>), Expected = new[] { typeof(BaseClassIList<string>), typeof(object) } },

                // generic type parameter
                new { Type = typeof(BaseGenericClass<,>).GetGenericArguments()[0], Expected = new[] { typeof(object) } },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.GetInheritancePath()).ToList();

            // Assert
            for (var x = 0; x < actuals.Count; x++)
            {
                actuals[x].Should().Equal(typesAndExpected[x].Expected);
            }
        }

        [Fact]
        public static void HasDefaultConstructor___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.HasDefaultConstructor(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void HasDefaultConstructor___Should_return_false___When_type_is_an_open_type()
        {
            // Arrange
            var types = TestTypes.OpenTypes;

            // Act
            var actual = types.Select(_ => _.HasDefaultConstructor());

            // Assert
            actual.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void HasDefaultConstructor___Should_return_false___When_type_is_closed_without_default_constructor()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedInterfaceTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(TestTypes.ClosedArrayTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(new[]
                {
                    typeof(NoDefaultConstructorClass),
                    typeof(ChildOfAbstractClassWithPublicParameterlessConstructor),
                    typeof(ChildOfAbstractClassWithProtectedParameterlessConstructor),
                })
                .ToList();

            // Act
            var actual = types.Select(_ => _.HasDefaultConstructor());

            // Assert
            actual.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void HasDefaultConstructor___Should_return_true___When_type_is_closed_and_has_a_default_constructor()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.ClosedClassTypes)
                .ToList();

            // Act
            var actual = types.Select(_ => _.HasDefaultConstructor());

            // Assert
            actual.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void HasBaseType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.HasBaseType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void HasBaseType___Should_return_false___When_base_type_is_not_object()
        {
            // Arrange
            var types = new Type[0]
                .Concat(
                    new[]
                    {
                        typeof(object),
                    })
                .Concat(TestTypes.ClosedInterfaceTypes)
                .Concat(TestTypes.OpenInterfaceTypesWithoutGenericTypeDefinitionTypes)
                .ToList();

            // Act
            var actuals = types.Select(_ => _.HasBaseType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void HasBaseType___Should_return_true___When_base_type_is_object()
        {
            // Arrange
            var types = new Type[0]
                .Concat(
                    new[]
                    {
                        typeof(NonGenericClassDictionary),
                        typeof(PublicEnum),
                        typeof(TestClass),
                        typeof(Dictionary<int, string>),
                    })
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(TestTypes.ClosedArrayTypes)
                .ToList();

            // Act
            var actuals = types.Select(_ => _.HasBaseType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void HasObjectAsBaseType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.HasObjectAsBaseType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void HasObjectAsBaseType___Should_return_false___When_base_type_is_not_object()
        {
            // Arrange
            var types = new Type[0]
                .Concat(
                    new[]
                    {
                        typeof(object),
                        typeof(NonGenericClassDictionary),
                        typeof(PublicEnum),
                    })
                .Concat(TestTypes.ClosedInterfaceTypes)
                .Concat(TestTypes.OpenInterfaceTypesWithoutGenericTypeDefinitionTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(TestTypes.ClosedArrayTypes)
                .ToList();

            // Act
            var actuals = types.Select(_ => _.HasObjectAsBaseType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void HasObjectAsBaseType___Should_return_true___When_base_type_is_object()
        {
            // Arrange
            var types = new Type[0]
                .Concat(
                    new[]
                    {
                        typeof(TestClass),
                        typeof(Dictionary<int, string>),
                    })
                .Concat(TestTypes.ClosedAnonymousTypes)
                .ToList();

            // Act
            var actuals = types.Select(_ => _.HasObjectAsBaseType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void HasWorkingDefaultComparer_type___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.HasWorkingDefaultComparer(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void HasWorkingDefaultComparer_type___Should_throw_NotSupportedException___When_parameter_type_is_an_open_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(new[]
                {
                    typeof(IComparable<>),
                    typeof(ICustomGenericComparable<>),
                    typeof(CustomGenericComparableClass<>),
                })
                .ToList();

            // Act
            var actuals = types.Select(_ => Record.Exception(() => _.HasWorkingDefaultComparer())).ToList();

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<NotSupportedException>();
                actual.Message.Should().Contain("Parameter 'type' is an open type; open types are not supported for that parameter");
            }
        }

        [Fact]
        public static void HasWorkingDefaultComparer_type___Should_return_false___When_parameter_type_does_not_have_a_working_default_comparer()
        {
            // Arrange
            var types = new[]
            {
                typeof(NonComparableClass),
                typeof(INonComparable),
                typeof(ComparableOfStringClass),
                typeof(IComparable<string>),
            };

            // Act
            var actuals = types.Select(_ => _.HasWorkingDefaultComparer()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void HasWorkingDefaultComparer_type___Should_return_true___When_parameter_type_has_a_working_default_comparer()
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
                typeof(ICustomGenericComparable),
                typeof(ICustomGenericComparable<string>),
                typeof(ICustomComparable),
                typeof(CustomGenericComparableClass),
                typeof(CustomGenericComparableClass<string>),
                typeof(CustomComparableClass),
            };

            // Act
            var actuals = types.Select(_ => _.HasWorkingDefaultComparer()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void HasWorkingDefaultComparer_type___Should_return_false___When_parameter_type_is_not_comparable()
        {
            // Arrange
            var types = new[]
            {
                typeof(NonComparableClass),
            };

            // Act
            var actuals = types.Select(_ => _.HasWorkingDefaultComparer()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void HasWorkingDefaultComparer_type___Should_return_true___When_parameter_type_is_comparable()
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
            var actuals = types.Select(_ => _.HasWorkingDefaultComparer()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
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
        public static void IsAssignableTo___Should_throw_NotSupportedException___When_parameter_type_is_an_open_type()
        {
            // Arrange
            var types = TestTypes.OpenTypes;

            // Act
            var actuals = types.Select(_ => Record.Exception(() => _.IsAssignableTo(typeof(object))));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<NotSupportedException>();
                actual.Message.Should().Contain("Parameter 'type' is an open type; open types are not supported for that parameter.");
            }
        }

        [Fact]
        public static void IsAssignableTo___Should_throw_NotSupportedException___When_parameter_otherType_is_an_open_type_but_not_a_generic_type_definition()
        {
            // Arrange
            var types = TestTypes.OpenTypesWithoutGenericTypeDefinitionTypes;

            // Act
            var actuals = types.Select(_ => Record.Exception(() => typeof(object).IsAssignableTo(_)));

            // Assert
            foreach (var actual in actuals)
            {
                actual.Should().BeOfType<NotSupportedException>();
                actual.Message.Should().Contain("Parameter 'otherType' is an open type, but not a generic type definition; the only open types that are supported are generic type definitions for that parameter.");
            }
        }

        [Fact]
        public static void IsAssignableTo___Should_return_true___When_type_is_equal_to_otherType()
        {
            // Arrange
            var types = TestTypes.ClosedTypes;

            // Act
            var actuals1 = types.Select(_ => _.IsAssignableTo(_, treatGenericTypeDefinitionAsAssignableTo: false)).ToList();
            var actuals2 = types.Select(_ => _.IsAssignableTo(_, treatGenericTypeDefinitionAsAssignableTo: true)).ToList();

            // Assert
            actuals1.Should().AllBeEquivalentTo(true);
            actuals2.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsAssignableTo___Should_return_true___When_IsAssignableFrom_returns_true()
        {
            // Arrange
            var types = new[]
            {
                new { Type = typeof(string), OtherType = typeof(object) },
                new { Type = typeof(List<string>), OtherType = typeof(IList) },
                new { Type = typeof(List<string>), OtherType = typeof(IList<string>) },
                new { Type = typeof(DerivedClassIList<string>), OtherType = typeof(BaseClassIList<string>) },
            };

            // Act
            var actuals1 = types.Select(_ => _.Type.IsAssignableTo(_.OtherType, treatGenericTypeDefinitionAsAssignableTo: false)).ToList();
            var actuals2 = types.Select(_ => _.Type.IsAssignableTo(_.OtherType, treatGenericTypeDefinitionAsAssignableTo: true)).ToList();

            // Assert
            actuals1.Should().AllBeEquivalentTo(true);
            actuals2.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsAssignableTo___Should_return_false___When_IsAssignableFrom_returns_false_and_otherType_is_not_a_generic_type_definition()
        {
            // Arrange
            var types = new[]
            {
                new { Type = typeof(List<string>), OtherType = typeof(List<object>) },
                new { Type = typeof(List<string>), OtherType = typeof(IList<object>) },
                new { Type = typeof(object), OtherType = typeof(string) },
                new { Type = typeof(BaseClassIList<string>), OtherType = typeof(DerivedClassIList<string>) },
                new { Type = typeof(IList<string>), OtherType = typeof(BaseClassIList<string>) },
            };

            // Act
            var actuals1 = types.Select(_ => _.Type.IsAssignableTo(_.OtherType, treatGenericTypeDefinitionAsAssignableTo: false)).ToList();
            var actuals2 = types.Select(_ => _.Type.IsAssignableTo(_.OtherType, treatGenericTypeDefinitionAsAssignableTo: true)).ToList();

            // Assert
            actuals1.Should().AllBeEquivalentTo(false);
            actuals2.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsAssignableTo___Should_return_true___When_type_GenericTypeDefinition_is_equal_to_otherType_and_treatGenericTypeDefinitionAsAssignableTo_is_true()
        {
            // Arrange
            var types = TestTypes.ClosedTypes.Where(_ => _.IsGenericType).ToList();

            // Act
            var actuals = types.Select(_ => _.IsAssignableTo(_.GetGenericTypeDefinition(), treatGenericTypeDefinitionAsAssignableTo: true));

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsAssignableTo___Should_return_false___When_type_GenericTypeDefinition_is_equal_to_otherType_and_treatGenericTypeDefinitionAsAssignableTo_is_false()
        {
            // Arrange
            var types = TestTypes.ClosedTypes.Where(_ => _.IsGenericType).ToList();

            // Act
            var actuals = types.Select(_ => _.IsAssignableTo(_.GetGenericTypeDefinition(), treatGenericTypeDefinitionAsAssignableTo: false));

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsAssignableTo___Should_return_true___When_type_implements_or_inherits_an_interface_whose_generic_type_definition_equals_otherType_and_treatGenericTypeDefinitionAsAssignableTo_is_true()
        {
            // Arrange
            var types = new[]
            {
                new { Type = typeof(List<string>), OtherType = typeof(IList<>) },
                new { Type = typeof(List<string>), OtherType = typeof(IEnumerable<>) },
                new { Type = typeof(IList<string>), OtherType = typeof(IEnumerable<>) },
                new { Type = typeof(DerivedClassIList<string>), OtherType = typeof(IEnumerable<>) },
                new { Type = typeof(DerivedClassIList<string>), OtherType = typeof(BaseClassIList<>) },
            };

            // Act
            var actuals = types.Select(_ => _.Type.IsAssignableTo(_.OtherType, treatGenericTypeDefinitionAsAssignableTo: true));

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsAssignableTo___Should_return_false___When_type_implements_or_inherits_an_interface_whose_generic_type_definition_equals_otherType_and_treatGenericTypeDefinitionAsAssignableTo_is_false()
        {
            // Arrange
            var types = new[]
            {
                new { Type = typeof(List<string>), OtherType = typeof(IList<>) },
                new { Type = typeof(List<string>), OtherType = typeof(IEnumerable<>) },
                new { Type = typeof(IList<string>), OtherType = typeof(IEnumerable<>) },
                new { Type = typeof(DerivedClassIList<string>), OtherType = typeof(IEnumerable<>) },
                new { Type = typeof(DerivedClassIList<string>), OtherType = typeof(BaseClassIList<>) },
            };

            // Act
            var actuals = types.Select(_ => _.Type.IsAssignableTo(_.OtherType, treatGenericTypeDefinitionAsAssignableTo: false));

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsAssignableTo___Should_return_false___When_type_does_not_implement_nor_inherit_an_interface_whose_generic_type_definition_equals_otherType()
        {
            // Arrange
            var types = new[]
            {
                new { Type = typeof(IList<string>), OtherType = typeof(List<>) },
                new { Type = typeof(IEnumerable<string>), OtherType = typeof(IList<>) },
                new { Type = typeof(IEnumerable<string>), OtherType = typeof(List<>) },
                new { Type = typeof(BaseClassIList<string>), OtherType = typeof(DerivedClassIList<>) },
                new { Type = typeof(IList<string>), OtherType = typeof(BaseClassIList<>) },
            };

            // Act
            var actuals1 = types.Select(_ => _.Type.IsAssignableTo(_.OtherType, treatGenericTypeDefinitionAsAssignableTo: false));
            var actuals2 = types.Select(_ => _.Type.IsAssignableTo(_.OtherType, treatGenericTypeDefinitionAsAssignableTo: true));

            // Assert
            actuals1.Should().AllBeEquivalentTo(false);
            actuals2.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsAssignableTo___Should_return_true___When_type_BaseType_implements_or_inherits_an_interface_whose_generic_type_definition_equals_otherType_and_treatGenericTypeDefinitionAsAssignableTo_is_true()
        {
            // Arrange, Act
            var actual = typeof(GenericClassList<string>).IsAssignableTo(typeof(List<>), treatGenericTypeDefinitionAsAssignableTo: true);

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public static void IsAssignableTo___Should_return_false___When_type_BaseType_implements_or_inherits_an_interface_whose_generic_type_definition_equals_otherType_and_treatGenericTypeDefinitionAsAssignableTo_is_false()
        {
            // Arrange, Act
            var actual = typeof(GenericClassList<string>).IsAssignableTo(typeof(List<>), treatGenericTypeDefinitionAsAssignableTo: false);

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public static void IsClosedTypeAssignableToNull___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsClosedTypeAssignableToNull(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsClosedTypeAssignableToNull___Should_return_false___When_parameter_type_an_open_type()
        {
            // Arrange
            var types = TestTypes.OpenTypes;

            // Act
            var actuals = types.Select(_ => _.IsClosedTypeAssignableToNull());

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedTypeAssignableToNull___Should_return_false___When_parameter_type_is_not_assignable_to_null()
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
            var actuals = types.Select(_ => _.IsClosedTypeAssignableToNull()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedTypeAssignableToNull___Should_return_true___When_parameter_type_is_assignable_to_null()
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
                typeof(TestClass),
                typeof(DerivedClassIDictionary<string, string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsClosedTypeAssignableToNull()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsClosedAnonymousType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsClosedAnonymousType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsClosedAnonymousType___Should_return_false___When_parameter_type_is_not_a_closed_anonymous_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.ClosedTypes)
                .Concat(TestTypes.OpenTypes)
                .Except(TestTypes.ClosedAnonymousTypes)
                .ToList();

            // Act
            var actuals = types.Select(_ => _.IsClosedAnonymousType());

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedAnonymousType___Should_return_true___When_parameter_type_is_a_closed_anonymous_type()
        {
            // Arrange
            var types = TestTypes.ClosedAnonymousTypes;

            // Act
            var actuals = types.Select(_ => _.IsClosedAnonymousType());

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsClosedAnonymousTypeFastCheck___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsClosedAnonymousTypeFastCheck(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsClosedAnonymousTypeFastCheck___Should_return_false___When_parameter_type_is_not_a_closed_anonymous_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.ClosedTypes)
                .Concat(TestTypes.OpenTypes)
                .Except(TestTypes.ClosedAnonymousTypes)
                .ToList();

            // Act
            var actuals = types.Select(_ => _.IsClosedAnonymousTypeFastCheck());

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedAnonymousTypeFastCheck___Should_return_true___When_parameter_type_is_a_closed_anonymous_type()
        {
            // Arrange
            var types = TestTypes.ClosedAnonymousTypes;

            // Act
            var actuals = types.Select(_ => _.IsClosedAnonymousTypeFastCheck());

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsClosedGenericType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsClosedGenericType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsClosedGenericType___Should_return_false___When_parameter_type_is_not_closed_generic_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedNonGenericStructTypes)
                .Concat(TestTypes.ClosedArrayTypes)
                .Concat(
                    new[]
                    {
                        typeof(IEnumerable),
                        typeof(object),
                        typeof(TestClass),
                        typeof(TestClassWithNestedClass.NestedInTestClass),
                    })
                .ToList();

            // Act
            var actuals = types.Select(_ => _.IsClosedGenericType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedGenericType___Should_return_true___When_parameter_type_is_a_closed_generic_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(
                    new[]
                    {
                        typeof(IReadOnlyList<string>),
                        typeof(IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]>),
                        typeof(Dictionary<int, string>),
                        typeof(DerivedGenericClass<int>),
                    })
                .ToList();

            // Act
            var actuals = types.Select(_ => _.IsClosedGenericType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsClosedNonAnonymousClassType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsClosedNonAnonymousClassType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsClosedNonAnonymousClassType___Should_return_false___When_parameter_type_is_not_a_class_or_anonymous_or_not_closed()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedInterfaceTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .ToList();

            // Act
            var actuals = types.Select(_ => _.IsClosedNonAnonymousClassType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedNonAnonymousClassType___Should_return_true___When_parameter_type_is_a_not_anonymous_closed_class_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.ClosedClassTypes)
                .Concat(TestTypes.ClosedArrayTypes)
                .ToList();

            // Act
            var actuals = types.Select(_ => _.IsClosedNonAnonymousClassType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsClosedNullableType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsClosedNullableType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsClosedNullableType___Should_return_false___When_parameter_type_is_not_closed_nullable_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedTypes)
                .Except(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(Nullable<>),
                    typeof(GenericClassWithNullableProperty<>).GetProperty("Value").PropertyType,
                })
                .ToList();

            // Act
            var actuals = types.Select(_ => _.IsClosedNullableType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedNullableType___Should_return_true___When_parameter_type_is_a_closed_nullable()
        {
            // Arrange
            var types = TestTypes.ClosedNullableTypes;

            // Act
            var actuals = types.Select(_ => _.IsClosedNullableType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsClosedSystemCollectionType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsClosedSystemCollectionType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsClosedSystemCollectionType___Should_return_false___When_parameter_type_is_not_a_closed_System_Collection_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(Collection<>),
                    typeof(ICollection<>),
                    typeof(ReadOnlyCollection<>),
                    typeof(IReadOnlyCollection<>),
                    typeof(List<>),
                    typeof(IList<>),
                    typeof(IReadOnlyList<>),
                    typeof(Dictionary<,>),
                    typeof(IDictionary<,>),
                    typeof(ReadOnlyDictionary<,>),
                    typeof(IReadOnlyDictionary<,>),
                    typeof(ConcurrentDictionary<,>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IEnumerable<>),
                    typeof(IEnumerable<string>),
                    typeof(IDictionary<string, string>),
                    typeof(IReadOnlyDictionary<string, string>),
                    typeof(Dictionary<string, string>),
                    typeof(ReadOnlyDictionary<string, string>),
                    typeof(ConcurrentDictionary<string, string>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => _.IsClosedSystemCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedSystemCollectionType___Should_return_true___When_parameter_type_is_a_closed_System_Collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(Collection<string>),
                typeof(ICollection<string>),
                typeof(ReadOnlyCollection<string>),
                typeof(IReadOnlyCollection<string>),
                typeof(List<string>),
                typeof(IList<string>),
                typeof(IReadOnlyList<string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsClosedSystemCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsClosedSystemDictionaryType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsClosedSystemDictionaryType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsClosedSystemDictionaryType___Should_return_false___When_parameter_type_is_not_a_closed_System_Dictionary_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(Collection<>),
                    typeof(ICollection<>),
                    typeof(ReadOnlyCollection<>),
                    typeof(IReadOnlyCollection<>),
                    typeof(List<>),
                    typeof(IList<>),
                    typeof(IReadOnlyList<>),
                    typeof(Dictionary<,>),
                    typeof(IDictionary<,>),
                    typeof(ReadOnlyDictionary<,>),
                    typeof(IReadOnlyDictionary<,>),
                    typeof(ConcurrentDictionary<,>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IEnumerable<>),
                    typeof(IEnumerable<string>),
                    typeof(IEnumerable<KeyValuePair<string, string>>),
                    typeof(Collection<Guid>),
                    typeof(ICollection<bool>),
                    typeof(ReadOnlyCollection<DateTime>),
                    typeof(IReadOnlyCollection<TimeSpan>),
                    typeof(List<TestClass>),
                    typeof(IList<int?>),
                    typeof(IReadOnlyList<int[]>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => _.IsClosedSystemDictionaryType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedSystemDictionaryType___Should_return_true___When_parameter_type_is_a_closed_System_Dictionary_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(Dictionary<string, string>),
                typeof(IDictionary<string, string>),
                typeof(ReadOnlyDictionary<string, string>),
                typeof(IReadOnlyDictionary<string, string>),
                typeof(ConcurrentDictionary<string, string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsClosedSystemDictionaryType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsClosedSystemEnumerableType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsClosedSystemEnumerableType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsClosedSystemEnumerableType___Should_return_false___When_parameter_type_is_not_a_closed_Enumerable_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(Collection<>),
                    typeof(ICollection<>),
                    typeof(ReadOnlyCollection<>),
                    typeof(IReadOnlyCollection<>),
                    typeof(List<>),
                    typeof(IList<>),
                    typeof(IReadOnlyList<>),
                    typeof(Dictionary<,>),
                    typeof(IDictionary<,>),
                    typeof(ReadOnlyDictionary<,>),
                    typeof(IReadOnlyDictionary<,>),
                    typeof(ConcurrentDictionary<,>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IEnumerable<>),
                    typeof(IDictionary<string, string>),
                    typeof(IReadOnlyDictionary<string, string>),
                    typeof(Dictionary<string, string>),
                    typeof(ReadOnlyDictionary<string, string>),
                    typeof(ConcurrentDictionary<string, string>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                    typeof(ICollection<string>),
                    typeof(IReadOnlyCollection<string>),
                    typeof(Collection<string>),
                    typeof(ReadOnlyCollection<string>),
                    typeof(List<string>),
                    typeof(IList<string>),
                    typeof(IReadOnlyList<string>),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => _.IsClosedSystemEnumerableType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedSystemEnumerableType___Should_return_true___When_parameter_type_is_a_closed_ordered_System_Collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(IEnumerable<string>),
                typeof(IEnumerable<int>),
                typeof(IEnumerable<TestClass>),
            };

            // Act
            var actuals = types.Select(_ => _.IsClosedSystemEnumerableType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsClosedSystemKeyValuePairType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsClosedSystemKeyValuePairType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsClosedSystemKeyValuePairType___Should_return_false___When_parameter_type_is_not_a_closed_KeyValuePair()
        {
            // Arrange
            var types = TestTypes.AllTypes
                .Where(_ => !_.Name.StartsWith("KeyValuePair", StringComparison.OrdinalIgnoreCase))
                .Concat(
                    new[]
                    {
                        typeof(KeyValuePair<,>),
                    })
                .ToArray();

            // Act
            var actuals = types.Select(_ => _.IsClosedSystemKeyValuePairType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedSystemEnumerableType___Should_return_true___When_parameter_type_is_a_closed_KeyValuePair()
        {
            // Arrange
            var types = new[]
            {
                typeof(KeyValuePair<string, object>),
                typeof(KeyValuePair<object, string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsClosedSystemKeyValuePairType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsClosedSystemOrderedCollectionType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsClosedSystemOrderedCollectionType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsClosedSystemOrderedCollectionType___Should_return_false___When_parameter_type_is_not_a_closed_ordered_System_Collection_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(Collection<>),
                    typeof(ICollection<>),
                    typeof(ReadOnlyCollection<>),
                    typeof(IReadOnlyCollection<>),
                    typeof(List<>),
                    typeof(IList<>),
                    typeof(IReadOnlyList<>),
                    typeof(Dictionary<,>),
                    typeof(IDictionary<,>),
                    typeof(ReadOnlyDictionary<,>),
                    typeof(IReadOnlyDictionary<,>),
                    typeof(ConcurrentDictionary<,>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IEnumerable<>),
                    typeof(IEnumerable<string>),
                    typeof(IDictionary<string, string>),
                    typeof(IReadOnlyDictionary<string, string>),
                    typeof(Dictionary<string, string>),
                    typeof(ReadOnlyDictionary<string, string>),
                    typeof(ConcurrentDictionary<string, string>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                    typeof(ICollection<string>),
                    typeof(IReadOnlyCollection<string>),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => _.IsClosedSystemOrderedCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedSystemOrderedCollectionType___Should_return_true___When_parameter_type_is_a_closed_ordered_System_Collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(Collection<string>),
                typeof(ReadOnlyCollection<string>),
                typeof(List<string>),
                typeof(IList<string>),
                typeof(IReadOnlyList<string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsClosedSystemOrderedCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsClosedSystemUnorderedCollectionType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsClosedSystemUnorderedCollectionType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsClosedSystemUnorderedCollectionType___Should_return_false___When_parameter_type_is_not_a_closed_unordered_System_Collection_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(Collection<>),
                    typeof(ICollection<>),
                    typeof(ReadOnlyCollection<>),
                    typeof(IReadOnlyCollection<>),
                    typeof(List<>),
                    typeof(IList<>),
                    typeof(IReadOnlyList<>),
                    typeof(Dictionary<,>),
                    typeof(IDictionary<,>),
                    typeof(ReadOnlyDictionary<,>),
                    typeof(IReadOnlyDictionary<,>),
                    typeof(ConcurrentDictionary<,>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IEnumerable<>),
                    typeof(IEnumerable<string>),
                    typeof(IDictionary<string, string>),
                    typeof(IReadOnlyDictionary<string, string>),
                    typeof(Dictionary<string, string>),
                    typeof(ReadOnlyDictionary<string, string>),
                    typeof(ConcurrentDictionary<string, string>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                    typeof(Collection<string>),
                    typeof(ReadOnlyCollection<string>),
                    typeof(List<string>),
                    typeof(IList<string>),
                    typeof(IReadOnlyList<string>),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => _.IsClosedSystemUnorderedCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsClosedSystemUnorderedCollectionType___Should_return_true___When_parameter_type_is_a_closed_unordered_System_Collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(ICollection<string>),
                typeof(IReadOnlyCollection<string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsClosedSystemUnorderedCollectionType()).ToList();

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
        public static void IsNullableType___Should_return_false___When_parameter_type_is_not_a_nullable_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Except(new[] { typeof(Nullable<>) })
                .Concat(TestTypes.ClosedTypes)
                .Except(TestTypes.ClosedNullableTypes)
                .ToList();

            // Act
            var actuals = types.Select(_ => _.IsNullableType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsNullableType___Should_return_true___When_parameter_type_is_a_nullable_type()
        {
            // Arrange
            var types = TestTypes.ClosedNullableTypes
                .Concat(new[]
                {
                    typeof(GenericClassWithNullableProperty<>).GetProperty("Value").PropertyType,
                    typeof(Nullable<>),
                })
                .ToList();

            // Act
            var actuals = types.Select(_ => _.IsNullableType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsSystemType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsSystemType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsSystemType___Should_return_false___When_parameter_type_is_not_system_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(
                    new[]
                    {
                        typeof(INonComparable),
                        typeof(TestClass),
                        typeof(TestClassWithNestedClass.NestedInTestClass),
                        typeof(DerivedGenericClass<int>),
                        typeof(DerivedGenericClass<>).BaseType,
                        typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType,
                    })
                .ToList();

            // Act
            var actuals = types.Select(_ => _.IsSystemType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsSystemType___Should_return_true___When_parameter_type_is_system_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedInterfaceTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(TestTypes.ClosedArrayTypes)
                .Concat(TestTypes.OpenArrayTypes)
                .Concat(TestTypes.OpenInterfaceTypesWithoutGenericTypeDefinitionTypes)
                .Concat(TestTypes.GenericParameterTypes)
                .Concat(
                    new[]
                    {
                        typeof(object),
                        typeof(Dictionary<int, string>),
                        typeof(List<>).MakeGenericType(typeof(List<>)),
                    })
                .ToList();

            // Act
            var actuals = types.Select(_ => _.IsSystemType()).ToList();

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
        public static void IsSystemCollectionType___Should_return_false___When_parameter_type_is_not_a_System_Collection_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.GenericTypeDefinitions)
                .Except(new[] { typeof(Collection<>), typeof(ICollection<>), typeof(ReadOnlyCollection<>), typeof(IReadOnlyCollection<>), typeof(List<>), typeof(IList<>), typeof(IReadOnlyList<>) })
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(List<>).MakeArrayType(),
                    typeof(DerivedGenericClass<>).BaseType,
                    typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType,
                    typeof(BaseGenericClass<,>).GetGenericArguments()[0],
                    typeof(Dictionary<,>),
                    typeof(IDictionary<,>),
                    typeof(ReadOnlyDictionary<,>),
                    typeof(IReadOnlyDictionary<,>),
                    typeof(ConcurrentDictionary<,>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IEnumerable<>),
                    typeof(IEnumerable<string>),
                    typeof(IDictionary<string, string>),
                    typeof(IReadOnlyDictionary<string, string>),
                    typeof(Dictionary<string, string>),
                    typeof(ReadOnlyDictionary<string, string>),
                    typeof(ConcurrentDictionary<string, string>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => _.IsSystemCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsSystemCollectionType___Should_return_true___When_parameter_type_is_a_System_Collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(List<>).MakeGenericType(typeof(List<>)),
                typeof(GenericClassList<>).BaseType,
                typeof(IReadOnlyCollection<>).MakeGenericType(typeof(IReadOnlyCollection<>)),
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
        public static void IsSystemDictionaryType___Should_return_false___When_parameter_type_is_not_a_System_Dictionary_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Except(new[] { typeof(Dictionary<,>), typeof(IDictionary<,>), typeof(ReadOnlyDictionary<,>), typeof(IReadOnlyDictionary<,>), typeof(ConcurrentDictionary<,>) })
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(Collection<>),
                    typeof(ICollection<>),
                    typeof(ReadOnlyCollection<>),
                    typeof(IReadOnlyCollection<>),
                    typeof(List<>),
                    typeof(IList<>),
                    typeof(IReadOnlyList<>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IEnumerable<>),
                    typeof(IEnumerable<string>),
                    typeof(IEnumerable<KeyValuePair<string, string>>),
                    typeof(Collection<Guid>),
                    typeof(ICollection<bool>),
                    typeof(ReadOnlyCollection<DateTime>),
                    typeof(IReadOnlyCollection<TimeSpan>),
                    typeof(List<TestClass>),
                    typeof(IList<int?>),
                    typeof(IReadOnlyList<int[]>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => _.IsSystemDictionaryType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsSystemDictionaryType___Should_return_true___When_parameter_type_is_a_System_Dictionary_type()
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
        public static void IsSystemEnumerableType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsSystemEnumerableType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsSystemEnumerableType___Should_return_false___When_parameter_type_is_not_an_Enumerable_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(Collection<>),
                    typeof(ICollection<>),
                    typeof(ReadOnlyCollection<>),
                    typeof(IReadOnlyCollection<>),
                    typeof(List<>),
                    typeof(IList<>),
                    typeof(IReadOnlyList<>),
                    typeof(Dictionary<,>),
                    typeof(IDictionary<,>),
                    typeof(ReadOnlyDictionary<,>),
                    typeof(IReadOnlyDictionary<,>),
                    typeof(ConcurrentDictionary<,>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IDictionary<string, string>),
                    typeof(IReadOnlyDictionary<string, string>),
                    typeof(Dictionary<string, string>),
                    typeof(ReadOnlyDictionary<string, string>),
                    typeof(ConcurrentDictionary<string, string>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                    typeof(ICollection<string>),
                    typeof(IReadOnlyCollection<string>),
                    typeof(Collection<string>),
                    typeof(ReadOnlyCollection<string>),
                    typeof(List<string>),
                    typeof(IList<string>),
                    typeof(IReadOnlyList<string>),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => _.IsSystemEnumerableType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsSystemEnumerableType___Should_return_true___When_parameter_type_is_an_ordered_System_Collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(IEnumerable<>),
                typeof(IEnumerable<string>),
                typeof(IEnumerable<int>),
                typeof(IEnumerable<TestClass>),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemEnumerableType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsSystemKeyValuePairType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsSystemKeyValuePairType(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsSystemKeyValuePairType___Should_return_false___When_parameter_type_is_not_a_System_KeyValuePair()
        {
            // Arrange
            var types = TestTypes.AllTypes
                .Where(_ => !_.Name.StartsWith("KeyValuePair", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            // Act
            var actuals = types.Select(_ => _.IsSystemKeyValuePairType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsSystemKeyValuePairType___Should_return_true___When_parameter_type_is_a_System_KeyValuePair()
        {
            // Arrange
            var types = new[]
            {
                typeof(KeyValuePair<,>),
                typeof(KeyValuePair<object, int>),
                typeof(KeyValuePair<int, object>),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemKeyValuePairType()).ToList();

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
        public static void IsSystemOrderedCollectionType___Should_return_false___When_parameter_type_is_not_an_ordered_System_Collection_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.GenericTypeDefinitions)
                .Except(new[] { typeof(Collection<>), typeof(ReadOnlyCollection<>), typeof(List<>), typeof(IList<>), typeof(IReadOnlyList<>) })
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(List<>).MakeArrayType(),
                    typeof(DerivedGenericClass<>).BaseType,
                    typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType,
                    typeof(BaseGenericClass<,>).GetGenericArguments()[0],
                    typeof(ICollection<>),
                    typeof(IReadOnlyCollection<>),
                    typeof(Dictionary<,>),
                    typeof(IDictionary<,>),
                    typeof(ReadOnlyDictionary<,>),
                    typeof(IReadOnlyDictionary<,>),
                    typeof(ConcurrentDictionary<,>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IEnumerable<>),
                    typeof(IEnumerable<string>),
                    typeof(IDictionary<string, string>),
                    typeof(IReadOnlyDictionary<string, string>),
                    typeof(Dictionary<string, string>),
                    typeof(ReadOnlyDictionary<string, string>),
                    typeof(ConcurrentDictionary<string, string>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                    typeof(ICollection<string>),
                    typeof(IReadOnlyCollection<string>),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => _.IsSystemOrderedCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsSystemOrderedCollectionType___Should_return_true___When_parameter_type_is_an_ordered_System_Collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(List<>).MakeGenericType(typeof(List<>)),
                typeof(GenericClassList<>).BaseType,
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
        public static void IsSystemUnorderedCollectionType___Should_return_false___When_parameter_type_is_not_an_unordered_System_Collection_type()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.GenericTypeDefinitions)
                .Except(new[] { typeof(ICollection<>), typeof(IReadOnlyCollection<>) })
                .Concat(TestTypes.ClosedValueTupleTypes)
                .Concat(TestTypes.ClosedAnonymousTypes)
                .Concat(TestTypes.ClosedStructTypes)
                .Concat(TestTypes.ClosedNullableTypes)
                .Concat(new[]
                {
                    typeof(List<>).MakeArrayType(),
                    typeof(DerivedGenericClass<>).BaseType,
                    typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType,
                    typeof(BaseGenericClass<,>).GetGenericArguments()[0],
                    typeof(List<>).MakeGenericType(typeof(List<>)),
                    typeof(GenericClassList<>).BaseType,
                    typeof(Collection<>),
                    typeof(ReadOnlyCollection<>),
                    typeof(List<>),
                    typeof(IList<>),
                    typeof(IReadOnlyList<>),
                    typeof(Dictionary<,>),
                    typeof(IDictionary<,>),
                    typeof(ReadOnlyDictionary<,>),
                    typeof(IReadOnlyDictionary<,>),
                    typeof(ConcurrentDictionary<,>),
                    typeof(TestClass),
                    typeof(IComparable),
                    typeof(IComparable<string>),
                    typeof(IEnumerable),
                    typeof(IEnumerable<>),
                    typeof(IEnumerable<string>),
                    typeof(IDictionary<string, string>),
                    typeof(IReadOnlyDictionary<string, string>),
                    typeof(Dictionary<string, string>),
                    typeof(ReadOnlyDictionary<string, string>),
                    typeof(ConcurrentDictionary<string, string>),
                    typeof(BaseClassIList<string>),
                    typeof(DerivedClassIList<DateTime?>),
                    typeof(GenericClassList<Guid?>),
                    typeof(NonGenericClassCollection),
                    typeof(IGenericIReadOnlyCollection<bool>),
                    typeof(INonGenericIReadOnlyCollection),
                    typeof(BaseClassIDictionary<DateTime, string>),
                    typeof(DerivedClassIDictionary<TestClass, int>),
                    typeof(GenericClassDictionary<TimeSpan, bool?>),
                    typeof(NonGenericClassDictionary),
                    typeof(IGenericIReadOnlyDictionary<string, TestClass>),
                    typeof(INonGenericIReadOnlyDictionary),
                    typeof(Collection<string>),
                    typeof(ReadOnlyCollection<string>),
                    typeof(List<string>),
                    typeof(IList<string>),
                    typeof(IReadOnlyList<string>),
                })
                .ToArray();

            // Act
            var actuals = types.Select(_ => _.IsSystemUnorderedCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsSystemUnorderedCollectionType___Should_return_true___When_parameter_type_is_an_unordered_System_Collection_type()
        {
            // Arrange
            var types = new[]
            {
                typeof(IReadOnlyCollection<>).MakeGenericType(typeof(IReadOnlyCollection<>)),
                typeof(ICollection<>),
                typeof(IReadOnlyCollection<>),
                typeof(ICollection<string>),
                typeof(IReadOnlyCollection<string>),
            };

            // Act
            var actuals = types.Select(_ => _.IsSystemUnorderedCollectionType()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void IsTypeAssignableToNull___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.IsTypeAssignableToNull(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void IsTypeAssignableToNull___Should_return_false___When_parameter_type_is_not_assignable_to_null()
        {
            // Arrange
            var types = new[]
            {
                typeof(int),
                typeof(Guid),
                typeof(bool),
                typeof(DateTime),
                typeof(GenericClassWithNullableProperty<>).GetGenericArguments().First(),
            };

            // Act
            var actuals = types.Select(_ => _.IsTypeAssignableToNull()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public static void IsTypeAssignableToNull___Should_return_true___When_parameter_type_is_assignable_to_null()
        {
            // Arrange
            var types = new Type[0]
                .Concat(TestTypes.OpenTypes)
                .Except(TestTypes.ClosedValueTupleTypes.Select(_ => _.GetGenericTypeDefinition()))
                .Except(TestTypes.ClosedGenericStructTypes.Select(_ => _.GetGenericTypeDefinition()))
                .Concat(
                    new[]
                    {
                        typeof(int?),
                        typeof(Guid?),
                        typeof(bool?),
                        typeof(DateTime?),
                        typeof(string),
                        typeof(List<string>),
                        typeof(TestClass),
                        typeof(DerivedClassIDictionary<string, string>),
                        typeof(Nullable<>),
                    });

            // Act
            var actuals = types.Select(_ => _.IsTypeAssignableToNull()).ToList();

            // Assert
            actuals.Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public static void MakeGenericTypeOrNull___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.MakeGenericTypeOrNull(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void MakeGenericTypeOrNull___Should_return_null___When_type_cannot_be_created()
        {
            // Arrange
            var types1 = new Type[0]
                .Concat(TestTypes.ClosedTypes)
                .Concat(TestTypes.OpenTypes)
                .ToList();

            var types2 = new[]
            {
                typeof(IReadOnlyDictionary<,>),
                typeof(Dictionary<,>),
            };

            var types3 = new[]
            {
                typeof(IReadOnlyDictionary<string, string>),
                typeof(Dictionary<string, string>),
            };

            var types4 = new[]
            {
                typeof(Nullable<>),
            };

            // Act
            var actuals1 = types1.Select(_ => _.MakeGenericTypeOrNull()).ToList();
            var actuals2a = types2.Select(_ => _.MakeGenericTypeOrNull(typeof(string))).ToList();
            var actuals2b = types2.Select(_ => _.MakeGenericTypeOrNull(typeof(string), typeof(string), typeof(string))).ToList();
            var actuals3 = types3.Select(_ => _.MakeGenericTypeOrNull(typeof(string), typeof(string))).ToList();
            var actuals4a = types4.Select(_ => _.MakeGenericTypeOrNull(typeof(string))).ToList();
            var actuals4b = types4.Select(_ => _.MakeGenericTypeOrNull(null)).ToList();

            // Assert
            actuals1.Should().AllBeEquivalentTo((Type)null);
            actuals2a.Should().AllBeEquivalentTo((Type)null);
            actuals2b.Should().AllBeEquivalentTo((Type)null);
            actuals3.Should().AllBeEquivalentTo((Type)null);
            actuals4a.Should().AllBeEquivalentTo((Type)null);
            actuals4b.Should().AllBeEquivalentTo((Type)null);
        }

        [Fact]
        public static void MakeGenericTypeOrNull___Should_return_constructed_generic_type___When_type_can_be_created()
        {
            // Arrange
            var types = new[]
            {
                typeof(IReadOnlyDictionary<,>),
                typeof(Dictionary<,>),
            };

            var expected = new[]
            {
                typeof(IReadOnlyDictionary<int, string>),
                typeof(Dictionary<int, string>),
            };

            // Act
            var actuals = types.Select(_ => _.MakeGenericTypeOrNull(typeof(int), typeof(string))).ToList();

            // Assert
            actuals.Should().BeEquivalentTo(expected);
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
                new { Type = (first: "one", second: 7).GetType(), Expected = Invariant($"ValueTuple<string, int> || System.ValueTuple<T1, T2> => {ValueTupleAssemblyNameAndVersion} | string => {MsCorLibAssemblyNameAndVersion} | int => {MsCorLibAssemblyNameAndVersion}") },

                // anonymous type:
                new { Type = anonymousObject.GetType(), Expected = Invariant($"{anonymousTypeName}<bool, {innerAnonymousTypeName}<int>> || {anonymousTypeName}<T1, T2> => {ThisAssemblyNameAndVersion} | bool => {MsCorLibAssemblyNameAndVersion} | {innerAnonymousTypeName}<T1> => {ThisAssemblyNameAndVersion} | int => {MsCorLibAssemblyNameAndVersion}") },

                // anonymous type generic type definition:
                new { Type = anonymousObject.GetType().GetGenericTypeDefinition(), Expected = Invariant($"{anonymousTypeName}<T1, T2> || {anonymousTypeName}<T1, T2> => {ThisAssemblyNameAndVersion}") },

                // generic open constructed types:
                new { Type = typeof(DerivedGenericClass<>).BaseType, Expected = Invariant($"BaseGenericClass<string, TDerived> || OBeautifulCode.Type.Recipes.Test.BaseGenericClass<TBase1, TBase2> => {ThisAssemblyNameAndVersion} | string => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType, Expected = Invariant($"OrphanedGenericClass<DerivedGenericClass<TDerived>> || OBeautifulCode.Type.Recipes.Test.OrphanedGenericClass<TOrphaned> => {ThisAssemblyNameAndVersion} | OBeautifulCode.Type.Recipes.Test.DerivedGenericClass<TDerived> => {ThisAssemblyNameAndVersion}") },

                // generic parameter:
                new { Type = typeof(BaseGenericClass<,>).GetGenericArguments()[0], Expected = "TBase1" },

                // generic type definitions:
                new { Type = typeof(IList<>), Expected = Invariant($"IList<T> || System.Collections.Generic.IList<T> => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(List<>), Expected = Invariant($"List<T> || System.Collections.Generic.List<T> => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<,>), Expected = Invariant($"IReadOnlyDictionary<TKey, TValue> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(DerivedGenericClass<>), Expected = Invariant($"DerivedGenericClass<TDerived> || OBeautifulCode.Type.Recipes.Test.DerivedGenericClass<TDerived> => {ThisAssemblyNameAndVersion}") },

                // other types
                new { Type = new DerivedGenericClass<int>[0].GetType(), Expected = Invariant($"DerivedGenericClass<int>[] || OBeautifulCode.Type.Recipes.Test.DerivedGenericClass<TDerived> => {ThisAssemblyNameAndVersion} | int => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(DerivedGenericClass<>.NestedInDerivedGeneric), Expected = Invariant($"DerivedGenericClass<TDerived>.NestedInDerivedGeneric || OBeautifulCode.Type.Recipes.Test.DerivedGenericClass<TDerived>.NestedInDerivedGeneric => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(string), Expected = Invariant($"string || string => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(int), Expected = Invariant($"int || int => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(int?), Expected = Invariant($"int? || int => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(Guid), Expected = Invariant($"Guid || System.Guid => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(Guid?), Expected = Invariant($"Guid? || System.Guid => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(TestClass), Expected = Invariant($"TestClass || OBeautifulCode.Type.Recipes.Test.TestClass => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(TestClassWithNestedClass.NestedInTestClass), Expected = Invariant($"TestClassWithNestedClass.NestedInTestClass || OBeautifulCode.Type.Recipes.Test.TestClassWithNestedClass.NestedInTestClass => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<string, int?>), Expected = Invariant($"IReadOnlyDictionary<string, int?> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibAssemblyNameAndVersion} | string => {MsCorLibAssemblyNameAndVersion} | int => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = Invariant($"IReadOnlyDictionary<string, Guid?> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibAssemblyNameAndVersion} | string => {MsCorLibAssemblyNameAndVersion} | System.Guid => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(string[]), Expected = Invariant($"string[] || string => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(int?[]), Expected = Invariant($"int?[] || int => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(TestClass[]), Expected = Invariant($"TestClass[] || OBeautifulCode.Type.Recipes.Test.TestClass => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(Guid?[]), Expected = Invariant($"Guid?[] || System.Guid => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(IList<int?[]>), Expected = Invariant($"IList<int?[]> || System.Collections.Generic.IList<T> => {MsCorLibAssemblyNameAndVersion} | int => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool?>[]), Expected = Invariant($"IReadOnlyDictionary<TestClass, bool?>[] || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibAssemblyNameAndVersion} | OBeautifulCode.Type.Recipes.Test.TestClass => {ThisAssemblyNameAndVersion} | bool => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<bool[], TestClass>), Expected = Invariant($"IReadOnlyDictionary<bool[], TestClass> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibAssemblyNameAndVersion} | bool => {MsCorLibAssemblyNameAndVersion} | OBeautifulCode.Type.Recipes.Test.TestClass => {ThisAssemblyNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool[]>), Expected = Invariant($"IReadOnlyDictionary<TestClass, bool[]> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibAssemblyNameAndVersion} | OBeautifulCode.Type.Recipes.Test.TestClass => {ThisAssemblyNameAndVersion} | bool => {MsCorLibAssemblyNameAndVersion}") },
                new { Type = typeof(IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]>), Expected = Invariant($"IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]> || System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibAssemblyNameAndVersion} | System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> => {MsCorLibAssemblyNameAndVersion} | System.Guid => {MsCorLibAssemblyNameAndVersion} | int => {MsCorLibAssemblyNameAndVersion} | System.Collections.Generic.IList<T> => {MsCorLibAssemblyNameAndVersion} | System.Collections.Generic.IList<T> => {MsCorLibAssemblyNameAndVersion} | short => {MsCorLibAssemblyNameAndVersion}") },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.ToStringReadable(ToStringReadableOptions.IncludeAssemblyDetails)).ToList();

            // Assert
            typesAndExpected.Select(_ => _.Expected).Should().Equal(actuals);
        }

        [Fact]
        public static void ToStringWithoutGenericComponent___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.ToStringWithoutGenericComponent(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void ToStringWithoutGenericComponent___Should_return_string_representation_of_type_without_generic_component___When_called()
        {
            // Arrange
            var innerAnonymousObject = new { InnerAnonymous = 6 };

            var anonymousObject = new { Anonymous = true, Inner = innerAnonymousObject };
            var anonymousTypeName = new Regex("AnonymousType\\d*").Match(anonymousObject.GetType().Name).ToString();

            var typesAndExpected = new[]
            {
                new { Type = anonymousObject.GetType(), Expected = anonymousTypeName },
                new { Type = anonymousObject.GetType().GetGenericTypeDefinition(), Expected = anonymousTypeName },
                new { Type = typeof(DerivedGenericClass<>).BaseType, Expected = "BaseGenericClass" },
                new { Type = typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType, Expected = "OrphanedGenericClass" },
                new { Type = typeof(BaseGenericClass<,>).GetGenericArguments()[0], Expected = "TBase1" },
                new { Type = typeof(DerivedGenericClass<>), Expected = "DerivedGenericClass" },
                new { Type = new DerivedGenericClass<int>[0].GetType(), Expected = "DerivedGenericClass[]" },
                new { Type = typeof(DerivedGenericClass<>.NestedInDerivedGeneric), Expected = "NestedInDerivedGeneric" },
                new { Type = typeof(string), Expected = "string" },
                new { Type = typeof(int), Expected = "int" },
                new { Type = typeof(int?), Expected = "Nullable" },
                new { Type = typeof(Guid), Expected = "Guid" },
                new { Type = typeof(Guid?), Expected = "Nullable" },
                new { Type = typeof(TestClass), Expected = "TestClass" },
                new { Type = typeof(TestClassWithNestedClass.NestedInTestClass), Expected = "NestedInTestClass" },
                new { Type = typeof(IReadOnlyDictionary<string, int?>), Expected = "IReadOnlyDictionary" },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = "IReadOnlyDictionary" },
                new { Type = typeof(string[]), Expected = "string[]" },
                new { Type = typeof(int?[]), Expected = "Nullable[]" },
                new { Type = typeof(TestClass[]), Expected = "TestClass[]" },
                new { Type = typeof(Guid?[]), Expected = "Nullable[]" },
                new { Type = typeof(IList<int?[]>), Expected = "IList" },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool?>[]), Expected = "IReadOnlyDictionary[]" },
                new { Type = typeof(IReadOnlyDictionary<bool[], TestClass>), Expected = "IReadOnlyDictionary" },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool[]>), Expected = "IReadOnlyDictionary" },
                new { Type = typeof(IList<>), Expected = "IList" },
                new { Type = typeof(List<>), Expected = "List" },
                new { Type = typeof(IReadOnlyDictionary<,>), Expected = "IReadOnlyDictionary" },
                new { Type = typeof(IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]>), Expected = "IReadOnlyDictionary" },
                new { Type = (first: "one", second: 10).GetType(), Expected = "ValueTuple" },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.ToStringWithoutGenericComponent()).ToList();

            // Assert
            typesAndExpected.Select(_ => _.Expected).Should().Equal(actuals);
        }

        [Fact]
        public static void ToStringXmlDoc___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.ToStringXmlDoc(null, throwIfNoCompatibleStringExists: A.Dummy<bool>(), A.Dummy<ToStringXmlDocOptions>()));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void ToStringXmlDoc___Should_throw_NotSupportedException___When_parameter_throwIfNoCompatibleStringExists_is_true_and_parameter_type_is_an_anonymous_type()
        {
            // Arrange
            var innerAnonymousObject = new { InnerAnonymous = 6 };

            var anonymousObject = new { Anonymous = true, Inner = innerAnonymousObject };

            var types = new[]
            {
                new { Anonymous = true }.GetType(),
                anonymousObject.GetType(),
                anonymousObject.GetType().GetGenericTypeDefinition(),
            };

            // Act
            var actuals = types.Select(_ => Record.Exception(() => _.ToStringXmlDoc(throwIfNoCompatibleStringExists: true))).ToList();

            // Assert
            actuals.Should().AllBeOfType<NotSupportedException>();
            actuals.Select(_ => _.Message.Should().Be("Anonymous types are not supported.")).ToList();
        }

        [Fact]
        public static void ToStringXmlDoc___Should_return_null___When_parameter_throwIfNoCompatibleStringExists_is_false_and_parameter_type_is_an_anonymous_type()
        {
            // Arrange
            var innerAnonymousObject = new { InnerAnonymous = 6 };

            var anonymousObject = new { Anonymous = true, Inner = innerAnonymousObject };

            var types = new[]
            {
                new { Anonymous = true }.GetType(),
                anonymousObject.GetType(),
                anonymousObject.GetType().GetGenericTypeDefinition(),
            };

            // Act
            var actuals = types.Select(_ => Record.Exception(() => _.ToStringXmlDoc(throwIfNoCompatibleStringExists: false))).ToList();

            // Assert
            actuals.Select(_ => _.Should().BeNull()).ToList();
        }

        [Fact]
        public static void ToStringXmlDoc___Should_throw_NotSupportedException___When_parameter_throwIfNoCompatibleStringExists_is_true_and_type_is_a_generic_parameter()
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
            var actuals = types.Select(_ => Record.Exception(() => _.ToStringXmlDoc(throwIfNoCompatibleStringExists: true))).ToList();

            // Assert
            actuals.Should().AllBeOfType<NotSupportedException>();
            actuals.Select(_ => _.Message.Should().Be("Generic parameters not supported.")).ToList();
        }

        [Fact]
        public static void ToStringXmlDoc___Should_return_null___When_parameter_throwIfNoCompatibleStringExists_is_false_and_type_is_a_generic_parameter()
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
            var actuals = types.Select(_ => Record.Exception(() => _.ToStringXmlDoc(throwIfNoCompatibleStringExists: false))).ToList();

            // Assert
            actuals.Select(_ => _.Should().BeNull()).ToList();
        }

        [Fact]
        public static void ToStringXmlDoc___Should_return_xml_doc_compatible_representation_of_the_specified_type___When_parameter_options_is_None()
        {
            // Arrange
            var typesAndExpected = new[]
            {
                // value tuple:
                new { Type = (first: "one", second: 7).GetType(), Expected = "ValueTuple{String, Int32}" },

                // generic open constructed types:
                new { Type = typeof(DerivedGenericClass<>).BaseType, Expected = "BaseGenericClass{String, TDerived}" },
                new { Type = typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType, Expected = "OrphanedGenericClass{DerivedGenericClass}" },

                // generic type definitions:
                new { Type = typeof(IList<>), Expected = "IList{T}" },
                new { Type = typeof(List<>), Expected = "List{T}" },
                new { Type = typeof(IReadOnlyDictionary<,>), Expected = "IReadOnlyDictionary{TKey, TValue}" },
                new { Type = typeof(DerivedGenericClass<>), Expected = "DerivedGenericClass{TDerived}" },

                // other types
                new { Type = new DerivedGenericClass<int>[0].GetType(), Expected = "Array" },

                new { Type = typeof(DerivedGenericClass<>.NestedInDerivedGeneric), Expected = "DerivedGenericClass{TDerived}.NestedInDerivedGeneric" },
                new { Type = typeof(string), Expected = "string" },
                new { Type = typeof(int), Expected = "int" },
                new { Type = typeof(int?), Expected = "Nullable{Int32}" },
                new { Type = typeof(Guid), Expected = "Guid" },
                new { Type = typeof(Guid?), Expected = "Nullable{Guid}" },
                new { Type = typeof(TestClass), Expected = "TestClass" },

                new { Type = typeof(TestClassWithNestedClass.NestedInTestClass), Expected = "TestClassWithNestedClass.NestedInTestClass" },
                new { Type = typeof(IReadOnlyDictionary<string, int?>), Expected = "IReadOnlyDictionary{String, Nullable}" },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = "IReadOnlyDictionary{String, Nullable}" },
                new { Type = typeof(string[]), Expected = "Array" },
                new { Type = typeof(int?[]), Expected = "Array" },
                new { Type = typeof(TestClass[]), Expected = "Array" },
                new { Type = typeof(Guid?[]), Expected = "Array" },
                new { Type = typeof(IList<int?[]>), Expected = "IList{Array}" },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool?>[]), Expected = "Array" },
                new { Type = typeof(IReadOnlyDictionary<bool[], TestClass>), Expected = "IReadOnlyDictionary{Array, TestClass}" },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool[]>), Expected = "IReadOnlyDictionary{TestClass, Array}" },
                new { Type = typeof(IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>>), Expected = "IReadOnlyDictionary{IReadOnlyDictionary, IList}" },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.ToStringXmlDoc(throwIfNoCompatibleStringExists: true, ToStringXmlDocOptions.None)).ToList();

            // Assert
            typesAndExpected.Select(_ => _.Expected).Should().Equal(actuals);
        }

        [Fact]
        public static void ToStringXmlDoc___Should_return_xml_doc_compatible_representation_of_the_specified_type_with_namespaces_included___When_parameter_options_is_IncludeNamespace()
        {
            // Arrange
            var typesAndExpected = new[]
            {
                // value tuple:
                new { Type = (first: "one", second: 7).GetType(), Expected = "System.ValueTuple{String, Int32}" },

                // generic open constructed types:
                new { Type = typeof(DerivedGenericClass<>).BaseType, Expected = "OBeautifulCode.Type.Recipes.Test.BaseGenericClass{String, TDerived}" },
                new { Type = typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType, Expected = "OBeautifulCode.Type.Recipes.Test.OrphanedGenericClass{DerivedGenericClass}" },

                // generic type definitions:
                new { Type = typeof(IList<>), Expected = "System.Collections.Generic.IList{T}" },
                new { Type = typeof(List<>), Expected = "System.Collections.Generic.List{T}" },
                new { Type = typeof(IReadOnlyDictionary<,>), Expected = "System.Collections.Generic.IReadOnlyDictionary{TKey, TValue}" },
                new { Type = typeof(DerivedGenericClass<>), Expected = "OBeautifulCode.Type.Recipes.Test.DerivedGenericClass{TDerived}" },

                // other types
                new { Type = new DerivedGenericClass<int>[0].GetType(), Expected = "System.Array" },

                new { Type = typeof(DerivedGenericClass<>.NestedInDerivedGeneric), Expected = "OBeautifulCode.Type.Recipes.Test.DerivedGenericClass{TDerived}.NestedInDerivedGeneric" },
                new { Type = typeof(string), Expected = "string" },
                new { Type = typeof(int), Expected = "int" },
                new { Type = typeof(int?), Expected = "System.Nullable{Int32}" },
                new { Type = typeof(Guid), Expected = "System.Guid" },
                new { Type = typeof(Guid?), Expected = "System.Nullable{Guid}" },
                new { Type = typeof(TestClass), Expected = "OBeautifulCode.Type.Recipes.Test.TestClass" },

                new { Type = typeof(TestClassWithNestedClass.NestedInTestClass), Expected = "OBeautifulCode.Type.Recipes.Test.TestClassWithNestedClass.NestedInTestClass" },
                new { Type = typeof(IReadOnlyDictionary<string, int?>), Expected = "System.Collections.Generic.IReadOnlyDictionary{String, Nullable}" },
                new { Type = typeof(IReadOnlyDictionary<string, Guid?>), Expected = "System.Collections.Generic.IReadOnlyDictionary{String, Nullable}" },
                new { Type = typeof(string[]), Expected = "System.Array" },
                new { Type = typeof(int?[]), Expected = "System.Array" },
                new { Type = typeof(TestClass[]), Expected = "System.Array" },
                new { Type = typeof(Guid?[]), Expected = "System.Array" },
                new { Type = typeof(IList<int?[]>), Expected = "System.Collections.Generic.IList{Array}" },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool?>[]), Expected = "System.Array" },
                new { Type = typeof(IReadOnlyDictionary<bool[], TestClass>), Expected = "System.Collections.Generic.IReadOnlyDictionary{Array, TestClass}" },
                new { Type = typeof(IReadOnlyDictionary<TestClass, bool[]>), Expected = "System.Collections.Generic.IReadOnlyDictionary{TestClass, Array}" },
                new { Type = typeof(IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>>), Expected = "System.Collections.Generic.IReadOnlyDictionary{IReadOnlyDictionary, IList}" },
            };

            // Act
            var actuals = typesAndExpected.Select(_ => _.Type.ToStringXmlDoc(throwIfNoCompatibleStringExists: true, ToStringXmlDocOptions.IncludeNamespace)).ToList();

            // Assert
            typesAndExpected.Select(_ => _.Expected).Should().Equal(actuals);
        }

        [Fact]
        public static void TryMakeGenericType___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => TypeExtensions.TryMakeGenericType(null, out Type result));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("type");
        }

        [Fact]
        public static void TryMakeGenericType___Should_return_false_and_set_genericType_to_null___When_type_cannot_be_created()
        {
            // Arrange
            var types1 = new Type[0]
                .Concat(TestTypes.ClosedTypes)
                .Concat(TestTypes.OpenTypes)
                .ToList();

            var types2 = new[]
            {
                typeof(IReadOnlyDictionary<,>),
                typeof(Dictionary<,>),
            };

            var types3 = new[]
            {
                typeof(IReadOnlyDictionary<string, string>),
                typeof(Dictionary<string, string>),
            };

            var types4 = new[]
            {
                typeof(Nullable<>),
            };

            // Act, Assert
            foreach (var type in types1)
            {
                var actual = type.TryMakeGenericType(out Type genericType);

                actual.Should().BeFalse();
                genericType.Should().BeNull();
            }

            foreach (var type in types2)
            {
                var actual = type.TryMakeGenericType(out Type genericType, typeof(string));

                actual.Should().BeFalse();
                genericType.Should().BeNull();
            }

            foreach (var type in types2)
            {
                var actual = type.TryMakeGenericType(out Type genericType, typeof(string), typeof(string), typeof(string));

                actual.Should().BeFalse();
                genericType.Should().BeNull();
            }

            foreach (var type in types3)
            {
                var actual = type.TryMakeGenericType(out Type genericType, typeof(string), typeof(string));

                actual.Should().BeFalse();
                genericType.Should().BeNull();
            }

            foreach (var type in types4)
            {
                var actual = type.TryMakeGenericType(out Type genericType, typeof(string));

                actual.Should().BeFalse();
                genericType.Should().BeNull();
            }

            foreach (var type in types4)
            {
                var actual = type.TryMakeGenericType(out Type genericType, null);

                actual.Should().BeFalse();
                genericType.Should().BeNull();
            }
        }

        [Fact]
        public static void TryMakeGenericType___Should_return_true_and_set_genericType_to_constructed_type___When_type_can_be_created()
        {
            // Arrange
            var types = new[]
            {
                typeof(IReadOnlyDictionary<,>),
                typeof(Dictionary<,>),
            };

            var expected = new[]
            {
                typeof(IReadOnlyDictionary<int, string>),
                typeof(Dictionary<int, string>),
            };

            // Act, Assert
            for (var x = 0; x < types.Length; x++)
            {
                var actual = types[x].TryMakeGenericType(out Type genericType, typeof(int), typeof(string));

                actual.Should().BeTrue();
                genericType.Should().Be(expected[x]);
            }
        }
    }
}
