// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeModel{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// This class is used to "model-ize" a type that cannot easily implement <see cref="IModel{T}"/>.
    /// </summary>
    /// <remarks>
    /// This is used to satisfy the compiler when a model type is required, but the type in question
    /// cannot easily be made into a model and there is no intention of using any of the model "features".
    /// </remarks>
    /// <typeparam name="T">The type to model-ize.</typeparam>
    public class FakeModel<T> : IModel<FakeModel<T>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeModel{T}"/> class.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public FakeModel(
            T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the wrapped value.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Determines whether two objects of type <see cref="FakeModel{T}"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = ObcSuppressBecause.CA_ALL_NatureOfTypeNecessitatesIgnoringAllWarnings)]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "right", Justification = ObcSuppressBecause.CA_ALL_NatureOfTypeNecessitatesIgnoringAllWarnings)]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "left", Justification = ObcSuppressBecause.CA_ALL_NatureOfTypeNecessitatesIgnoringAllWarnings)]
        public static bool operator ==(FakeModel<T> left, FakeModel<T> right)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="FakeModel{T}"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "right", Justification = ObcSuppressBecause.CA_ALL_NatureOfTypeNecessitatesIgnoringAllWarnings)]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "left", Justification = ObcSuppressBecause.CA_ALL_NatureOfTypeNecessitatesIgnoringAllWarnings)]
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = ObcSuppressBecause.CA_ALL_NatureOfTypeNecessitatesIgnoringAllWarnings)]
        public static bool operator !=(FakeModel<T> left, FakeModel<T> right)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = ObcSuppressBecause.CA_ALL_NatureOfTypeNecessitatesIgnoringAllWarnings)]
        public bool Equals(
            FakeModel<T> other)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as FakeModel<T>);

        /// <inheritdoc cref="IHashable" />
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = ObcSuppressBecause.CA_ALL_NatureOfTypeNecessitatesIgnoringAllWarnings)]
        public override int GetHashCode() => throw new System.NotImplementedException();

        /// <inheritdoc />
        public FakeModel<T> DeepClone() => throw new System.NotImplementedException();

        /// <inheritdoc />
        public object Clone() => throw new System.NotImplementedException();

        /// <inheritdoc cref="IStringRepresentable" />
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = ObcSuppressBecause.CA_ALL_NatureOfTypeNecessitatesIgnoringAllWarnings)]
        public override string ToString() => throw new System.NotImplementedException();

        /// <inheritdoc />
        public IReadOnlyList<SelfValidationFailure> GetSelfValidationFailures() => throw new System.NotImplementedException();

        /// <inheritdoc />
        public IReadOnlyList<ValidationFailure> GetValidationFailures(ValidationOptions options = null, PropertyPathTracker propertyPathTracker = null) => throw new System.NotImplementedException();
    }
}
