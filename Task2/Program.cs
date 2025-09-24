using Task2.Comparer;
using Task2.Model;

var people = new List<Person>
{
    new Person(3, "John", "Smith"),
    new Person(1, "Alice", "Brown"),
    new Person(2, "Alice", "Adams"),
    new Person(10, "Alice", "Brown")
};

bool anotherVersion = true;

var (cmp, eq) = PersonComparers.Get(anotherVersion);

people.Sort();
Console.WriteLine("Sorted by Id:");
people.ForEach(Console.WriteLine);
Console.WriteLine(" ");

people.Sort(cmp);
Console.WriteLine("Sorted by Id:");
people.ForEach(Console.WriteLine);

var set = new HashSet<Person>(eq);
foreach (var p in people) set.Add(p);

foreach (var p in set) Console.WriteLine(p);