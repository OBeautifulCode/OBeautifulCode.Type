// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullEvent.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Null object pattern implementation for <see cref="EventBase"/>.
    /// </summary>
    public partial class NullEvent : EventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullEvent"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp; probably best to put <see cref="DateTime.MinValue"/> converted to UTC.  Cannot be defaulted do to framework limitations.</param>
        public NullEvent(DateTime timestampUtc)
            : base(timestampUtc)
        {
        }
    }
}
