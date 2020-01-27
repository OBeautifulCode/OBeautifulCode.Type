// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeclareToStringMethod.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Declares the <see cref="IStringRepresentable.ToString"/> method.
    /// </summary>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IDeclareToStringMethod
    {
        /// <summary>
        /// Construct a friendly string representation of this object.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        // ReSharper disable once UnusedMember.Global
        string ToString();
    }
}
