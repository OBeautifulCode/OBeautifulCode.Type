// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncSpecificReturningProtocolBase{TOperation,TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Threading.Tasks;

    using OBeautifulCode.Execution.Recipes;

    /// <summary>
    /// Protocol that gives pass-through implementation for the synchronous execution for <see cref="ISyncAndAsyncReturningProtocol{TOperation,TResult}"/>.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
    public abstract class AsyncSpecificReturningProtocolBase<TOperation, TResult> : ISyncAndAsyncReturningProtocol<TOperation, TResult>
        where TOperation : IReturningOperation<TResult>
    {
        /// <inheritdoc />
        public TResult Execute(
            TOperation operation)
        {
            var task = this.ExecuteAsync(operation);

            var result = task.RunUntilCompletion();

            return result;
        }

        /// <inheritdoc />
        public abstract Task<TResult> ExecuteAsync(
            TOperation operation);
    }
}
