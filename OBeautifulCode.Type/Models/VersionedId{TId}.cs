// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionedId{TId}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Represents a versioned identifier.
    /// </summary>
    /// <remarks>
    /// This object is useful in modeling identifiable objects that change over time but retain the same identifier.
    /// </remarks>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public partial class VersionedId<TId> : IHaveId<TId>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VersionedId{TId}"/> class.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <param name="version">The version of the identifier.</param>
        public VersionedId(
            TId id,
            Version version)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (version == null)
            {
                throw new ArgumentNullException(nameof(version));
            }

            this.Id = id;
            this.Version = version;
        }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public TId Id { get; private set; }

        /// <summary>
        /// Gets the version of the identifier.
        /// </summary>
        public Version Version { get; private set; }
    }
}