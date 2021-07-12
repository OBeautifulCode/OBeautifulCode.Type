// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IObject.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// An interface alternative to <see cref="object"/>.
    /// Enables a method (typically a constructor) to take-in an object of any type.
    /// </summary>
    /// <remarks>
    /// This is particularly useful in providing a facility for a caller to construct
    /// your object with their domain-specific context and then serialize your object.
    /// Serializing an object having a property of type <see cref="object"/> can
    /// break some serializers.
    /// </remarks>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = ObcSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface IObject
    {
    }
}
