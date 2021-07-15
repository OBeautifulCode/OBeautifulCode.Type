// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISyncAndAsyncReturningProtocol{TOperation,TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Executes a <see cref="IReturningOperation{TResult}"/> both synchronously and asynchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
    public interface ISyncAndAsyncReturningProtocol<TOperation, TResult> : ISyncReturningProtocol<TOperation, TResult>, IAsyncReturningProtocol<TOperation, TResult>
        where TOperation : IReturningOperation<TResult>
    {
    }
}