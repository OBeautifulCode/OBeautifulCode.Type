// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveId{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Represents an object that has a unique identifier.
    /// </summary>
    /// <typeparam name="T">The type of the unique identifier.</typeparam>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IHaveId<T> : IHaveId
    {
        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        T Id { get; }
    }
}
