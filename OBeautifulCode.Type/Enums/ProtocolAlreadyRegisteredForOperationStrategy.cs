// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolAlreadyRegisteredForOperationStrategy.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Determines what do when registering a protocol that supports an operation
    /// that is already registered with the protocol factory via some other protocol.
    /// </summary>
    public enum ProtocolAlreadyRegisteredForOperationStrategy
    {
        /// <summary>
        /// Unknown (default)
        /// </summary>
        Unknown,

        /// <summary>
        /// Throw a <see cref="OpExecutionFailedException"/>.
        /// </summary>
        Throw,

        /// <summary>
        /// Replace the protocol in-use for the operation with the specified protocol.
        /// </summary>
        Replace,

        /// <summary>
        /// Skip the operation; the already specified protocol will be returned for this operation.
        /// </summary>
        Skip,
    }
}