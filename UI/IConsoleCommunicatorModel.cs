namespace UI.Models;

public interface IConsoleCommunicatorModel
{
    public bool IsClosed { get; set; }
    public bool IsConsole { get; }
    public void AddToPrint(string value);
    public void AddToPrint(object? value);
    public string?[] GetToPrint();
}
