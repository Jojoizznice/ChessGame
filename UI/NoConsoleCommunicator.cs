using UI.Models;
using System;

namespace UI;

internal class NoConsoleCommunicator : IConsoleCommunicatorModel
{
    public bool IsClosed { get; set; }
    public bool IsConsole => false;

    public void AddToPrint(string value) { }

    public void AddToPrint(object? value) { }

    public string?[] GetToPrint()
    {
        throw new InvalidOperationException("This IConsoleCommunicatorModel is write-only");
    }
}
