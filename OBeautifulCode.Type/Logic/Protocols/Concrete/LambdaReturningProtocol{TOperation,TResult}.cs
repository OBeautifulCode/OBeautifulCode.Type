// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaReturningProtocol{TOperation,TResult}.cs" company="OBeautifulCode">
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
    /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
    public class LambdaReturningProtocol<TOperation, TResult> : ISyncAndAsyncReturningProtocol<TOperation, TResult>
    where TOperation : IReturningOperation<TResult>
    {
        private readonly Func<TOperation, TResult> synchronousLambda;

        private readonly Func<TOperation, Task<TResult>> asyncAsynchronousLambda;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaReturningProtocol{TOperation,TResult}"/> class.
        /// </summary>
        /// <param name="asynchronousLambda">The lambda to protocol the operation.</param>
        public LambdaReturningProtocol(
            Func<TOperation, Task<TResult>> asynchronousLambda)
        {
            if (asynchronousLambda == null)
            {
                throw new ArgumentNullException(nameof(asynchronousLambda));
            }

            this.asyncAsynchronousLambda = asynchronousLambda;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaReturningProtocol{TOperation,TResult}"/> class.
        /// </summary>
        /// <param name="synchronousLambda">The lambda to protocol the operation.</param>
        public LambdaReturningProtocol(
            Func<TOperation, TResult> synchronousLambda)
        {
            if (synchronousLambda == null)
            {
                throw new ArgumentNullException(nameof(synchronousLambda));
            }

            this.synchronousLambda = synchronousLambda;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaReturningProtocol{TOperation,TResult}"/> class.
        /// </summary>
        /// <param name="synchronousLambda">The synchronous lambda to protocol the operation.</param>
        /// <param name="asynchronousLambda">The asynchronous lambda to protocol the operation.</param>
        public LambdaReturningProtocol(
            Func<TOperation, TResult> synchronousLambda,
            Func<TOperation, Task<TResult>> asynchronousLambda)
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
        public TResult Execute(
            TOperation operation)
        {
            var result = this.synchronousLambda != null
                ? this.synchronousLambda(operation)
                : this.asyncAsynchronousLambda(operation).RunUntilCompletion();

            return result;
        }

        /// <inheritdoc />
        public async Task<TResult> ExecuteAsync(
            TOperation operation)
        {
            var result = await (this.asyncAsynchronousLambda != null
                ? this.asyncAsynchronousLambda(operation)
                : Task.FromResult(this.synchronousLambda(operation)));

            return result;
        }
    }
}
