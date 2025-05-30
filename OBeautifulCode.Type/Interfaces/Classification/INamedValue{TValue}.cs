﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INamedValue{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Interface to declare having a value of a specified type with an associated name.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    public interface INamedValue<TValue> : INamedValue, IHaveValue<TValue>
    {
    }
}
