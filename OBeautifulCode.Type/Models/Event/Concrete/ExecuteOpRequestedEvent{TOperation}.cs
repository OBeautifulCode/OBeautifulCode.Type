// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteOpRequestedEvent{TOperation}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Event with a <typeparamref name="TOperation"/> to execute.
    /// </summary>
    /// <typeparam name="TOperation"><see cref="IOperation"/> to execute.</typeparam>
    public partial class ExecuteOpRequestedEvent<TOperation> : EventBase, IHaveDetails
        where TOperation : IOperation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteOpRequestedEvent{TOperation}"/> class.
        /// </summary>
        /// <param name="operation">Operation to execute.</param>
        /// <param name="timestampUtc">The timestamp of the event in UTC.</param>
        /// <param name="details">The optional details about the completion.</param>
        public ExecuteOpRequestedEvent(
            TOperation operation,
            DateTime timestampUtc,
            string details = null)
            : base(timestampUtc)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            this.Operation = operation;
            this.Details = details;
        }

        /// <summary>
        /// Gets the operation.
        /// </summary>
        /// <value>The operation.</value>
        public TOperation Operation { get; private set; }

        /// <inheritdoc />
        public string Details { get; private set; }
    }
}
