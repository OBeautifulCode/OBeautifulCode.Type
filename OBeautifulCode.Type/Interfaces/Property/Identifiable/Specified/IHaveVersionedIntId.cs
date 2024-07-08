// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveVersionedIntId.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Represents an object that has a versioned <see cref="int"/> unique identifier.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public interface IHaveVersionedIntId : IHaveVersionedId<int>, IHaveIntId
    {
    }
}