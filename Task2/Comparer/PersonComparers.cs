using Task2.Model;

namespace Task2.Comparer;

using System;
using System.Collections.Generic;

public static class PersonComparers
{
    private class IdComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y) => x.Id.CompareTo(y.Id);
    }

    private class NameEqualityComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
           
           return x?.FirstName == y?.FirstName && x?.LastName == y?.LastName;
        }
            

        public int GetHashCode(Person obj)
        { 
            return HashCode.Combine(obj.FirstName, obj.LastName);
        } 
    }

    public static (IComparer<Person> comparer, IEqualityComparer<Person> equality) Get(bool anotherVersion)
    {
        if (anotherVersion)
        {
            return (new IdComparer(), new NameEqualityComparer());
        }
        else
        {
            return (Comparer<Person>.Default, EqualityComparer<Person>.Default);
        }
    }
}
