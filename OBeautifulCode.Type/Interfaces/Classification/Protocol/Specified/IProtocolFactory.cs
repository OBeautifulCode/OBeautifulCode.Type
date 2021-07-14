// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProtocolFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Returns the protocol to use for a given operation.
    /// </summary>
    public interface IProtocolFactory :
        ISyncAndAsyncReturningProtocol<GetProtocolOp, IProtocol>
    {
    }
}
