﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatedComparableTestScenario{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.CodeGen.ModelObject.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.CodeGen.ModelObject.Recipes
{
    using global::System.Collections.Generic;

    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Specifies a scenario for comparability tests.
    /// </summary>
    /// <typeparam name="T">The type of the object being tested.</typeparam>
#if !OBeautifulCodeCodeGenSolution
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.CodeGen.ModelObject.Recipes", "See package version number")]
    internal
#else
    public
#endif
    class ValidatedComparableTestScenario<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatedComparableTestScenario{T}"/> class.
        /// </summary>
        /// <param name="id">The identifier of the scenario.</param>
        /// <param name="referenceObject">The reference object.</param>
        /// <param name="objectsThatAreEqualToButNotTheSameAsReferenceObject">Objects that are equal to but not the same object in-memory as the <paramref name="referenceObject"/>.</param>
        /// <param name="objectsThatAreLessThanReferenceObject">Objects that are less than the <paramref name="referenceObject"/>.</param>
        /// <param name="objectsThatAreGreaterThanReferenceObject">Objects that are greater than the <paramref name="referenceObject"/>.</param>
        /// <param name="objectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject">Objects that derive from <typeparamref name="T"/>, but are not of the same type as the <paramref name="referenceObject"/>.</param>
        /// <param name="objectsThatAreNotOfTheSameTypeAsReferenceObject">Objects that are not the same type as the <paramref name="referenceObject"/>.</param>
        public ValidatedComparableTestScenario(
            string id,
            T referenceObject,
            IReadOnlyList<T> objectsThatAreEqualToButNotTheSameAsReferenceObject,
            IReadOnlyList<T> objectsThatAreLessThanReferenceObject,
            IReadOnlyList<T> objectsThatAreGreaterThanReferenceObject,
            IReadOnlyList<T> objectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject,
            IReadOnlyList<object> objectsThatAreNotOfTheSameTypeAsReferenceObject)
        {
            new { id }.AsTest().Must().NotBeNullNorWhiteSpace();
            new { referenceObject }.AsTest().Must().NotBeNull(id);
            new { objectsThatAreEqualToButNotTheSameAsReferenceObject }.AsTest().Must().NotBeNull(id).And().NotContainAnyNullElements(id);
            new { objectsThatAreLessThanReferenceObject }.AsTest().Must().NotBeNull(id).And().NotContainAnyNullElements(id);
            new { objectsThatAreGreaterThanReferenceObject }.AsTest().Must().NotBeNull(id).And().NotContainAnyNullElements(id);
            new { objectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject }.AsTest().Must().NotBeNull(id).And().NotContainAnyNullElements(id);
            new { objectsThatAreNotOfTheSameTypeAsReferenceObject }.AsTest().Must().NotBeNull(id).And().NotContainAnyNullElements(id);

            this.Id = id;
            this.ReferenceObject = referenceObject;
            this.ObjectsThatAreEqualToButNotTheSameAsReferenceObject = objectsThatAreEqualToButNotTheSameAsReferenceObject;
            this.ObjectsThatAreLessThanReferenceObject = objectsThatAreLessThanReferenceObject;
            this.ObjectsThatAreGreaterThanReferenceObject = objectsThatAreGreaterThanReferenceObject;
            this.ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject = objectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject;
            this.ObjectsThatAreNotOfTheSameTypeAsReferenceObject = objectsThatAreNotOfTheSameTypeAsReferenceObject;
        }

        /// <summary>
        /// Gets the identifier of the scenario.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the reference object.
        /// </summary>
        public T ReferenceObject { get; }

        /// <summary>
        /// Gets objects that are equal to but not the same object in-memory as the <see cref="ReferenceObject"/>.
        /// </summary>
        public IReadOnlyList<T> ObjectsThatAreEqualToButNotTheSameAsReferenceObject { get; }

        /// <summary>
        /// Gets objects that are less than the <see cref="ReferenceObject"/>.
        /// </summary>
        public IReadOnlyList<T> ObjectsThatAreLessThanReferenceObject { get; }

        /// <summary>
        /// Gets objects that are greater than the <see cref="ReferenceObject"/>.
        /// </summary>
        public IReadOnlyList<T> ObjectsThatAreGreaterThanReferenceObject { get; }

        /// <summary>
        /// Gets or sets objects that derive from <typeparamref name="T"/>, but are not of the same type as the <see cref="ReferenceObject"/>.
        /// </summary>
        public IReadOnlyList<T> ObjectsThatDeriveFromScenarioTypeButAreNotOfTheSameTypeAsReferenceObject { get; }

        /// <summary>
        /// Gets objects that are not the same type as the <see cref="ReferenceObject"/>.
        /// </summary>
        public IReadOnlyList<object> ObjectsThatAreNotOfTheSameTypeAsReferenceObject { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = this.Id;

            return result;
        }
    }
}
