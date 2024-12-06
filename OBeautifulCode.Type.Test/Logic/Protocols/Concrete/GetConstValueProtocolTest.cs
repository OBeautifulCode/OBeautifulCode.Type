// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetConstValueProtocolTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;
    using System.Threading.Tasks;
    using FakeItEasy;
    using OBeautifulCode.Assertion.Recipes;
    using Xunit;

    public static class GetConstValueProtocolTest
    {
        [Fact]
        public static void Execute___Should_throw_ArgumentNullException___When_parameter_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new GetConstValueProtocol<Version>();

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("operation");
        }

        [Fact]
        public static async Task ExecuteAsync___Should_throw_ArgumentNullException___When_parameter_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new GetConstValueProtocol<Version>();

            // Act
            var actual = await Record.ExceptionAsync(() => systemUnderTest.ExecuteAsync(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("operation");
        }

        [Fact]
        public static void Execute___Should_return_specified_value___When_called()
        {
            // Arrange
            var operation = A.Dummy<GetConstValueOp<Version>>();

            var systemUnderTest = new GetConstValueProtocol<Version>();

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(operation.Value);
        }

        [Fact]
        public static async Task ExecuteAsync___Should_return_specified_value___When_called()
        {
            // Arrange
            var operation = A.Dummy<GetConstValueOp<Version>>();

            var systemUnderTest = new GetConstValueProtocol<Version>();

            // Act
            var actual = await systemUnderTest.ExecuteAsync(operation);

            // Assert
            actual.AsTest().Must().BeEqualTo(operation.Value);
        }
    }
}
