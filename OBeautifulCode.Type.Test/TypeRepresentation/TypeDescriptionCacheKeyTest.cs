// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeDescriptionCacheKeyTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System.Collections.Generic;
    using FakeItEasy;

    using FluentAssertions;
    using OBeautifulCode.AutoFakeItEasy;
    using Xunit;

    public static class TypeDescriptionCacheKeyTest
    {
        [Fact]
        public static void Functions_as_dictionary_key()
        {
            // Arrange
            var dictionary = new Dictionary<TypeDescriptionCacheKey, string>();
            var key1 = A.Dummy<TypeDescriptionCacheKey>();
            var key1a = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), key1.TypeMatchStrategy, key1.MultipleMatchStrategy);
            var key1b = new TypeDescriptionCacheKey(key1.TypeDescription, A.Dummy<TypeMatchStrategy>().ThatIsNot(key1.TypeMatchStrategy), key1.MultipleMatchStrategy);
            var key1c = new TypeDescriptionCacheKey(key1.TypeDescription, key1.TypeMatchStrategy, A.Dummy<MultipleMatchStrategy>().ThatIsNot(key1.MultipleMatchStrategy));
            var key2 = A.Dummy<TypeDescriptionCacheKey>();

            // Act
            dictionary.Add(key1, nameof(key1));
            dictionary.Add(key1a, nameof(key1a));
            dictionary.Add(key1b, nameof(key1b));
            dictionary.Add(key1c, nameof(key1c));
            dictionary.Add(key2, nameof(key2));

            // Assert
            dictionary[key1].Should().Be(nameof(key1));
            dictionary[key1a].Should().Be(nameof(key1a));
            dictionary[key1b].Should().Be(nameof(key1b));
            dictionary[key1c].Should().Be(nameof(key1c));
            dictionary[key2].Should().Be(nameof(key2));
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
        {
            // Arrange
            TypeDescriptionCacheKey typeDescriptionCacheKey1 = null;
            TypeDescriptionCacheKey typeDescriptionCacheKey2 = null;

            // Act
            var result = typeDescriptionCacheKey1 == typeDescriptionCacheKey2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            TypeDescriptionCacheKey typeDescriptionCacheKey1 = null;
            var typeDescriptionCacheKey2 = A.Dummy<TypeDescriptionCacheKey>();

            // Act
            var result1 = typeDescriptionCacheKey1 == typeDescriptionCacheKey2;
            var result2 = typeDescriptionCacheKey2 == typeDescriptionCacheKey1;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange
            var typeDescriptionCacheKey = A.Dummy<TypeDescriptionCacheKey>();

            // Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result = typeDescriptionCacheKey == typeDescriptionCacheKey;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void EqualsOperator___Should_return_false___When_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var typeDescriptionCacheKey1A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey1B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey1A.TypeMatchStrategy, typeDescriptionCacheKey1A.MultipleMatchStrategy);

            var typeDescriptionCacheKey2A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey2B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey2A.TypeMatchStrategy, typeDescriptionCacheKey2A.MultipleMatchStrategy);

            var typeDescriptionCacheKey3A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey3B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey3A.TypeMatchStrategy, typeDescriptionCacheKey3A.MultipleMatchStrategy);

            // Act
            var result1 = typeDescriptionCacheKey1A == typeDescriptionCacheKey1B;
            var result2 = typeDescriptionCacheKey2A == typeDescriptionCacheKey2B;
            var result3 = typeDescriptionCacheKey3A == typeDescriptionCacheKey3B;

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void EqualsOperator___Should_return_true___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var typeDescriptionCacheKey1A = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), A.Dummy<TypeMatchStrategy>(), A.Dummy<MultipleMatchStrategy>());
            var typeDescriptionCacheKey1B = new TypeDescriptionCacheKey(typeDescriptionCacheKey1A.TypeDescription, typeDescriptionCacheKey1A.TypeMatchStrategy, typeDescriptionCacheKey1A.MultipleMatchStrategy);

            // Act
            var result1 = typeDescriptionCacheKey1A == typeDescriptionCacheKey1B;

            // Assert
            result1.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
        {
            // Arrange
            TypeDescriptionCacheKey typeDescriptionCacheKey1 = null;
            TypeDescriptionCacheKey typeDescriptionCacheKey2 = null;

            // Act
            var result = typeDescriptionCacheKey1 != typeDescriptionCacheKey2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
        {
            // Arrange
            TypeDescriptionCacheKey typeDescriptionCacheKey1 = null;
            var typeDescriptionCacheKey2 = A.Dummy<TypeDescriptionCacheKey>();

            // Act
            var result1 = typeDescriptionCacheKey1 != typeDescriptionCacheKey2;
            var result2 = typeDescriptionCacheKey2 != typeDescriptionCacheKey1;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_same_object_is_on_both_sides_of_operator()
        {
            // Arrange
            var typeDescriptionCacheKey = A.Dummy<TypeDescriptionCacheKey>();

            // Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result = typeDescriptionCacheKey != typeDescriptionCacheKey;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_true___When_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var typeDescriptionCacheKey1A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey1B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey1A.TypeMatchStrategy, typeDescriptionCacheKey1A.MultipleMatchStrategy);

            var typeDescriptionCacheKey2A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey2B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey2A.TypeMatchStrategy, typeDescriptionCacheKey2A.MultipleMatchStrategy);

            var typeDescriptionCacheKey3A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey3B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey3A.TypeMatchStrategy, typeDescriptionCacheKey3A.MultipleMatchStrategy);

            // Act
            var result1 = typeDescriptionCacheKey1A != typeDescriptionCacheKey1B;
            var result2 = typeDescriptionCacheKey2A != typeDescriptionCacheKey2B;
            var result3 = typeDescriptionCacheKey3A != typeDescriptionCacheKey3B;

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
        }

        [Fact]
        public static void NotEqualsOperator___Should_return_false___When_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var typeDescriptionCacheKey1A = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), A.Dummy<TypeMatchStrategy>(), A.Dummy<MultipleMatchStrategy>());
            var typeDescriptionCacheKey1B = new TypeDescriptionCacheKey(typeDescriptionCacheKey1A.TypeDescription, typeDescriptionCacheKey1A.TypeMatchStrategy, typeDescriptionCacheKey1A.MultipleMatchStrategy);

            // Act
            var result1 = typeDescriptionCacheKey1A != typeDescriptionCacheKey1B;

            // Assert
            result1.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_parameter_other_is_null()
        {
            // Arrange
            var typeDescriptionCacheKey = A.Dummy<TypeDescriptionCacheKey>();

            // Act
            var result1 = typeDescriptionCacheKey.Equals(null);

            // Assert
            result1.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var typeDescriptionCacheKey = A.Dummy<TypeDescriptionCacheKey>();

            // Act
            var result = typeDescriptionCacheKey.Equals(typeDescriptionCacheKey);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Equals___Should_return_false___When_calling_typed_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var typeDescriptionCacheKey1A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey1B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey1A.TypeMatchStrategy, typeDescriptionCacheKey1A.MultipleMatchStrategy);

            var typeDescriptionCacheKey2A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey2B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey2A.TypeMatchStrategy, typeDescriptionCacheKey2A.MultipleMatchStrategy);

            var typeDescriptionCacheKey3A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey3B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey3A.TypeMatchStrategy, typeDescriptionCacheKey3A.MultipleMatchStrategy);

            // Act
            var result1 = typeDescriptionCacheKey1A.Equals(typeDescriptionCacheKey1B);
            var result2 = typeDescriptionCacheKey2A.Equals(typeDescriptionCacheKey2B);
            var result3 = typeDescriptionCacheKey3A.Equals(typeDescriptionCacheKey3B);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [Fact]
        public static void Equals___Should_return_true___When_calling_typed_overload_and_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var typeDescriptionCacheKey1A = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), A.Dummy<TypeMatchStrategy>(), A.Dummy<MultipleMatchStrategy>());
            var typeDescriptionCacheKey1B = new TypeDescriptionCacheKey(typeDescriptionCacheKey1A.TypeDescription, typeDescriptionCacheKey1A.TypeMatchStrategy, typeDescriptionCacheKey1A.MultipleMatchStrategy);

            // Act
            var result1 = typeDescriptionCacheKey1A.Equals(typeDescriptionCacheKey1B);

            // Assert
            result1.Should().BeTrue();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "untyped", Justification = "Spelling/name is correct.")]
        [Fact]
        public static void Equals___Should_return_false___When_calling_untyped_overload_and_parameter_other_is_null()
        {
            // Arrange
            var typeDescriptionCacheKey = A.Dummy<TypeDescriptionCacheKey>();

            // Act
            var result1 = typeDescriptionCacheKey.Equals(null);

            // Assert
            result1.Should().BeFalse();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "untyped", Justification = "Spelling/name is correct.")]
        [Fact]
        public static void Equals___Should_return_false___When_calling_untyped_overload_and_parameter_other_is_not_of_the_same_type()
        {
            // Arrange
            var typeDescriptionCacheKey1 = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey2 = A.Dummy<string>();

            // Act
            var result1 = typeDescriptionCacheKey1.Equals((object)typeDescriptionCacheKey2);

            // Assert
            result1.Should().BeFalse();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "untyped", Justification = "Spelling/name is correct.")]
        [Fact]
        public static void Equals___Should_return_true___When_calling_untyped_overload_and_parameter_other_is_same_object()
        {
            // Arrange
            var typeDescriptionCacheKey = A.Dummy<TypeDescriptionCacheKey>();

            // Act
            var result = typeDescriptionCacheKey.Equals((object)typeDescriptionCacheKey);

            // Assert
            result.Should().BeTrue();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "untyped", Justification = "Spelling/name is correct.")]
        [Fact]
        public static void Equals___Should_return_false___When_calling_untyped_overload_and_objects_being_compared_have_different_property_values()
        {
            // Arrange
            var typeDescriptionCacheKey1A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey1B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey1A.TypeMatchStrategy, typeDescriptionCacheKey1A.MultipleMatchStrategy);

            var typeDescriptionCacheKey2A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey2B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey2A.TypeMatchStrategy, typeDescriptionCacheKey2A.MultipleMatchStrategy);

            var typeDescriptionCacheKey3A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey3B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey3A.TypeMatchStrategy, typeDescriptionCacheKey3A.MultipleMatchStrategy);

            // Act
            var result1 = typeDescriptionCacheKey1A.Equals((object)typeDescriptionCacheKey1B);
            var result2 = typeDescriptionCacheKey2A.Equals((object)typeDescriptionCacheKey2B);
            var result3 = typeDescriptionCacheKey3A.Equals((object)typeDescriptionCacheKey3B);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "untyped", Justification = "Spelling/name is correct.")]
        [Fact]
        public static void Equals___Should_return_true___When_calling_untyped_overload_and_objects_being_compared_have_same_property_values()
        {
            // Arrange
            var typeDescriptionCacheKey1A = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), A.Dummy<TypeMatchStrategy>(), A.Dummy<MultipleMatchStrategy>());
            var typeDescriptionCacheKey1B = new TypeDescriptionCacheKey(typeDescriptionCacheKey1A.TypeDescription, typeDescriptionCacheKey1A.TypeMatchStrategy, typeDescriptionCacheKey1A.MultipleMatchStrategy);

            // Act
            var result1 = typeDescriptionCacheKey1A.Equals((object)typeDescriptionCacheKey1B);

            // Assert
            result1.Should().BeTrue();
        }

        [Fact]
        public static void GetHashCode___Should_not_be_equal___When_objects_have_different_property_values()
        {
            // Arrange
            var typeDescriptionCacheKey1A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey1B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey1A.TypeMatchStrategy, typeDescriptionCacheKey1A.MultipleMatchStrategy);

            var typeDescriptionCacheKey2A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey2B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey2A.TypeMatchStrategy, typeDescriptionCacheKey2A.MultipleMatchStrategy);

            var typeDescriptionCacheKey3A = A.Dummy<TypeDescriptionCacheKey>();
            var typeDescriptionCacheKey3B = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), typeDescriptionCacheKey3A.TypeMatchStrategy, typeDescriptionCacheKey3A.MultipleMatchStrategy);

            // Act
            var hash1A = typeDescriptionCacheKey1A.GetHashCode();
            var hash1B = typeDescriptionCacheKey1B.GetHashCode();

            var hash2A = typeDescriptionCacheKey2A.GetHashCode();
            var hash2B = typeDescriptionCacheKey2B.GetHashCode();

            var hash3A = typeDescriptionCacheKey3A.GetHashCode();
            var hash3B = typeDescriptionCacheKey3B.GetHashCode();

            // Assert
            hash1A.Should().NotBe(hash1B);
            hash2A.Should().NotBe(hash2B);
            hash3A.Should().NotBe(hash3B);
        }

        [Fact]
        public static void GetHashCode___Should_be_equal___When_objects_have_the_same_property_values()
        {
            // Arrange
            var typeDescriptionCacheKey1A = new TypeDescriptionCacheKey(A.Dummy<TypeDescription>(), A.Dummy<TypeMatchStrategy>(), A.Dummy<MultipleMatchStrategy>());
            var typeDescriptionCacheKey1B = new TypeDescriptionCacheKey(typeDescriptionCacheKey1A.TypeDescription, typeDescriptionCacheKey1A.TypeMatchStrategy, typeDescriptionCacheKey1A.MultipleMatchStrategy);

            // Act
            var hash1A = typeDescriptionCacheKey1A.GetHashCode();
            var hash1B = typeDescriptionCacheKey1B.GetHashCode();

            // Assert
            hash1A.Should().Be(hash1B);
        }

        // ReSharper restore InconsistentNaming
    }
}
