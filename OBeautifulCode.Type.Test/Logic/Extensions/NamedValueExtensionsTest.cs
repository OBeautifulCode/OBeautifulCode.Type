// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamedValueExtensionsTest.cs" company="OBeautifulCode">
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
    using Xunit;

    public static class NamedValueExtensionsTest
    {
        [Fact]
        public static void DeepCloneWithAdditionalValue___Should_throw_ArgumentNullException___When_parameter_source_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ((IReadOnlyList<NamedValue<int>>)null).DeepCloneWithAdditionalValue(A.Dummy<NamedValue<int>>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("source");
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Addis", Justification = ObcSuppressBecause.CA1702_CompoundWordsShouldBeCasedCorrectly_AnalyzerIsIncorrectlyDetectingCompoundWords)]
        public static void DeepCloneWithAdditionalValue___Should_throw_ArgumentNullException___When_parameter_valueToAdd_is_null()
        {
            // Arrange
            var source = new List<NamedValue<int>>
            {
                A.Dummy<NamedValue<int>>(),
                A.Dummy<NamedValue<int>>(),
            };

            // Act
            var actual = Record.Exception(() => source.DeepCloneWithAdditionalValue(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("valueToAdd");
        }

        [Fact]
        public static void DeepCloneWithAdditionalValue___Should_deep_clone_collection_and_add_valueToAdd___When_called()
        {
            // Arrange
            var source = new List<NamedValue<Version>>
            {
                A.Dummy<NamedValue<Version>>(),
                A.Dummy<NamedValue<Version>>(),
            };

            var valueToAdd = A.Dummy<NamedValue<Version>>();

            var expected = source
                .Concat(new[] { valueToAdd })
                .ToList();

            // Act
            var actual = source.DeepCloneWithAdditionalValue(valueToAdd).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
            actual[0].AsTest().Must().NotBeSameReferenceAs(expected[0]);
            actual[1].AsTest().Must().NotBeSameReferenceAs(expected[1]);
        }

        [Fact]
        public static void GetNames_IReadOnlyList___Should_throw_ArgumentNullException___When_parameter_source_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ((IReadOnlyList<NamedValue<int>>)null).GetNames());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("source");
        }

        [Fact]
        public static void GetNames_IReadOnlyList___Should_throw_ArgumentException___When_parameter_source_contains_a_null_element()
        {
            // Arrange
            var source = new List<NamedValue<int>>
            {
                A.Dummy<NamedValue<int>>(),
                null,
                A.Dummy<NamedValue<int>>(),
            };

            // Act
            var actual = Record.Exception(() => source.GetNames());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("source contains a null element");
        }

        [Fact]
        public static void GetNames_IReadOnlyList___Should_return_empty_collection___When_parameter_source_is_empty()
        {
            // Arrange
            var source = new List<NamedValue<int>>();

            // Act
            var actual = source.GetNames();

            // Assert
            actual.AsTest().Must().BeEmptyEnumerable();
        }

        [Fact]
        public static void GetNames_IReadOnlyList___Should_return_names_in_order___When_called()
        {
            // Arrange
            var source1 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", A.Dummy<int>()),
            };

            var source2 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", A.Dummy<int>()),
                new NamedValue<int>("name-1", A.Dummy<int>()),
            };

            var source3 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", A.Dummy<int>()),
                new NamedValue<int>("name-2", A.Dummy<int>()),
            };

            var source4 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", A.Dummy<int>()),
                new NamedValue<int>("name-2", A.Dummy<int>()),
                new NamedValue<int>("name-3", A.Dummy<int>()),
                new NamedValue<int>("name-1", A.Dummy<int>()),
            };

            var expected1 = new[]
            {
                "name-1",
            };

            var expected2 = new[]
            {
                "name-1",
                "name-1",
            };

            var expected3 = new[]
            {
                "name-1",
                "name-2",
            };

            var expected4 = new[]
            {
                "name-1",
                "name-2",
                "name-3",
                "name-1",
            };

            // Act
            var actual1 = source1.GetNames().ToArray();
            var actual2 = source2.GetNames().ToArray();
            var actual3 = source3.GetNames().ToArray();
            var actual4 = source4.GetNames().ToArray();

            // Assert
            actual1.AsTest().Must().BeEqualTo(expected1);
            actual2.AsTest().Must().BeEqualTo(expected2);
            actual3.AsTest().Must().BeEqualTo(expected3);
            actual4.AsTest().Must().BeEqualTo(expected4);
        }

        [Fact]
        public static void GetNames_IReadOnlyCollection___Should_throw_ArgumentNullException___When_parameter_source_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ((IReadOnlyCollection<NamedValue<int>>)null).GetNames());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("source");
        }

        [Fact]
        public static void GetNames_IReadOnlyCollection___Should_throw_ArgumentException___When_parameter_source_contains_a_null_element()
        {
            // Arrange
            IReadOnlyCollection<NamedValue<int>> source = new List<NamedValue<int>>
            {
                A.Dummy<NamedValue<int>>(),
                null,
                A.Dummy<NamedValue<int>>(),
            };

            // Act
            var actual = Record.Exception(() => source.GetNames());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("source contains a null element");
        }

        [Fact]
        public static void GetNames_IReadOnlyCollection___Should_return_empty_collection___When_parameter_source_is_empty()
        {
            // Arrange
            IReadOnlyCollection<NamedValue<int>> source = new List<NamedValue<int>>();

            // Act
            var actual = source.GetNames();

            // Assert
            actual.AsTest().Must().BeEmptyEnumerable();
        }

        [Fact]
        public static void GetNames_IReadOnlyCollection___Should_return_names_in_order___When_called()
        {
            // Arrange
            IReadOnlyCollection<NamedValue<int>> source1 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", A.Dummy<int>()),
            };

            IReadOnlyCollection<NamedValue<int>> source2 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", A.Dummy<int>()),
                new NamedValue<int>("name-1", A.Dummy<int>()),
            };

            IReadOnlyCollection<NamedValue<int>> source3 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", A.Dummy<int>()),
                new NamedValue<int>("name-2", A.Dummy<int>()),
            };

            IReadOnlyCollection<NamedValue<int>> source4 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", A.Dummy<int>()),
                new NamedValue<int>("name-2", A.Dummy<int>()),
                new NamedValue<int>("name-3", A.Dummy<int>()),
                new NamedValue<int>("name-1", A.Dummy<int>()),
            };

            var expected1 = new[]
            {
                "name-1",
            };

            var expected2 = new[]
            {
                "name-1",
                "name-1",
            };

            var expected3 = new[]
            {
                "name-1",
                "name-2",
            };

            var expected4 = new[]
            {
                "name-1",
                "name-2",
                "name-3",
                "name-1",
            };

            // Act
            var actual1 = source1.GetNames().ToArray();
            var actual2 = source2.GetNames().ToArray();
            var actual3 = source3.GetNames().ToArray();
            var actual4 = source4.GetNames().ToArray();

            // Assert
            actual1.AsTest().Must().BeEqualTo(expected1);
            actual2.AsTest().Must().BeEqualTo(expected2);
            actual3.AsTest().Must().BeEqualTo(expected3);
            actual4.AsTest().Must().BeEqualTo(expected4);
        }

        [Fact]
        public static void GetValues_IReadOnlyList___Should_throw_ArgumentNullException___When_parameter_source_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ((IReadOnlyList<NamedValue<int>>)null).GetValues());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("source");
        }

        [Fact]
        public static void GetValues_IReadOnlyList___Should_throw_ArgumentException___When_parameter_source_contains_a_null_element()
        {
            // Arrange
            var source = new List<NamedValue<int>>
            {
                A.Dummy<NamedValue<int>>(),
                null,
                A.Dummy<NamedValue<int>>(),
            };

            // Act
            var actual = Record.Exception(() => source.GetValues());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("source contains a null element");
        }

        [Fact]
        public static void GetValues_IReadOnlyList___Should_return_empty_collection___When_parameter_source_is_empty()
        {
            // Arrange
            var source = new List<NamedValue<int>>();

            // Act
            var actual = source.GetValues();

            // Assert
            actual.AsTest().Must().BeEmptyEnumerable();
        }

        [Fact]
        public static void GetValues_IReadOnlyList___Should_return_names_in_order___When_called()
        {
            // Arrange
            var source1 = new List<NamedValue<int>>
            {
                new NamedValue<int>(A.Dummy<string>(), 1),
            };

            var source2 = new List<NamedValue<int>>
            {
                new NamedValue<int>(A.Dummy<string>(), 1),
                new NamedValue<int>(A.Dummy<string>(), 1),
            };

            var source3 = new List<NamedValue<int>>
            {
                new NamedValue<int>(A.Dummy<string>(), 1),
                new NamedValue<int>(A.Dummy<string>(), 2),
            };

            var source4 = new List<NamedValue<int>>
            {
                new NamedValue<int>(A.Dummy<string>(), 1),
                new NamedValue<int>(A.Dummy<string>(), 2),
                new NamedValue<int>(A.Dummy<string>(), 3),
                new NamedValue<int>(A.Dummy<string>(), 1),
            };

            var expected1 = new[]
            {
                1,
            };

            var expected2 = new[]
            {
                1,
                1,
            };

            var expected3 = new[]
            {
                1,
                2,
            };

            var expected4 = new[]
            {
                1,
                2,
                3,
                1,
            };

            // Act
            var actual1 = source1.GetValues().ToArray();
            var actual2 = source2.GetValues().ToArray();
            var actual3 = source3.GetValues().ToArray();
            var actual4 = source4.GetValues().ToArray();

            // Assert
            actual1.AsTest().Must().BeEqualTo(expected1);
            actual2.AsTest().Must().BeEqualTo(expected2);
            actual3.AsTest().Must().BeEqualTo(expected3);
            actual4.AsTest().Must().BeEqualTo(expected4);
        }

        [Fact]
        public static void GetValues_IReadOnlyCollection___Should_throw_ArgumentNullException___When_parameter_source_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ((IReadOnlyCollection<NamedValue<int>>)null).GetValues());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("source");
        }

        [Fact]
        public static void GetValues_IReadOnlyCollection___Should_throw_ArgumentException___When_parameter_source_contains_a_null_element()
        {
            // Arrange
            IReadOnlyCollection<NamedValue<int>> source = new List<NamedValue<int>>
            {
                A.Dummy<NamedValue<int>>(),
                null,
                A.Dummy<NamedValue<int>>(),
            };

            // Act
            var actual = Record.Exception(() => source.GetValues());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("source contains a null element");
        }

        [Fact]
        public static void GetValues_IReadOnlyCollection___Should_return_empty_collection___When_parameter_source_is_empty()
        {
            // Arrange
            IReadOnlyCollection<NamedValue<int>> source = new List<NamedValue<int>>();

            // Act
            var actual = source.GetValues();

            // Assert
            actual.AsTest().Must().BeEmptyEnumerable();
        }

        [Fact]
        public static void GetValues_IReadOnlyCollection___Should_return_names_in_order___When_called()
        {
            // Arrange
            IReadOnlyCollection<NamedValue<int>> source1 = new List<NamedValue<int>>
            {
                new NamedValue<int>(A.Dummy<string>(), 1),
            };

            IReadOnlyCollection<NamedValue<int>> source2 = new List<NamedValue<int>>
            {
                new NamedValue<int>(A.Dummy<string>(), 1),
                new NamedValue<int>(A.Dummy<string>(), 1),
            };

            IReadOnlyCollection<NamedValue<int>> source3 = new List<NamedValue<int>>
            {
                new NamedValue<int>(A.Dummy<string>(), 1),
                new NamedValue<int>(A.Dummy<string>(), 2),
            };

            IReadOnlyCollection<NamedValue<int>> source4 = new List<NamedValue<int>>
            {
                new NamedValue<int>(A.Dummy<string>(), 1),
                new NamedValue<int>(A.Dummy<string>(), 2),
                new NamedValue<int>(A.Dummy<string>(), 3),
                new NamedValue<int>(A.Dummy<string>(), 1),
            };

            var expected1 = new[]
            {
                1,
            };

            var expected2 = new[]
            {
                1,
                1,
            };

            var expected3 = new[]
            {
                1,
                2,
            };

            var expected4 = new[]
            {
                1,
                2,
                3,
                1,
            };

            // Act
            var actual1 = source1.GetValues().ToArray();
            var actual2 = source2.GetValues().ToArray();
            var actual3 = source3.GetValues().ToArray();
            var actual4 = source4.GetValues().ToArray();

            // Assert
            actual1.AsTest().Must().BeEqualTo(expected1);
            actual2.AsTest().Must().BeEqualTo(expected2);
            actual3.AsTest().Must().BeEqualTo(expected3);
            actual4.AsTest().Must().BeEqualTo(expected4);
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyList___Should_throw_ArgumentNullException___When_parameter_source_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ((IReadOnlyList<NamedValue<int>>)null).GetSingleValue(A.Dummy<string>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("source");
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyList___Should_throw_ArgumentException___When_parameter_source_contains_a_null_element()
        {
            // Arrange
            var source = new List<NamedValue<int>>
            {
                A.Dummy<NamedValue<int>>(),
                null,
                A.Dummy<NamedValue<int>>(),
            };

            // Act
            var actual = Record.Exception(() => source.GetSingleValue(A.Dummy<string>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("source contains a null element");
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyList___Should_throw_ArgumentNullException___When_parameter_name_is_null()
        {
            // Arrange
            var source = Some.ReadOnlyDummies<NamedValue<string>>().ToList();

            // Act
            var actual = Record.Exception(() => source.GetSingleValue(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("name");
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyList___Should_throw_ArgumentException___When_parameter_name_is_white_space()
        {
            // Arrange
            var source = Some.ReadOnlyDummies<NamedValue<string>>().ToList();

            // Act
            var actual = Record.Exception(() => source.GetSingleValue(" \r\n  "));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("name is white space");
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyList___Should_throw_InvalidOperationException___When_name_not_found()
        {
            // Arrange
            var source = Some.ReadOnlyDummies<NamedValue<string>>().ToList();

            // Act
            var actual = Record.Exception(() => source.GetSingleValue(A.Dummy<string>()));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyList___Should_throw_InvalidOperationException___When_name_found_multiple_times()
        {
            // Arrange
            var source1 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", 1),
                new NamedValue<int>("name-1", 1),
            };

            var source2 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-2", 2),
                new NamedValue<int>("name-1", 1),
                new NamedValue<int>("name-3", 3),
                new NamedValue<int>("name-1", 1),
            };

            // Act
            var actual1 = Record.Exception(() => source1.GetSingleValue(A.Dummy<string>()));
            var actual2 = Record.Exception(() => source2.GetSingleValue(A.Dummy<string>()));

            // Assert
            actual1.AsTest().Must().BeOfType<InvalidOperationException>();
            actual2.AsTest().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyList___Should_return_single_value___When_called()
        {
            // Arrange
            var source1 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", 1),
            };

            var source2 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", 1),
                new NamedValue<int>("name-2", 2),
            };

            var source3 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", 1),
                new NamedValue<int>("name-2", 2),
                new NamedValue<int>("name-3", 3),
                new NamedValue<int>("name-1", 1),
            };

            // Act
            var actual1 = source1.GetSingleValue("name-1");
            var actual2 = source2.GetSingleValue("name-2");
            var actual3 = source3.GetSingleValue("name-3");

            // Assert
            actual1.AsTest().Must().BeEqualTo(1);
            actual2.AsTest().Must().BeEqualTo(2);
            actual3.AsTest().Must().BeEqualTo(3);
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyCollection___Should_throw_ArgumentNullException___When_parameter_source_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ((IReadOnlyCollection<NamedValue<int>>)null).GetSingleValue(A.Dummy<string>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("source");
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyCollection___Should_throw_ArgumentException___When_parameter_source_contains_a_null_element()
        {
            // Arrange
            IReadOnlyCollection<NamedValue<int>> source = new List<NamedValue<int>>
            {
                A.Dummy<NamedValue<int>>(),
                null,
                A.Dummy<NamedValue<int>>(),
            };

            // Act
            var actual = Record.Exception(() => source.GetSingleValue(A.Dummy<string>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("source contains a null element");
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyCollection___Should_throw_ArgumentNullException___When_parameter_name_is_null()
        {
            // Arrange
            IReadOnlyCollection<NamedValue<string>> source = Some.ReadOnlyDummies<NamedValue<string>>().ToList();

            // Act
            var actual = Record.Exception(() => source.GetSingleValue(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("name");
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyCollection___Should_throw_ArgumentException___When_parameter_name_is_white_space()
        {
            // Arrange
            IReadOnlyCollection<NamedValue<string>> source = Some.ReadOnlyDummies<NamedValue<string>>().ToList();

            // Act
            var actual = Record.Exception(() => source.GetSingleValue(" \r\n  "));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("name is white space");
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyCollection___Should_throw_InvalidOperationException___When_name_not_found()
        {
            // Arrange
            IReadOnlyCollection<NamedValue<string>> source = Some.ReadOnlyDummies<NamedValue<string>>().ToList();

            // Act
            var actual = Record.Exception(() => source.GetSingleValue(A.Dummy<string>()));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyCollection___Should_throw_InvalidOperationException___When_name_found_multiple_times()
        {
            // Arrange
            IReadOnlyCollection<NamedValue<int>> source1 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", 1),
                new NamedValue<int>("name-1", 1),
            };

            IReadOnlyCollection<NamedValue<int>> source2 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-2", 2),
                new NamedValue<int>("name-1", 1),
                new NamedValue<int>("name-3", 3),
                new NamedValue<int>("name-1", 1),
            };

            // Act
            var actual1 = Record.Exception(() => source1.GetSingleValue(A.Dummy<string>()));
            var actual2 = Record.Exception(() => source2.GetSingleValue(A.Dummy<string>()));

            // Assert
            actual1.AsTest().Must().BeOfType<InvalidOperationException>();
            actual2.AsTest().Must().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public static void GetSingleValue_IReadOnlyCollection___Should_return_single_value___When_called()
        {
            // Arrange
            IReadOnlyCollection<NamedValue<int>> source1 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", 1),
            };

            IReadOnlyCollection<NamedValue<int>> source2 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", 1),
                new NamedValue<int>("name-2", 2),
            };

            IReadOnlyCollection<NamedValue<int>> source3 = new List<NamedValue<int>>
            {
                new NamedValue<int>("name-1", 1),
                new NamedValue<int>("name-2", 2),
                new NamedValue<int>("name-3", 3),
                new NamedValue<int>("name-1", 1),
            };

            // Act
            var actual1 = source1.GetSingleValue("name-1");
            var actual2 = source2.GetSingleValue("name-2");
            var actual3 = source3.GetSingleValue("name-3");

            // Assert
            actual1.AsTest().Must().BeEqualTo(1);
            actual2.AsTest().Must().BeEqualTo(2);
            actual3.AsTest().Must().BeEqualTo(3);
        }
    }
}