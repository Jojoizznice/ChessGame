using UI.Models;

namespace Schach.Console;

public class ConsoleCommunicator(bool isConsole) : IConsoleCommunicatorModel
{
    public bool IsClosed { get; set; }
    public bool IsConsole { get; } = isConsole;

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
            return;
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
