using UI;
using System.Windows.Threading;

namespace Schach.Console;

internal class Program
{
    static MainWindow? mw = null;
    static ConsoleCommunicator? comm = null;

    [STAThread]
    static void Main(string[] args)
    {
        comm = new(true);
        Thread windowThread = new(new ThreadStart(OpenMainWindow));
        windowThread.SetApartmentState(ApartmentState.STA);
        windowThread.IsBackground = true;
        windowThread.Name = "Window";
        windowThread.Start();

        Thread.CurrentThread.Name = "Console";

        System.Console.WriteLine("Waiting for app to start");
        while (mw is null)
        {
            Thread.Sleep(50);
        }

        Iterator(mw).Wait();
    }

    static void OpenMainWindow()
    {
        MainWindow mainWindow = new(comm!);
        mainWindow.Show();
        mw = mainWindow;
        Dispatcher.Run();
    }

    static async Task Iterator(MainWindow window)
    {
        System.Console.WriteLine("Started Console");
        while (true)
        {
            var list = window.GetToPrint();
            foreach (var toPrint in list)
            {
                System.Console.WriteLine(toPrint);
            }

            if (window.IsClosed)
                break;

            await Task.Delay(100);
        }
        System.Console.WriteLine("Closing Console");
    }
}