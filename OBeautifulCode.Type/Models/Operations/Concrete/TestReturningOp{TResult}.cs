// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestReturningOp{TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// A returning operation for testing.
    /// </summary>
    /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
    public partial class TestReturningOp<TResult> : ReturningOperationBase<TResult>, IHaveDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestReturningOp{TResult}"/> class.
        /// </summary>
        /// <param name="details">Details about the operation.</param>
        public TestReturningOp(
            string details)
        {
            this.Details = details;
        }

        /// <summary>
        /// Gets details about the operation.
        /// </summary>
        public string Details { get; private set; }
    }
}