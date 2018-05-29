﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeComparer.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.TypeRepresentation
{
    using System;
    using System.Collections.Generic;

    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Type comparer using the provided strategy.
    /// </summary>
    public class TypeComparer : IEqualityComparer<Type>, IEqualityComparer<TypeDescription>
    {
        /// <summary>
        /// The strategy to use when comparing types.
        /// </summary>
        private readonly TypeMatchStrategy typeMatchStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeComparer"/> class.
        /// </summary>
        /// <param name="typeMatchStrategy">Strategy for use when matching the type.</param>
        public TypeComparer(TypeMatchStrategy typeMatchStrategy)
        {
            this.typeMatchStrategy = typeMatchStrategy;
        }

        /// <inheritdoc />
        public bool Equals(Type x, Type y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            var ret = this.Equals(
                x.Namespace,
                x.Name,
                x.AssemblyQualifiedName,
                y.Namespace,
                y.Name,
                y.AssemblyQualifiedName);

            return ret;
        }

        /// <inheritdoc />
        public bool Equals(TypeDescription x, TypeDescription y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            var ret = this.Equals(
                x.Namespace,
                x.Name,
                x.AssemblyQualifiedName,
                y.Namespace,
                y.Name,
                y.AssemblyQualifiedName);
            return ret;
        }

        /// <summary>
        /// Equals compare that takes raw strings instead of types.
        /// </summary>
        /// <param name="namespaceX">Namespace of type X.</param>
        /// <param name="nameX">Name of type X.</param>
        /// <param name="assemblyQualifiedNameX">AssemblyQualifiedName of type X.</param>
        /// <param name="namespaceY">Namespace of type Y.</param>
        /// <param name="nameY">Name of type Y.</param>
        /// <param name="assemblyQualifiedNameY">AssemblyQualifiedName of type Y.</param>
        /// <returns>True for equality and false otherwise.</returns>
        public bool Equals(string namespaceX, string nameX, string assemblyQualifiedNameX, string namespaceY, string nameY, string assemblyQualifiedNameY)
        {
            bool ret;
            switch (this.typeMatchStrategy)
            {
                case TypeMatchStrategy.NamespaceAndName:
                    ret = namespaceX == namespaceY && nameX == nameY;
                    break;
                case TypeMatchStrategy.AssemblyQualifiedName:
                    if (assemblyQualifiedNameX == null || assemblyQualifiedNameY == null)
                    {
                        throw new ArgumentException(
                            "Type(s) AssemblyQualifiedName property was null so catch use matching strategy: AssemblyQualifiedName");
                    }

                    ret = assemblyQualifiedNameX == assemblyQualifiedNameY;
                    break;
                default:
                    throw new ArgumentException("Unsupported matching strategy: " + this.typeMatchStrategy);
            }

            return ret;
        }

        /// <inheritdoc />
        public int GetHashCode(TypeDescription obj)
        {
            new { obj }.Must().NotBeNull();

            switch (this.typeMatchStrategy)
            {
                case TypeMatchStrategy.NamespaceAndName:
                    return (obj.Namespace + "." + obj.Name).GetHashCode();
                case TypeMatchStrategy.AssemblyQualifiedName:
                    if (obj.AssemblyQualifiedName == null)
                    {
                        throw new ArgumentException(
                            "Type's AssemblyQualifiedName property was null so catch use matching strategy: AssemblyQualifiedName");
                    }

                    return obj.AssemblyQualifiedName.GetHashCode();
                default:
                    throw new ArgumentException("Unsupported matching strategy: " + this.typeMatchStrategy);
            }
        }

        /// <inheritdoc />
        public int GetHashCode(Type obj)
        {
            return this.GetHashCode(obj.ToTypeDescription());
        }
    }
}