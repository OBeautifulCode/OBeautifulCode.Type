// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyDescriptionTests.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;
    using System.IO;
    using System.Linq;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public static class AssemblyDescriptionTests
    {
        [Fact]
        public static void ToString___Should_be_useful()
        {
            // Arrange
            var name = A.Dummy<string>();
            var version = A.Dummy<string>();
            var filePath = A.Dummy<string>();
            var frameworkVersion = A.Dummy<string>();
            var systemUnderTest = new AssemblyDescription(name, version, filePath, frameworkVersion);

            // Act
            var actualToString = systemUnderTest.ToString();

            // Assert
            actualToString.Should().Contain(name);
            actualToString.Should().Contain(version);
            actualToString.Should().Contain(filePath);
            actualToString.Should().Contain(frameworkVersion);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Not necessary for the test.")]
        [Fact(Skip = "Cannot get consistent AppVeyor and local passing.")]
        public static void CreateFromFile_VerifyWillUseAlreadyLoadedIfSpecified()
        {
            // NOTE: This test passes in AppVeyor but sometimes fails locally.

            // NOTE: these tests have to be done serially so i'm doing both in one test, the second will pollute the AppDomain if it runs first...

            // arrange
            var codeBase = typeof(AssemblyDescription).Assembly.CodeBase;
            var assemblyFilePath = new Uri(codeBase).PathAndQuery.Replace('/', Path.DirectorySeparatorChar);

            // act
            AssemblyDescription.CreateFromFile(assemblyFilePath);

            // assert
            AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).SelectMany(
                a =>
                {
                    try
                    {
                        return a.GetExportedTypes();
                    }
                    catch (Exception)
                    {
                        /* no-op */
                    }

                    return Enumerable.Empty<Type>();
                }).SingleOrDefault(_ => _.Name == nameof(AssemblyDescription)).Should().NotBeNull();

            // act
            AssemblyDescription.CreateFromFile(assemblyFilePath, false);

            // assert
            var loadedAssemblyDescription = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).SelectMany(
                a =>
                {
                    try
                    {
                        return a.GetExportedTypes();
                    }
                    catch (Exception)
                    {
                        /* no-op */
                    }

                    return Enumerable.Empty<Type>();
                }).Where(_ => _.Name == nameof(AssemblyDescription)).ToList();

            loadedAssemblyDescription.Should().HaveCount(2);
        }
    }
}