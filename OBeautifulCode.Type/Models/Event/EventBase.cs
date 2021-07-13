// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Base implementation of <see cref="IEvent"/>.
    /// </summary>
    public abstract partial class EventBase : IEvent, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventBase"/> class.
        /// </summary>
        /// <param name="timestampUtc">The time of the event in UTC.</param>
        protected EventBase(
            DateTime timestampUtc)
        {
            if (timestampUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(Invariant($"{nameof(timestampUtc)}.{nameof(DateTime.Kind)} is not {nameof(DateTimeKind)}.{nameof(DateTimeKind.Utc)}."));
            }

            this.TimestampUtc = timestampUtc;
        }

        /// <inheritdoc />
        public DateTime TimestampUtc { get; private set; }
    }
}