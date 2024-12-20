﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetConstValueOp{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Gets a specified fixed value.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    public partial class GetConstValueOp<TValue> : ReturningOperationBase<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetConstValueOp{TValue}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public GetConstValueOp(
            TValue value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public TValue Value { get; private set; }
    }
}