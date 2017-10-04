// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeDescriptionExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.TypeRepresentation.Test
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using FluentAssertions;

    using Xunit;

    public static class TypeDescriptionExtensionsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void ToTypeDescription___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => TypeDescriptionExtensions.ToTypeDescription(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void ToTypeDescription___Should_return_type_description___When_called()
        {
            // Arrange
            var type = typeof(string);

            // Act
            var description = type.ToTypeDescription();

            // Assert
            description.AssemblyQualifiedName.Should().Be(type.AssemblyQualifiedName);
            description.Namespace.Should().Be(type.Namespace);
            description.Name.Should().Be(type.Name);
        }

        [Fact]
        public static void Resolve___Should_return_null___For_missing_type()
        {
            // Arrange
            var toFind = new TypeDescription("namespace", "name", "assemblyqualifiedname");

            // Act
            var result = toFind.ResolveFromLoadedTypes();

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public static void Resolve_ThrowOnMultiple___Should_return_valid_type___For_found_type()
        {
            // Arrange
            var type = typeof(string);
            var toFind = type.ToTypeDescription();

            // Act
            var result = toFind.ResolveFromLoadedTypes(TypeMatchStrategy.NamespaceAndName, MultipleMatchStrategy.ThrowOnMultiple);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(type);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFile", Justification = "Used on purpose.")]
        public static void Resolve_ThrowOnMultiple___Should_throw___For_multiple_found_types()
        {
            // Arrange
            var testAssemblyRootPath =
                Path.Combine(
                    new Uri(typeof(TypeDescriptionExtensionsTest).Assembly.CodeBase).LocalPath.Replace(
                        "\\OBeautifulCode.TypeRepresentation.Test.DLL",
                        string.Empty),
                    "..",
                    "..",
                    "TestingAssemblies");

            Assembly.LoadFile(Path.Combine(testAssemblyRootPath, "Newtonsoft.Json.8.0.3.testDll"));
            Assembly.LoadFile(Path.Combine(testAssemblyRootPath, "Newtonsoft.Json.9.0.1.testDll"));
            var toFind = new TypeDescription("Newtonsoft.Json", "JsonConvert", "FullNameShouldNotBeUsed");
            Action action = () => toFind.ResolveFromLoadedTypes(TypeMatchStrategy.NamespaceAndName, MultipleMatchStrategy.ThrowOnMultiple);

            // Act & Assert
            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFile", Justification = "Used on purpose.")]
        public static void Resolve_ThrowOnMultiple___Should_return_valid_type___When_multiple_same_types_found()
        {
            // Arrange
            var testAssemblyRootPath =
                Path.Combine(
                    new Uri(typeof(TypeDescriptionExtensionsTest).Assembly.CodeBase).LocalPath.Replace(
                        "\\OBeautifulCode.TypeRepresentation.Test.DLL",
                        string.Empty),
                    "..",
                    "..",
                    "TestingAssemblies");

            Assembly.LoadFile(Path.Combine(testAssemblyRootPath, "Newtonsoft.Json.8.0.3.testDll"));
            Assembly.LoadFile(Path.Combine(testAssemblyRootPath, "Newtonsoft.Json.8.0.3.testDll"));
            var toFind = new TypeDescription("Newtonsoft.Json", "JsonConvert", "FullNameShouldNotBeUsed");

            // Act
            var result = toFind.ResolveFromLoadedTypes(TypeMatchStrategy.NamespaceAndName, MultipleMatchStrategy.OldestVersion);

            // Assert
            result.Should().NotBeNull();
            result.Assembly.GetName().Version.ToString(4).Should().Be("8.0.0.0");
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFile", Justification = "Used on purpose.")]
        public static void Resolve_Newest___Should_return_valid_type___For_found_type()
        {
            // Arrange
            var testAssemblyRootPath =
                Path.Combine(
                    new Uri(typeof(TypeDescriptionExtensionsTest).Assembly.CodeBase).LocalPath.Replace(
                        "\\OBeautifulCode.TypeRepresentation.Test.DLL",
                        string.Empty),
                    "..",
                    "..",
                    "TestingAssemblies");

            Assembly.LoadFile(Path.Combine(testAssemblyRootPath, "Newtonsoft.Json.8.0.3.testDll"));
            Assembly.LoadFile(Path.Combine(testAssemblyRootPath, "Newtonsoft.Json.9.0.1.testDll"));
            var toFind = new TypeDescription("Newtonsoft.Json", "JsonConvert", "FullNameShouldNotBeUsed");

            // Act
            var result = toFind.ResolveFromLoadedTypes(TypeMatchStrategy.NamespaceAndName, MultipleMatchStrategy.NewestVersion);

            // Assert
            result.Should().NotBeNull();
            result.Assembly.GetName().Version.ToString(4).Should().Be("9.0.0.0");
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFile", Justification = "Used on purpose.")]
        public static void Resolve_Oldest___Should_return_valid_type___For_found_type()
        {
            // Arrange
            var testAssemblyRootPath =
                Path.Combine(
                    new Uri(typeof(TypeDescriptionExtensionsTest).Assembly.CodeBase).LocalPath.Replace(
                        "\\OBeautifulCode.TypeRepresentation.Test.DLL",
                        string.Empty),
                    "..",
                    "..",
                    "TestingAssemblies");

            Assembly.LoadFile(Path.Combine(testAssemblyRootPath, "Newtonsoft.Json.8.0.3.testDll"));
            Assembly.LoadFile(Path.Combine(testAssemblyRootPath, "Newtonsoft.Json.9.0.1.testDll"));
            var toFind = new TypeDescription("Newtonsoft.Json", "JsonConvert", "FullNameShouldNotBeUsed");

            // Act
            var result = toFind.ResolveFromLoadedTypes(TypeMatchStrategy.NamespaceAndName, MultipleMatchStrategy.OldestVersion);

            // Assert
            result.Should().NotBeNull();
            result.Assembly.GetName().Version.ToString(4).Should().Be("8.0.0.0");
        }

        // ReSharper restore InconsistentNaming
    }
}
