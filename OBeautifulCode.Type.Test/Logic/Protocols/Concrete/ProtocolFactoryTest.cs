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
        public static void RegisterProtocolForSupportedOperations___Should_throw_ArgumentNullException___When_parameter_protocolType_is_null()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            // Act
            var actual = Record.Exception(() => systemUnderTest.RegisterProtocolForSupportedOperations(null, A.Dummy<IProtocol>));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("protocolType");
        }

        [Fact]
        public static void RegisterProtocolForSupportedOperations___Should_throw_ArgumentException___When_parameter_protocolType_is_not_assignable_to_IProtocol()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            // Act
            var actual = Record.Exception(() => systemUnderTest.RegisterProtocolForSupportedOperations(typeof(object), A.Dummy<IProtocol>));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString("protocolType 'object' is not assignable to IProtocol");
        }

        [Fact]
        public static void RegisterProtocolForSupportedOperations___Should_throw_ArgumentNullException___When_parameter_getProtocolFunc_is_null()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            // Act
            var actual = Record.Exception(() => systemUnderTest.RegisterProtocolForSupportedOperations(A.Dummy<IProtocol>().GetType(), null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("getProtocolFunc");
        }

        [Fact]
        public static void RegisterProtocolForSupportedOperations___Should_throw_ArgumentOutOfRangeException___When_parameter_protocolAlreadyRegisteredForOperationStrategy_is_Unknown()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            // Act
            var actual = Record.Exception(() => systemUnderTest.RegisterProtocolForSupportedOperations(A.Dummy<IProtocol>().GetType(), () => A.Dummy<IProtocol>(), ProtocolAlreadyRegisteredForOperationStrategy.Unknown));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentOutOfRangeException>();
            actual.Message.AsTest().Must().ContainString("protocolAlreadyRegisteredForOperationStrategy");
            actual.Message.AsTest().Must().ContainString("Unknown");
        }

        [Fact]
        public static void RegisterProtocolForSupportedOperations___Should_throw_ArgumentException___When_protocol_does_not_execute_any_operations()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            // Act
            var actual = Record.Exception(() => systemUnderTest.RegisterProtocolForSupportedOperations(typeof(ProtocolWithNoOperations), A.Dummy<IProtocol>));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"Protocol '{nameof(ProtocolFactoryTest)}.{nameof(ProtocolWithNoOperations)}' does not execute any operations."));
        }

        [Fact]
        public static void RegisterProtocolForSupportedOperations___Should_throw_OpExecutionFailedException___When_protocolAlreadyRegisteredForOperationStrategy_is_Throw_and_protocol_executes_an_operation_that_already_been_registered()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SharedOperationProtocol1), () => new SharedOperationProtocol1());

            // Act
            var actual = Record.Exception(() => systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SharedOperationProtocol2), () => new SharedOperationProtocol2(), ProtocolAlreadyRegisteredForOperationStrategy.Throw));

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
        public static void Execute___Should_throw_OpExecutionFailedException___When_there_is_no_protocol_registered_for_the_operation_and_missingProtocolStrategy_is_Throw()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            var operation = new GetProtocolOp(new SharedOperation(), MissingProtocolStrategy.Throw);

            // Act
            var actual = Record.Exception(() => systemUnderTest.Execute(operation));

            // Assert
            actual.AsTest().Must().BeOfType<OpExecutionFailedException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"There is no protocol registered for the specified operation: '{nameof(ProtocolFactoryTest)}.{nameof(SharedOperation)}'"));
        }

        [Fact]
        public static void Execute___Should_return_null___When_there_is_no_protocol_registered_for_the_operation_and_missingProtocolStrategy_is_ReturnNull()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            var operation = new GetProtocolOp(new SharedOperation(), MissingProtocolStrategy.ReturnNull);

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.AsTest().Must().BeNull();
        }

        [Fact]
        public static void Execute___Should_throw_InvalidOperationException___When_getProtocolFunc_returns_null()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SharedOperationProtocol1), () => null);

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

            systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SharedOperationProtocol1), () => protocol1);
            systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SiblingOperationProtocol), () => protocol2);

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
        public static void Execute___Should_return_originally_registered_protocol___When_second_protocol_supporting_same_operations_is_registered_with_ProtocolAlreadyRegisteredForOperationStrategy_Skip()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            IProtocol protocol1 = new SharedOperationProtocol1();
            IProtocol protocol2 = new SiblingOperationProtocol();

            systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SharedOperationProtocol1), () => protocol1);
            systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SiblingOperationProtocol), () => protocol2);

            systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SharedOperationProtocol1), () => new SharedOperationProtocol1(), ProtocolAlreadyRegisteredForOperationStrategy.Skip);
            systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SiblingOperationProtocol), () => new SiblingOperationProtocol(), ProtocolAlreadyRegisteredForOperationStrategy.Skip);

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
        public static void Execute___Should_return_last_registered_protocol___When_second_protocol_supporting_same_operation_is_registered_with_ProtocolAlreadyRegisteredForOperationStrategy_Replace()
        {
            // Arrange
            var systemUnderTest = new ProtocolFactory();

            IProtocol protocol1 = new SharedOperationProtocol1();
            IProtocol protocol2 = new SiblingOperationProtocol();

            systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SharedOperationProtocol1), () => new SharedOperationProtocol1());
            systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SiblingOperationProtocol), () => new SiblingOperationProtocol());

            systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SharedOperationProtocol1), () => protocol1, ProtocolAlreadyRegisteredForOperationStrategy.Replace);
            systemUnderTest.RegisterProtocolForSupportedOperations(typeof(SiblingOperationProtocol), () => protocol2, ProtocolAlreadyRegisteredForOperationStrategy.Replace);

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
        public static void ShallowClone___Should_return_a_shallow_clone___When_called()
        {
            // Arrange
            var systemUnderTest1 = new ProtocolFactory();
            var systemUnderTest2 = new ProtocolFactory();

            IProtocol protocol1 = new SharedOperationProtocol1();
            IProtocol protocol2 = new SiblingOperationProtocol();

            systemUnderTest1.RegisterProtocolForSupportedOperations(typeof(SharedOperationProtocol1), () => protocol1);
            systemUnderTest2.RegisterProtocolForSupportedOperations(typeof(SharedOperationProtocol1), () => protocol1);

            // Act
            var actual1 = systemUnderTest1.ShallowClone();
            var actual2 = systemUnderTest2.ShallowClone();

            actual1.RegisterProtocolForSupportedOperations(typeof(SiblingOperationProtocol), () => protocol2);
            systemUnderTest2.RegisterProtocolForSupportedOperations(typeof(SiblingOperationProtocol), () => protocol2);

            // Assert
            var operation1 = new GetProtocolOp(new SharedOperation());
            var protocolFromOriginal1 = systemUnderTest1.Execute(operation1);
            var protocolFromOriginal2 = systemUnderTest2.Execute(operation1);
            var protocolFromClone1 = actual1.Execute(operation1);
            var protocolFromClone2 = actual2.Execute(operation1);
            protocolFromOriginal1.AsTest().Must().BeSameReferenceAs(protocol1);
            protocolFromOriginal2.AsTest().Must().BeSameReferenceAs(protocol1);
            protocolFromClone1.AsTest().Must().BeSameReferenceAs(protocol1);
            protocolFromClone2.AsTest().Must().BeSameReferenceAs(protocol1);

            var operation2 = new GetProtocolOp(new SiblingOperation1(), MissingProtocolStrategy.ReturnNull);
            protocolFromOriginal1 = systemUnderTest1.Execute(operation2);
            protocolFromOriginal2 = systemUnderTest2.Execute(operation2);
            protocolFromClone1 = actual1.Execute(operation2);
            protocolFromClone2 = actual2.Execute(operation2);
            protocolFromOriginal1.AsTest().Must().BeNull();
            protocolFromOriginal2.AsTest().Must().BeSameReferenceAs(protocol2);
            protocolFromClone1.AsTest().Must().BeSameReferenceAs(protocol2);
            protocolFromClone2.AsTest().Must().BeNull();
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
