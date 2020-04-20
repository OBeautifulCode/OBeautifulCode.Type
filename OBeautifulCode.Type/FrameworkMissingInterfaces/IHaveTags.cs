// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveTags.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an object that can be tagged using key/value pairs of strings.
    /// </summary>
    public interface IHaveTags
    {
        /// <summary>
        /// Gets or sets tags as key/value pairs of strings.
        /// </summary>
        /// <remarks>
        /// null is valid value.  If the tags have a single dimension/no grouping concept,
        /// then they can be specified as the keys with entirely null values.
        /// </remarks>
        IReadOnlyDictionary<string, string> Tags { get; set; }
    }
}
