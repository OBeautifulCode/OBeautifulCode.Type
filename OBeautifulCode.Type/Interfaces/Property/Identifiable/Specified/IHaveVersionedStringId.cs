// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveVersionedStringId.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Represents an object that has a versioned <see cref="string"/> unique identifier.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public interface IHaveVersionedStringId : IHaveVersionedId<string>, IHaveStringId
    {
    }
}