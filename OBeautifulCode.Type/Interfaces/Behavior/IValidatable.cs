// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidatable.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Collections.Generic;

    /// <summary>
    /// Validates an object.
    /// </summary>
    /// <remarks>
    /// Validation is the act of taking some subject object, running self-validation
    /// (more on this coming) to get any validation failures, then iterating through the
    /// object's properties and recursively running validation on them.  In this manner,
    /// validation can collect validation failures from the entire object graph.
    /// The contract provides flexibility via an options parameter, so in practice you
    /// are not required to validate the entire graph
    /// (e.g. return on first validation failure, only validate the subject object, not it's properties).
    /// It's best to think about self-validation like the validation that occurs in an
    /// object's constructor.  You assume that because an object MyObject was passed in,
    /// it was constructed and thus passed whatever validation was run in MyObject's constructor
    /// (otherwise it would have thrown).  So the kind of validations that might occur in the
    /// constructor is to check that parameters are not null, collections are not empty, perhaps
    /// enum values or string values are as expected.
    /// So self-validation is NOT responsible for running self-validation on the object's properties.
    /// </remarks>
    public interface IValidatable
    {
        /// <summary>
        /// Gets the self-validation failures, if there are any.
        /// </summary>
        /// <remarks>
        /// This is validation performed on the subject object itself.
        /// This method should not recurse through the object's properties to check for validity.
        /// For example, if the object has a property that is an <see cref="IReadOnlyList{T}"/>,
        /// self-validation might check that that list is not null nor empty.  However, it will not
        /// iterate through the list and ask each element to perform validation on itself.
        /// </remarks>
        /// <returns>
        /// The list of validation failures -or- null or empty if there are none.
        /// If the list contains any null elements, those will be discarded
        /// (NOT treated as a validation failure) by <see cref="GetValidationFailures"/>.
        /// </returns>
        IReadOnlyList<SelfValidationFailure> GetSelfValidationFailures();

        /// <summary>
        /// Gets all validation failures, subject to the specified validation options.
        /// </summary>
        /// <param name="options">
        /// OPTIONAL validation options that control how validation is performed.
        /// DEFAULT is to validate the subject object as well as all of it's properties,
        /// recursing thru the entire object graph and not stopping at any point until
        /// the entire graph is traversed, performing self-validation first and then
        /// validating properties.
        /// </param>
        /// <param name="propertyPathTracker">
        /// OPTIONAL object that tracks the path taken in the traversal of some object's
        /// property graph, ending at this object.
        /// DEFAULT is to let the method construct it's own tracker, treating this object
        /// as the starting point of the traversal.
        /// If specified, implementers should add to this path while validating.
        /// </param>
        /// <returns>
        /// The resulting validation failures in the order they were discovered,
        /// or an empty list if there aren't any validation failures.
        /// </returns>
        IReadOnlyList<ValidationFailure> GetValidationFailures(
            ValidationOptions options = null,
            PropertyPathTracker propertyPathTracker = null);
    }
}
