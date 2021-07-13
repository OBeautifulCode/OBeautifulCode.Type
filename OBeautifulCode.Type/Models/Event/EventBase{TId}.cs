// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventBase{TId}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Base implementation of <see cref="IEvent{TId}"/>.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class EventBase<TId> : EventBase, IEvent<TId>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventBase{TId}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        protected EventBase(
            TId id,
            DateTime timestampUtc)
            : base(timestampUtc)
        {
            this.Id = id;
        }

        /// <inheritdoc />
        public TId Id { get; private set; }
    }
}