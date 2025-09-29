// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationOrder.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Specifies the order in which self-validation and property validation occurs.
    /// </summary>
    public enum ValidationOrder
    {
        /// <summary>
        /// Perform self-validation then validate properties.
        /// </summary>
        SelfThenProperties,

        /// <summary>
        /// Validate properties first, then perform self-validation.
        /// </summary>
        PropertiesThenSelf,
    }
}
