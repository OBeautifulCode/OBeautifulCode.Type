// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThrowOpExecutionAbortedExceptionOp{TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Operation that throws a <see cref="OpExecutionAbortedExceptionBase"/>.
    /// </summary>
    /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ThrowOpExecutionAbortedExceptionOp<TResult> : ReturningOperationBase<TResult>, IThrowOpExecutionAbortedExceptionOp, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThrowOpExecutionAbortedExceptionOp{TResult}"/> class.
        /// </summary>
        /// <param name="details">Details about the operation.</param>
        public ThrowOpExecutionAbortedExceptionOp(
            string details)
        {
            this.Details = details;
        }

        /// <inheritdoc />
        public string Details { get; private set; }
    }
}
