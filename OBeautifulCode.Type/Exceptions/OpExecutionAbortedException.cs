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
        /// <param name="executingOperation">OPTIONAL operation that was executing and is now aborted.  DEFAULT is to omit that operation.</param>
        /// <param name="abortingOperation">OPTIONAL operation that aborted the execution of <paramref name="executingOperation"/>.  DEFAULT is to omit that operation.</param>
        public OpExecutionAbortedException(
            IOperation executingOperation = null,
            IOperation abortingOperation = null)
            : base(executingOperation, abortingOperation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionAbortedException"/> class.
        /// </summary>
        /// <param name="message">Message for exception.</param>
        /// <param name="executingOperation">OPTIONAL operation that was executing and is now aborted.  DEFAULT is to omit that operation.</param>
        /// <param name="abortingOperation">OPTIONAL operation that aborted the execution of <paramref name="executingOperation"/>.  DEFAULT is to omit that operation.</param>
        public OpExecutionAbortedException(
            string message,
            IOperation executingOperation = null,
            IOperation abortingOperation = null)
            : base(message, executingOperation, abortingOperation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionAbortedException"/> class.
        /// </summary>
        /// <param name="message">Message for exception.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="executingOperation">OPTIONAL operation that was executing and is now aborted.  DEFAULT is to omit that operation.</param>
        /// <param name="abortingOperation">OPTIONAL operation that aborted the execution of <paramref name="executingOperation"/>.  DEFAULT is to omit that operation.</param>
        public OpExecutionAbortedException(
            string message,
            Exception innerException,
            IOperation executingOperation = null,
            IOperation abortingOperation = null)
            : base(message, innerException, executingOperation, abortingOperation)
        {
        }
    }
}
