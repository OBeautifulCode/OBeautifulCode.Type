// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IForsakeDeepCloneWithViaCodeGen.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Used on a type to indicate that DeepCloneWith... methods should be forsaken (don't generate code).
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = ObcSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]

    // ReSharper disable once UnusedMember.Global
    public interface IForsakeDeepCloneWithViaCodeGen
    {
    }
}
