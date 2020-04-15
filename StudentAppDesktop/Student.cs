using System;

namespace StudentAppDesktop
{
    public class Student : IComparable<Student>, ICloneable
    {
        public Student(string firstName, string lastName, string middleName, ushort birthYear, double avgScore = 0)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthYear = birthYear;
            AvgScore = avgScore;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public ushort BirthYear { get; set; }
        public double AvgScore { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}";

        public object Clone()
        {
            return new Student(FirstName, LastName, MiddleName, BirthYear, AvgScore);
        }

        public int CompareTo(Student other)
        {
            return Compare(this, other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Student)obj);
        }

        public bool Equals(Student student)
        {
            if (ReferenceEquals(student, null)) return false;
            return FirstName == student.FirstName && LastName == student.LastName && MiddleName == student.MiddleName;
        }

        public override int GetHashCode()
        {
            var PRIME = 59;
            var result = 1;
            result = result * PRIME + (FirstName == null ? 43 : FirstName.GetHashCode());
            result = result * PRIME + (LastName == null ? 43 : LastName.GetHashCode());
            result = result * PRIME + (MiddleName == null ? 43 : MiddleName.GetHashCode());
            return result;
        }

        public static bool operator ==(Student a, Student b)
        {
            return Compare(a, b) == 0;
        }

        public static bool operator !=(Student a, Student b)
        {
            return Compare(a, b) != 0;
        }

        public static bool operator >=(Student a, Student b)
        {
            return Compare(a, b) <= 0;
        }

        public static bool operator <=(Student a, Student b)
        {
            return Compare(a, b) >= 0;
        }

        public static bool operator >(Student a, Student b)
        {
            return Compare(a, b) < 0;
        }

        public static bool operator <(Student a, Student b)
        {
            return Compare(a, b) > 0;
        }

        private static int Compare(Student a, Student b)
        {
            if (ReferenceEquals(a, null)) return ReferenceEquals(b, null) ? 0 : -1;
            if (ReferenceEquals(b, null)) return 1;

            var compare = string.CompareOrdinal(a.FirstName, b.FirstName);
            if (compare != 0) return compare;

            compare = string.CompareOrdinal(a.MiddleName, b.MiddleName);
            if (compare != 0) return compare;

            return string.CompareOrdinal(a.LastName, b.LastName);
        }

        public override string ToString()
        {
            return $"Student {FirstName} {MiddleName} {LastName}. " +
                   $"Year oof birth: {BirthYear}. " +
                   $"Average score: {AvgScore}";
        }
    }
}
