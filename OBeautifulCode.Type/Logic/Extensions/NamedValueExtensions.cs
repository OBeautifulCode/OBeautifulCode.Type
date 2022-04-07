// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamedValueExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OBeautifulCode.Cloning.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Extension methods related to <see cref="NamedValue{TValue}"/>.
    /// </summary>
    public static class NamedValueExtensions
    {
        /// <summary>
        /// Deeps the clones an ordered collection of <see cref="NamedValue{TValue}"/>
        /// and adds a specified <see cref="NamedValue{TValue}"/> to the resulting collection.
        /// </summary>
        /// <typeparam name="TValue">The type of values.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="valueToAdd">The value to add to the resulting collection.</param>
        /// <returns>
        /// A deep clone of the specified source collection with the specified value added.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="valueToAdd"/> is null.</exception>
        public static IReadOnlyList<NamedValue<TValue>> DeepCloneWithAdditionalValue<TValue>(
            this IReadOnlyList<NamedValue<TValue>> source,
            NamedValue<TValue> valueToAdd)
        {
            source = source ?? new List<NamedValue<TValue>>();

            if (valueToAdd == null)
            {
                throw new ArgumentNullException(nameof(valueToAdd));
            }

            var result = source.DeepClone().ToList();

            result.Add(valueToAdd);

            return result;
        }

        /// <summary>
        /// Deeps the clones an ordered collection of <see cref="NamedValue{TValue}"/>
        /// and adds a specified <see cref="NamedValue{TValue}"/> to the resulting collection.
        /// </summary>
        /// <typeparam name="TValue">The type of values.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="valueToAdd">The value to add to the resulting collection.</param>
        /// <returns>
        /// A deep clone of the specified source collection with the specified value added.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="valueToAdd"/> is null.</exception>
        public static IReadOnlyCollection<NamedValue<TValue>> DeepCloneWithAdditionalValue<TValue>(
            this IReadOnlyCollection<NamedValue<TValue>> source,
            NamedValue<TValue> valueToAdd)
        {
            source = source ?? new List<NamedValue<TValue>>();

            if (valueToAdd == null)
            {
                throw new ArgumentNullException(nameof(valueToAdd));
            }

            var result = source.DeepClone().ToList();

            result.Add(valueToAdd);

            return result;
        }

        /// <summary>
        /// Gets the names of an ordered collection of <see cref="NamedValue{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of values.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <returns>
        /// The names of the specified ordered collection of <see cref="NamedValue{TValue}"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="source"/> contains a null element.</exception>
        public static IReadOnlyList<string> GetNames<TValue>(
            this IReadOnlyList<NamedValue<TValue>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var result = new List<string>(source.Count);

            foreach (var element in source)
            {
                if (element == null)
                {
                    throw new ArgumentException(Invariant($"{nameof(source)} contains a null element."));
                }

                result.Add(element.Name);
            }

            return result;
        }

        /// <summary>
        /// Gets the names of an unordered collection of <see cref="NamedValue{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of values.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <returns>
        /// The names of the specified unordered collection of <see cref="NamedValue{TValue}"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="source"/> contains a null element.</exception>
        public static IReadOnlyCollection<string> GetNames<TValue>(
            this IReadOnlyCollection<NamedValue<TValue>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var result = source.ToList().GetNames();

            return result;
        }

        /// <summary>
        /// Gets the values of an ordered collection of <see cref="NamedValue{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of values.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <returns>
        /// The values of the specified ordered collection of <see cref="NamedValue{TValue}"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="source"/> contains a null element.</exception>
        public static IReadOnlyList<TValue> GetValues<TValue>(
            this IReadOnlyList<NamedValue<TValue>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var result = new List<TValue>(source.Count);

            foreach (var element in source)
            {
                if (element == null)
                {
                    throw new ArgumentException(Invariant($"{nameof(source)} contains a null element."));
                }

                result.Add(element.Value);
            }

            return result;
        }

        /// <summary>
        /// Gets the values of an unordered collection of <see cref="NamedValue{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of values.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <returns>
        /// The values of the specified unordered collection of <see cref="NamedValue{TValue}"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="source"/> contains a null element.</exception>
        public static IReadOnlyCollection<TValue> GetValues<TValue>(
            this IReadOnlyCollection<NamedValue<TValue>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var result = source.ToList().GetValues();

            return result;
        }

        /// <summary>
        /// Gets the single value contained in an ordered collection of <see cref="NamedValue{TValue}"/> having a specified name.
        /// </summary>
        /// <typeparam name="TValue">The type of values.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="name">The name to search for.</param>
        /// <returns>
        /// The single value having the specified name.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is white space.</exception>
        /// <exception cref="ArgumentException"><paramref name="source"/> contains a null element.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> contains no elements named <paramref name="name"/> -or- contains two or more elements named <paramref name="name"/>.</exception>
        public static TValue GetSingleValue<TValue>(
            this IReadOnlyList<NamedValue<TValue>> source,
            string name)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(source)} contains a null element."));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Invariant($"{nameof(name)} is white space."));
            }

            var result = source.Single(_ => _.Name == name).Value;

            return result;
        }

        /// <summary>
        /// Gets the single value contained in an unordered collection of <see cref="NamedValue{TValue}"/> having a specified name.
        /// </summary>
        /// <typeparam name="TValue">The type of values.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="name">The name to search for.</param>
        /// <returns>
        /// The single value having the specified name.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is white space.</exception>
        /// <exception cref="ArgumentException"><paramref name="source"/> contains a null element.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> contains no elements named <paramref name="name"/> -or- contains two or more elements named <paramref name="name"/>.</exception>
        public static TValue GetSingleValue<TValue>(
            this IReadOnlyCollection<NamedValue<TValue>> source,
            string name)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var result = source.ToList().GetSingleValue(name);

            return result;
        }
    }
}
