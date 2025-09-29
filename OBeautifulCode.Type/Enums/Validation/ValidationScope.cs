// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationScope.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// The scope of the validation.
    /// </summary>
    public enum ValidationScope
    {
        /// <summary>
        /// Validate the subject object as well as all of it's properties,
        /// recursing thru the entire object graph.
        /// </summary>
        SelfAndProperties,

        /// <summary>
        /// Only validate the object that is the subject of validation (self-validation).
        /// Do not recurse thru the object's properties.
        /// </summary>
        SelfOnly,
    }
}
