// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IComparableForRelativeSortOrder{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Defines a generalized comparison method that a value type or class implements
    /// to create a type-specific comparison method for ordering or sorting its instances.
    /// </summary>
    /// <typeparam name="T">The type of the objects being compared.</typeparam>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IComparableForRelativeSortOrder<T> : IComparable<T>, IComparable
    {
        /// <summary>
        /// Compares the current instance with another object of the same type and
        /// returns a value that indicates whether the current instance precedes, follows,
        /// or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared.
        /// </returns>
        RelativeSortOrder CompareToForRelativeSortOrder(T other);
    }
}
