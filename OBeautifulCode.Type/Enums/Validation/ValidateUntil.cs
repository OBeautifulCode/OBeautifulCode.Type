// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateUntil.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Specifies when the validation should stop.
    /// </summary>
    public enum ValidateUntil
    {
        /// <summary>
        /// Do not stop until the object graph is fully traversed, regardless of any validation
        /// failures that are encountered.
        /// </summary>
        FullyTraversed,

        /// <summary>
        /// Stop when the first object containing one or more validation failures is encountered.
        /// </summary>
        /// <remarks>
        /// Calling <see cref="IValidatable.GetSelfValidationFailures"/> may yield multiple failures.
        /// The validation framework can only evaluate whether to stop after this call and thus it's possible
        /// to return multiple validation failures.  The only guarantee is that no other object will be validated.
        /// </remarks>
        FirstInvalidObject,
    }
}
