
﻿using MyList;

class Program
{
    static void Main()
    {
        var list = new MyList<int>();

        Console.WriteLine("Adding numbers 1 to 10...");
        for (int i = 1; i <= 10; i++)
        {
            list.Add(i);
        }

        PrintList(list);

        Console.WriteLine("\nInsert 99 at index 5:");
        list.Insert(5, 99);
        PrintList(list);

        Console.WriteLine("\nRemove element 99:");
        list.Remove(99);
        PrintList(list);

        Console.WriteLine("\nRemove element at index 0:");
        list.RemoveAt(0);
        PrintList(list);

        Console.WriteLine("\nIndexOf(7): " + list.IndexOf(7));
        Console.WriteLine("Contains(42): " + list.Contains(42));
        Console.WriteLine("Contains(9): " + list.Contains(9));

        Console.WriteLine("\nAccess with indexer: list[3] = " + list[3]);
        list[3] = 123;
        Console.WriteLine("After setting list[3] = 123:");
        PrintList(list);

        Console.WriteLine("\nClearing list...");
        list.Clear();
        Console.WriteLine("Count after clear: " + list.Count);
    }

    static void PrintList(MyList<int> list)
    {
        Console.WriteLine("List contents (Count=" + list.Count + "):");
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}
