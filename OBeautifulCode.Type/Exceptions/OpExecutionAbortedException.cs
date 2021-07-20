// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpExecutionAbortedException.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Exception thrown when the execution of an <see cref="IOperation"/> is aborted.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = ObcSuppressBecause.CA1032_ImplementStandardExceptionConstructors_ExceptionUsedInternallyAndConstructorsEnsureRequiredInfoAvailableWhenCaught)]
    public class OpExecutionAbortedException : OpExecutionAbortedExceptionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionAbortedException"/> class.
        /// </summary>
        /// <param name="operation">The operation that aborted the execution.</param>
        public OpExecutionAbortedException(
            IOperation operation)
            : base(operation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionAbortedException"/> class.
        /// </summary>
        /// <param name="message">Message for exception.</param>
        /// <param name="operation">The operation that aborted the execution.</param>
        public OpExecutionAbortedException(
            string message,
            IOperation operation)
            : base(message, operation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionAbortedException"/> class.
        /// </summary>
        /// <param name="message">Message for exception.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="operation">The operation that aborted the execution.</param>
        public OpExecutionAbortedException(
            string message,
            Exception innerException,
            IOperation operation)
            : base(message, innerException, operation)
        {
        }
    }
}
