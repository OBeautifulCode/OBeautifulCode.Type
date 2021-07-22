// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolExtensionsTest.cs" company="OBeautifulCode">
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

    using static System.FormattableString;

    public static class ProtocolExtensionsTest
    {
        [Fact]
        public static void ExecuteViaReflection_void___Should_throw_ArgumentNullException___When_parameter_protocol_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ProtocolExtensions.ExecuteViaReflection(null, A.Dummy<IOperation>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("protocol");
        }

        [Fact]
        public static void ExecuteViaReflection_void___Should_throw_ArgumentNullException___When_parameter_operation_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => A.Dummy<IProtocol>().ExecuteViaReflection(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("operation");
        }

        [Fact]
        public static void ExecuteViaReflection_void___Should_throw_ArgumentException___When_protocol_cannot_execute_operation()
        {
            // Arrange
            var operation = new DummyVoidOperation();

            var protocol1 = new ProtocolWithNoOperations();
            var protocol2 = new SiblingOperationProtocol();

            // Act
            var actual1 = Record.Exception(() => protocol1.ExecuteViaReflection(operation));
            var actual2 = Record.Exception(() => protocol2.ExecuteViaReflection(operation));

            // Assert
            actual1.AsTest().Must().BeOfType<ArgumentException>();
            actual1.Message.AsTest().Must().ContainString(Invariant($"The specified '{nameof(ProtocolExtensionsTest)}.{nameof(ProtocolWithNoOperations)}' protocol does not have a void Execute method with a single parameter that the specified '{nameof(ProtocolExtensionsTest)}.{nameof(DummyVoidOperation)}' operation is assignable to."));

            actual2.AsTest().Must().BeOfType<ArgumentException>();
            actual2.Message.AsTest().Must().ContainString(Invariant($"The specified '{nameof(ProtocolExtensionsTest)}.{nameof(SiblingOperationProtocol)}' protocol does not have a void Execute method with a single parameter that the specified '{nameof(ProtocolExtensionsTest)}.{nameof(DummyVoidOperation)}' operation is assignable to."));
        }

        [Fact]
        public static void ExecuteViaReflection_void___Should_throw_ArgumentException___When_protocol_has_multiple_execute_methods_for_operation()
        {
            // Arrange
            var operation = new ChildVoidOperation();

            var protocol = new FamilyVoidProtocol();

            // Act
            var actual = Record.Exception(() => protocol.ExecuteViaReflection(operation));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"The specified '{nameof(ProtocolExtensionsTest)}.{nameof(FamilyVoidProtocol)}' protocol has more than one void Execute method with a single parameter that the specified '{nameof(ProtocolExtensionsTest)}.{nameof(ChildVoidOperation)}' operation is assignable to."));
        }

        [Fact]
        public static void ExecuteViaReflection_void___Should_throw_same_exception_throw_by_protocol___When_protocol_throws()
        {
            // Arrange
            var operation = new ThrowingVoidOp();

            var protocol = new ThrowingProtocol();

            // Act
            var actual = Record.Exception(() => protocol.ExecuteViaReflection(operation));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().BeEqualTo("execute-void-sync");
        }

        [Fact]
        public static void ExecuteViaReflection_void___Should_execute_the_operation___When_called()
        {
            // Arrange
            var operation1Counter = 4;
            var operation2Counter = 10;

            var operation1 = new SiblingOperation1
            {
                ActionToRun = () => operation1Counter++,
            };

            var operation2 = new SiblingOperation2
            {
                ActionToRun = () => operation2Counter++,
            };

            var protocol = new SiblingOperationProtocol();

            // Act, Assert
            protocol.ExecuteViaReflection(operation1);
            operation1Counter.AsTest().Must().BeEqualTo(5);

            protocol.ExecuteViaReflection(operation2);
            operation2Counter.AsTest().Must().BeEqualTo(11);

            protocol.ExecuteViaReflection(operation1);
            operation1Counter.AsTest().Must().BeEqualTo(6);
        }

        [Fact]
        public static void ExecuteViaReflection_TResult___Should_throw_ArgumentNullException___When_parameter_protocol_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ProtocolExtensions.ExecuteViaReflection<int>(null, A.Dummy<IOperation>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("protocol");
        }

        [Fact]
        public static void ExecuteViaReflection_TResult___Should_throw_ArgumentNullException___When_parameter_operation_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => A.Dummy<IProtocol>().ExecuteViaReflection<int>(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("operation");
        }

        [Fact]
        public static void ExecuteViaReflection_TResult___Should_throw_ArgumentException___When_protocol_cannot_execute_operation()
        {
            // Arrange
            var operation = new DummyReturningOperation();

            var protocol1 = new ProtocolWithNoOperations();
            var protocol2 = new SiblingOperationProtocol();

            // Act
            var actual1 = Record.Exception(() => protocol1.ExecuteViaReflection<int>(operation));
            var actual2 = Record.Exception(() => protocol2.ExecuteViaReflection<int>(operation));

            // Assert
            actual1.AsTest().Must().BeOfType<ArgumentException>();
            actual1.Message.AsTest().Must().ContainString(Invariant($"The specified '{nameof(ProtocolExtensionsTest)}.{nameof(ProtocolWithNoOperations)}' protocol does not have a returning Execute method with a single parameter that the specified '{nameof(ProtocolExtensionsTest)}.{nameof(DummyReturningOperation)}' operation is assignable to AND a return type that is assignable to the specified 'int' return type."));

            actual2.AsTest().Must().BeOfType<ArgumentException>();
            actual2.Message.AsTest().Must().ContainString(Invariant($"The specified '{nameof(ProtocolExtensionsTest)}.{nameof(SiblingOperationProtocol)}' protocol does not have a returning Execute method with a single parameter that the specified '{nameof(ProtocolExtensionsTest)}.{nameof(DummyReturningOperation)}' operation is assignable to AND a return type that is assignable to the specified 'int' return type."));
        }

        [Fact]
        public static void ExecuteViaReflection_TResult___Should_throw_ArgumentException___When_protocol_has_multiple_execute_methods_for_operation()
        {
            // Arrange
            var operation = new ChildReturningOperation();

            var protocol = new FamilyReturningProtocol();

            // Act
            var actual = Record.Exception(() => protocol.ExecuteViaReflection<int>(operation));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"The specified '{nameof(ProtocolExtensionsTest)}.{nameof(FamilyReturningProtocol)}' protocol has more than one returning Execute method with a single parameter that the specified '{nameof(ProtocolExtensionsTest)}.{nameof(ChildReturningOperation)}' operation is assignable to AND a return type that is assignable to the specified 'int' return type."));
        }

        [Fact]
        public static void ExecuteViaReflection_TResult___Should_throw_same_exception_throw_by_protocol___When_protocol_throws()
        {
            // Arrange
            var operation = new ThrowingReturningOp();

            var protocol = new ThrowingProtocol();

            // Act
            var actual = Record.Exception(() => protocol.ExecuteViaReflection<bool>(operation));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().BeEqualTo("execute-returning-sync");
        }

        [Fact]
        public static void ExecuteViaReflection_TResult___Should_execute_the_operation___When_called()
        {
            // Arrange
            var operation1 = new SiblingOperation3
            {
                Value = 4,
            };

            var operation2 = new SiblingOperation4
            {
                Value = 6,
            };

            var operation3 = new SiblingOperation3
            {
                Value = 1,
            };

            var protocol = new SiblingOperationProtocol();

            // Act
            var actual1 = protocol.ExecuteViaReflection<int>(operation1);
            var actual2 = protocol.ExecuteViaReflection<int>(operation2);
            var actual3 = protocol.ExecuteViaReflection<int>(operation3);

            // Assert
            actual1.AsTest().Must().BeEqualTo(5);
            actual2.AsTest().Must().BeEqualTo(8);
            actual3.AsTest().Must().BeEqualTo(2);
        }

        [Fact]
        public static async Task ExecuteViaReflectionAsync_void___Should_throw_ArgumentNullException___When_parameter_protocol_is_null()
        {
            // Arrange, Act
            var actual = await Record.ExceptionAsync(() => ProtocolExtensions.ExecuteViaReflectionAsync(null, A.Dummy<IOperation>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("protocol");
        }

        [Fact]
        public static async Task ExecuteViaReflectionAsync_void___Should_throw_ArgumentNullException___When_parameter_operation_is_null()
        {
            // Arrange, Act
            var actual = await Record.ExceptionAsync(() => A.Dummy<IProtocol>().ExecuteViaReflectionAsync(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("operation");
        }

        [Fact]
        public static async Task ExecuteViaReflectionAsync_void___Should_throw_ArgumentException___When_protocol_cannot_execute_operation()
        {
            // Arrange
            var operation = new DummyVoidOperation();

            var protocol1 = new ProtocolWithNoOperations();
            var protocol2 = new SiblingOperationProtocol();

            // Act
            var actual1 = await Record.ExceptionAsync(() => protocol1.ExecuteViaReflectionAsync(operation));
            var actual2 = await Record.ExceptionAsync(() => protocol2.ExecuteViaReflectionAsync(operation));

            // Assert
            actual1.AsTest().Must().BeOfType<ArgumentException>();
            actual1.Message.AsTest().Must().ContainString(Invariant($"The specified '{nameof(ProtocolExtensionsTest)}.{nameof(ProtocolWithNoOperations)}' protocol does not have a void ExecuteAsync method with a single parameter that the specified '{nameof(ProtocolExtensionsTest)}.{nameof(DummyVoidOperation)}' operation is assignable to."));

            actual2.AsTest().Must().BeOfType<ArgumentException>();
            actual2.Message.AsTest().Must().ContainString(Invariant($"The specified '{nameof(ProtocolExtensionsTest)}.{nameof(SiblingOperationProtocol)}' protocol does not have a void ExecuteAsync method with a single parameter that the specified '{nameof(ProtocolExtensionsTest)}.{nameof(DummyVoidOperation)}' operation is assignable to."));
        }

        [Fact]
        public static async Task ExecuteViaReflectionAsync_void___Should_throw_ArgumentException___When_protocol_has_multiple_execute_methods_for_operation()
        {
            // Arrange
            var operation = new ChildVoidOperation();

            var protocol = new FamilyVoidProtocol();

            // Act
            var actual = await Record.ExceptionAsync(() => protocol.ExecuteViaReflectionAsync(operation));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"The specified '{nameof(ProtocolExtensionsTest)}.{nameof(FamilyVoidProtocol)}' protocol has more than one void ExecuteAsync method with a single parameter that the specified '{nameof(ProtocolExtensionsTest)}.{nameof(ChildVoidOperation)}' operation is assignable to."));
        }

        [Fact]
        public static async Task ExecuteViaReflectionAsync_void___Should_throw_same_exception_throw_by_protocol___When_protocol_throws()
        {
            // Arrange
            var operation = new ThrowingVoidOp();

            var protocol = new ThrowingProtocol();

            // Act
            var actual = await Record.ExceptionAsync(() => protocol.ExecuteViaReflectionAsync(operation));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().BeEqualTo("execute-void-async");
        }

        [Fact]
        public static async Task ExecuteViaReflectionAsync_void___Should_execute_the_operation___When_called()
        {
            // Arrange
            var operation1Counter = 4;
            var operation2Counter = 10;

            var operation1 = new SiblingOperation1
            {
                ActionToRun = () => operation1Counter++,
            };

            var operation2 = new SiblingOperation2
            {
                ActionToRun = () => operation2Counter++,
            };

            var protocol = new SiblingOperationProtocol();

            // Act, Assert
            await protocol.ExecuteViaReflectionAsync(operation1);
            operation1Counter.AsTest().Must().BeEqualTo(6);

            await protocol.ExecuteViaReflectionAsync(operation2);
            operation2Counter.AsTest().Must().BeEqualTo(12);

            await protocol.ExecuteViaReflectionAsync(operation1);
            operation1Counter.AsTest().Must().BeEqualTo(8);
        }

        [Fact]
        public static async Task ExecuteViaReflectionAsync_TResult___Should_throw_ArgumentNullException___When_parameter_protocol_is_null()
        {
            // Arrange, Act
            var actual = await Record.ExceptionAsync(() => ProtocolExtensions.ExecuteViaReflectionAsync<int>(null, A.Dummy<IOperation>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("protocol");
        }

        [Fact]
        public static async Task ExecuteViaReflectionAsync_TResult___Should_throw_ArgumentNullException___When_parameter_operation_is_null()
        {
            // Arrange, Act
            var actual = await Record.ExceptionAsync(() => A.Dummy<IProtocol>().ExecuteViaReflectionAsync<int>(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("operation");
        }

        [Fact]
        public static async Task ExecuteViaReflectionAsync_TResult___Should_throw_ArgumentException___When_protocol_cannot_execute_operation()
        {
            // Arrange
            var operation = new DummyReturningOperation();

            var protocol1 = new ProtocolWithNoOperations();
            var protocol2 = new SiblingOperationProtocol();

            // Act
            var actual1 = await Record.ExceptionAsync(() => protocol1.ExecuteViaReflectionAsync<int>(operation));
            var actual2 = await Record.ExceptionAsync(() => protocol2.ExecuteViaReflectionAsync<int>(operation));

            // Assert
            actual1.AsTest().Must().BeOfType<ArgumentException>();
            actual1.Message.AsTest().Must().ContainString(Invariant($"The specified '{nameof(ProtocolExtensionsTest)}.{nameof(ProtocolWithNoOperations)}' protocol does not have a returning ExecuteAsync method with a single parameter that the specified '{nameof(ProtocolExtensionsTest)}.{nameof(DummyReturningOperation)}' operation is assignable to AND a return type that is assignable to the specified 'int' return type."));

            actual2.AsTest().Must().BeOfType<ArgumentException>();
            actual2.Message.AsTest().Must().ContainString(Invariant($"The specified '{nameof(ProtocolExtensionsTest)}.{nameof(SiblingOperationProtocol)}' protocol does not have a returning ExecuteAsync method with a single parameter that the specified '{nameof(ProtocolExtensionsTest)}.{nameof(DummyReturningOperation)}' operation is assignable to AND a return type that is assignable to the specified 'int' return type."));
        }

        [Fact]
        public static async Task ExecuteViaReflectionAsync_TResult___Should_throw_ArgumentException___When_protocol_has_multiple_execute_methods_for_operation()
        {
            // Arrange
            var operation = new ChildReturningOperation();

            var protocol = new FamilyReturningProtocol();

            // Act
            var actual = await Record.ExceptionAsync(() => protocol.ExecuteViaReflectionAsync<int>(operation));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().ContainString(Invariant($"The specified '{nameof(ProtocolExtensionsTest)}.{nameof(FamilyReturningProtocol)}' protocol has more than one returning ExecuteAsync method with a single parameter that the specified '{nameof(ProtocolExtensionsTest)}.{nameof(ChildReturningOperation)}' operation is assignable to AND a return type that is assignable to the specified 'int' return type."));
        }

        [Fact]
        public static async Task ExecuteViaReflectionAsync_TResult___Should_throw_same_exception_throw_by_protocol___When_protocol_throws()
        {
            // Arrange
            var operation = new ThrowingReturningOp();

            var protocol = new ThrowingProtocol();

            // Act
            var actual = await Record.ExceptionAsync(() => protocol.ExecuteViaReflectionAsync<bool>(operation));

            // Assert
            actual.AsTest().Must().BeOfType<InvalidOperationException>();
            actual.Message.AsTest().Must().BeEqualTo("execute-returning-async");
        }

        [Fact]
        public static async Task ExecuteViaReflectionAsync_TResult___Should_execute_the_operation___When_called()
        {
            // Arrange
            var operation1 = new SiblingOperation3
            {
                Value = 4,
            };

            var operation2 = new SiblingOperation4
            {
                Value = 6,
            };

            var operation3 = new SiblingOperation3
            {
                Value = 1,
            };

            var protocol = new SiblingOperationProtocol();

            // Act
            var actual1 = await protocol.ExecuteViaReflectionAsync<int>(operation1);
            var actual2 = await protocol.ExecuteViaReflectionAsync<int>(operation2);
            var actual3 = await protocol.ExecuteViaReflectionAsync<int>(operation3);

            // Assert
            actual1.AsTest().Must().BeEqualTo(7);
            actual2.AsTest().Must().BeEqualTo(10);
            actual3.AsTest().Must().BeEqualTo(4);
        }

        [Fact]
        public static void ToProtocolFactory___Should_throw_ArgumentNullException___When_parameter_protocol_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ProtocolExtensions.ToProtocolFactory(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("protocol");
        }

        [Fact]
        public static void ToProtocolFactory___Should_create_working_factory___When_called()
        {
            // Arrange
            IProtocol protocol = new SiblingOperationProtocol();

            // Act
            var actual = protocol.ToProtocolFactory();

            // Assert
            actual.Execute(new GetProtocolOp(new SiblingOperation4())).AsTest().Must().BeSameReferenceAs(protocol);
        }

        private class DummyVoidOperation : IVoidOperation
        {
        }

        private class DummyReturningOperation : IReturningOperation<int>
        {
        }

        private class SiblingOperation1 : IVoidOperation
        {
            public Action ActionToRun { get; set; }
        }

        private class SiblingOperation2 : IVoidOperation
        {
            public Action ActionToRun { get; set; }
        }

        private class SiblingOperation3 : IReturningOperation<int>
        {
            public int Value { get; set; }
        }

        private class SiblingOperation4 : IReturningOperation<int>
        {
            public int Value { get; set; }
        }

        private abstract class ParentVoidOperationBase : IVoidOperation
        {
        }

        private class ChildVoidOperation : ParentVoidOperationBase
        {
        }

        private abstract class ParentReturningOperationBase : IReturningOperation<int>
        {
        }

        private class ChildReturningOperation : ParentReturningOperationBase
        {
        }

        private class ProtocolWithNoOperations : IProtocol
        {
        }

        private class ThrowingVoidOp : IVoidOperation
        {
        }

        private class ThrowingReturningOp : IReturningOperation<bool>
        {
        }

        private class ThrowingProtocol :
            ISyncAndAsyncVoidProtocol<ThrowingVoidOp>,
            ISyncAndAsyncReturningProtocol<ThrowingReturningOp, bool>
        {
            public void Execute(ThrowingVoidOp operation)
            {
                throw new InvalidOperationException("execute-void-sync");
            }

            public Task ExecuteAsync(ThrowingVoidOp operation)
            {
                throw new InvalidOperationException("execute-void-async");
            }

            public bool Execute(ThrowingReturningOp operation)
            {
                throw new InvalidOperationException("execute-returning-sync");
            }

            public Task<bool> ExecuteAsync(ThrowingReturningOp operation)
            {
                throw new InvalidOperationException("execute-returning-async");
            }
        }

        private class SiblingOperationProtocol :
            ISyncVoidProtocol<SiblingOperation1>,
            ISyncVoidProtocol<SiblingOperation2>,
            ISyncReturningProtocol<SiblingOperation3, int>,
            ISyncReturningProtocol<SiblingOperation4, int>,
            IAsyncVoidProtocol<SiblingOperation1>,
            IAsyncVoidProtocol<SiblingOperation2>,
            IAsyncReturningProtocol<SiblingOperation3, int>,
            IAsyncReturningProtocol<SiblingOperation4, int>
        {
            public void Execute(SiblingOperation1 operation)
            {
                operation.ActionToRun?.Invoke();
            }

            public void Execute(SiblingOperation2 operation)
            {
                operation.ActionToRun?.Invoke();
            }

            public int Execute(SiblingOperation3 operation)
            {
                var result = operation.Value + 1;

                return result;
            }

            public int Execute(SiblingOperation4 operation)
            {
                var result = operation.Value + 2;

                return result;
            }

            public async Task ExecuteAsync(SiblingOperation1 operation)
            {
                operation.ActionToRun?.Invoke();
                operation.ActionToRun?.Invoke();

                await Task.FromResult(1);
            }

            public async Task ExecuteAsync(SiblingOperation2 operation)
            {
                operation.ActionToRun?.Invoke();
                operation.ActionToRun?.Invoke();

                await Task.FromResult(1);
            }

            public async Task<int> ExecuteAsync(SiblingOperation3 operation)
            {
                var result = operation.Value + 3;

                await Task.FromResult(result);

                return result;
            }

            public async Task<int> ExecuteAsync(SiblingOperation4 operation)
            {
                var result = operation.Value + 4;

                await Task.FromResult(result);

                return result;
            }
        }

        private class FamilyVoidProtocol :
            ISyncVoidProtocol<ParentVoidOperationBase>,
            ISyncVoidProtocol<ChildVoidOperation>,
            IAsyncVoidProtocol<ParentVoidOperationBase>,
            IAsyncVoidProtocol<ChildVoidOperation>
        {
            public void Execute(ParentVoidOperationBase operation)
            {
                throw new NotImplementedException();
            }

            public void Execute(ChildVoidOperation operation)
            {
                throw new NotImplementedException();
            }

            public Task ExecuteAsync(ParentVoidOperationBase operation)
            {
                throw new NotImplementedException();
            }

            public Task ExecuteAsync(ChildVoidOperation operation)
            {
                throw new NotImplementedException();
            }
        }

        private class FamilyReturningProtocol :
            ISyncReturningProtocol<ParentReturningOperationBase, int>,
            ISyncReturningProtocol<ChildReturningOperation, int>,
            IAsyncReturningProtocol<ParentReturningOperationBase, int>,
            IAsyncReturningProtocol<ChildReturningOperation, int>
        {
            public int Execute(ParentReturningOperationBase operation)
            {
                throw new NotImplementedException();
            }

            public int Execute(ChildReturningOperation operation)
            {
                throw new NotImplementedException();
            }

            public Task<int> ExecuteAsync(ParentReturningOperationBase operation)
            {
                throw new NotImplementedException();
            }

            public Task<int> ExecuteAsync(ChildReturningOperation operation)
            {
                throw new NotImplementedException();
            }
        }
    }
}