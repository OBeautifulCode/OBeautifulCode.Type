// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UtcDateTimeRangeInclusive.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Represents a range of <see cref="DateTime"/>, inclusive of the endpoints.
    /// </summary>
    public partial class UtcDateTimeRangeInclusive : IModelViaCodeGen, IDeclareToStringMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtcDateTimeRangeInclusive"/> class.
        /// </summary>
        /// <param name="startDateTimeInUtc">The start of the range in UTC.</param>
        /// <param name="endDateTimeInUtc">The end of the range in UTC.</param>
        /// <exception cref="ArgumentException"><paramref name="startDateTimeInUtc"/> is not <see cref="DateTimeKind.Utc"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="endDateTimeInUtc"/> is not <see cref="DateTimeKind.Utc"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startDateTimeInUtc"/> is greater than <paramref name="endDateTimeInUtc"/>.</exception>
        public UtcDateTimeRangeInclusive(
            DateTime startDateTimeInUtc,
            DateTime endDateTimeInUtc)
        {
            if (startDateTimeInUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(Invariant($"{nameof(startDateTimeInUtc)} {nameof(DateTimeKind)} is not {nameof(DateTimeKind.Utc)}"), nameof(startDateTimeInUtc));
            }

            if (endDateTimeInUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(Invariant($"{nameof(endDateTimeInUtc)} {nameof(DateTimeKind)} is not {nameof(DateTimeKind.Utc)}"), nameof(endDateTimeInUtc));
            }

            if (startDateTimeInUtc > endDateTimeInUtc)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(startDateTimeInUtc)} is > {nameof(endDateTimeInUtc)}"));
            }

            this.StartDateTimeInUtc = startDateTimeInUtc;
            this.EndDateTimeInUtc = endDateTimeInUtc;
        }

        /// <summary>
        /// Gets the start of the range in UTC.  The range includes this endpoint.
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public DateTime StartDateTimeInUtc { get; private set; }

        /// <summary>
        /// Gets the end of the range in UTC.  The range includes this endpoint.
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public DateTime EndDateTimeInUtc { get; private set; }

        /// <inheritdoc cref="IDeclareToStringMethod" />
        public override string ToString()
        {
            var result = Invariant($"{this.StartDateTimeInUtc} to {this.EndDateTimeInUtc}");

            return result;
        }
    }
}
