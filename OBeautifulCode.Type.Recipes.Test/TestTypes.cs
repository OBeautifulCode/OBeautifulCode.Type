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

    public class TestTypes
    {
        public Type[] OpenTypes => new[] { typeof(List<>).MakeArrayType() };

        public Type[] OpenArrayTypes => new[] { typeof(List<>).MakeArrayType() };
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
