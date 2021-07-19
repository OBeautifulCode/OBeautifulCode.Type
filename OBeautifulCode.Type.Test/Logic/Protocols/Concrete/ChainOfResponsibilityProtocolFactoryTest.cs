// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChainOfResponsibilityProtocolFactoryTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;

    using OBeautifulCode.Assertion.Recipes;

    using Xunit;

    using static System.FormattableString;

    public static class ChainOfResponsibilityProtocolFactoryTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_protocolFactoriesToUseInOrder_contains_a_null_element()
        {
            // Arrange, Act
            var actual = Record.Exception(() => new ChainOfResponsibilityProtocolFactory(new[] { new ChainOfResponsibilityProtocolFactory(), null, new ChainOfResponsibilityProtocolFactory() }));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("protocolFactoriesToUseInOrder contains a null element");
        }

        [Fact]
        public static void AddToEndOfChain___Should_throw_ArgumentNullException___When_parameter_protocolFactory_is_null()
        {
            // Arrange
            var systemUnderTest = new ChainOfResponsibilityProtocolFactory();

            // Act
            var actual = Record.Exception(() => systemUnderTest.AddToEndOfChain(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("protocolFactory");
        }

        [Fact]
        public static void Execute___Should_throw_ArgumentNullException___When_parameter_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new ChainOfResponsibilityProtocolFactory();

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("operation");
        }

        [Fact]
        public static void Execute___Should_throw_InvalidOperationException___When_there_is_no_protocol_registered_for_the_operation_and_missingProtocolStrategy_is_Throw()
        {
            // Arrange
            var systemUnderTest = new ChainOfResponsibilityProtocolFactory();

            var operation = new GetProtocolOp(new SharedOperation(), MissingProtocolStrategy.Throw);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute(operation));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no protocol registered for the specified operation: '{nameof(ChainOfResponsibilityProtocolFactoryTest)}.{nameof(SharedOperation)}'"));
        }

        [Fact]
        public static void Execute___Should_return_null___When_there_is_no_protocol_registered_for_the_operation_and_missingProtocolStrategy_is_ReturnNull()
        {
            // Arrange
            var systemUnderTest = new ChainOfResponsibilityProtocolFactory();

            var operation = new GetProtocolOp(new SharedOperation(), MissingProtocolStrategy.ReturnNull);

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void Execute___Should_return_registered_protocol___When_called()
        {
            // Arrange
            var systemUnderTest = new ChainOfResponsibilityProtocolFactory();

            IProtocol protocol1 = new SharedOperationProtocol1();
            IProtocol protocol2 = new SiblingOperationProtocol();
            IProtocol protocol3 = new SharedOperationProtocol2();

            var protocolFactory1 = protocol1.ToProtocolFactory();
            var protocolFactory2 = protocol2.ToProtocolFactory();
            var protocolFactory3 = protocol3.ToProtocolFactory();

            systemUnderTest.AddToEndOfChain(protocolFactory1);
            systemUnderTest.AddToEndOfChain(protocolFactory2);
            systemUnderTest.AddToEndOfChain(protocolFactory3);

            var operation1 = new GetProtocolOp(new SharedOperation());
            var operation2 = new GetProtocolOp(new SiblingOperation1());
            var operation3 = new GetProtocolOp(new SiblingOperation2());

            // Act
            var actual1 = systemUnderTest.Execute(operation1);
            var actual2 = systemUnderTest.Execute(operation2);
            var actual3 = systemUnderTest.Execute(operation3);

            // Assert
            actual1.AsTest().Must().BeSameReferenceAs(protocol1);
            actual2.AsTest().Must().BeSameReferenceAs(protocol2);
            actual3.AsTest().Must().BeSameReferenceAs(protocol2);
        }

        [Fact]
        public static void Constructor___Should_register_protocol_factories_in_order_specified___When_called()
        {
            // Arrange
            IProtocol protocol1 = new SharedOperationProtocol1();
            IProtocol protocol2 = new SiblingOperationProtocol();
            IProtocol protocol3 = new SharedOperationProtocol2();

            var protocolFactory1 = protocol1.ToProtocolFactory();
            var protocolFactory2 = protocol2.ToProtocolFactory();
            var protocolFactory3 = protocol3.ToProtocolFactory();

            var systemUnderTest = new ChainOfResponsibilityProtocolFactory(new[] { protocolFactory1, protocolFactory2, protocolFactory3 });

            var operation1 = new GetProtocolOp(new SharedOperation());
            var operation2 = new GetProtocolOp(new SiblingOperation1());
            var operation3 = new GetProtocolOp(new SiblingOperation2());

            // Act
            var actual1 = systemUnderTest.Execute(operation1);
            var actual2 = systemUnderTest.Execute(operation2);
            var actual3 = systemUnderTest.Execute(operation3);

            // Assert
            actual1.AsTest().Must().BeSameReferenceAs(protocol1);
            actual2.AsTest().Must().BeSameReferenceAs(protocol2);
            actual3.AsTest().Must().BeSameReferenceAs(protocol2);
        }

        private class SharedOperation : IVoidOperation
        {
        }

        private class SiblingOperation1 : IVoidOperation
        {
        }

        private class SiblingOperation2 : IVoidOperation
        {
        }

        private class SharedOperationProtocol1 : SyncSpecificVoidProtocolBase<SharedOperation>
        {
            public override void Execute(SharedOperation operation)
            {
                throw new NotImplementedException();
            }
        }

        private class SharedOperationProtocol2 : SyncSpecificVoidProtocolBase<SharedOperation>
        {
            public override void Execute(SharedOperation operation)
            {
                throw new NotImplementedException();
            }
        }

        private class SiblingOperationProtocol : ISyncVoidProtocol<SiblingOperation1>, ISyncVoidProtocol<SiblingOperation2>
        {
            public void Execute(SiblingOperation1 operation)
            {
                throw new NotImplementedException();
            }

            public void Execute(SiblingOperation2 operation)
            {
                throw new NotImplementedException();
            }
        }
    }
}
