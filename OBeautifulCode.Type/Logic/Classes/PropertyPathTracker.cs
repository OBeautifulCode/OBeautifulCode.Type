// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyPathTracker.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static System.FormattableString;

    /// <summary>
    /// Tracks the path taken in the traversal of an object's property graph.
    /// </summary>
    public class PropertyPathTracker
    {
        private readonly Stack<string> segments = new Stack<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyPathTracker"/> class.
        /// </summary>
        /// <param name="segmentSeparator">OPTIONAL separator to use to distinguish segments in the path.  DEFAULT is the period character.</param>
        public PropertyPathTracker(
            string segmentSeparator = ".")
        {
            if (segmentSeparator == null)
            {
                throw new ArgumentNullException(nameof(segmentSeparator));
            }

            if (string.IsNullOrWhiteSpace(segmentSeparator))
            {
                throw new ArgumentException(Invariant($"{nameof(segmentSeparator)} is white space."), nameof(segmentSeparator));
            }

            this.SegmentSeparator = segmentSeparator;
        }

        /// <summary>
        /// Gets the separator to use to distinguish segments in the path.
        /// </summary>
        public string SegmentSeparator { get; }

        /// <summary>
        /// Gets the full path.
        /// </summary>
        public string FullPath => string.Join(this.SegmentSeparator, this.segments.Reverse());

        /// <summary>
        /// Gets a value indicating whether there are any segments.
        /// </summary>
        public bool HasSegments => this.segments.Any();

        /// <summary>
        /// Pushes a segment onto the path.
        /// </summary>
        /// <param name="segment">The segment traversed.</param>
        /// <returns>
        /// An object that, when disposed, removes the segment from the path.
        /// </returns>
        public IDisposable Push(
            string segment)
        {
            this.segments.Push(segment);

            var result = new PopOnDispose(this);

            return result;
        }

        private class PopOnDispose : IDisposable
        {
            private readonly PropertyPathTracker tracker;

            public PopOnDispose(PropertyPathTracker tracker) => this.tracker = tracker;

            public void Dispose() => this.tracker.segments.Pop();
        }
    }
}
