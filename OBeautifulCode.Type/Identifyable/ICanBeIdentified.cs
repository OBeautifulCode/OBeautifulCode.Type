// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICanBeIdentified.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents an object that has a unique identifier.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "We prefer empty interfaces over attributes.")]
    public interface ICanBeIdentified
    {
    }
}
