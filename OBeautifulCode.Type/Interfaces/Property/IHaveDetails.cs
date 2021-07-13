// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveDetails.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Interface to declare having details or context on some action or event.
    /// </summary>
    public interface IHaveDetails
    {
        /// <summary>
        /// Gets the details.
        /// </summary>
        /// <value>The details.</value>
        string Details { get; }
    }
}