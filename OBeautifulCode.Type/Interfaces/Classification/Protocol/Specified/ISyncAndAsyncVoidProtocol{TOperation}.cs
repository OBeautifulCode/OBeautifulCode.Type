// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISyncAndAsyncVoidProtocol{TOperation}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Executes a <see cref="IVoidOperation"/> both synchronously and asynchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface ISyncAndAsyncVoidProtocol<TOperation> : ISyncVoidProtocol<TOperation>, IAsyncVoidProtocol<TOperation>
        where TOperation : IVoidOperation
    {
    }
}