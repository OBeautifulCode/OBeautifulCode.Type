// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeDescriptionExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.TypeRepresentation
{
    using System;
    using System.Linq;

    using OBeautifulCode.Reflection.Recipes;

    using Spritely.Recipes;

    /// <summary>
    /// Class to hold extension method on the type object.
    /// </summary>
    public static class TypeDescriptionExtensions
    {
        /// <summary>
        /// Creates a new type description from a given type.
        /// </summary>
        /// <param name="type">Input type to use.</param>
        /// <returns>Type description describing input type.</returns>
        public static TypeDescription ToTypeDescription(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var result = new TypeDescription(type.Namespace, type.Name, type.AssemblyQualifiedName);

            return result;
        }

        /// <summary>
        /// Attempts to resolve the <see cref="TypeDescription"/> from all loaded types in all assemblies in the current app domain.
        /// </summary>
        /// <param name="typeDescription">Type description to search for.</param>
        /// <param name="typeMatchStrategy">Optional matching strategy (default is loose - namespace and name match).</param>
        /// <param name="multipleMatchStrategy">Optional logic on how to deal with multiples found (default is strict - throw on multiple).</param>
        /// <returns>Type if found, null otherwise.</returns>
        public static Type ResolveFromLoadedTypes(this TypeDescription typeDescription, TypeMatchStrategy typeMatchStrategy = TypeMatchStrategy.NamespaceAndName, MultipleMatchStrategy multipleMatchStrategy = MultipleMatchStrategy.ThrowOnMultiple)
        {
            new { typeDescription }.Must().NotBeNull().OrThrowFirstFailure();

            // first deal with special hack implementation of array types...
            if (typeDescription.Name.Contains("[]") || typeDescription.AssemblyQualifiedName.Contains("[]"))
            {
                var arrayItemTypeDescription = new TypeDescription
                                               {
                                                   AssemblyQualifiedName = typeDescription.AssemblyQualifiedName.Replace("[]", string.Empty),
                                                   Namespace = typeDescription.Namespace,
                                                   Name = typeDescription.Name.Replace("[]", string.Empty),
                                               };

                var arrayItemType = arrayItemTypeDescription.ResolveFromLoadedTypes(typeMatchStrategy, multipleMatchStrategy);
                return arrayItemType?.MakeArrayType();
            }

            // if it's not an array type then run normal logic
            var loadedAssemblies = AssemblyLoader.GetLoadedAssemblies();
            var allTypes = loadedAssemblies.Distinct().ToList().GetTypesFromAssemblies().Distinct().ToList();

            var typeComparer = new TypeComparer(typeMatchStrategy);
            var allMatchingTypes = allTypes.Where(_ => typeComparer.Equals(_.ToTypeDescription(), typeDescription)).ToList();

            switch (multipleMatchStrategy)
            {
                case MultipleMatchStrategy.ThrowOnMultiple:
                    if (allMatchingTypes.Count > 1)
                    {
                        var message = "Found multiple versions and multiple match strategy was: " + multipleMatchStrategy;
                        var types = string.Join(",", allMatchingTypes.Select(_ => _.AssemblyQualifiedName + " at " + _.Assembly.CodeBase));
                        throw new InvalidOperationException(message + "; types found: " + types);
                    }
                    else
                    {
                        return allMatchingTypes.SingleOrDefault();
                    }

                case MultipleMatchStrategy.NewestVersion:
                    return allMatchingTypes.OrderByDescending(_ => (_.Assembly.GetName().Version ?? new Version(0, 0, 0, 1)).ToString()).FirstOrDefault();
                case MultipleMatchStrategy.OldestVersion:
                    return allMatchingTypes.OrderBy(_ => (_.Assembly.GetName().Version ?? new Version(0, 0, 0, 1)).ToString()).FirstOrDefault();
                default:
                    throw new NotSupportedException("Multiple match strategy not supported: " + multipleMatchStrategy);
            }
        }
    }
}
