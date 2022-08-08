namespace Console;

internal class Program
{
    [STAThread]
    static void Main()
    {
        System.Console.WriteLine("Hello, World!"); 
        UI.MainWindow mainWindow = new();
        mainWindow.ShowDialog();
    }
}