// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStringRepresentable.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Creates a string representation of an instance of the object.
    /// </summary>
    public interface IStringRepresentable
    {
        /// <summary>
        /// Construct a friendly string representation of this object.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        // ReSharper disable once UnusedMember.Global
        string ToString();
    }
}
