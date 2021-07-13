// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISyncAndAsyncReturningProtocol{TOperation,TReturn}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Executes a <see cref="IReturningOperation{TReturn}"/> both synchronously and asynchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <typeparam name="TReturn">The type that the operation returns.</typeparam>
    public interface ISyncAndAsyncReturningProtocol<TOperation, TReturn> : ISyncReturningProtocol<TOperation, TReturn>, IAsyncReturningProtocol<TOperation, TReturn>
        where TOperation : IReturningOperation<TReturn>
    {
    }
}