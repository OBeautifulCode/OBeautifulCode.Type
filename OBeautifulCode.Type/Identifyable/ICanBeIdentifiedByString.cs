// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICanBeIdentifiedByString.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Represents an object that has a string unique identifier.
    /// </summary>
    public interface ICanBeIdentifiedByString : ICanBeIdentified
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        string Id { get; set; }
    }
}
