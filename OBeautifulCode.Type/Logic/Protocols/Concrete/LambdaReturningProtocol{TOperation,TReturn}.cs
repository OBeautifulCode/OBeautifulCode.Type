// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaReturningProtocol{TOperation,TReturn}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Threading.Tasks;

    using OBeautifulCode.Execution.Recipes;

    /// <summary>
    /// Protocolizes a returning lambda.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    /// <typeparam name="TReturn">Type of the return.</typeparam>
    public class LambdaReturningProtocol<TOperation, TReturn> : ISyncAndAsyncReturningProtocol<TOperation, TReturn>
    where TOperation : IReturningOperation<TReturn>
    {
        private readonly Func<TOperation, TReturn> synchronousLambda;

        private readonly Func<TOperation, Task<TReturn>> asyncAsynchronousLambda;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaReturningProtocol{TOperation,TReturn}"/> class.
        /// </summary>
        /// <param name="asynchronousLambda">The lambda to protocol the operation.</param>
        public LambdaReturningProtocol(
            Func<TOperation, Task<TReturn>> asynchronousLambda)
        {
            if (asynchronousLambda == null)
            {
                throw new ArgumentNullException(nameof(asynchronousLambda));
            }

            this.asyncAsynchronousLambda = asynchronousLambda;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaReturningProtocol{TOperation,TReturn}"/> class.
        /// </summary>
        /// <param name="synchronousLambda">The lambda to protocol the operation.</param>
        public LambdaReturningProtocol(
            Func<TOperation, TReturn> synchronousLambda)
        {
            if (synchronousLambda == null)
            {
                throw new ArgumentNullException(nameof(synchronousLambda));
            }

            this.synchronousLambda = synchronousLambda;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaReturningProtocol{TOperation,TReturn}"/> class.
        /// </summary>
        /// <param name="synchronousLambda">The synchronous lambda to protocol the operation.</param>
        /// <param name="asynchronousLambda">The asynchronous lambda to protocol the operation.</param>
        public LambdaReturningProtocol(
            Func<TOperation, TReturn> synchronousLambda,
            Func<TOperation, Task<TReturn>> asynchronousLambda)
        {
            if (synchronousLambda == null)
            {
                throw new ArgumentNullException(nameof(synchronousLambda));
            }

            if (asynchronousLambda == null)
            {
                throw new ArgumentNullException(nameof(asynchronousLambda));
            }

            this.synchronousLambda = synchronousLambda;
            this.asyncAsynchronousLambda = asynchronousLambda;
        }

        /// <inheritdoc />
        public TReturn Execute(
            TOperation operation)
        {
            var result = this.synchronousLambda != null
                ? this.synchronousLambda(operation)
                : this.asyncAsynchronousLambda(operation).RunUntilCompletion();

            return result;
        }

        /// <inheritdoc />
        public async Task<TReturn> ExecuteAsync(
            TOperation operation)
        {
            var result = await (this.asyncAsynchronousLambda != null
                ? this.asyncAsynchronousLambda(operation)
                : Task.FromResult(this.synchronousLambda(operation)));

            return result;
        }
    }
}
