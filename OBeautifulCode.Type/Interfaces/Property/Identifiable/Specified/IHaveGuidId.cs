// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveGuidId.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Represents an object that has a <see cref="Guid"/> unique identifier.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public interface IHaveGuidId : IHaveId<Guid>
    {
    }
}