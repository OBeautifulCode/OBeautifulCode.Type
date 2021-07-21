// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpExecutionFailedExceptionBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Base class for exception thrown when the execution of an <see cref="IOperation"/> has failed.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = ObcSuppressBecause.CA1710_IdentifiersShouldHaveCorrectSuffix_ExceptionBaseClassShouldEndWithBase)]
    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = ObcSuppressBecause.CA2237_MarkISerializableTypesWithSerializable_ExceptionOnlyUsedInternallyAndWillNeverBeSerialized)]
    public abstract class OpExecutionFailedExceptionBase : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionFailedExceptionBase"/> class.
        /// </summary>
        /// <param name="operation">The operation whose execution failed.</param>
        protected OpExecutionFailedExceptionBase(
            IOperation operation)
            : base()
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionFailedExceptionBase"/> class.
        /// </summary>
        /// <param name="message">Message for exception.</param>
        /// <param name="operation">The operation whose execution failed.</param>
        protected OpExecutionFailedExceptionBase(
            string message,
            IOperation operation)
            : base(message)
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionFailedExceptionBase"/> class.
        /// </summary>
        /// <param name="message">Message for exception.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="operation">The operation whose execution failed.</param>
        protected OpExecutionFailedExceptionBase(
            string message,
            Exception innerException,
            IOperation operation)
            : base(message, innerException)
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Gets the operation that was executing and is now failed.
        /// </summary>
        public IOperation Operation { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = string.Empty;

            if (this.Operation != null)
            {
                result = result + "This operation's execution failed: " + this.Operation + Environment.NewLine;
            }

            result = result + base.ToString();

            return result;
        }
    }
}
