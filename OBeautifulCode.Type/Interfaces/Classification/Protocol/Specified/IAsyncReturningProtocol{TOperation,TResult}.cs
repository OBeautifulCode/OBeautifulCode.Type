// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAsyncReturningProtocol{TOperation,TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Threading.Tasks;

    /// <summary>
    /// Executes a <see cref="IReturningOperation{TResult}"/> asynchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
    public interface IAsyncReturningProtocol<TOperation, TResult> : IProtocol<TOperation>
        where TOperation : IReturningOperation<TResult>
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The result of the operation.</returns>
        Task<TResult> ExecuteAsync(TOperation operation);
    }
}
