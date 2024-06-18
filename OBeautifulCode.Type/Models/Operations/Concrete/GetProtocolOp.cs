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
    // ReSharper disable once RedundantExtendsListEntry
    public partial class GetProtocolOp : ReturningOperationBase<IProtocol>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProtocolOp"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="missingProtocolStrategy">OPTIONAL value that determine what to do if the protocol is missing.  DEFAULT is to throw.</param>
        public GetProtocolOp(
            IOperation operation,
            MissingProtocolStrategy missingProtocolStrategy = MissingProtocolStrategy.Throw)
        {
            // ReSharper disable once JoinNullCheckWithUsage - prefer to check variable first
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (missingProtocolStrategy == MissingProtocolStrategy.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(missingProtocolStrategy), $"{nameof(missingProtocolStrategy)} is {nameof(MissingProtocolStrategy.Unknown)}.");
            }

            this.Operation = operation;
            this.MissingProtocolStrategy = missingProtocolStrategy;
        }

        /// <summary>
        /// Gets the operation.
        /// </summary>
        public IOperation Operation { get; private set; }

        /// <summary>
        /// Gets a value that determines what to do if the protocol is missing.
        /// </summary>
        public MissingProtocolStrategy MissingProtocolStrategy { get; private set; }
    }
}
