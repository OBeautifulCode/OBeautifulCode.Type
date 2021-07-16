// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolFactoryExtensionsTest.cs" company="OBeautifulCode">
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

    public static class ProtocolFactoryExtensionsTest
    {
        [Fact]
        public static void GetProtocolAndExecuteViaReflection_void___Should_throw_ArgumentNullException___When_parameter_protocolFactory_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ProtocolFactoryExtensions.GetProtocolAndExecuteViaReflection(null, A.Dummy<IVoidOperation>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("protocolFactory");
        }

        [Fact]
        public static void GetProtocolAndExecuteViaReflection_void___Should_execute_the_operation___When_called()
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

            var protocolFactory = new ProtocolFactory();

            protocolFactory.RegisterProtocol(typeof(SiblingOperationProtocol), () => new SiblingOperationProtocol());

            // Act, Assert
            protocolFactory.GetProtocolAndExecuteViaReflection(operation1);
            operation1Counter.AsTest().Must().BeEqualTo(5);

            protocolFactory.GetProtocolAndExecuteViaReflection(operation2);
            operation2Counter.AsTest().Must().BeEqualTo(11);

            protocolFactory.GetProtocolAndExecuteViaReflection(operation1);
            operation1Counter.AsTest().Must().BeEqualTo(6);
        }

        [Fact]
        public static async Task GetProtocolAndExecuteViaReflectionAsync_void___Should_throw_ArgumentNullException___When_parameter_protocolFactory_is_null()
        {
            // Arrange, Act
            var actual = await Record.ExceptionAsync(() => ProtocolFactoryExtensions.GetProtocolAndExecuteViaReflectionAsync(null, A.Dummy<IVoidOperation>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("protocolFactory");
        }

        [Fact]
        public static async Task GetProtocolAndExecuteViaReflectionAsync_void___Should_execute_the_operation___When_called()
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

            var protocolFactory = new ProtocolFactory();

            protocolFactory.RegisterProtocol(typeof(SiblingOperationProtocol), () => new SiblingOperationProtocol());

            // Act, Assert
            await protocolFactory.GetProtocolAndExecuteViaReflectionAsync(operation1);
            operation1Counter.AsTest().Must().BeEqualTo(6);

            await protocolFactory.GetProtocolAndExecuteViaReflectionAsync(operation2);
            operation2Counter.AsTest().Must().BeEqualTo(12);

            await protocolFactory.GetProtocolAndExecuteViaReflectionAsync(operation1);
            operation1Counter.AsTest().Must().BeEqualTo(8);
        }

        [Fact]
        public static void GetProtocolAndExecuteViaReflection_TResult___Should_throw_ArgumentNullException___When_parameter_protocolFactory_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ProtocolFactoryExtensions.GetProtocolAndExecuteViaReflection(null, A.Dummy<IReturningOperation<Version>>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("protocolFactory");
        }

        [Fact]
        public static void GetProtocolAndExecuteViaReflection_TResult___Should_execute_the_operation___When_called()
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

            var protocolFactory = new ProtocolFactory();

            protocolFactory.RegisterProtocol(typeof(SiblingOperationProtocol), () => new SiblingOperationProtocol());

            // Act
            var actual1 = protocolFactory.GetProtocolAndExecuteViaReflection<int>(operation1);
            var actual2 = protocolFactory.GetProtocolAndExecuteViaReflection<int>(operation2);
            var actual3 = protocolFactory.GetProtocolAndExecuteViaReflection<int>(operation3);

            // Assert
            actual1.AsTest().Must().BeEqualTo(5);
            actual2.AsTest().Must().BeEqualTo(8);
            actual3.AsTest().Must().BeEqualTo(2);
        }

        [Fact]
        public static async Task GetProtocolAndExecuteViaReflectionAsync_TResult___Should_throw_ArgumentNullException___When_parameter_protocolFactory_is_null()
        {
            // Arrange, Act
            var actual = await Record.ExceptionAsync(() => ProtocolFactoryExtensions.GetProtocolAndExecuteViaReflectionAsync(null, A.Dummy<IReturningOperation<Version>>()));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("protocolFactory");
        }

        [Fact]
        public static async Task GetProtocolAndExecuteViaReflectionAsync_TResult___Should_execute_the_operation___When_called()
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

            var protocolFactory = new ProtocolFactory();

            protocolFactory.RegisterProtocol(typeof(SiblingOperationProtocol), () => new SiblingOperationProtocol());

            // Act
            var actual1 = await protocolFactory.GetProtocolAndExecuteViaReflectionAsync<int>(operation1);
            var actual2 = await protocolFactory.GetProtocolAndExecuteViaReflectionAsync<int>(operation2);
            var actual3 = await protocolFactory.GetProtocolAndExecuteViaReflectionAsync<int>(operation3);

            // Assert
            actual1.AsTest().Must().BeEqualTo(7);
            actual2.AsTest().Must().BeEqualTo(10);
            actual3.AsTest().Must().BeEqualTo(4);
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
    }
}