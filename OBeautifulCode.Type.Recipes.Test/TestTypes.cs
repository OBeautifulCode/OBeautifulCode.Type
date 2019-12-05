// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestTypes.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using OBeautifulCode.Type.Recipes.Test.Internal;

    public static class TestTypes
    {
        public static IReadOnlyCollection<Type> ClosedValueTupleTypes => new[]
        {
            (first: "one", second: 7).GetType(),
        };

        public static IReadOnlyCollection<Type> ClosedAnonymousTypes => new[]
        {
            new { SomeProperty = "property value" }.GetType(),
            new { Anonymous = true, Inner = new { InnerAnonymous = 6 } }.GetType(),
        };

        public static IReadOnlyCollection<Type> ClosedInterfaceTypes => new[]
        {
            typeof(IEnumerable),
            typeof(IReadOnlyList<string>),
            typeof(IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]>),
        };

        public static IReadOnlyCollection<Type> ClosedClassTypes => new[]
        {
            typeof(object),
            typeof(TestClass),
            typeof(TestClassWithNestedClass.NestedInTestClass),
            typeof(Dictionary<int, string>),
            typeof(DerivedGenericClass<int>),
        };

        public static IReadOnlyCollection<Type> ClosedStructTypes => new[]
        {
            typeof(int),
            typeof(DateTime),
            typeof(Guid),
        };

        public static IReadOnlyCollection<Type> ClosedNullableTypes => new[]
        {
            typeof(int?),
            typeof(DateTime?),
            typeof(Guid?),
        };

        public static IReadOnlyCollection<Type> ClosedArrayTypes => new[]
        {
            typeof(int[]),
            typeof(int?[]),
            typeof(Guid[]),
            typeof(Guid?[]),
            typeof(TestClass[]),
            typeof(IReadOnlyList<string>[]),
            typeof(Dictionary<int, string>[]),
            new DerivedGenericClass<int>[0].GetType(),
        };

        public static IReadOnlyCollection<Type> ClosedTypes => new Type[0]
            .Concat(ClosedValueTupleTypes)
            .Concat(ClosedAnonymousTypes)
            .Concat(ClosedInterfaceTypes)
            .Concat(ClosedClassTypes)
            .Concat(ClosedStructTypes)
            .Concat(ClosedNullableTypes)
            .Concat(ClosedArrayTypes)
            .ToArray();

        public static IReadOnlyCollection<Type> GenericTypeDefinitions => new Type[0]
            .Concat(ClosedTypes)
            .Concat(OpenTypesWithoutGenericTypeDefinitionTypes)
            .Where(_ => _.IsGenericType)
            .Select(_ => _.GetGenericTypeDefinition())
            .ToArray();

        public static IReadOnlyCollection<Type> OpenArrayTypes => new[]
        {
            typeof(List<>).MakeArrayType(),
        };

        public static IReadOnlyCollection<Type> OpenClassTypesWithoutGenericTypeDefinitionTypes => new[]
        {
            typeof(DerivedGenericClass<>).BaseType,
            typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType,
            typeof(List<>).MakeGenericType(typeof(List<>)),
            typeof(GenericClassList<>).BaseType, // https://stackoverflow.com/questions/59141721/why-is-the-basetype-of-a-generic-type-definition-not-itself-a-generic-type-defin
        };

        public static IReadOnlyCollection<Type> OpenInterfaceTypesWithoutGenericTypeDefinitionTypes => new[]
        {
            typeof(IReadOnlyCollection<>).MakeGenericType(typeof(IReadOnlyCollection<>)),
        };

        public static IReadOnlyCollection<Type> GenericParameterTypes => new[]
        {
            typeof(BaseGenericClass<,>).GetGenericArguments()[0],
        };

        public static IReadOnlyCollection<Type> OpenTypesWithoutGenericTypeDefinitionTypes => new Type[0]
            .Concat(OpenArrayTypes)
            .Concat(OpenClassTypesWithoutGenericTypeDefinitionTypes)
            .Concat(OpenInterfaceTypesWithoutGenericTypeDefinitionTypes)
            .Concat(GenericParameterTypes)
            .ToArray();

        public static IReadOnlyCollection<Type> OpenTypes => new Type[0]
            .Concat(OpenTypesWithoutGenericTypeDefinitionTypes)
            .Concat(GenericTypeDefinitions)
            .ToArray();
    }

    public class TestClass
    {
    }

    public sealed class TestClassWithNestedClass
    {
        private TestClassWithNestedClass()
        {
        }

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = ObcSuppressBecause.CA1034_NestedTypesShouldNotBeVisible_VisibleNestedTypeRequiredForTesting)]
        public class NestedInTestClass
        {
        }
    }

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = ObcSuppressBecause.CA1710_IdentifiersShouldHaveCorrectSuffix_NameDirectlyExtendedOrImplementedTypeAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public class BaseClassIList<T> : IList<T>
    {
        public int Count { get; }

        public bool IsReadOnly { get; }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = ObcSuppressBecause.CA1065_DoNotRaiseExceptionsInUnexpectedLocations_ThrowNotImplementedExceptionWhenForcedToSpecifyMemberThatWillNeverBeUsedInTesting)]
        public T this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
    }

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = ObcSuppressBecause.CA1710_IdentifiersShouldHaveCorrectSuffix_NameDirectlyExtendedOrImplementedTypeAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public class DerivedClassIList<T> : BaseClassIList<T>
    {
    }

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = ObcSuppressBecause.CA1710_IdentifiersShouldHaveCorrectSuffix_NameDirectlyExtendedOrImplementedTypeAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public class GenericClassList<T> : List<T>
    {
    }

    public class NonGenericClassCollection : Collection<string>
    {
    }

    #pragma warning disable SA1201 // Elements should appear in the correct order
    public interface INonGenericIReadOnlyCollection : IReadOnlyCollection<string>
    {
    }

    public interface IGenericIReadOnlyCollection<T> : IReadOnlyCollection<T>
    {
    }
#pragma warning restore SA1201 // Elements should appear in the correct order

    public class BaseClassIDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public int Count { get; }

        public bool IsReadOnly { get; }

        public ICollection<TKey> Keys { get; }

        public ICollection<TValue> Values { get; }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = ObcSuppressBecause.CA1065_DoNotRaiseExceptionsInUnexpectedLocations_ThrowNotImplementedExceptionWhenForcedToSpecifyMemberThatWillNeverBeUsedInTesting)]
        public TValue this[TKey key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }
    }

    public class DerivedClassIDictionary<TKey, TValue> : BaseClassIDictionary<TKey, TValue>
    {
    }

    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = ObcSuppressBecause.CA2237_MarkISerializableTypesWithSerializable_UsedForTestingWithNoIntentionToSerialize)]
    public class GenericClassDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
    }

    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = ObcSuppressBecause.CA2237_MarkISerializableTypesWithSerializable_UsedForTestingWithNoIntentionToSerialize)]
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = ObcSuppressBecause.CA1710_IdentifiersShouldHaveCorrectSuffix_NameDirectlyExtendedOrImplementedTypeAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public class NonGenericClassDictionary : Dictionary<string, int?>
    {
    }

#pragma warning disable SA1201 // Elements should appear in the correct order
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = ObcSuppressBecause.CA1710_IdentifiersShouldHaveCorrectSuffix_NameDirectlyExtendedOrImplementedTypeAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = ObcSuppressBecause.CA1711_IdentifiersShouldNotHaveIncorrectSuffix_TypeNameAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public interface IGenericIReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
    }

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = ObcSuppressBecause.CA1710_IdentifiersShouldHaveCorrectSuffix_NameDirectlyExtendedOrImplementedTypeAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = ObcSuppressBecause.CA1711_IdentifiersShouldNotHaveIncorrectSuffix_TypeNameAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public interface INonGenericIReadOnlyDictionary : IReadOnlyDictionary<int, DateTime>
    {
    }

    #pragma warning restore SA1201 // Elements should appear in the correct order

    public class NonComparableClass
    {
    }

    [SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = ObcSuppressBecause.CA1036_OverrideMethodsOnComparableTypes_TypeCreatedForTestsThatRequireComparableTypeButDoNotUseTypeToPerformComparisons)]
    public class ComparableClass : IComparable<ComparableClass>
    {
        public int CompareTo(ComparableClass other)
        {
            throw new NotImplementedException();
        }
    }

    #pragma warning disable SA1201 // Elements should appear in the correct order
    public interface INonComparable
    {
    }

    public interface ICustomGenericComparable : IComparable<ICustomGenericComparable>
    {
    }

    public interface ICustomGenericComparable<T> : IComparable<ICustomGenericComparable<T>>
    {
    }

    public interface ICustomComparable : IComparable
    {
    }

#pragma warning restore SA1201 // Elements should appear in the correct order

    [SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = ObcSuppressBecause.CA1036_OverrideMethodsOnComparableTypes_TypeCreatedForTestsThatRequireComparableTypeButDoNotUseTypeToPerformComparisons)]
    public class CustomGenericComparableClass : IComparable<CustomGenericComparableClass>
    {
        public int CompareTo(CustomGenericComparableClass other)
        {
            throw new NotImplementedException();
        }
    }

    [SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = ObcSuppressBecause.CA1036_OverrideMethodsOnComparableTypes_TypeCreatedForTestsThatRequireComparableTypeButDoNotUseTypeToPerformComparisons)]
    public class CustomGenericComparableClass<T> : IComparable<CustomGenericComparableClass<T>>
    {
        public int CompareTo(CustomGenericComparableClass<T> other)
        {
            throw new NotImplementedException();
        }
    }

    [SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = ObcSuppressBecause.CA1036_OverrideMethodsOnComparableTypes_TypeCreatedForTestsThatRequireComparableTypeButDoNotUseTypeToPerformComparisons)]
    public class CustomComparableClass : IComparable
    {
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    [SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = ObcSuppressBecause.CA1036_OverrideMethodsOnComparableTypes_TypeCreatedForTestsThatRequireComparableTypeButDoNotUseTypeToPerformComparisons)]
    public class ComparableOfStringClass : IComparable<string>
    {
        public int CompareTo(string other)
        {
            throw new NotImplementedException();
        }
    }

    public class BaseGenericClass<TBase1, TBase2>
    {
    }

    public class DerivedGenericClass<TDerived> : BaseGenericClass<string, TDerived>
    {
#pragma warning disable SA1401 // Fields should be private
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = ObcSuppressBecause.CA1051_DoNotDeclareVisibleInstanceFields_TypeUsedInTestingThatRequiresInstanceFieldToBeVisible)]
        public OrphanedGenericClass<DerivedGenericClass<TDerived>> DerivedGenericClassField;
#pragma warning restore SA1401 // Fields should be private

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = ObcSuppressBecause.CA1034_NestedTypesShouldNotBeVisible_VisibleNestedTypeRequiredForTesting)]
        public class NestedInDerivedGeneric
        {
        }
    }

    public class OrphanedGenericClass<TOrphaned>
    {
    }
}
