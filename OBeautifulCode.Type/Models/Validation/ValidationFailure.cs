// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationFailure.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections.Generic;
    using static System.FormattableString;

    /// <summary>
    /// A validation failure.
    /// </summary>
    /// <remarks>
    /// See <see cref="IValidatable"/> for more details about validation.
    /// </remarks>
    public partial class ValidationFailure : IModelViaCodeGen, IDeclareToStringMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFailure"/> class.
        /// </summary>
        /// <param name="originType">
        /// The type of the object that yielded the <see cref="SelfValidationFailure"/> that is the source of this failure.
        /// For example, if the self-validation of MyObject resulted in a validation failure
        /// (e.g. MyObject.MyProperty was unexpectedly null), the origin type would be MyObject, NOT the type of MyProperty
        /// (which isn't as useful for debugging purposes).  This type points a validator to the code that yielded
        /// the validation failure.  Anyways, a validation failure might involve two or more properties that are
        /// validated together so there isn't necessarily a single property type.
        /// </param>
        /// <param name="path">
        /// The path to the object that has failed validation.
        /// For example "Property1[3].Property2" when the object being validated has a property called "Property1" of type
        /// <see cref="IReadOnlyList{T}"/> whose 4th element (index 3 in the list) is an object having a validation failure
        /// on the object's "Property2" property.
        /// </param>
        /// <param name="message">
        /// The validation failure message with details about the failed validation.
        /// For example "Property2 is null".
        /// </param>
        public ValidationFailure(
            string originType,
            string path,
            string message)
        {
            if (originType == null)
            {
                throw new ArgumentNullException(nameof(originType));
            }

            if (string.IsNullOrWhiteSpace(originType))
            {
                throw new ArgumentException(Invariant($"{nameof(originType)} is white space."), nameof(originType));
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(Invariant($"{nameof(path)} is white space."), nameof(path));
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(Invariant($"{nameof(message)} is white space."), nameof(message));
            }

            this.OriginType = originType;
            this.Path = path;
            this.Message = message;
        }

        /// <summary>
        /// Gets the type of the object that yielded the <see cref="SelfValidationFailure"/> that is the source of this failure.
        /// </summary>
        /// <remarks>
        /// For example, if the self-validation of MyObject resulted in a validation failure
        /// (e.g. MyObject.MyProperty was unexpectedly null), the origin type would be MyObject, NOT the type of MyProperty
        /// (which isn't as useful for debugging purposes).  This type points a validator to the code that yielded
        /// the validation failure.  Anyways, a validation failure might involve two or more properties that are
        /// validated together so there isn't necessarily a single property type.
        /// </remarks>
        public string OriginType { get; private set; }

        /// <summary>
        /// Gets the path to the object that has failed validation.
        /// </summary>
        /// <remarks>
        /// For example "Property1[3].Property2" when the object being validated has a property called "Property1" of type
        /// <see cref="IReadOnlyList{T}"/> whose 4th element (index 3 in the list) is an object having a validation failure
        /// on the object's "Property2" property.
        /// </remarks>
        public string Path { get; private set; }

        /// <summary>
        /// Gets validation failure message with details about the failed validation.
        /// </summary>
        /// <remarks>
        /// For example "Property2 is null".
        /// </remarks>
        public string Message { get; private set; }

        /// <inheritdoc cref="IDeclareToStringMethod" />
        public override string ToString()
        {
            var result = Invariant($"{this.OriginType} : {this.Path} => {this.Message}");

            return result;
        }
    }
}
