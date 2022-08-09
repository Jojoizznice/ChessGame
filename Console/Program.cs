using UI;

namespace Schach.Console;

internal class Program
{
    [STAThread]
    static void Main()
    {
        System.Console.WriteLine("Starting");

        ConsoleCommunicator communicator = new(true);
        MainWindow mainWindow = new(communicator);
        mainWindow.ShowDialog();
    }
}