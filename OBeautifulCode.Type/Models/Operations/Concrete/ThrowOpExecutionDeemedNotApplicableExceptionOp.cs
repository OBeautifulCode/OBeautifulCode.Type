// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThrowOpExecutionDeemedNotApplicableExceptionOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Operation that throws a <see cref="OpExecutionAbortedExceptionBase"/>.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ThrowOpExecutionDeemedNotApplicableExceptionOp : VoidOperationBase, IThrowOpExecutionDeemedNotApplicableExceptionOp, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThrowOpExecutionDeemedNotApplicableExceptionOp"/> class.
        /// </summary>
        /// <param name="details">Details about the operation.</param>
        public ThrowOpExecutionDeemedNotApplicableExceptionOp(
            string details)
        {
            this.Details = details;
        }

        /// <inheritdoc />
        public string Details { get; private set; }
    }
}
