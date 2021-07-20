// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpExecutionAbortedExceptionBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Base class for exception thrown when the execution of an <see cref="IOperation"/> is aborted.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = ObcSuppressBecause.CA1710_IdentifiersShouldHaveCorrectSuffix_ExceptionBaseClassShouldEndWithBase)]
    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = ObcSuppressBecause.CA2237_MarkISerializableTypesWithSerializable_ExceptionOnlyUsedInternallyAndWillNeverBeSerialized)]
    public abstract class OpExecutionAbortedExceptionBase : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionAbortedExceptionBase"/> class.
        /// </summary>
        /// <param name="operation">The operation that aborted the execution.</param>
        protected OpExecutionAbortedExceptionBase(
            IOperation operation)
            : base()
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionAbortedExceptionBase"/> class.
        /// </summary>
        /// <param name="message">Message for exception.</param>
        /// <param name="operation">The operation that aborted the execution.</param>
        protected OpExecutionAbortedExceptionBase(
            string message,
            IOperation operation)
            : base(message)
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionAbortedExceptionBase"/> class.
        /// </summary>
        /// <param name="message">Message for exception.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="operation">The operation that aborted the execution.</param>
        protected OpExecutionAbortedExceptionBase(
            string message,
            Exception innerException,
            IOperation operation)
            : base(message, innerException)
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Gets the operation that aborted the execution.
        /// </summary>
        public IOperation Operation { get; private set; }
    }
}
