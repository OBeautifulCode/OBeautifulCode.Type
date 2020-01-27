// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeclareEqualsMethod{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Declares the <see cref="IEquatable{T}.Equals(T)"/> method.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare for equality.</typeparam>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IDeclareEqualsMethod<T>
    {
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        bool Equals(T other);
    }
}
