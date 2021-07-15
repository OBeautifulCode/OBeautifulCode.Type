// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SyncSpecificReturningProtocolBase{TOperation,TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Threading.Tasks;

    /// <summary>
    /// Protocol that gives pass-through implementation for the asynchronous execution for <see cref="ISyncAndAsyncReturningProtocol{TOperation,TResult}"/>.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
    public abstract class SyncSpecificReturningProtocolBase<TOperation, TResult> : ISyncAndAsyncReturningProtocol<TOperation, TResult>
        where TOperation : IReturningOperation<TResult>
    {
        /// <inheritdoc />
        public abstract TResult Execute(
            TOperation operation);

        /// <inheritdoc />
        public async Task<TResult> ExecuteAsync(
            TOperation operation)
        {
            var syncResult = this.Execute(operation);

            var result = await Task.FromResult(syncResult);

            return result;
        }
    }
}
