﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModel.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.Build.Analyzers;

    /// <summary>
    /// Represents the contract of a model object.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = SuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface IModel
    {
    }
}
