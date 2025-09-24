namespace PhoneDictionary.Model;

public class SavedEntity
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
        if (obj is SavedEntity entity)
            return FirstName.Equals(entity.FirstName, StringComparison.OrdinalIgnoreCase)
                   && LastName.Equals(entity.LastName, StringComparison.OrdinalIgnoreCase);
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName.ToLower(), LastName.ToLower());
    }

}