// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEvent{TId}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// An record of something that has occurred and has a unique identifier.
    /// </summary>
    /// <typeparam name="TId">Type of the identifier.</typeparam>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = ObcSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface IEvent<TId> : IEvent, IHaveId<TId>
    {
    }
}