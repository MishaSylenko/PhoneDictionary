using System;
using PhoneDictionary.Helper;
using PhoneDictionary.Model;

class Program
{
    static void Main()
    {
        var phoneBook = new PhoneDictionary.Helper.PhoneDictionary();

        // Create entries
        var john = new SavedEntity("John", "Doe", "123 Main St");
        john.AddPhoneNumber("123-456-7890");
        john.AddPhoneNumber("987-654-3210");

        var jane = new SavedEntity("Jane", "Doe", "456 Elm St");
        jane.AddPhoneNumber("555-123-4567");

        var alice = new SavedEntity("Alice", "Smith", "789 Oak St");
        alice.AddPhoneNumber("222-333-4444");

        // Add entries to dictionary
        phoneBook.AddEntry(john);
        phoneBook.AddEntry(jane);
        phoneBook.AddEntry(alice);

        Console.WriteLine("=== All Entries Added ===");

        // Search full name
        Console.WriteLine("\nSearch John Doe:");
        foreach (var entry in phoneBook.Search("John", "Doe"))
            Console.WriteLine(entry);

        // Search by first name
        Console.WriteLine("\nSearch by first name 'Jane':");
        foreach (var entry in phoneBook.SearchByFirstName("Jane"))
            Console.WriteLine(entry);

        // Search by last name
        Console.WriteLine("\nSearch by last name 'Doe':");
        foreach (var entry in phoneBook.SearchByLastName("Doe"))
            Console.WriteLine(entry);

        // Search by name and address
        Console.WriteLine("\nSearch John Doe at '123 Main St':");
        foreach (var entry in phoneBook.SearchByNameAndAddress("John", "Doe", "123 Main St"))
            Console.WriteLine(entry);

        // Remove a phone number
        Console.WriteLine("\nRemoving one phone number from John Doe...");
        john.RemovePhoneNumber("987-654-3210");
        foreach (var entry in phoneBook.Search("John", "Doe"))
            Console.WriteLine(entry);

        // Remove entry completely
        Console.WriteLine("\nRemoving Alice Smith from directory...");
        phoneBook.RemoveEntry(alice);
        foreach (var entry in phoneBook.Search("Alice", "Smith"))
            Console.WriteLine(entry);

        // Remove full directory of Doe family
        Console.WriteLine("\nRemoving directory of Jane Doe...");
        phoneBook.RemoveDirectory("Jane", "Doe");
        foreach (var entry in phoneBook.SearchByLastName("Doe"))
            Console.WriteLine(entry);

        Console.WriteLine("\n=== Program Finished ===");
    }
}
