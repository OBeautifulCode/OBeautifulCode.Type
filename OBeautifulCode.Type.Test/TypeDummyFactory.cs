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
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Math.Recipes;

    /// <summary>
    /// A Dummy Factory for types in <see cref="OBeautifulCode.Type"/>.
    /// </summary>
#if !OBeautifulCodeTypeSolution
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Type.Test", "See package version number")]
    internal
#else
    public
#endif
    class TypeDummyFactory : DefaultTypeDummyFactory
    {
        private static readonly IReadOnlyList<Type> LoadedTypes =
            AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => (a != null) && (!a.IsDynamic))
                .SelectMany(_ => _.GetTypes())
                .Where(_ => _ != null)
                .Where(_ => !string.IsNullOrWhiteSpace(_.Namespace))
                .ToList();

        private static readonly IReadOnlyList<Type> AppDomainClosedTypes =
            LoadedTypes
                .Where(_ => !_.ContainsGenericParameters)
                .ToList();

        private static readonly IReadOnlyList<Type> AppDomainGenericTypeDefinitions =
            LoadedTypes
                .Where(_ => _.IsGenericTypeDefinition)
                .ToList();

        public TypeDummyFactory()
        {
            //---------------------------------------------------------------
            // REMOVE WHEN WE CAN CODE GEN AND THIS MOVES INTO DESIGNER
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(ExecuteOpRequestedEvent<Version, NullVoidOp>),
                        typeof(ExecuteOpRequestedEvent<NullVoidOp>),
                        typeof(NullEvent),
                        typeof(NullEvent<Version>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (EventBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(ExecuteOpRequestedEvent<Version, NullVoidOp>),
                        typeof(NullEvent<Version>)
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (EventBase<Version>)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(NullVoidOp),
                        typeof(NullReturningOp<Version>),
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (OperationBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(NullVoidOp),
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (VoidOperationBase)AD.ummy(randomType);

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var availableTypes = new[]
                    {
                        typeof(NullReturningOp<Version>),
                    };

                    var randomIndex = ThreadSafeRandom.Next(0, availableTypes.Length);

                    var randomType = availableTypes[randomIndex];

                    var result = (ReturningOperationBase<Version>)AD.ummy(randomType);

                    return result;
                });

            //---------------------------------------------------------------
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
            {
                var startDateTime = A.Dummy<DateTime>();

                var endDateTime = A.Dummy<DateTime>().ThatIs(_ => _ >= startDateTime);

                var result = new UtcDateTimeRangeInclusive(startDateTime.ToUniversalTime(), endDateTime.ToUniversalTime());

                return result;
            });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    Type result;

                    if (ThreadSafeRandom.Next(0, 2) == 0)
                    {
                        result = GetRandomClosedTypeInAppDomain();
                    }
                    else
                    {
                        while (true)
                        {
                            var genericTypeDefinition = GetRandomGenericTypeDefinitionInAppDomain();

                            var typeArguments = Enumerable.Range(0, genericTypeDefinition.GetGenericArguments().Length).Select(_ => GetRandomClosedTypeInAppDomain()).ToArray();

                            try
                            {
                                result = genericTypeDefinition.MakeGenericType(typeArguments);

                                break;
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new NullEvent(
                    A.Dummy<DateTime>().ToUniversalTime()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new NullEvent<Version>(
                    A.Dummy<Version>(),
                    A.Dummy<DateTime>().ToUniversalTime()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ExecuteOpRequestedEvent<NullVoidOp>(
                    A.Dummy<NullVoidOp>(),
                    A.Dummy<DateTime>().ToUniversalTime(),
                    A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new ExecuteOpRequestedEvent<Version, NullReturningOp<Version>>(
                    A.Dummy<Version>(),
                    A.Dummy<NullReturningOp<Version>>(),
                    A.Dummy<DateTime>().ToUniversalTime(),
                    A.Dummy<string>()));

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new NullVoidOp());

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new NullReturningOp<Version>());
        }

        private Type GetRandomClosedTypeInAppDomain()
        {
            var randomIndex = ThreadSafeRandom.Next(0, AppDomainClosedTypes.Count);

            var result = AppDomainClosedTypes[randomIndex];

            return result;
        }

        private Type GetRandomGenericTypeDefinitionInAppDomain()
        {
            var randomIndex = ThreadSafeRandom.Next(0, AppDomainGenericTypeDefinitions.Count);

            var result = AppDomainGenericTypeDefinitions[randomIndex];

            return result;
        }
    }
}