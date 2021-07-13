// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISyncVoidProtocol{TOperation}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Executes a <see cref="IVoidOperation"/> synchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface ISyncVoidProtocol<TOperation> : IProtocol<TOperation>
        where TOperation : IVoidOperation
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        void Execute(TOperation operation);
    }
}
