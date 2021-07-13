// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveTags.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an object that can be tagged using named strings.
    /// </summary>
    public interface IHaveTags
    {
        /// <summary>
        /// Gets the tags as named strings.
        /// </summary>
        /// <remarks>
        /// null is valid value.
        /// If the tags have a single dimension/no grouping concept,
        /// then they can be specified as the names with null values.
        /// </remarks>
        IReadOnlyCollection<NamedValue<string>> Tags { get; }
    }
}
