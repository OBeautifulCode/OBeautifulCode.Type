// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIdentifiableByString.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Represents an object that has a string unique identifier.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public interface IIdentifiableByString : IIdentifiable
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        string Id { get; set; }
    }
}
