using System;
using System.Collections.Generic;

public class ElementalWords
{
    private readonly Dictionary<string, string> _elements;
    private readonly Dictionary<int, List<List<string>>> _memo;

    public ElementalWords(Dictionary<string, string> elements)
    {
        _elements = elements;
        _memo = new Dictionary<int, List<List<string>>>();
    }

    public List<List<string>> ElementalForms(string word)
    {
        return FindElementalForms(word, 0);
    }

    private List<List<string>> FindElementalForms(string word, int index)
    {
        if (index == word.Length)
            return new List<List<string>> { new List<string>() };

        if (_memo.TryGetValue(index, out var cachedResults))
            return cachedResults;

        var results = new List<List<string>>();

        for (int len = 1; len <= 3 && index + len <= word.Length; ++len)
        {
            var prefix = word.Substring(index, len);

            if (_elements.ContainsKey(prefix.ToLower()))
            {
                var restResults = FindElementalForms(word, index + len);
                foreach (var restResult in restResults)
                {
                    var newPath = new List<string> { $"{_elements[prefix]} ({prefix.ToUpper()})" };
                    newPath.AddRange(restResult);
                    results.Add(newPath);
                }
            }
        }

        _memo[index] = results;
        return results;
    }
}

class Program
{
    static void Main()
    {
        var elements = new Dictionary<string, string>
        {
            {"h", "Hydrogen"}, {"he", "Helium"}, {"li", "Lithium"},
            {"na", "Sodium"}, {"n", "Nitrogen"}, {"ac", "Actinium"},
            {"k", "Potassium"}, {"s", "Sulfur"}, {"sn", "Tin"},
             
            // Include temporary symbols for elements 113, 115, 117, and 118 as needed
        };

        var solver = new ElementalWords(elements);
        var word = "snack";
        var forms = solver.ElementalForms(word);

        Console.WriteLine($"Word: {word}");
        foreach (var form in forms)
        {
            Console.WriteLine($"[{string.Join(", ", form)}]");
        }
    }
}
