// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReturningOperationBase{TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Base implementation of <see cref="IReturningOperation{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class ReturningOperationBase<TResult> : OperationBase, IReturningOperation<TResult>, IModelViaCodeGen
    {
    }
}