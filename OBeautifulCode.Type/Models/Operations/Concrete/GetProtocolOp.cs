// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetProtocolOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Gets the protocol to use for a specified <see cref="IOperation"/> object.
    /// </summary>
    public partial class GetProtocolOp : ReturningOperationBase<IProtocol>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProtocolOp"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        public GetProtocolOp(
            IOperation operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            this.Operation = operation;
        }

        /// <summary>
        /// Gets the operation.
        /// </summary>
        public IOperation Operation { get; private set; }
    }
}
