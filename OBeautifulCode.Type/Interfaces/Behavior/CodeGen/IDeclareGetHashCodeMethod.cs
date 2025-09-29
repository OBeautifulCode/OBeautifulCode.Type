// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeclareGetHashCodeMethod.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Declares the <see cref="IHashable.GetHashCode"/> method.
    /// </summary>
    public interface IDeclareGetHashCodeMethod
    {
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        // ReSharper disable once UnusedMember.Global
        int GetHashCode();
    }
}
