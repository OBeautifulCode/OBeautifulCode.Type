// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatableExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Extension methods on <see cref="IValidatable"/>.
    /// </summary>
    public static class ValidatableExtensions
    {
        /// <summary>
        /// Determines if a specified object is valid.
        /// </summary>
        /// <param name="validatable">The subject object.</param>
        /// <param name="failures">The validation failures encountered or an empty list if none.</param>
        /// <param name="options">
        /// OPTIONAL validation options that control how validation is performed.
        /// DEFAULT is to validate the subject object as well as all of it's properties,
        /// recursing thru the entire object graph and not stopping at any point until
        /// the entire graph is traversed, performing "self validation" first and then
        /// validating properties.
        /// </param>
        /// <returns>
        /// true if the object is valid, otherwise false.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = ObcSuppressBecause.CA1021_AvoidOutParameters_OutParameterRequiredForTryMethod)]
        public static bool IsValid(
            this IValidatable validatable,
            out IReadOnlyList<ValidationFailure> failures,
            ValidationOptions options = null)
        {
            if (validatable == null)
            {
                throw new ArgumentNullException(nameof(validatable));
            }

            failures = validatable.GetValidationFailures(options);

            var result = !failures.Any();

            return result;
        }

        /// <summary>
        /// Throws a <see cref="ValidationException"/> if the specified object is invalid.
        /// </summary>
        /// <param name="validatable">The subject object.</param>
        /// <param name="options">
        /// OPTIONAL validation options that control how validation is performed.
        /// DEFAULT is to validate the subject object as well as all of it's properties,
        /// recursing thru the entire object graph and not stopping at any point until
        /// the entire graph is traversed, performing "self validation" first and then
        /// validating properties.
        /// </param>
        public static void ThrowIfInvalid(
            this IValidatable validatable,
            ValidationOptions options = null)
        {
            if (validatable == null)
            {
                throw new ArgumentNullException(nameof(validatable));
            }

            if (!validatable.IsValid(out var failures, options))
            {
                throw new ValidationException(failures);
            }
        }
    }
}
