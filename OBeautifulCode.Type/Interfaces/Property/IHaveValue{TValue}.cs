// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveValue{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Interface to declare having a typed value.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    public interface IHaveValue<TValue> : IHaveValue
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>
        /// The value.
        /// </returns>
        TValue Value { get; }
    }
}
