// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationException.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Exception thrown when there are validation failures.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = ObcSuppressBecause.CA1032_ImplementStandardExceptionConstructors_ExceptionUsedInternallyAndConstructorsEnsureRequiredInfoAvailableWhenCaught)]
    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = ObcSuppressBecause.CA2237_MarkISerializableTypesWithSerializable_ExceptionOnlyUsedInternallyAndWillNeverBeSerialized)]
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="failures">The validation failures.</param>
        public ValidationException(
            IReadOnlyList<ValidationFailure> failures)
        {
            this.Failures = failures;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the validation failures.</param>
        /// <param name="failures">The validation failures.</param>
        public ValidationException(
            string message,
            IReadOnlyList<ValidationFailure> failures)
            : base(message)
        {
            this.Failures = failures;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the validation failures.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="failures">The validation failures.</param>
        public ValidationException(
            string message,
            Exception innerException,
            IReadOnlyList<ValidationFailure> failures)
            : base(message, innerException)
        {
            this.Failures = failures;
        }

        /// <summary>
        /// Gets the validation failures.
        /// </summary>
        public IReadOnlyList<ValidationFailure> Failures { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = string.Empty;

            if (this.Failures != null)
            {
                result += "Validation failures: ";

                foreach (var failure in this.Failures)
                {
                    if (failure != null)
                    {
                        result += Environment.NewLine + "- " + failure;
                    }
                }

                result += Environment.NewLine;
            }

            result += base.ToString();

            return result;
        }
    }
}
