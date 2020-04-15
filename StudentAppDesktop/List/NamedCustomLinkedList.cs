using System;

namespace StudentAppDesktop.List
{
    public class NamedCustomLinkedList<T> : CustomLinkedList<T>, IComparable<NamedCustomLinkedList<T>>
           where T : IComparable<T>, ICloneable
    {
        public NamedCustomLinkedList(string name)
        {
            Name = name;
        }

        public NamedCustomLinkedList(NamedCustomLinkedList<T> source) : base(source)
        {
            Name = source.Name;
        }

        public string Name { get; }

        public int CompareTo(NamedCustomLinkedList<T> other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.CompareOrdinal(Name, other.Name);
        }

        public override object Clone()
        {
            return new NamedCustomLinkedList<T>(this);
        }

        public static bool operator >(NamedCustomLinkedList<T> aList, NamedCustomLinkedList<T> bList)
        {
            return string.CompareOrdinal(aList?.Name, bList?.Name) < 0;
        }

        public static bool operator <(NamedCustomLinkedList<T> aList, NamedCustomLinkedList<T> bList)
        {
            return string.CompareOrdinal(aList?.Name, bList?.Name) > 0;
        }

        public static bool operator >=(NamedCustomLinkedList<T> aList, NamedCustomLinkedList<T> bList)
        {
            return string.CompareOrdinal(aList?.Name, bList?.Name) <= 0;
        }

        public static bool operator <=(NamedCustomLinkedList<T> aList, NamedCustomLinkedList<T> bList)
        {
            return string.CompareOrdinal(aList?.Name, bList?.Name) >= 0;
        }
    }
}
