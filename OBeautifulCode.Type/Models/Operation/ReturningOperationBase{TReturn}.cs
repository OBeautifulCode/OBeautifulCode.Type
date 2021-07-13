// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReturningOperationBase{TReturn}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Base implementation of <see cref="IReturningOperation{TReturn}"/>.
    /// </summary>
    /// <typeparam name="TReturn">The type of the object that the operation returns.</typeparam>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class ReturningOperationBase<TReturn> : OperationBase, IReturningOperation<TReturn>, IModelViaCodeGen
    {
    }
}