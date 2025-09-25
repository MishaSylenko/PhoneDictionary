using PhoneDictionary.Model;

namespace PhoneDictionary.Helper;

public class PhoneDictionary
{
    private readonly CustomDictionary<string, HashSet<SavedEntity>> _phoneDictionary = new(100);
    
    private static string GetKey(string firstName, string lastName) =>
        $"{firstName.Trim().ToLower()} {lastName.Trim().ToLower()}";
    
    public void AddEntry(SavedEntity entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        var key = GetKey(entry.FirstName, entry.LastName);
        if (_phoneDictionary.ContainsKey(key))
        {
            var existingSet = _phoneDictionary[key];
            existingSet.Add(entry);
        }
        else
        {
            _phoneDictionary.Add(key, [entry]);
        }
    }
    
    public void RemoveEntry(SavedEntity entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        var key = GetKey(entry.FirstName, entry.LastName);
        if (_phoneDictionary.ContainsKey(key))
        {
            var existingList = _phoneDictionary[key];
            existingList.Remove(entry);
            if (existingList.Count == 0)
            {
                _phoneDictionary.Remove(key);
            }
        }
    }
    
    public IEnumerable<SavedEntity> Search(string firstName, string lastName)
    {
        if( string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("First name and last name cannot be null or empty.");
        
        var key = GetKey(firstName, lastName);
        if (_phoneDictionary.ContainsKey(key))
        {
            return _phoneDictionary[key];
        }
        return Enumerable.Empty<SavedEntity>();
    }
    
    public IEnumerable<SavedEntity> SearchByFirstName(string firstName)
    {
        if( string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be null or empty.");
        
        var keyPart = firstName.Trim().ToLower();
        var results = new List<SavedEntity>();
        foreach (var kvp in _phoneDictionary)
        {
            var name = kvp.Key.Trim().Split(' ').First();
            if (name == keyPart)
            {
                results.AddRange(kvp.Value);
            }
        }
        return results;
    }

    public IEnumerable<SavedEntity> SearchByLastName(string lastName)
    {
        if(string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be null or empty.");
        var keyPart = lastName.Trim().ToLower();
        var results = new List<SavedEntity>();
        foreach (var kvp in _phoneDictionary)
        {
            var surname = kvp.Key.Trim().Split(' ').Last();
            if (surname == keyPart)
            {
                results.AddRange(kvp.Value);
            }
        }
        return results;
    }
    
    public void RemoveDirectory(string firstName, string lastName)
    {
        if( string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("First name and last name cannot be null or empty.");
        
        var key = GetKey(firstName, lastName);
        _phoneDictionary.Remove(key);
    }
    
    public IEnumerable<SavedEntity> SearchByNameAndAddress(string firstName, string lastName, string address)
    {
        if( string.IsNullOrWhiteSpace(firstName) 
            || string.IsNullOrWhiteSpace(lastName) 
            || string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("First name and last name cannot be null or empty.");
        
        var key = GetKey(firstName, lastName);
        if (_phoneDictionary.ContainsKey(key))
        {
            return _phoneDictionary[key].Where(e => e.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
        }
        return Enumerable.Empty<SavedEntity>();
    }
}