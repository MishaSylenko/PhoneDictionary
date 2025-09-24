namespace Task2.Model;

public class Person : IEquatable<Person>, IComparable<Person>
{
    public int Id { get; }
    public string FirstName { get; private init; }
    public string LastName { get; private init; }

    public Person(int id, string firstName, string lastName )
    {
        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name cannot be null or empty.");

        FirstName = firstName;
        LastName = lastName;
        Id = id;
    }

    public bool Equals(Person? other)
    {
        if (other == null) return false;
        return Id == other.Id;
    }

    public int CompareTo(Person? other)
    {
        if (other == null) return 1;
        return string.Compare(FirstName + LastName, other.FirstName + other.LastName, StringComparison.Ordinal);
        
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Person);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}, Id: {Id}";
    }
}