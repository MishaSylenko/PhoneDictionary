namespace PhoneDictionary.Model;

public class SavedEntity : IEquatable<SavedEntity>
{
    public string FirstName { get; private init; }

    public string LastName { get; private init; }
    
    private readonly List<string> _phoneNumbers;
    
    public IReadOnlyList<string> PhoneNumbers => _phoneNumbers.AsReadOnly();
    public string Address { get; set; }
    
    public SavedEntity(string fName, string lName, string? address)
    {
        if (string.IsNullOrWhiteSpace(fName)) throw new ArgumentException("First name cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(lName)) throw new ArgumentException("Last name cannot be null or empty.");
        FirstName = fName;
        LastName = lName;
        Address = address ?? "No Address";
        _phoneNumbers = new List<string>();
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}, Phones: {string.Join(", ", _phoneNumbers)}, Address: {Address}";
    }
    
    public void AddPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be null or empty.");
        _phoneNumbers.Add(phoneNumber);
    }
    
    public void RemovePhoneNumber(string phoneNumber)
    {
        _phoneNumbers.Remove(phoneNumber);
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SavedEntity)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_phoneNumbers, FirstName, LastName, Address);
    }

    public bool Equals(SavedEntity? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return FirstName == other.FirstName && LastName == other.LastName;
    }
}