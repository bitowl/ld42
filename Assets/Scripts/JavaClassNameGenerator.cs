using UnityEngine;

public class JavaClassNameGenerator {
    public static string[] firstNames = {
        "Car",
        "Dog",
        "Cat",
        "Human",
        "Text",
        "Mail",
        "User",
        "Account",
        "Project",
        "Document",
        "Block",
        "Display",
        "Element",
        "Transaction",
        "Glass",
        "Wire"
    };

    public static string[] suffixes = {
        "Factory",
        "Provider",
        "Interface",
        "Builder",
        "Reference",
        "Proxy",
        "Contract",
        "Strategy",
        "Listener",
        "Manager",
        "Generator",
        "Base",
        "Event"
    };

    public static string GenerateClassName(int maxSuffixCount) {
        string className = firstNames[Random.Range(0, firstNames.Length)];
        
        var suffixCount = Random.Range(0, maxSuffixCount + 1);
        for (int i = 0; i < suffixCount; i++)
        {
            className += suffixes[Random.Range(0, suffixes.Length)];
        }
        return className;
    }
}