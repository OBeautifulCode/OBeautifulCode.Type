// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModelViaCodeGen.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.Build.Analyzers;

    /// <summary>
    /// Represents an object that is expected to be an
    /// <see cref="IModel{T}"/> that is implemented with generated code.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = SuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface IModelViaCodeGen
    {
    }
}
