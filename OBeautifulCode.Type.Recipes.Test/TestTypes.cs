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
    using System.Linq;

    public static class TestTypes
    {
        public static Type[] ClosedValueTupleTypes => new[]
        {
            (first: "one", second: 7).GetType(),
        };

        public static Type[] ClosedAnonymousTypes => new[]
        {
            new { SomeProperty = "property value" }.GetType(),
            new { Anonymous = true, Inner = new { InnerAnonymous = 6 } }.GetType(),
        };

        public static Type[] ClosedInterfaceTypes => new[]
        {
            typeof(IEnumerable),
            typeof(IReadOnlyList<string>),
            typeof(IReadOnlyDictionary<IReadOnlyDictionary<Guid[], int?>, IList<IList<short>>[]>),
        };

        public static Type[] ClosedClassTypes => new[]
        {
            typeof(object),
            typeof(TestClass),
            typeof(TestClassWithNestedClass.NestedInTestClass),
            typeof(Dictionary<int, string>),
            typeof(DerivedGenericClass<int>),
        };

        public static Type[] ClosedStructTypes => new[]
        {
            typeof(int),
            typeof(DateTime),
            typeof(Guid),
        };

        public static Type[] ClosedNullableTypes => new[]
        {
            typeof(int?),
            typeof(DateTime?),
            typeof(Guid?),
        };

        public static Type[] ClosedArrayTypes => new[]
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

        public static Type[] ClosedTypes => new Type[0]
            .Concat(ClosedValueTupleTypes)
            .Concat(ClosedAnonymousTypes)
            .Concat(ClosedInterfaceTypes)
            .Concat(ClosedClassTypes)
            .Concat(ClosedStructTypes)
            .Concat(ClosedNullableTypes)
            .Concat(ClosedArrayTypes)
            .ToArray();

        public static Type[] GenericTypeDefinitions => new Type[0]
            .Concat(ClosedTypes)
            .Concat(OpenTypesWithoutGenericTypeDefinitionTypes)
            .Where(_ => _.IsGenericType)
            .Select(_ => _.GetGenericTypeDefinition())
            .ToArray();

        public static Type[] OpenArrayTypes => new[]
        {
            typeof(List<>).MakeArrayType(),
        };

        public static Type[] OpenClassTypesWithoutGenericTypeDefinitionTypes => new[]
        {
            typeof(DerivedGenericClass<>).BaseType,
            typeof(DerivedGenericClass<>).GetField(nameof(DerivedGenericClass<string>.DerivedGenericClassField)).FieldType,
            typeof(List<>).MakeGenericType(typeof(List<>)),
            typeof(GenericClassList<>).BaseType, // https://stackoverflow.com/questions/59141721/why-is-the-basetype-of-a-generic-type-definition-not-itself-a-generic-type-defin
        };

        public static Type[] OpenInterfaceTypesWithoutGenericTypeDefinitionTypes => new[]
        {
            typeof(IReadOnlyCollection<>).MakeGenericType(typeof(IReadOnlyCollection<>)),
        };

        public static Type[] GenericParameterTypes => new[]
        {
            typeof(BaseGenericClass<,>).GetGenericArguments()[0],
        };

        public static Type[] OpenTypesWithoutGenericTypeDefinitionTypes => new Type[0]
            .Concat(OpenArrayTypes)
            .Concat(OpenClassTypesWithoutGenericTypeDefinitionTypes)
            .Concat(OpenInterfaceTypesWithoutGenericTypeDefinitionTypes)
            .Concat(GenericParameterTypes)
            .ToArray();

        public static Type[] OpenTypes => new Type[0]
            .Concat(OpenTypesWithoutGenericTypeDefinitionTypes)
            .Concat(GenericTypeDefinitions)
            .ToArray();
    }

    public class TestClass
    {
    }

    public class TestClassWithNestedClass
    {
        public class NestedInTestClass
        {
        }
    }

    public class BaseClassIList<T> : IList<T>
    {
        public int Count { get; }

        public bool IsReadOnly { get; }

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

    public class DerivedClassIList<T> : BaseClassIList<T>
    {
    }

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

    public class GenericClassDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
    }

    public class NonGenericDictionaryClass : Dictionary<string, int?>
    {
    }

    #pragma warning disable SA1201 // Elements should appear in the correct order
    public interface IGenericIReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
    }

    public interface INonGenericIReadOnlyDictionary : IReadOnlyDictionary<int, DateTime>
    {
    }

    #pragma warning restore SA1201 // Elements should appear in the correct order

    public class NonComparableClass
    {
    }

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

    public class CustomGenericComparableClass : IComparable<CustomGenericComparableClass>
    {
        public int CompareTo(CustomGenericComparableClass other)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomGenericComparableClass<T> : IComparable<CustomGenericComparableClass<T>>
    {
        public int CompareTo(CustomGenericComparableClass<T> other)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomComparableClass : IComparable
    {
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class ComparableOfStringClass : IComparable<string>
    {
        public int CompareTo(string other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
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
        public OrphanedGenericClass<DerivedGenericClass<TDerived>> DerivedGenericClassField;
        #pragma warning restore SA1401 // Fields should be private

        public class NestedInDerivedGeneric
        {
        }
    }

    public class OrphanedGenericClass<TOrphaned>
    {
    }
}
