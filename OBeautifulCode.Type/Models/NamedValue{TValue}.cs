// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamedValue{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// A value with an associate name.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public partial class NamedValue<TValue> : IModelViaCodeGen
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

        /// <summary>
        /// Gets the name of the value.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public TValue Value { get; private set; }
    }
}
