﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncSpecificVoidProtocolBase{TOperation}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Threading.Tasks;

    using OBeautifulCode.Execution.Recipes;

    /// <summary>
    /// Protocol that gives pass-through implementation for the synchronous execution for <see cref="ISyncAndAsyncVoidProtocol{TOperation}"/>.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    public abstract class AsyncSpecificVoidProtocolBase<TOperation> : ISyncAndAsyncVoidProtocol<TOperation>
        where TOperation : IVoidOperation
    {
        /// <inheritdoc />
        public void Execute(
            TOperation operation)
        {
            Func<Task> executeAsyncFunc = () => this.ExecuteAsync(operation);

            executeAsyncFunc.ExecuteSynchronously();
        }

        /// <inheritdoc />
        public abstract Task ExecuteAsync(
            TOperation operation);
    }
}
