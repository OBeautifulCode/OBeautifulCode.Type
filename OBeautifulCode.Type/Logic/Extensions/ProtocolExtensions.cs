// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using OBeautifulCode.Type.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods related to <see cref="IProtocol"/>.
    /// </summary>
    public static class ProtocolExtensions
    {
        private static readonly ConcurrentDictionary<Tuple<Type, Type>, MethodInfo> CachedExecuteSyncVoidTypesToMethodInfoMap =
            new ConcurrentDictionary<Tuple<Type, Type>, MethodInfo>();

        private static readonly ConcurrentDictionary<Tuple<Type, Type, Type>, MethodInfo> CachedExecuteSyncReturningTypesToMethodInfoMap =
            new ConcurrentDictionary<Tuple<Type, Type, Type>, MethodInfo>();

        private static readonly ConcurrentDictionary<Tuple<Type, Type>, MethodInfo> CachedExecuteAsyncVoidTypesToMethodInfoMap =
            new ConcurrentDictionary<Tuple<Type, Type>, MethodInfo>();

        private static readonly ConcurrentDictionary<Tuple<Type, Type, Type>, MethodInfo> CachedExecuteAsyncReturningTypesToMethodInfoMap =
            new ConcurrentDictionary<Tuple<Type, Type, Type>, MethodInfo>();

        /// <summary>
        /// Executes the specified non-returning operation synchronously, using a specified protocol.
        /// </summary>
        /// <remarks>
        /// This is intended for use with <see cref="IProtocol"/>.
        /// Be aware that this will throw a runtime failure if the protocol cannot execute the operation.
        /// This is generally used in conjunction with an <see cref="IProtocolFactory"/>
        /// which will ensure that the protocol coming out of the factory can in fact execute the specified operation.
        /// </remarks>
        /// <param name="protocol">The protocol.</param>
        /// <param name="operation">The operation.</param>
        public static void ExecuteViaReflection(
            this IProtocol protocol,
            IOperation operation)
        {
            if (protocol == null)
            {
                throw new ArgumentNullException(nameof(protocol));
            }

            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var protocolType = protocol.GetType();

            var operationType = operation.GetType();

            var cacheKey = new Tuple<Type, Type>(protocolType, operationType);

            if (!CachedExecuteSyncVoidTypesToMethodInfoMap.TryGetValue(cacheKey, out var methodInfo))
            {
                var methodInfos = protocolType
                    .GetMethods()
                    .Where(_ => _.Name == nameof(ISyncVoidProtocol<NullVoidOp>.Execute))
                    .Where(_ => _.ReturnType == typeof(void))
                    .Where(_ => _.GetParameters().Length == 1)
                    .Where(_ => _.GetParameters().Single().ParameterType.IsAssignableFrom(operationType))
                    .ToList();

                if (!methodInfos.Any())
                {
                    throw new ArgumentException(Invariant($"The specified '{protocolType.ToStringReadable()}' protocol does not have a void {nameof(ISyncVoidProtocol<NullVoidOp>.Execute)} method with a single parameter that the specified '{operationType.ToStringReadable()}' operation is assignable to."));
                }

                if (methodInfos.Count > 1)
                {
                    throw new ArgumentException(Invariant($"The specified '{protocolType.ToStringReadable()}' protocol has more than one void {nameof(ISyncVoidProtocol<NullVoidOp>.Execute)} method with a single parameter that the specified '{operationType.ToStringReadable()}' operation is assignable to."));
                }

                methodInfo = methodInfos.Single();

                CachedExecuteSyncVoidTypesToMethodInfoMap.TryAdd(cacheKey, methodInfo);
            }

            // ReSharper disable once CoVariantArrayConversion
            methodInfo.Invoke(protocol, new[] { operation });
        }

        /// <summary>
        /// Executes the specified returning operation synchronously, using a specified protocol.
        /// </summary>
        /// <remarks>
        /// This is intended for use with <see cref="IProtocol"/>.
        /// Be aware that this will throw a runtime failure if the protocol cannot execute the operation.
        /// This is generally used in conjunction with an <see cref="IProtocolFactory"/>
        /// which will ensure that the protocol coming out of the factory can in fact execute the specified operation.
        /// </remarks>
        /// <typeparam name="TReturn">The type of return.</typeparam>
        /// <param name="protocol">The protocol.</param>
        /// <param name="operation">The operation.</param>
        /// <returns>
        /// The result of executing the specified operation using the specified protocol.
        /// </returns>
        public static TReturn ExecuteViaReflection<TReturn>(
            this IProtocol protocol,
            IOperation operation)
        {
            if (protocol == null)
            {
                throw new ArgumentNullException(nameof(protocol));
            }

            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var protocolType = protocol.GetType();

            var operationType = operation.GetType();

            var returnType = typeof(TReturn);

            var cacheKey = new Tuple<Type, Type, Type>(protocolType, operationType, returnType);

            if (!CachedExecuteSyncReturningTypesToMethodInfoMap.TryGetValue(cacheKey, out var methodInfo))
            {
                var methodInfos = protocolType
                    .GetMethods()
                    .Where(_ => _.Name == nameof(ISyncReturningProtocol<NullReturningOp<object>, object>.Execute))
                    .Where(_ => returnType.IsAssignableFrom(_.ReturnType))
                    .Where(_ => _.GetParameters().Length == 1)
                    .Where(_ => _.GetParameters().Single().ParameterType.IsAssignableFrom(operationType))
                    .ToList();

                if (!methodInfos.Any())
                {
                    throw new ArgumentException(Invariant($"The specified '{protocolType.ToStringReadable()}' protocol does not have a returning {nameof(ISyncReturningProtocol<NullReturningOp<object>, object>.Execute)} method with a single parameter that the specified '{operationType.ToStringReadable()}' operation is assignable to AND a return type that is assignable to the specified '{returnType.ToStringReadable()}' return type."));
                }

                if (methodInfos.Count > 1)
                {
                    throw new ArgumentException(Invariant($"The specified '{protocolType.ToStringReadable()}' protocol has more than one returning {nameof(ISyncReturningProtocol<NullReturningOp<object>, object>.Execute)} method with a single parameter that the specified '{operationType.ToStringReadable()}' operation is assignable to AND a return type that is assignable to the specified '{returnType.ToStringReadable()}' return type."));
                }

                methodInfo = methodInfos.Single();

                CachedExecuteSyncReturningTypesToMethodInfoMap.TryAdd(cacheKey, methodInfo);
            }

            // ReSharper disable once CoVariantArrayConversion
            var invokeResult = methodInfo.Invoke(protocol, new[] { operation });

            var result = (TReturn)invokeResult;

            return result;
        }

        /// <summary>
        /// Executes the specified non-returning operation asynchronously, using a specified protocol.
        /// </summary>
        /// <remarks>
        /// This is intended for use with <see cref="IProtocol"/>.
        /// Be aware that this will throw a runtime failure if the protocol cannot execute the operation.
        /// This is generally used in conjunction with an <see cref="IProtocolFactory"/>
        /// which will ensure that the protocol coming out of the factory can in fact execute the specified operation.
        /// </remarks>
        /// <param name="protocol">The protocol.</param>
        /// <param name="operation">The operation.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public static Task ExecuteViaReflectionAsync(
            this IProtocol protocol,
            IOperation operation)
        {
            if (protocol == null)
            {
                throw new ArgumentNullException(nameof(protocol));
            }

            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var protocolType = protocol.GetType();

            var operationType = operation.GetType();

            var cacheKey = new Tuple<Type, Type>(protocolType, operationType);

            if (!CachedExecuteAsyncVoidTypesToMethodInfoMap.TryGetValue(cacheKey, out var methodInfo))
            {
                var methodInfos = protocolType
                    .GetMethods()
                    .Where(_ => _.Name == nameof(IAsyncVoidProtocol<NullVoidOp>.ExecuteAsync))
                    .Where(_ => _.ReturnType == typeof(Task))
                    .Where(_ => _.GetParameters().Length == 1)
                    .Where(_ => _.GetParameters().Single().ParameterType.IsAssignableFrom(operationType))
                    .ToList();

                if (!methodInfos.Any())
                {
                    throw new ArgumentException(Invariant($"The specified '{protocolType.ToStringReadable()}' protocol does not have a void {nameof(IAsyncVoidProtocol<NullVoidOp>.ExecuteAsync)} method with a single parameter that the specified '{operationType.ToStringReadable()}' operation is assignable to."));
                }

                if (methodInfos.Count > 1)
                {
                    throw new ArgumentException(Invariant($"The specified '{protocolType.ToStringReadable()}' protocol has more than one void {nameof(IAsyncVoidProtocol<NullVoidOp>.ExecuteAsync)} method with a single parameter that the specified '{operationType.ToStringReadable()}' operation is assignable to."));
                }

                methodInfo = methodInfos.Single();

                CachedExecuteAsyncVoidTypesToMethodInfoMap.TryAdd(cacheKey, methodInfo);
            }

            // ReSharper disable once CoVariantArrayConversion
            var invokeResult = methodInfo.Invoke(protocol, new[] { operation });

            var result = (Task)invokeResult;

            return result;
        }

        /// <summary>
        /// Executes the specified returning operation asynchronously, using a specified protocol.
        /// </summary>
        /// <remarks>
        /// This is intended for use with <see cref="IProtocol"/>.
        /// Be aware that this will throw a runtime failure if the protocol cannot execute the operation.
        /// This is generally used in conjunction with an <see cref="IProtocolFactory"/>
        /// which will ensure that the protocol coming out of the factory can in fact execute the specified operation.
        /// </remarks>
        /// <typeparam name="TReturn">The type of return.</typeparam>
        /// <param name="protocol">The protocol.</param>
        /// <param name="operation">The operation.</param>
        /// <returns>
        /// The result of executing the specified operation using the specified protocol.
        /// </returns>
        public static Task<TReturn> ExecuteViaReflectionAsync<TReturn>(
            this IProtocol protocol,
            IOperation operation)
        {
            if (protocol == null)
            {
                throw new ArgumentNullException(nameof(protocol));
            }

            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var protocolType = protocol.GetType();

            var operationType = operation.GetType();

            var returnType = typeof(TReturn);

            var cacheKey = new Tuple<Type, Type, Type>(protocolType, operationType, returnType);

            if (!CachedExecuteAsyncReturningTypesToMethodInfoMap.TryGetValue(cacheKey, out var methodInfo))
            {
                var methodInfos = protocolType
                    .GetMethods()
                    .Where(_ => _.Name == nameof(IAsyncReturningProtocol<NullReturningOp<object>, object>.ExecuteAsync))
                    .Where(_ => (_.ReturnType.GetGenericTypeDefinitionOrSpecifiedType() == typeof(Task<>)) && returnType.IsAssignableFrom(_.ReturnType.GenericTypeArguments.Single()))
                    .Where(_ => _.GetParameters().Length == 1)
                    .Where(_ => _.GetParameters().Single().ParameterType.IsAssignableFrom(operationType))
                    .ToList();

                if (!methodInfos.Any())
                {
                    throw new ArgumentException(Invariant($"The specified '{protocolType.ToStringReadable()}' protocol does not have a returning {nameof(IAsyncReturningProtocol<NullReturningOp<object>, object>.ExecuteAsync)} method with a single parameter that the specified '{operationType.ToStringReadable()}' operation is assignable to AND a return type that is assignable to the specified '{returnType.ToStringReadable()}' return type."));
                }

                if (methodInfos.Count > 1)
                {
                    throw new ArgumentException(Invariant($"The specified '{protocolType.ToStringReadable()}' protocol has more than one returning {nameof(IAsyncReturningProtocol<NullReturningOp<object>, object>.ExecuteAsync)} method with a single parameter that the specified '{operationType.ToStringReadable()}' operation is assignable to AND a return type that is assignable to the specified '{returnType.ToStringReadable()}' return type."));
                }

                methodInfo = methodInfos.Single();

                CachedExecuteAsyncReturningTypesToMethodInfoMap.TryAdd(cacheKey, methodInfo);
            }

            // ReSharper disable once CoVariantArrayConversion
            var invokeResult = methodInfo.Invoke(protocol, new[] { operation });

            var result = (Task<TReturn>)invokeResult;

            return result;
        }
    }
}
