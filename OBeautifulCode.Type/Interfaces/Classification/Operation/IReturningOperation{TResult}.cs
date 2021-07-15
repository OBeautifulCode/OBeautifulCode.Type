// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReturningOperation{TResult}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Interface necessary for a returning operation to connect to a protocol.
    /// </summary>
    /// <typeparam name="TResult">The type returned when the operation is executed.</typeparam>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = ObcSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface IReturningOperation<TResult> : IOperation
    {
    }
}