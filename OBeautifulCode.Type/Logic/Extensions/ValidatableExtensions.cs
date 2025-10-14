// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatableExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Equality.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="IValidatable"/>.
    /// </summary>
    public static class ValidatableExtensions
    {
        /// <summary>
        /// Determines if a specified object is valid.
        /// </summary>
        /// <param name="validatable">The subject object.</param>
        /// <param name="failures">The validation failures encountered or an empty list if none.</param>
        /// <param name="options">
        /// OPTIONAL validation options that control how validation is performed.
        /// DEFAULT is to validate the subject object as well as all of it's properties,
        /// recursing thru the entire object graph and not stopping at any point until
        /// the entire graph is traversed, performing "self validation" first and then
        /// validating properties.
        /// </param>
        /// <returns>
        /// true if the object is valid, otherwise false.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = ObcSuppressBecause.CA1021_AvoidOutParameters_OutParameterRequiredForTryMethod)]
        public static bool IsValid(
            this IValidatable validatable,
            out IReadOnlyList<ValidationFailure> failures,
            ValidationOptions options = null)
        {
            if (validatable == null)
            {
                throw new ArgumentNullException(nameof(validatable));
            }

            failures = validatable.GetValidationFailures(options);

            var result = !failures.Any();

            return result;
        }

        /// <summary>
        /// Throws a <see cref="ValidationException"/> if the specified object is invalid.
        /// </summary>
        /// <param name="validatable">The subject object.</param>
        /// <param name="options">
        /// OPTIONAL validation options that control how validation is performed.
        /// DEFAULT is to validate the subject object as well as all of it's properties,
        /// recursing thru the entire object graph and not stopping at any point until
        /// the entire graph is traversed, performing "self validation" first and then
        /// validating properties.
        /// </param>
        public static void ThrowIfInvalid(
            this IValidatable validatable,
            ValidationOptions options = null)
        {
            if (validatable == null)
            {
                throw new ArgumentNullException(nameof(validatable));
            }

            if (!validatable.IsValid(out var failures, options))
            {
                throw new ValidationException(failures);
            }
        }

        /// <summary>
        /// Gets the validation failures for a specified object.
        /// </summary>
        /// <remarks>
        /// This method should be called by <see cref="IValidatable.GetValidationFailures(ValidationOptions, PropertyPathTracker)"/>.
        /// The intention is not to have some end user call this method directly.
        /// This method does the heavy-lifting to efficiently recurse thru <paramref name="objectToValidate"/>'s object graph and
        /// validating any objects that are <see cref="IValidatable"/> (e.g. List{List{SomeValidatableType}})".
        /// </remarks>
        /// <param name="objectToValidate">The object to validate.</param>
        /// <param name="options">Validation options that control how validation is performed.</param>
        /// <param name="propertyPathTracker">An object that tracks the path taken in the traversal of some object's property graph, ending at <paramref name="objectToValidate"/>.</param>
        /// <param name="segmentName">The name of the segment that identifies <paramref name="objectToValidate"/> at the end of the path being tracked by <paramref name="propertyPathTracker"/>.</param>
        /// <returns>
        /// The validation failures or an empty list if there are none.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object", Justification = ObcSuppressBecause.CA1720_IdentifiersShouldNotContainTypeNames_TypeNameAddsClarityToIdentifierAndNoGoodAlternative)]
        public static IReadOnlyList<ValidationFailure> GetValidationFailures(
            object objectToValidate,
            ValidationOptions options,
            PropertyPathTracker propertyPathTracker,
            string segmentName)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (propertyPathTracker == null)
            {
                throw new ArgumentNullException(nameof(propertyPathTracker));
            }

            if (segmentName == null)
            {
                throw new ArgumentNullException(nameof(segmentName));
            }

            if (string.IsNullOrWhiteSpace(segmentName))
            {
                throw new ArgumentException(Invariant($"{nameof(segmentName)} is white space."), nameof(segmentName));
            }

            var objectsVisitedHash = new HashSet<object>(new ReferenceEqualityComparer<object>());

            var result = GetValidationFailuresInternal(
                objectToValidate,
                options,
                propertyPathTracker,
                segmentName,
                objectsVisitedHash);

            return result;
        }

        private static IReadOnlyList<ValidationFailure> GetValidationFailuresInternal(
            object objectToValidate,
            ValidationOptions options,
            PropertyPathTracker propertyPathTracker,
            string segmentName,
            HashSet<object> objectsVisitedHash)
        {
            // If the object it null it can't be validated.
            if (objectToValidate == null)
            {
                return new ValidationFailure[0];
            }

            // Avoid objects you've already visited.  This protects against circular references.
            // It's also improves performance.  You may have an object that's enumerable that
            // exposes the collection being enumerated as a property on the object.
            // Per the heuristic below, we enumerate and then visit properties.
            if (!objectsVisitedHash.Add(objectToValidate))
            {
                return new ValidationFailure[0];
            }

            // If object is IValidatable, call GetValidationFailures(), no further exploration needed.
            if (objectToValidate is IValidatable validatable)
            {
                using (propertyPathTracker.Push(segmentName))
                {
                    var validationFailures = validatable.GetValidationFailures(options, propertyPathTracker);

                    return validationFailures;
                }
            }

            // Skip types that cannot possibly contain a validatable object.
            var type = objectToValidate.GetType();

            if (!type.IsTypeThatMayContainValidatableObject())
            {
                return new ValidationFailure[0];
            }

            // Iterate thru dictionaries and enumerables, which often don't expose
            // the underlying collection as a public property.
            // First check if object is a dictionary, so that we can apply segment naming conventions
            // that make it easier to identify where in the dictionary the invalid object lives.
            // Then fallback to iterating thru enumerables.
            var result = new List<ValidationFailure>();

            var stopOnFirstObjectWithFailures = options.ValidateUntil == ValidateUntil.FirstInvalidObject;

            if (objectToValidate is IDictionary dictionary)
            {
                foreach (DictionaryEntry entry in dictionary)
                {
                    var dictionaryEntrySegmentName = Invariant($"{segmentName}[\"{entry.Key}\"]");

                    var keyValidationFailures = GetValidationFailuresInternal(
                        entry.Key,
                        options,
                        propertyPathTracker,
                        Invariant($"{dictionaryEntrySegmentName}:Key"),
                        objectsVisitedHash);

                    result.AddRange(keyValidationFailures);

                    if (stopOnFirstObjectWithFailures && result.Any())
                    {
                        return result;
                    }

                    var valueValidationFailures = GetValidationFailuresInternal(
                        entry.Value,
                        options,
                        propertyPathTracker,
                        dictionaryEntrySegmentName,
                        objectsVisitedHash);

                    result.AddRange(valueValidationFailures);

                    if (stopOnFirstObjectWithFailures && result.Any())
                    {
                        return result;
                    }
                }
            }
            else if (objectToValidate is IEnumerable enumerable)
            {
                var index = 0L;

                foreach (var element in enumerable)
                {
                    var enumerableElementSegmentName = Invariant($"{segmentName}[{index}]");

                    var elementValidationFailures = GetValidationFailuresInternal(
                        element,
                        options,
                        propertyPathTracker,
                        enumerableElementSegmentName,
                        objectsVisitedHash);

                    result.AddRange(elementValidationFailures);

                    if (stopOnFirstObjectWithFailures && result.Any())
                    {
                        return result;
                    }

                    index++;
                }
            }

            // Finally, traverse properties to catch any objects not yielded by any enumerator
            // (e.g. Item1 and Item2 in Tuple<T1, T1>).  We are only concerned with public instance properties.
            // The scope of this problem is model object validation, so any fields or private members
            // are not of concern.  In fact, we are really only doing this cover System types that are generic
            // and thus allow us to store validatable objects within those objects.  There are alternate
            // approaches that could be taken like filtering on generics and scoping to those generic properties,
            // but we aren't 100% sure that that's a comprehensive strategy.
            var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in propertyInfos)
            {
                // Skip indexed properties.  Should have explored those above in the dictionary and enumerable cases.
                if (propertyInfo.GetIndexParameters().Length == 0)
                {
                    if (propertyInfo.CanRead)
                    {
                        var propertyValue = propertyInfo.GetValue(objectToValidate);

                        var propertySegmentName = Invariant($"{segmentName}{propertyPathTracker.SegmentSeparator}{propertyInfo.Name}");

                        var propertyValidationFailures = GetValidationFailuresInternal(
                            propertyValue,
                            options,
                            propertyPathTracker,
                            propertySegmentName,
                            objectsVisitedHash);

                        result.AddRange(propertyValidationFailures);

                        if (stopOnFirstObjectWithFailures && result.Any())
                        {
                            return result;
                        }
                    }
                }
            }

            return result;
        }

        private static bool IsTypeThatMayContainValidatableObject(
            this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            // Primitives cannot be validatable (int, bool, double, char, etc).
            if (type.IsPrimitive)
            {
                return false;
            }

            // Enums cannot be validatable.
            if (type.IsEnum)
            {
                return false;
            }

            // These common types cannot be validatable.
            // Note there's a huge list of types we could check (e.g. TimeZoneInfo, Assembly, DBNull)
            // but it's not likely that you would use those types in a model object.
            // Many of them won't serialize.
            if ((type == typeof(string)) ||
                (type == typeof(decimal)) ||
                (type == typeof(DateTime)) ||
                (type == typeof(DateTimeOffset)) ||
                (type == typeof(TimeSpan)) ||
                (type == typeof(Guid)) ||
                (type == typeof(Version)) ||
                (type == typeof(Uri)))
            {
                return false;
            }

            // Arrays may or may not be validatable based on element type.
            if (type.IsArray)
            {
                var result = type.GetElementType().IsTypeThatMayContainValidatableObject();

                return result;
            }

            // It's impossible for object.GetType() to return a Nullable type.
            // - Nullable WITH value -> boxes to underlying type
            // - Nullable WITHOUT value -> boxes to null
            // However, in a recursive call to this method to check an array's element type,
            // it IS possible to get a nullable type (e.g. byte?[])
            // Handle Nullable<T> by checking the underlying type
            var nullableUnderlyingType = Nullable.GetUnderlyingType(type);
            if (nullableUnderlyingType != null)
            {
                var result = nullableUnderlyingType.IsTypeThatMayContainValidatableObject();

                return result;
            }

            // There are potentially other strategies that could be employed,
            // but would affect performance:
            // - If namespace is not null and does not start with "System", could be validatable.
            // - Any IEnumerable could be validatable.
            // - If generic, check each generic type argument.
            // - If it's a value type and we know we are only dealing with System types, can't be validatable.
            // - Check common types like List<> and Dictionary<,>
            return true;
        }
    }
}
