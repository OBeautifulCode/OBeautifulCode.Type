// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModel.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Represents the contract of a model object.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = ObcSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface IModel : IObject, IStringRepresentable, IHashable, ICloneable
    {
    }
}
