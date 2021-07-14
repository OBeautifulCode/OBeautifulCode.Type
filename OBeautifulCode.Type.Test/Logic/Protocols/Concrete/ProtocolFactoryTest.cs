// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolFactoryTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FakeItEasy;

    using OBeautifulCode.Assertion.Recipes;

    using Xunit;

    using static System.FormattableString;

    public static class ProtocolFactoryTest
    {
        [Fact]
        public static void RegisterProtocol___Should_throw_ArgumentNullException___When_parameter_protocolType_is_null()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            // Act
            var actual = Record.Exception(() => systemUnderTest.RegisterProtocol(null, A.Dummy<IProtocol>));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("protocolType");
        }

        [Fact]
        public static void RegisterProtocol___Should_throw_ArgumentException___When_parameter_protocolType_is_not_assignable_to_IProtocol()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            // Act
            var actual = Record.Exception(() => systemUnderTest.RegisterProtocol(typeof(object), A.Dummy<IProtocol>));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("protocolType 'object' is not assignable to IProtocol");
        }

        [Fact]
        public static void RegisterProtocol___Should_throw_ArgumentNullException___When_parameter_getProtocolFunc_is_null()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            // Act
            var actual = Record.Exception(() => systemUnderTest.RegisterProtocol(A.Dummy<IProtocol>().GetType(), null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("getProtocolFunc");
        }

        [Fact]
        public static void RegisterProtocol___Should_throw_ArgumentException___When_protocol_does_not_execute_any_operations()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            // Act
            var actual = Record.Exception(() => systemUnderTest.RegisterProtocol(typeof(ProtocolWithNoOperations), A.Dummy<IProtocol>));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Protocol '{nameof(ProtocolFactoryTest)}.{nameof(ProtocolWithNoOperations)}' does not execute any operations."));
        }

        [Fact]
        public static void RegisterProtocol___Should_throw_ArgumentException___When_protocol_executes_an_operation_that_already_been_registered()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            systemUnderTest.RegisterProtocol(typeof(SharedOperationProtocol1), () => new SharedOperationProtocol1());

            // Act
            var actual = Record.Exception(() => systemUnderTest.RegisterProtocol(typeof(SharedOperationProtocol2), () => new SharedOperationProtocol2()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Protocol '{nameof(ProtocolFactoryTest)}.{nameof(SharedOperationProtocol2)}' executes an operation that has already been registered: '{nameof(ProtocolFactoryTest)}.{nameof(SharedOperation)}'."));
        }

        [Fact]
        public static void Execute___Should_throw_ArgumentNullException___When_parameter_operation_is_null()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("operation");
        }

        [Fact]
        public static void Execute___Should_throw_InvalidOperationException___When_there_is_no_protocol_registered_for_the_operation()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            var operation = new GetProtocolOp(new SharedOperation());

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute(operation));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no protocol registered for the specified operation: '{nameof(ProtocolFactoryTest)}.{nameof(SharedOperation)}'"));
        }

        [Fact]
        public static void Execute___Should_throw_InvalidOperationException___When_getProtocolFunc_returns_null()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            systemUnderTest.RegisterProtocol(typeof(SharedOperationProtocol1), () => null);

            var operation = new GetProtocolOp(new SharedOperation());

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute(operation));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"The func to get the protocol for the following specified operation returned null: '{nameof(ProtocolFactoryTest)}.{nameof(SharedOperation)}'"));
        }

        [Fact]
        public static void Execute___Should_return_registered_protocol___When_called()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            IProtocol protocol1 = new SharedOperationProtocol1();
            IProtocol protocol2 = new SiblingOperationProtocol();

            systemUnderTest.RegisterProtocol(typeof(SharedOperationProtocol1), () => protocol1);
            systemUnderTest.RegisterProtocol(typeof(SiblingOperationProtocol), () => protocol2);

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
        public static void Constructor___Should_register_protocols_in_protocolTypeToGetProtocolFuncMap___When_called()
        {
            // Arrange
            IProtocol protocol1 = new SharedOperationProtocol1();
            IProtocol protocol2 = new SiblingOperationProtocol();

            var protocolTypeToGetProtocolFuncMap = new Dictionary<Type, Func<IProtocol>>
            {
                { typeof(SharedOperationProtocol1), () => protocol1 },
                { typeof(SiblingOperationProtocol), () => protocol2 },
            };

            var systemUnderTest = new ProtocolFactory(protocolTypeToGetProtocolFuncMap);

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

        private class ProtocolWithNoOperations : IProtocol
        {
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

        private class SharedOperationProtocol2 : AsyncSpecificVoidProtocolBase<SharedOperation>
        {
            public override Task ExecuteAsync(SharedOperation operation)
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
