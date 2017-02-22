// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DescribedSerializationTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.TypeRepresentation.Test
{
    using System;

    using FakeItEasy;

    using FluentAssertions;

    using OBeautifulCode.TypeRepresentation;

    using Xunit;

    public static class DescribedSerializationTest
    {
        // ReSharper disable PossibleNullReferenceException
        [Fact]
        public static void Constructor__Should_throw_ArgumentNullException___When_parameter_typeDescription_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => new DescribedSerialization(null, A.Dummy<string>(), A.Dummy<string>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("typeDescription");
        }

        [Fact]
        public static void Constructor__Should_throw_ArgumentNullException___When_parameter_payload_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => new DescribedSerialization(A.Dummy<TypeDescription>(), null, A.Dummy<string>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("payload");
        }

        [Fact]
        public static void Constructor__Should_throw_ArgumentException___When_parameter_payload_is_white_space()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new DescribedSerialization(A.Dummy<TypeDescription>(), string.Empty, A.Dummy<string>()));
            var ex2 = Record.Exception(() => new DescribedSerialization(A.Dummy<TypeDescription>(), "    ", A.Dummy<string>()));
            var ex3 = Record.Exception(() => new DescribedSerialization(A.Dummy<TypeDescription>(), "  \r\n  ", A.Dummy<string>()));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("payload");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("payload");

            ex3.Should().BeOfType<ArgumentException>();
            ex3.Message.Should().Contain("payload");
        }

        [Fact]
        public static void Constructor__Should_throw_ArgumentNullException___When_parameter_serializer_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => new DescribedSerialization(A.Dummy<TypeDescription>(), A.Dummy<string>(), null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("serializer");
        }

        [Fact]
        public static void Constructor__Should_throw_ArgumentException___When_parameter_serializer_is_white_space()
        {
            // Arrange, Act
            var ex1 = Record.Exception(() => new DescribedSerialization(A.Dummy<TypeDescription>(), A.Dummy<string>(), string.Empty));
            var ex2 = Record.Exception(() => new DescribedSerialization(A.Dummy<TypeDescription>(), A.Dummy<string>(), "    "));
            var ex3 = Record.Exception(() => new DescribedSerialization(A.Dummy<TypeDescription>(), A.Dummy<string>(), "  \r\n  "));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("serializer");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("serializer");

            ex3.Should().BeOfType<ArgumentException>();
            ex3.Message.Should().Contain("serializer");
        }

        [Fact]
        public static void TypeDescription__Should_return_same_typeDescription_passed_to_constructor___When_getting()
        {
            // Arrange
            var typeDescription = A.Dummy<TypeDescription>();
            var payload = A.Dummy<string>();
            var serializer = A.Dummy<string>();
            var systemUnderTest = new DescribedSerialization(typeDescription, payload, serializer);

            // Act
            var actual = systemUnderTest.TypeDescription;

            // Assert
            actual.Should().BeSameAs(typeDescription);
        }

        [Fact]
        public static void Payload__Should_return_same_payload_passed_to_constructor___When_getting()
        {
            // Arrange
            var typeDescription = A.Dummy<TypeDescription>();
            var payload = A.Dummy<string>();
            var serializer = A.Dummy<string>();
            var systemUnderTest = new DescribedSerialization(typeDescription, payload, serializer);

            // Act
            var actual = systemUnderTest.Payload;

            // Assert
            actual.Should().Be(payload);
        }

        [Fact]
        public static void Serializer__Should_return_same_serializer_passed_to_constructor___When_getting()
        {
            // Arrange
            var typeDescription = A.Dummy<TypeDescription>();
            var payload = A.Dummy<string>();
            var serializer = A.Dummy<string>();
            var systemUnderTest = new DescribedSerialization(typeDescription, payload, serializer);

            // Act
            var actual = systemUnderTest.Serializer;

            // Assert
            actual.Should().Be(serializer);
        }

        // ReSharper restore PossibleNullReferenceException
    }
}