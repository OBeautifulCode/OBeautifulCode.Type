// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISyncReturningProtocol{TOperation,TReturn}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Executes a <see cref="IReturningOperation{TReturn}"/> synchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <typeparam name="TReturn">The type that the operation returns.</typeparam>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface ISyncReturningProtocol<TOperation, TReturn> : IProtocol<TOperation>
        where TOperation : IReturningOperation<TReturn>
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The result of the operation.</returns>
        TReturn Execute(TOperation operation);
    }
}