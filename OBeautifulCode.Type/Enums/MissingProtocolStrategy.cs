// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MissingProtocolStrategy.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Determines what to do when a protocol is not found.
    /// </summary>
    public enum MissingProtocolStrategy
    {
        /// <summary>
        /// Unknown (default)
        /// </summary>
        Unknown,

        /// <summary>
        /// Throw an exception.
        /// </summary>
        Throw,

        /// <summary>
        /// Return null.
        /// </summary>
        ReturnNull,
    }
}
