// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModel{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Represents the contract of a model object.
    /// </summary>
    /// <typeparam name="T">The type of object being modeled.</typeparam>
    // ReSharper disable once UnusedMember.Global
    public interface IModel<T> : IModel, IEquatable<T>, IDeepCloneable<T>
    {
    }
}
