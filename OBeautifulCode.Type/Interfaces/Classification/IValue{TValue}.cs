// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValue{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Interface to declare having a value of a specified type.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    public interface IValue<TValue> : IValue, IHaveValue<TValue>
    {
    }
}
