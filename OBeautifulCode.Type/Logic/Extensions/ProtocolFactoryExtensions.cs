// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolFactoryExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Extension methods related to <see cref="IProtocolFactory"/>.
    /// </summary>
    public static class ProtocolFactoryExtensions
    {
        /// <summary>
        /// Executes a void operation synchronously using a protocol returned by a specified protocol factory.
        /// </summary>
        /// <param name="protocolFactory">The protocol factory.</param>
        /// <param name="operation">The operation.</param>
        public static void GetProtocolAndExecuteViaReflection(
            this ISyncReturningProtocol<GetProtocolOp, IProtocol> protocolFactory,
            IOperation operation)
        {
            if (protocolFactory == null)
            {
                throw new ArgumentNullException(nameof(protocolFactory));
            }

            var getProtocolOp = new GetProtocolOp(operation);

            var protocol = protocolFactory.Execute(getProtocolOp);

            protocol.ExecuteViaReflection(operation);
        }

        /// <summary>
        /// Executes a returning operation synchronously using a protocol returned by a specified protocol factory.
        /// </summary>
        /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
        /// <param name="protocolFactory">The protocol factory.</param>
        /// <param name="operation">The operation.</param>
        /// <returns>
        /// The results of executing the operation.
        /// </returns>
        public static TResult GetProtocolAndExecuteViaReflection<TResult>(
            this ISyncReturningProtocol<GetProtocolOp, IProtocol> protocolFactory,
            IOperation operation)
        {
            if (protocolFactory == null)
            {
                throw new ArgumentNullException(nameof(protocolFactory));
            }

            var getProtocolOp = new GetProtocolOp(operation);

            var protocol = protocolFactory.Execute(getProtocolOp);

            var result = protocol.ExecuteViaReflection<TResult>(operation);

            return result;
        }

        /// <summary>
        /// Executes a void operation asynchronously using a protocol returned by a specified protocol factory.
        /// </summary>
        /// <param name="protocolFactory">The protocol factory.</param>
        /// <param name="operation">The operation.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public static async Task GetProtocolAndExecuteViaReflectionAsync(
            this IAsyncReturningProtocol<GetProtocolOp, IProtocol> protocolFactory,
            IOperation operation)
        {
            if (protocolFactory == null)
            {
                throw new ArgumentNullException(nameof(protocolFactory));
            }

            var getProtocolOp = new GetProtocolOp(operation);

            var protocol = await protocolFactory.ExecuteAsync(getProtocolOp);

            await protocol.ExecuteViaReflectionAsync(operation);
        }

        /// <summary>
        /// Executes a returning operation asynchronously using a protocol returned by a specified protocol factory.
        /// </summary>
        /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
        /// <param name="protocolFactory">The protocol factory.</param>
        /// <param name="operation">The operation.</param>
        /// <returns>
        /// The results of executing the operation.
        /// </returns>
        public static async Task<TResult> GetProtocolAndExecuteViaReflectionAsync<TResult>(
            this IAsyncReturningProtocol<GetProtocolOp, IProtocol> protocolFactory,
            IOperation operation)
        {
            if (protocolFactory == null)
            {
                throw new ArgumentNullException(nameof(protocolFactory));
            }

            var getProtocolOp = new GetProtocolOp(operation);

            var protocol = await protocolFactory.ExecuteAsync(getProtocolOp);

            var result = await protocol.ExecuteViaReflectionAsync<TResult>(operation);

            return result;
        }
    }
}
