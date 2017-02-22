﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleExtensions.cs">
//     Copyright (c) 2016. All rights reserved. Licensed under the MIT license. See LICENSE file in
//     the project root for full license information.
// </copyright>
// <auto-generated>
// Sourced from NuGet package. Will be overwritten with package update except in Spritely.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace Spritely.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // See MustExtensions.cs for comments on type definitions
    using GetArguments = System.Func<System.Collections.Generic.IEnumerable<System.Tuple<System.Type, string, object>>>;
    using Rule = System.Tuple<System.Func<System.Type, object, bool>, System.Collections.Generic.IEnumerable<string>, System.Func<System.Type, System.Collections.Generic.IEnumerable<string>, object, string, System.Exception>>;

    /// <summary>
    ///     Contains built-in extensions for Must Rules.
    /// </summary>
#if !SpritelyRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("Spritely.Recipes", "See package version number")]
#pragma warning disable 0436
#endif
    internal static partial class RuleExtensions
    {
        /// <summary>
        /// Validates the arguments and appends the specified rule(s).
        /// </summary>
        /// <param name="validationPlan">The validation plan to update.</param>
        /// <param name="rules">The rules to add.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// If validationPlan or rules is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">ValidateArgumentsAndAppendRule requires at least 1 rule to append.</exception>
        public static Tuple<GetArguments, IEnumerable<Rule>> ValidateArgumentsAndAppendRule(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan, params Rule[] rules)
        {
            if (validationPlan == null)
            {
                throw new ArgumentNullException("validationPlan");
            }

            if (rules == null)
            {
                throw new ArgumentNullException("rules");
            }

            if (rules.Length < 1)
            {
                throw new ArgumentException("ValidateArgumentsAndAppendRule requires at least 1 rule to append.");
            }

            var result = Tuple.Create(
                validationPlan.Item1,
                validationPlan.Item2.Concat(rules));

            return result;
        }

        /// <summary>
        /// Adds a NotBeNull check to the validation plan rules.
        /// </summary>
        /// <param name="validationPlan">The validation plan.</param>
        /// <returns>A revised validation plan.</returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> NotBeNull(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan)
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.NotBeNull);
        }

        /// <summary>
        /// Adds a BeFalse check to the validation plan rules.
        /// </summary>
        /// <param name="validationPlan">The validation plan.</param>
        /// <returns>A revised validation plan.</returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> BeFalse(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan)
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.BeFalse);
        }

        /// <summary>
        /// Adds a BeTrue check to the validation plan rules.
        /// </summary>
        /// <param name="validationPlan">The validation plan.</param>
        /// <returns>A revised validation plan.</returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> BeTrue(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan)
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.BeTrue);
        }

        /// <summary>
        /// Adds a NotBeEmptyString check to the validation plan rules.
        /// </summary>
        /// <param name="validationPlan">The validation plan.</param>
        /// <returns>A revised validation plan.</returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> NotBeEmptyString(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan)
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.NotBeEmptyString);
        }

        /// <summary>
        /// Adds a NotBeWhiteSpace check to the validation plan rules.
        /// </summary>
        /// <param name="validationPlan">The validation plan.</param>
        /// <returns>A revised validation plan.</returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> NotBeWhiteSpace(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan)
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.NotBeWhiteSpace);
        }

        /// <summary>
        /// Adds a NotBeEmptyGuid check to the validation plan rules.
        /// </summary>
        /// <param name="validationPlan">The validation plan.</param>
        /// <returns>A revised validation plan.</returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> NotBeEmptyGuid(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan)
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.NotBeEmptyGuid);
        }

        /// <summary>
        /// Adds a NotBeDefault check to the validation plan rules.
        /// </summary>
        /// <typeparam name="T">The type of default to check for.</typeparam>
        /// <param name="validationPlan">The validation plan.</param>
        /// <returns>
        /// A revised validation plan.
        /// </returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> NotBeDefault<T>(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan)
            where T : IEquatable<T>
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.NotBeDefault<T>());
        }

        /// <summary>
        /// Adds a NotBeEmptyEnumerable check to the validation plan rules.
        /// </summary>
        /// <typeparam name="T">The type of value contained in the enumerable.</typeparam>
        /// <param name="validationPlan">The validation plan.</param>
        /// <returns>
        /// A revised validation plan.
        /// </returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> NotBeEmptyEnumerable<T>(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan)
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.NotBeEmptyEnumerable<T>());
        }

        /// <summary>
        /// Adds a NotContainAnyNulls check to the validation plan rules.
        /// </summary>
        /// <typeparam name="T">The type of value contained in the enumerable.</typeparam>
        /// <param name="validationPlan">The validation plan.</param>
        /// <returns>
        /// A revised validation plan.
        /// </returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> NotContainAnyNulls<T>(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan)
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.NotContainAnyNulls<T>());
        }

        /// <summary>
        /// Adds a ContainElement check to the validation plan rules.
        /// </summary>
        /// <typeparam name="T">The type of value contained in the enumerable.</typeparam>
        /// <param name="validationPlan">The validation plan.</param>
        /// <param name="element">The element to check for.</param>
        /// <returns>
        /// A revised validation plan.
        /// </returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> ContainElement<T>(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan, T element)
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.ContainElement(element));
        }

        /// <summary>
        /// Adds a NotContainElement check to the validation plan rules.
        /// </summary>
        /// <typeparam name="T">The type of value contained in the enumerable.</typeparam>
        /// <param name="validationPlan">The validation plan.</param>
        /// <param name="element">The element to check for.</param>
        /// <returns>
        /// A revised validation plan.
        /// </returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> NotContainElement<T>(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan, T element)
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.NotContainElement(element));
        }

        /// <summary>
        /// Adds an BeInRange check to the validation plan rules.
        /// </summary>
        /// <typeparam name="T">The type of compariable to check.</typeparam>
        /// <param name="validationPlan">The validation plan.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <returns>
        /// A revised validation plan.
        /// </returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> BeInRange<T>(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan, T minimum, T maximum)
            where T : IComparable<T>
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.BeInRange(minimum, maximum));
        }

        /// <summary>
        /// Adds a BeLessThan check to the validation plan rules.
        /// </summary>
        /// <typeparam name="T">The type of compariable to check.</typeparam>
        /// <param name="validationPlan">The validation plan.</param>
        /// <param name="requirement">The requirement to meet.</param>
        /// <returns>
        /// A revised validation plan.
        /// </returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> BeLessThan<T>(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan, T requirement)
            where T : IComparable<T>
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.BeLessThan(requirement));
        }

        /// <summary>
        /// Adds a BeLessThanOrEqualTo check to the validation plan rules.
        /// </summary>
        /// <typeparam name="T">The type of compariable to check.</typeparam>
        /// <param name="validationPlan">The validation plan.</param>
        /// <param name="requirement">The requirement to meet.</param>
        /// <returns>
        /// A revised validation plan.
        /// </returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> BeLessThanOrEqualTo<T>(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan, T requirement)
            where T : IComparable<T>
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.BeLessThanOrEqualTo(requirement));
        }

        /// <summary>
        /// Adds an NotBeEqualTo check to the validation plan rules.
        /// </summary>
        /// <typeparam name="T">The type of compariable to check.</typeparam>
        /// <param name="validationPlan">The validation plan.</param>
        /// <param name="requirement">The requirement to meet.</param>
        /// <returns>
        /// A revised validation plan.
        /// </returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> NotBeEqualTo<T>(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan, T requirement)
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.NotBeEqualTo(requirement));
        }

        /// <summary>
        /// Adds an BeEqualTo check to the validation plan rules.
        /// </summary>
        /// <typeparam name="T">The type of compariable to check.</typeparam>
        /// <param name="validationPlan">The validation plan.</param>
        /// <param name="requirement">The requirement to meet.</param>
        /// <returns>
        /// A revised validation plan.
        /// </returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> BeEqualTo<T>(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan, T requirement)
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.BeEqualTo(requirement));
        }

        /// <summary>
        /// Adds a GreatherThan check to the validation plan rules.
        /// </summary>
        /// <typeparam name="T">The type of compariable to check.</typeparam>
        /// <param name="validationPlan">The validation plan.</param>
        /// <param name="requirement">The requirement to meet.</param>
        /// <returns>
        /// A revised validation plan.
        /// </returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> BeGreaterThan<T>(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan, T requirement)
            where T : IComparable<T>
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.BeGreaterThan(requirement));
        }

        /// <summary>
        /// Adds a BeGreaterThanOrEqualTo check to the validation plan rules.
        /// </summary>
        /// <typeparam name="T">The type of compariable to check.</typeparam>
        /// <param name="validationPlan">The validation plan.</param>
        /// <param name="requirement">The requirement to meet.</param>
        /// <returns>
        /// A revised validation plan.
        /// </returns>
        public static Tuple<GetArguments, IEnumerable<Rule>> BeGreaterThanOrEqualTo<T>(this Tuple<GetArguments, IEnumerable<Rule>> validationPlan, T requirement)
            where T : IComparable<T>
        {
            return ValidateArgumentsAndAppendRule(validationPlan, Rules.BeGreaterThanOrEqualTo(requirement));
        }
    }
#if !SpritelyRecipesProject
#pragma warning restore 0436
#endif
}
