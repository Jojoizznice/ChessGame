using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UI.Models;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IConsoleCommunicatorModel _communicator;
        
        private int counter;
        private bool counterIsRunning;
        private double heightCache;
        private double widthCache;

        /// <summary>
        /// Initializes the MainWindow
        /// </summary>
        /// <param name="communicator">The ConsoleCommunicator used to communicate with console</param>
        public MainWindow(IConsoleCommunicatorModel communicator)
        {
            InitializeComponent();
            SizeChanged += OnResize;
            heightCache = Height;
            widthCache = Width;
            _communicator = communicator;
        }


        private void OnResize(object sender, SizeChangedEventArgs e)
        {
            counter = 100;
            if (!counterIsRunning)
            {
                Task.Run(() => Counter());
                counterIsRunning = true;
            }
        }

        private void Counter()
        {
            counter -= 1;
            Thread.Sleep(1);
            if (counter < 0)
            {
                TimerFinished();
            }
        }

        private void TimerFinished()
        {
            bool heightBigger = Height > heightCache;
            bool widthBigger = Width > widthCache;
            bool heightBiggerWidth = Height > Width;

            heightCache = Height;
            widthCache = Width;

            if (!heightBigger || !widthBigger)
            {
                if (heightBiggerWidth)
                {
                    Width = Height;
                    return;
                }

                Height = Width;
            }
        }
    }
}
