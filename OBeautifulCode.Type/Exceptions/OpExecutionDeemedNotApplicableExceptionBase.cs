// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpExecutionDeemedNotApplicableExceptionBase.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Base class for exception thrown when the execution of an <see cref="IOperation"/> deems itself not applicable.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = ObcSuppressBecause.CA1710_IdentifiersShouldHaveCorrectSuffix_ExceptionBaseClassShouldEndWithBase)]
    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = ObcSuppressBecause.CA2237_MarkISerializableTypesWithSerializable_ExceptionOnlyUsedInternallyAndWillNeverBeSerialized)]
    public abstract class OpExecutionDeemedNotApplicableExceptionBase : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionDeemedNotApplicableExceptionBase"/> class.
        /// </summary>
        /// <param name="operation">The operation whose execution is not applicable.</param>
        protected OpExecutionDeemedNotApplicableExceptionBase(
            IOperation operation)
            : base()
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionDeemedNotApplicableExceptionBase"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="operation">The operation whose execution is not applicable.</param>
        protected OpExecutionDeemedNotApplicableExceptionBase(
            string message,
            IOperation operation)
            : base(message)
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpExecutionDeemedNotApplicableExceptionBase"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="operation">The operation whose execution is not applicable.</param>
        protected OpExecutionDeemedNotApplicableExceptionBase(
            string message,
            Exception innerException,
            IOperation operation)
            : base(message, innerException)
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Gets the operation whose execution is not applicable.
        /// </summary>
        public IOperation Operation { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = string.Empty;

            if (this.Operation != null)
            {
                result = result + "This operation's execution was deemed not applicable: " + this.Operation + Environment.NewLine;
            }

            result = result + base.ToString();

            return result;
        }
    }
}
