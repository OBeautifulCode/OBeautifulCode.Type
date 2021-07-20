// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OBeautifulCode.Type.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Stock implementation of <see cref="IProtocolFactory"/>.
    /// </summary>
    public class ProtocolFactory : IProtocolFactory
    {
        private readonly ConcurrentDictionary<Type, Func<IProtocol>> operationTypeToGetProtocolFuncMap = new ConcurrentDictionary<Type, Func<IProtocol>>();

        private readonly object operationTypeToGetProtocolFuncMapSync = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolFactory"/> class.
        /// </summary>
        /// <param name="protocolTypeToGetProtocolFuncMap">
        /// A map of protocol type to a func that returns an instance of the protocol.
        /// Use the protocols' concrete types.
        /// These protocols can execute multiple operations and those will be honored in the factory.
        /// Protocols can also be registered after construction.
        /// </param>
        public ProtocolFactory(
            IReadOnlyDictionary<Type, Func<IProtocol>> protocolTypeToGetProtocolFuncMap = null)
        {
            if (protocolTypeToGetProtocolFuncMap != null)
            {
                foreach (var protocolType in protocolTypeToGetProtocolFuncMap.Keys)
                {
                    this.RegisterProtocolForSupportedOperations(protocolType, protocolTypeToGetProtocolFuncMap[protocolType]);
                }
            }
        }

        /// <summary>
        /// Registers a protocol with the factory to be used for the operations supported by the protocol.
        /// </summary>
        /// <param name="protocolType">The protocol's type.  Use concrete types.  These protocols can execute multiple operations and those will be honored in the factory.</param>
        /// <param name="getProtocolFunc">A func that gets an instance of the protocol.</param>
        /// <param name="protocolAlreadyRegisteredForOperationStrategy">OPTIONAL value that determines what do when the protocol supports an operation has already been registered via some other protocol.  DEFAULT is to throw an exception.</param>
        public void RegisterProtocolForSupportedOperations(
            Type protocolType,
            Func<IProtocol> getProtocolFunc,
            ProtocolAlreadyRegisteredForOperationStrategy protocolAlreadyRegisteredForOperationStrategy = ProtocolAlreadyRegisteredForOperationStrategy.Throw)
        {
            if (protocolType == null)
            {
                throw new ArgumentNullException(nameof(protocolType));
            }

            if (!typeof(IProtocol).IsAssignableFrom(protocolType))
            {
                throw new ArgumentException(Invariant($"{nameof(protocolType)} '{protocolType.ToStringReadable()}' is not assignable to {nameof(IProtocol)}"));
            }

            if (getProtocolFunc == null)
            {
                throw new ArgumentNullException(nameof(getProtocolFunc));
            }

            if (protocolAlreadyRegisteredForOperationStrategy == ProtocolAlreadyRegisteredForOperationStrategy.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(getProtocolFunc), Invariant($"{nameof(protocolAlreadyRegisteredForOperationStrategy)} is {nameof(ProtocolAlreadyRegisteredForOperationStrategy.Unknown)}."));
            }

            var supportedOperationTypes = GetSupportedOperationTypes(protocolType);

            foreach (var supportedOperationType in supportedOperationTypes)
            {
                lock (this.operationTypeToGetProtocolFuncMapSync)
                {
                    if (this.operationTypeToGetProtocolFuncMap.ContainsKey(supportedOperationType))
                    {
                        if (protocolAlreadyRegisteredForOperationStrategy == ProtocolAlreadyRegisteredForOperationStrategy.Throw)
                        {
                            throw new ArgumentException(Invariant($"Protocol '{protocolType.ToStringReadable()}' executes an operation that has already been registered: '{supportedOperationType.ToStringReadable()}'."));
                        }
                        else if (protocolAlreadyRegisteredForOperationStrategy == ProtocolAlreadyRegisteredForOperationStrategy.Skip)
                        {
                        }
                        else if (protocolAlreadyRegisteredForOperationStrategy == ProtocolAlreadyRegisteredForOperationStrategy.Replace)
                        {
                            this.operationTypeToGetProtocolFuncMap[supportedOperationType] = getProtocolFunc;
                        }
                        else
                        {
                            throw new NotSupportedException(Invariant($"This {nameof(ProtocolAlreadyRegisteredForOperationStrategy)} is not supported: {protocolAlreadyRegisteredForOperationStrategy}."));
                        }
                    }
                    else
                    {
                        this.operationTypeToGetProtocolFuncMap.TryAdd(supportedOperationType, getProtocolFunc);
                    }
                }
            }
        }

        /// <inheritdoc />
        public IProtocol Execute(
            GetProtocolOp operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var operationType = operation.Operation.GetType();

            // ReSharper disable once InconsistentlySynchronizedField
            if (!this.operationTypeToGetProtocolFuncMap.TryGetValue(operation.Operation.GetType(), out var getProtocolFunc))
            {
                if (operation.MissingProtocolStrategy == MissingProtocolStrategy.Throw)
                {
                    throw new InvalidOperationException(Invariant($"There is no protocol registered for the specified operation: '{operationType.ToStringReadable()}'."));
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

            var result = getProtocolFunc();

            if (result == null)
            {
                throw new InvalidOperationException(Invariant($"The func to get the protocol for the following specified operation returned null: '{operationType.ToStringReadable()}'."));
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<IProtocol> ExecuteAsync(
            GetProtocolOp operation)
        {
            var syncResult = this.Execute(operation);

            var result = await Task.FromResult(syncResult);

            return result;
        }

        private static IReadOnlyCollection<Type> GetSupportedOperationTypes(
            Type protocolType)
        {
            var result = protocolType
                .GetInterfaces()
                .Where(_ => _.GetGenericTypeDefinitionOrSpecifiedType() == typeof(IProtocol<>))
                .Select(_ => _.GenericTypeArguments.Single())
                .ToList();

            if (!result.Any())
            {
                throw new ArgumentException(Invariant($"Protocol '{protocolType.ToStringReadable()}' does not execute any operations."));
            }

            return result;
        }
    }
}
