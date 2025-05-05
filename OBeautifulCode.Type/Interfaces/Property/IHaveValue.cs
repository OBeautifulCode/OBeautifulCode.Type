// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveValue.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Interface to declare having a value.
    /// </summary>
    public interface IHaveValue
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>
        /// The value as an <see cref="object" />.
        /// </returns>
        object GetValue();

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <returns>
        /// The <see cref="Type" /> of the value.
        /// </returns>
        Type GetValueType();
    }
}
