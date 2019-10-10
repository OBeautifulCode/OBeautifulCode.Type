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
    public class UtcDateTimeRangeInclusive : IModel<UtcDateTimeRangeInclusive>
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

        /// <summary>
        /// Determines whether two objects of type <see cref="UtcDateTimeRangeInclusive"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items are equal; false otherwise.</returns>
        public static bool operator ==(
            UtcDateTimeRangeInclusive left,
            UtcDateTimeRangeInclusive right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result =
                (left.StartDateTimeInUtc == right.StartDateTimeInUtc) &&
                (left.EndDateTimeInUtc == right.EndDateTimeInUtc);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="UtcDateTimeRangeInclusive"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The item to compare.</param>
        /// <returns>True if the two items not equal; false otherwise.</returns>
        public static bool operator !=(
            UtcDateTimeRangeInclusive left,
            UtcDateTimeRangeInclusive right)
            => !(left == right);

        /// <inheritdoc />
        public bool Equals(UtcDateTimeRangeInclusive other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as UtcDateTimeRangeInclusive);

        /// <inheritdoc cref="IModel{T}" />
        public override int GetHashCode()
        {
            var result = 17;

            var multiplier = 37;

            result = (result * multiplier) + this.StartDateTimeInUtc.GetHashCode();
            result = (result * multiplier) + this.EndDateTimeInUtc.GetHashCode();

            return result;
        }

        /// <inheritdoc />
        public object Clone() => this.DeepClone();

        /// <inheritdoc />
        public UtcDateTimeRangeInclusive DeepClone()
        {
            var result = new UtcDateTimeRangeInclusive(this.StartDateTimeInUtc, this.EndDateTimeInUtc);

            return result;
        }

        /// <inheritdoc cref="IModel{T}" />
        public override string ToString()
        {
            var result = Invariant($"{this.StartDateTimeInUtc} to {this.EndDateTimeInUtc}");

            return result;
        }
    }
}
