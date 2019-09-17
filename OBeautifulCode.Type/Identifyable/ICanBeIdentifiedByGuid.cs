// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICanBeIdentifiedByGuid.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Represents an object that has a GUID unique identifier.
    /// </summary>
    public interface ICanBeIdentifiedByGuid : ICanBeIdentified
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        Guid Id { get; set; }
    }
}