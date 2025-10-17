// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelfValidationFailure.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static System.FormattableString;

    /// <summary>
    /// A self-validation failure.
    /// </summary>
    /// <remarks>
    /// See <see cref="IValidatable"/> for more details about self-validation.
    /// </remarks>
    public class SelfValidationFailure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfValidationFailure"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property that has failed a validation.</param>
        /// <param name="message">
        /// The validation failure message with details about the failed validation.
        /// For example "MyProperty is null".
        /// </param>
        public SelfValidationFailure(
            string propertyName,
            string message)
            : this(new[] { propertyName }, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfValidationFailure"/> class.
        /// </summary>
        /// <param name="propertyNames">
        /// The names of the properties that have, together, failed a single validation.
        /// Most often this collection contains a single property name.
        /// An example of multiple properties: { "Id", "IdToObjectMap" } when IdToObjectMap
        /// is a dictionary that is expected to contain the key Id.
        /// </param>
        /// <param name="message">
        /// The validation failure message with details about the failed validation.
        /// For example "MyProperty is null".
        /// </param>
        public SelfValidationFailure(
            IReadOnlyCollection<string> propertyNames,
            string message)
        {
            if (propertyNames == null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }

            if (propertyNames.Count == 0)
            {
                throw new ArgumentException(Invariant($"{nameof(propertyNames)} is an empty enumerable."));
            }

            if (propertyNames.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(propertyNames)} contains at least one null element."));
            }

            if (propertyNames.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException(Invariant($"{nameof(propertyNames)} contains an element that is white space."));
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(Invariant($"{nameof(message)} is white space."), nameof(message));
            }

            this.PropertyNames = propertyNames;
            this.Message = message;
        }

        /// <summary>
        /// Gets the names of the properties that have, together, failed a single validation.
        /// </summary>
        /// <remarks>
        /// Most often this collection contains a single property name.
        /// </remarks>
        public IReadOnlyCollection<string> PropertyNames { get; private set; }

        /// <summary>
        /// Gets validation failure message with details about the failed validation.
        /// </summary>
        /// <remarks>
        /// An example of multiple properties: { "Id", "IdToObjectMap" } when IdToObjectMap
        /// is a dictionary that is expected to contain the key Id.
        /// </remarks>
        public string Message { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = Invariant($"{string.Join("|", this.PropertyNames)} : {this.Message}");

            return result;
        }
    }
}
