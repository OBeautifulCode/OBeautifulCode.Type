﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamedValue{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using static System.FormattableString;

    /// <summary>
    /// A value with an associated name.
    /// </summary>
    /// <typeparam name="TValue">The type of value.</typeparam>
    public partial class NamedValue<TValue> : INamedValue<TValue>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedValue{TValue}"/> class.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The value.</param>
        public NamedValue(
            string name,
            TValue value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Invariant($"{nameof(name)} is white space."));
            }

            this.Name = name;
            this.Value = value;
        }

        /// <inheritdoc />
        public string Name { get; private set; }

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
