// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IShallowCloneable.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Supports shallow cloning, which creates a new instance of a class that is equal to
    /// a reference instance, but where the two instances share memory somewhere in their
    /// object graphs.
    /// </summary>
    /// <typeparam name="T">The type of object to clone.</typeparam>
    // ReSharper disable once UnusedMember.Global
    public interface IShallowCloneable<T> : ICloneable
    {
        /// <summary>
        /// Creates a new object that is a shallow clone of this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a shallow clone of this instance.
        /// </returns>
        // ReSharper disable once UnusedMember.Global
        T DeepClone();
    }
}
