// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Range{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// Represents a range of values.
    /// </summary>
    /// <remarks>
    /// There is no information added that specifies whether the range is inclusive or exclusive of the endpoints.
    /// </remarks>
    /// <typeparam name="T">The type of range.</typeparam>
    public partial class Range<T> : IModelViaCodeGen, IDeclareToStringMethod
        where T : IComparable<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Range{T}"/> class.
        /// </summary>
        /// <param name="start">The start of the range.</param>
        /// <param name="end">The end of the range.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/> is greater than <paramref name="end"/>.</exception>
        public Range(
            T start,
            T end)
        {
            if (start.CompareTo(end) > 0)
            {
                throw new ArgumentOutOfRangeException(Invariant($"{nameof(start)} is > {nameof(end)}."));
            }

            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Gets the start of the range.
        /// </summary>
        public T Start { get; private set; }

        /// <summary>
        /// Gets the end of the range.
        /// </summary>
        public T End { get; private set; }

        /// <inheritdoc cref="IDeclareToStringMethod" />
        public override string ToString()
        {
            var result = Invariant($"{this.Start} to {this.End}");

            return result;
        }
    }
}
