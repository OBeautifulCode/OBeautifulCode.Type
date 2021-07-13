// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAsyncVoidProtocol{TOperation}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Threading.Tasks;

    /// <summary>
    /// Executes a <see cref="IVoidOperation"/> asynchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface IAsyncVoidProtocol<TOperation> : IProtocol<TOperation>
        where TOperation : IVoidOperation
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task ExecuteAsync(TOperation operation);
    }
}