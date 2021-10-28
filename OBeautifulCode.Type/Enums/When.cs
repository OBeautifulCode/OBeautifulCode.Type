// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Specifies when something should or has happened.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "When", Justification = "This is the best name for this identifier.")]
    public enum When
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The thing should happen before some other thing.
        /// </summary>
        Before,

        /// <summary>
        /// The thing should happen at the same moment as some other thing.
        /// </summary>
        At,

        /// <summary>
        /// The thing should happen after some other thing.
        /// </summary>
        After,
    }
}
