﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeDummyFactory.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Type.Test source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;

    /// <inheritdoc />
#if !OBeautifulCodeTypeRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Type.Test", "See package version number")]
#endif
    // ReSharper disable once UnusedMember.Global
    public class TypeDummyFactory : IDummyFactory
    {
        public TypeDummyFactory()
        {
            AutoFixtureBackedDummyFactory.AddDummyCreator(() =>
            {
                var startDateTime = A.Dummy<DateTime>();

                var endDateTime = A.Dummy<DateTime>().ThatIs(_ => _ >= startDateTime);

                var result = new DateTimeRangeInclusive(startDateTime.ToUniversalTime(), endDateTime.ToUniversalTime());

                return result;
            });
        }

        /// <inheritdoc />
        public Priority Priority => new FakeItEasy.Priority(1);

        /// <inheritdoc />
        public bool CanCreate(Type type)
        {
            return false;
        }

        /// <inheritdoc />
        public object Create(Type type)
        {
            return null;
        }
    }
}