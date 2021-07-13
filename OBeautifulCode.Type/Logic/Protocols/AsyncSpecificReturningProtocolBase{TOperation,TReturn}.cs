// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncSpecificReturningProtocolBase{TOperation,TReturn}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Threading.Tasks;

    using OBeautifulCode.Execution.Recipes;

    /// <summary>
    /// Protocol that gives pass-through implementation for the synchronous execution for <see cref="ISyncAndAsyncReturningProtocol{TOperation,TReturn}"/>.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    /// <typeparam name="TReturn">Type of return.</typeparam>
    public abstract class AsyncSpecificReturningProtocolBase<TOperation, TReturn> : ISyncAndAsyncReturningProtocol<TOperation, TReturn>
        where TOperation : IReturningOperation<TReturn>
    {
        /// <inheritdoc />
        public TReturn Execute(
            TOperation operation)
        {
            var task = this.ExecuteAsync(operation);

            var result = task.RunUntilCompletion();

            return result;
        }

        /// <inheritdoc />
        public abstract Task<TReturn> ExecuteAsync(
            TOperation operation);
    }
}
