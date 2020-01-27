// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeclareDeepCloneMethod{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Declares the <see cref="IDeepCloneable{T}.DeepClone"/> method.
    /// </summary>
    /// <typeparam name="T">The type of object to clone.</typeparam>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IDeclareDeepCloneMethod<T>
    {
        /// <summary>
        /// Creates a new object that is a deep clone of this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a deep clone of this instance.
        /// </returns>
        // ReSharper disable once UnusedMember.Global
        T DeepClone();
    }
}
