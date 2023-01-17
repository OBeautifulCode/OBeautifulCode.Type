// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaVoidProtocol.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Threading.Tasks;

    using OBeautifulCode.Execution.Recipes;

    /// <summary>
    /// Protocolizes a non-returning lambda.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    public class LambdaVoidProtocol<TOperation> : ISyncAndAsyncVoidProtocol<TOperation>
    where TOperation : IVoidOperation
    {
        private readonly Action<TOperation> synchronousLambda;

        private readonly Func<TOperation, Task> asyncAsynchronousLambda;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaVoidProtocol{TOperation}"/> class.
        /// </summary>
        /// <param name="asynchronousLambda">The lambda to protocol the operation.</param>
        public LambdaVoidProtocol(
            Func<TOperation, Task> asynchronousLambda)
        {
            if (asynchronousLambda == null)
            {
                throw new ArgumentNullException(nameof(asynchronousLambda));
            }

            this.asyncAsynchronousLambda = asynchronousLambda;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaVoidProtocol{TOperation}"/> class.
        /// </summary>
        /// <param name="synchronousLambda">The lambda to protocol the operation.</param>
        public LambdaVoidProtocol(
            Action<TOperation> synchronousLambda)
        {
            if (synchronousLambda == null)
            {
                throw new ArgumentNullException(nameof(synchronousLambda));
            }

            this.synchronousLambda = synchronousLambda;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaVoidProtocol{TOperation}"/> class.
        /// </summary>
        /// <param name="synchronousLambda">The synchronous lambda to protocol the operation.</param>
        /// <param name="asynchronousLambda">The asynchronous lambda to protocol the operation.</param>
        public LambdaVoidProtocol(
            Action<TOperation> synchronousLambda,
            Func<TOperation, Task> asynchronousLambda)
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
        public void Execute(
            TOperation operation)
        {
            if (this.synchronousLambda != null)
            {
                this.synchronousLambda(operation);
            }
            else
            {
                Func<Task> asyncAsynchronousLambdaFunc = () => this.asyncAsynchronousLambda(operation);

                asyncAsynchronousLambdaFunc.ExecuteSynchronously();
            }
        }

        /// <inheritdoc />
        public async Task ExecuteAsync(
            TOperation operation)
        {
            if (this.asyncAsynchronousLambda != null)
            {
                await this.asyncAsynchronousLambda(operation);
            }
            else
            {
                await Task.Run(() => this.synchronousLambda(operation));
            }
        }
    }
}
