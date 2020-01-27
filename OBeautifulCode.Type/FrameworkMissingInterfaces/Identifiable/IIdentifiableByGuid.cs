// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIdentifiableByGuid.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Represents an object that has a GUID unique identifier.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public interface IIdentifiableByGuid : IIdentifiable
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Guid Id { get; set; }
    }
}