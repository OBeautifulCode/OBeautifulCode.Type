// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpExecutionDeemedNotApplicableException.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Exception thrown when the execution of an <see cref="IOperation"/> deems itself not applicable.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = ObcSuppressBecause.CA1032_ImplementStandardExceptionConstructors_ExceptionUsedInternallyAndConstructorsEnsureRequiredInfoAvailableWhenCaught)]
    public class OpExecutionDeemedNotApplicableException : OpExecutionDeemedNotApplicableExceptionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionDeemedNotApplicableException"/> class.
        /// </summary>
        /// <param name="operation">OPTIONAL operation whose execution is not applicable.  DEFAULT is to omit that operation.</param>
        public OpExecutionDeemedNotApplicableException(
            IOperation operation = null)
            : base(operation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionDeemedNotApplicableException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="operation">OPTIONAL operation whose execution is not applicable.  DEFAULT is to omit that operation.</param>
        public OpExecutionDeemedNotApplicableException(
            string message,
            IOperation operation = null)
            : base(message, operation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionDeemedNotApplicableException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="operation">OPTIONAL operation whose execution is not applicable.  DEFAULT is to omit that operation.</param>
        public OpExecutionDeemedNotApplicableException(
            string message,
            Exception innerException,
            IOperation operation = null)
            : base(message, innerException, operation)
        {
        }
    }
}
