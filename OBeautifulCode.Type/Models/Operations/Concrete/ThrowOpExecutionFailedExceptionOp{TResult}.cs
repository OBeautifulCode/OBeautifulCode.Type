// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThrowOpExecutionFailedExceptionOp{TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Operation that throws a <see cref="OpExecutionFailedExceptionBase"/>.
    /// </summary>
    /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ThrowOpExecutionFailedExceptionOp<TResult> : ReturningOperationBase<TResult>, IThrowOpExecutionFailedExceptionOp, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThrowOpExecutionFailedExceptionOp{TResult}"/> class.
        /// </summary>
        /// <param name="details">Details about the operation.</param>
        public ThrowOpExecutionFailedExceptionOp(
            string details)
        {
            this.Details = details;
        }

        /// <inheritdoc />
        public string Details { get; private set; }
    }
}
