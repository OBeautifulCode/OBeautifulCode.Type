// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveVersionedId{TId}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Represents an object that has a versioned unique identifier.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identifier.</typeparam>
    public interface IHaveVersionedId<TId> : IHaveVersionedId, IHaveId<TId>
    {
        /// <summary>
        /// Gets the versioned identifier.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        VersionedId<TId> VersionedId { get; }
    }
}
