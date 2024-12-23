// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThrowOpExecutionFailedExceptionOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Operation that throws a <see cref="OpExecutionFailedExceptionBase"/>.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ThrowOpExecutionFailedExceptionOp : VoidOperationBase, IThrowOpExecutionFailedExceptionOp, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThrowOpExecutionFailedExceptionOp"/> class.
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
