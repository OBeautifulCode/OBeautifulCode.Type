// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveTimestampUtc.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Represents an object that is timestamped in UTC.
    /// </summary>
    public interface IHaveTimestampUtc
    {
        /// <summary>
        /// Gets the timestamp in UTC.
        /// </summary>
        DateTime TimestampUtc { get; }
    }
}
