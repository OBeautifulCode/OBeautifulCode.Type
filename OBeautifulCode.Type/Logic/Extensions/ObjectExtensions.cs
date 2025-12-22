// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using OBeautifulCode.Equality.Recipes;

    /// <summary>
    /// Extension methods on <see cref="IObject"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Searches the full property graph of the specified <paramref name="rootObject"/>
        /// and returns all objects whose runtime type is compatible (is or derives from)
        /// the specified <typeparamref name="TTarget"/> type.
        /// </summary>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <param name="rootObject">The object whose property graph will be searched.</param>
        /// <returns>
        /// All objects compatible with <typeparamref name="TTarget"/> in the property graph of
        /// <paramref name="rootObject"/>.
        /// If <paramref name="rootObject"/> is itself compatible with <typeparamref name="TTarget"/>
        /// then it will be returned, but also it's properties will be searched for objects compatible
        /// with <typeparamref name="TTarget"/>.
        /// The same object will not be returned multiple times, regardless of whether
        /// it appears in the property graph multiple times.
        /// </returns>
        public static IReadOnlyCollection<TTarget> FindAllInPropertyGraph<TTarget>(
            this IObject rootObject)
            where TTarget : IObject
        {
            if (rootObject == null)
            {
                throw new ArgumentNullException(nameof(rootObject));
            }

            var result = rootObject.FindAllInPropertyGraphInternal<TTarget>().ToList();

            return result;
        }

        private static IEnumerable<TTarget> FindAllInPropertyGraphInternal<TTarget>(
            this IObject rootObject)
            where TTarget : IObject
        {
            var visited = new HashSet<object>(new ReferenceEqualityComparer<object>());

            foreach (var found in FindAllTargets(rootObject))
            {
                yield return found;
            }

            IEnumerable<TTarget> FindAllTargets(
                object subject)
            {
                if (subject == null)
                {
                    yield break;
                }

                // Only track reference types for cycles.
                // Also ensures that the same TTarget object isn't returned multiple times.
                var subjectType = subject.GetType();
                if (!subjectType.IsValueType)
                {
                    var subjectPreviouslyVisited = !visited.Add(subject);

                    if (subjectPreviouslyVisited)
                    {
                        yield break;
                    }
                }

                // If this node is (or derives from) the target type, return it.
                if (subject is TTarget target)
                {
                    yield return target;
                }

                // If it's a collection, traverse its items.
                if (subject is IEnumerable enumerableSubject)
                {
                    foreach (var element in enumerableSubject)
                    {
                        foreach (var found in FindAllTargets(element))
                        {
                            yield return found;
                        }
                    }

                    yield break;
                }

                // Traverse readable, non-indexer instance properties
                var propertiesToSearch = subjectType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var property in propertiesToSearch)
                {
                    if (!property.CanRead)
                    {
                        continue;
                    }

                    if (property.GetIndexParameters().Length != 0)
                    {
                        continue;
                    }

                    var propertyType = property.PropertyType;

                    // Prune obvious leaves
                    if (propertyType.IsPrimitive || propertyType.IsEnum)
                    {
                        continue;
                    }

                    // Common types whose properties don't need to be searched.
                    if ((propertyType == typeof(string)) ||
                        (propertyType == typeof(decimal)) ||
                        (propertyType == typeof(DateTime)) ||
                        (propertyType == typeof(DateTimeOffset)) ||
                        (propertyType == typeof(TimeSpan)) ||
                        (propertyType == typeof(Guid)) ||
                        (propertyType == typeof(Version)) ||
                        (propertyType == typeof(Uri)))
                    {
                        continue;
                    }

                    object propertyValue;
                    try
                    {
                        // This can execute code.
                        propertyValue = property.GetValue(subject, null);
                    }
                    catch
                    {
                        // Ignore properties whose getters throw
                        continue;
                    }

                    foreach (var found in FindAllTargets(propertyValue))
                    {
                        yield return found;
                    }
                }
            }
        }
    }
}
