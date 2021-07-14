// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionMatchStrategy.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Determines the acceptable version to use of some item.
    /// </summary>
    public enum VersionMatchStrategy
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Any version can be used.
        /// </summary>
        Any,

        /// <summary>
        /// Any single version can be used.  If multiple versions found then there's no match.
        /// </summary>
        AnySingleVersion,

        /// <summary>
        /// Use the max version.
        /// </summary>
        MaxVersion,

        /// <summary>
        /// Use the min version.
        /// </summary>
        MinVersion,

        /// <summary>
        /// Use a specified version.
        /// </summary>
        SpecifiedVersion,
    }
}