// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModel.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Represents the contract of a model object.
    /// </summary>
    /// <typeparam name="T">The type of object being modeled.</typeparam>
    // ReSharper disable once UnusedMember.Global
    public interface IModel<T> : IEquatable<T>, IDeepCloneable<T>
    {
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        // ReSharper disable once UnusedMember.Global
        int GetHashCode();

        /// <summary>
        /// Construct a friendly string representation of this object.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        // ReSharper disable once UnusedMember.Global
        string ToString();
    }
}
