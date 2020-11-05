﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeepCloneable.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Supports deep cloning, which creates a new instance of a class that is equal to
    /// a reference instance, but where the two instances do not share any memory between
    /// their full object graphs.
    /// </summary>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IDeepCloneable : ICloneable
    {
        /// <summary>
        /// Creates a new object that is a deep clone of this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a deep clone of this instance.
        /// </returns>
        // ReSharper disable once UnusedMember.Global
        object DeepCloneObject();
    }
}
