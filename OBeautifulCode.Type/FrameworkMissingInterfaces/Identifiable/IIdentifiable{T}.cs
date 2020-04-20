// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIdentifiable{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Represents an object that has a unique identifier.
    /// </summary>
    /// <typeparam name="T">The type of the unique identifier.</typeparam>
    // ReSharper disable once UnusedMember.Global
    public interface IIdentifiable<T> : IIdentifiable
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        T Id { get; set; }
    }
}
