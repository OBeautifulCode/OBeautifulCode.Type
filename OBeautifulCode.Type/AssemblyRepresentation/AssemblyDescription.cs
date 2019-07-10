﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyDescription.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using OBeautifulCode.Math.Recipes;
    using OBeautifulCode.Reflection.Recipes;
    using OBeautifulCode.Validation.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Description of <see cref="Assembly" />.
    /// </summary>
    public class AssemblyDescription : IEquatable<AssemblyDescription>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyDescription"/> class.
        /// </summary>
        /// <param name="name">Name of the assembly.</param>
        /// <param name="version">Version of the assembly.</param>
        /// <param name="filePath">File path of the assembly observed.</param>
        /// <param name="frameworkVersion">Framework of assembly.</param>
        public AssemblyDescription(string name, string version, string filePath, string frameworkVersion)
        {
            new { name }.Must().NotBeNullNorWhiteSpace();

            this.Name = name;
            this.Version = version;
            this.FilePath = filePath;
            this.FrameworkVersion = frameworkVersion;
        }

        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the version of the assembly.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Gets the file path the assembly was observed at.
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Gets the .NET framework the assembly was build for.
        /// </summary>
        public string FrameworkVersion { get; private set; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are equal.</returns>
        public static bool operator ==(AssemblyDescription first, AssemblyDescription second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return string.Equals(first.Name, second.Name, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(first.Version, second.Version, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(first.FilePath, second.FilePath, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(first.FrameworkVersion, second.FrameworkVersion, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are inequal.</returns>
        public static bool operator !=(AssemblyDescription first, AssemblyDescription second) => !(first == second);

        /// <summary>
        /// Reads the assembly from file path to create a <see cref="AssemblyDescription"/>.
        /// </summary>
        /// <param name="assemblyFilePath">The file path the assembly is at.</param>
        /// <param name="useAssemblyIfAlreadyInAppDomain">If the assembly file is already loaded in the AppDomain will use the existing Assembly. Default is true, false will force the file to be loaded...</param>
        /// <returns>Details about an assembly.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFile", Justification = "Need to be able to do this from a file also.")]
        public static AssemblyDescription CreateFromFile(string assemblyFilePath, bool useAssemblyIfAlreadyInAppDomain = true)
        {
            new { assemblyFilePath }.Must().NotBeNullNorWhiteSpace();

            Assembly assembly = null;

            if (useAssemblyIfAlreadyInAppDomain)
            {
                assembly =
                    AppDomain.CurrentDomain.GetAssemblies()
                             .Where(_ => !_.IsDynamic)
                             .SingleOrDefault(_ => _.CodeBase.ToUpperInvariant() == new Uri(assemblyFilePath).ToString().ToUpperInvariant());
            }

            if (assembly == null)
            {
                assembly = Assembly.LoadFile(assemblyFilePath);
            }

            return assembly.ToDescription();
        }

        /// <inheritdoc />
        public bool Equals(AssemblyDescription other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as AssemblyDescription);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.Name)
            .Hash(this.Version)
            .Hash(this.FilePath)
            .Hash(this.FrameworkVersion)
            .Value;

        /// <inheritdoc />
        public override string ToString()
        {
            var result = Invariant($"{nameof(AssemblyDescription)} - {nameof(this.Name)}: {this.Name}; {nameof(this.Version)}: {this.Version?.ToString() ?? "<null>"}; {nameof(this.FilePath)}: {this.FilePath ?? "<null>"}; {nameof(this.FrameworkVersion)}: {this.FrameworkVersion ?? "<null>"}.");
            return result;
        }
    }

#pragma warning disable SA1204 // Static elements should appear before instance elements

    /// <summary>
    /// Extensions to <see cref="AssemblyDescription" />.
    /// </summary>
    public static class AssemblyDescriptionExtensions
#pragma warning restore SA1204 // Static elements should appear before instance elements
    {
        /// <summary>
        /// Reads the assembly to create a new <see cref="AssemblyDescription"/>.
        /// </summary>
        /// <param name="assembly">The assembly object to interrogate.</param>
        /// <returns>Details about an assembly.</returns>
        public static AssemblyDescription ToDescription(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var codeBasesToIgnore = new List<string>(new[] { "Microsoft.GeneratedCode", "Anonymously Hosted DynamicMethods Assembly" });

            var asmName = assembly.GetName();

            var frameworkVersionNumber = assembly.ImageRuntimeVersion.Substring(1, 3);

            var version = asmName.Version.ToString();
            var name = asmName.Name;

            var codeBase = codeBasesToIgnore.Contains(name) ? name : assembly.GetCodeBaseAsPathInsteadOfUri();

            return new AssemblyDescription(name, version, codeBase, frameworkVersionNumber);
        }

        /// <summary>
        /// Froms the description.
        /// </summary>
        /// <param name="assemblyDescription">The assembly description.</param>
        /// <returns>System.Reflection.Assembly.</returns>
        public static Assembly FromDescription(
                    this AssemblyDescription assemblyDescription)
        {
            if (assemblyDescription == null)
            {
                throw new ArgumentNullException(nameof(assemblyDescription));
            }

            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            var results = allAssemblies
                              .Where(_ => _.ToDescription().Equals(assemblyDescription))
                              .ToList();

            if (!results.Any())
            {
                throw new ArgumentException(Invariant($"Could not find an assembly that matched description '{assemblyDescription}' in '{nameof(AppDomain)}'."));
            }

            if (results.Count > 1)
            {
                var foundAddIn = string.Join(",", results.Select(_ => _.ToString()));
                throw new ArgumentException(Invariant($"Found too many assemblies that matched description '{assemblyDescription}' in '{nameof(AppDomain)}'; {foundAddIn}."));
            }

            return results.Single();
        }
    }
}
