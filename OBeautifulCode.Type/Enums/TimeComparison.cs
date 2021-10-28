// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeComparison.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Specifies when, in time, something should or has happened relative to some subject time.
    /// </summary>
    public enum TimeComparison
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The thing should or has happen before some subject time.
        /// </summary>
        Before,

        /// <summary>
        /// The thing should or has happen at or before some subject time.
        /// </summary>
        AtOrBefore,

        /// <summary>
        /// The thing should or has happen at some subject time.
        /// </summary>
        At,

        /// <summary>
        /// The thing should or has happen at or after some subject time.
        /// </summary>
        AtOrAfter,

        /// <summary>
        /// The thing should or has happen after some subject time.
        /// </summary>
        After,
    }
}
