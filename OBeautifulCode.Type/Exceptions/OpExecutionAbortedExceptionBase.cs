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
        /// <param name="executingOperation">The operation that was executing and is now aborted.</param>
        /// <param name="abortingOperation">The operation that aborted the execution of <paramref name="executingOperation"/>.</param>
        protected OpExecutionAbortedExceptionBase(
            IOperation executingOperation,
            IOperation abortingOperation)
            : base()
        {
            this.ExecutingOperation = executingOperation;
            this.AbortingOperation = abortingOperation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionAbortedExceptionBase"/> class.
        /// </summary>
        /// <param name="message">Message for exception.</param>
        /// <param name="executingOperation">The operation that was executing and is now aborted.</param>
        /// <param name="abortingOperation">The operation that aborted the execution of <paramref name="executingOperation"/>.</param>
        protected OpExecutionAbortedExceptionBase(
            string message,
            IOperation executingOperation,
            IOperation abortingOperation)
            : base(message)
        {
            this.ExecutingOperation = executingOperation;
            this.AbortingOperation = abortingOperation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionAbortedExceptionBase"/> class.
        /// </summary>
        /// <param name="message">Message for exception.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="executingOperation">The operation that was executing and is now aborted.</param>
        /// <param name="abortingOperation">The operation that aborted the execution of <paramref name="executingOperation"/>.</param>
        protected OpExecutionAbortedExceptionBase(
            string message,
            Exception innerException,
            IOperation executingOperation,
            IOperation abortingOperation)
            : base(message, innerException)
        {
            this.ExecutingOperation = executingOperation;
            this.AbortingOperation = abortingOperation;
        }

        /// <summary>
        /// Gets the operation that was executing and is now aborted.
        /// </summary>
        public IOperation ExecutingOperation { get; private set; }

        /// <summary>
        /// Gets the operation that aborted the execution of <see cref="ExecutingOperation"/>.
        /// </summary>
        public IOperation AbortingOperation { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = string.Empty;

            if (this.ExecutingOperation != null)
            {
                result = result + "This operation was executing and is now aborted: " + this.ExecutingOperation + Environment.NewLine;
            }

            if (this.AbortingOperation != null)
            {
                result = result + "This operation caused the execution of another operation to be aborted: " + this.ExecutingOperation + Environment.NewLine;
            }

            result = result + base.ToString();

            return result;
        }
    }
}
