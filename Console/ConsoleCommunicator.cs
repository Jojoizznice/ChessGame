using UI.Models;
using System.Collections.Concurrent;

namespace Schach.Console;

public class ConsoleCommunicator : IConsoleCommunicatorModel
{
    public bool IsClosed { get; set; }
    public bool IsConsole { get; }

    public ConsoleCommunicator(bool isConsole)
    {
        IsConsole = isConsole;
    }

    readonly List<string?> strings = new();
    
    public void AddToPrint(string value)
    {
        strings.Add(value);
    }

    public void AddToPrint(object? value)
    {
        if (value is null)
        {
            strings.Add("");
        }

        strings.Add(value!.ToString());
    }

    public string?[] GetToPrint()
    {   
        var toReturn = strings.ToArray();
        strings.Clear();
        return toReturn;
    }
}
