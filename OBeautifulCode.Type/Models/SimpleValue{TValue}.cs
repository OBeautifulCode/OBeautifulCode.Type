// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleValue{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    /// <summary>
    /// Wraps a value.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    public partial class SimpleValue<TValue> : IValue<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleValue{TValue}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public SimpleValue(
            TValue value)
        {
            this.Value = value;
        }

        /// <inheritdoc />
        public TValue Value { get; private set; }

        /// <inheritdoc />
        public object GetValue()
        {
            var result = this.Value;

            return result;
        }

        /// <inheritdoc />
        public Type GetValueType()
        {
            var result = typeof(TValue);

            return result;
        }
    }
}
