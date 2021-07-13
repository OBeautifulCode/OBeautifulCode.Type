// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullEvent{TId}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Null object pattern implementation for <see cref="EventBase{TId}"/>.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public partial class NullEvent<TId> : EventBase<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullEvent{TId}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp; probably best to put <see cref="DateTime.MinValue"/> converted to UTC.  Cannot be defaulted do to framework limitations.</param>
        public NullEvent(
            TId id,
            DateTime timestampUtc)
            : base(id, timestampUtc)
        {
        }
    }
}
