// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeclareGetSelfValidationFailuresMethod.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Collections.Generic;

    /// <summary>
    /// Declares the <see cref="IValidatable.GetSelfValidationFailures"/> method.
    /// </summary>
    /// <remarks>
    /// See <see cref="IValidatable"/> for more details about self-validation.
    /// </remarks>
    public interface IDeclareGetSelfValidationFailuresMethod
    {
        /// <summary>
        /// Gets the self-validation failures, if there are any.
        /// </summary>
        /// <remarks>
        /// This is validation performed on the subject object itself.
        /// This method will/should not recurse through the object's properties to check for validity.
        /// For example, if the object has a property that is an <see cref="IReadOnlyList{T}"/>,
        /// self-validation might check that that list is not null nor empty.  However, it will not
        /// iterate through the list and ask each element to perform validation on itself.
        /// </remarks>
        /// <returns>
        /// The list of validation failures -or- null or empty if there are none.
        /// If the list contains any null elements, those will be discarded
        /// (NOT treated as a validation failure) by <see cref="IValidatable.GetValidationFailures"/>.
        /// </returns>
        IReadOnlyList<SelfValidationFailure> GetSelfValidationFailures();
    }
}
