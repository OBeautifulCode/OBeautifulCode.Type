// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChainOfResponsibilityProtocolFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OBeautifulCode.Type.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// An <see cref="IProtocolFactory"/> that uses the chain-of-responsibility design pattern to iterate
    /// through a specified, ordered list of protocol factories, looking for the first one that can
    /// execute a specified operation.
    /// </summary>
    public class ChainOfResponsibilityProtocolFactory : IProtocolFactory
    {
        private readonly List<IProtocolFactory> protocolFactoriesToUseInOrder = new List<IProtocolFactory>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainOfResponsibilityProtocolFactory"/> class.
        /// </summary>
        /// <param name="protocolFactoriesToUseInOrder">The protocol factories to use, in order of precedence.</param>
        public ChainOfResponsibilityProtocolFactory(
            IReadOnlyList<IProtocolFactory> protocolFactoriesToUseInOrder = null)
        {
            if (protocolFactoriesToUseInOrder != null)
            {
                if (protocolFactoriesToUseInOrder.Any(_ => _ == null))
                {
                    throw new ArgumentException(Invariant($"{nameof(protocolFactoriesToUseInOrder)} contains a null element."));
                }

                this.protocolFactoriesToUseInOrder.AddRange(protocolFactoriesToUseInOrder);
            }
        }

        /// <summary>
        /// Adds a protocol factory to the end of the chain-of-responsibility.
        /// </summary>
        /// <param name="protocolFactory">The protocol's type.  Use concrete types.  These protocols can execute multiple operations and those will be honored in the factory.</param>
        public void AddToEndOfChain(
            IProtocolFactory protocolFactory)
        {
            if (protocolFactory == null)
            {
                throw new ArgumentNullException(nameof(protocolFactory));
            }

            this.protocolFactoriesToUseInOrder.Add(protocolFactory);
        }

        /// <inheritdoc />
        public IProtocol Execute(
            GetProtocolOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var getProtocolOp = new GetProtocolOp(operation.Operation, MissingProtocolStrategy.ReturnNull);

            IProtocol result = null;

            foreach (var protocolFactory in this.protocolFactoriesToUseInOrder)
            {
                result = protocolFactory.Execute(getProtocolOp);

                if (result != null)
                {
                    break;
                }
            }

            if (result != null)
            {
                return result;
            }
            else if (operation.MissingProtocolStrategy == MissingProtocolStrategy.Throw)
            {
                throw new OpExecutionFailedException(Invariant($"There is no protocol registered for the specified operation: '{operation.Operation.GetType().ToStringReadable()}'."), operation);
            }
            else if (operation.MissingProtocolStrategy == MissingProtocolStrategy.ReturnNull)
            {
                return null;
            }
            else
            {
                throw new NotSupportedException(Invariant($"This {nameof(MissingProtocolStrategy)} is not supported: {operation.MissingProtocolStrategy}."));
            }
        }

        /// <inheritdoc />
        public async Task<IProtocol> ExecuteAsync(
            GetProtocolOp operation)
        {
            var syncResult = this.Execute(operation);

            var result = await Task.FromResult(syncResult);

            return result;
        }
    }
}
