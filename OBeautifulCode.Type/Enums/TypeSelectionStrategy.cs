// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeSelectionStrategy.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Specifies the strategy to use when you need to select a type.
    /// </summary>
    public enum TypeSelectionStrategy
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// Use the declared type.
        /// </summary>
        UseDeclaredType,

        /// <summary>
        /// Use the runtime type.
        /// </summary>
        UseRuntimeType,
    }
}
