// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestVoidOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// A void operation for testing.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class TestVoidOp : VoidOperationBase, IHaveDetails, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestVoidOp"/> class.
        /// </summary>
        /// <param name="details">Details about the operation.</param>
        public TestVoidOp(
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