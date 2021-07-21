// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpExecutionFailedException.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Exception thrown when the execution of an <see cref="IOperation"/> has failed.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = ObcSuppressBecause.CA1032_ImplementStandardExceptionConstructors_ExceptionUsedInternallyAndConstructorsEnsureRequiredInfoAvailableWhenCaught)]
    public class OpExecutionFailedException : OpExecutionFailedExceptionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionFailedException"/> class.
        /// </summary>
        /// <param name="operation">OPTIONAL operation whose execution failed.  DEFAULT is to omit that operation.</param>
        public OpExecutionFailedException(
            IOperation operation = null)
            : base(operation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionFailedException"/> class.
        /// </summary>
        /// <param name="message">Message for exception.</param>
        /// <param name="operation">OPTIONAL operation whose execution failed.  DEFAULT is to omit that operation.</param>
        public OpExecutionFailedException(
            string message,
            IOperation operation = null)
            : base(message, operation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionFailedException"/> class.
        /// </summary>
        /// <param name="message">Message for exception.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="operation">OPTIONAL operation whose execution failed.  DEFAULT is to omit that operation.</param>
        public OpExecutionFailedException(
            string message,
            Exception innerException,
            IOperation operation = null)
            : base(message, innerException, operation)
        {
        }
    }
}
