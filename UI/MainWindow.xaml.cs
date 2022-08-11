using System;
using System.Windows;
using UI.Models;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IConsoleCommunicatorModel _communicator;
        public string?[] GetToPrint()
        {
            string?[] resultFromComm = _communicator.GetToPrint();
            string?[] resultToReturn = new string[resultFromComm.Length];
            Array.Copy(resultFromComm, resultToReturn, resultFromComm.Length);
            return resultToReturn;
        }
        public bool IsClosed
        {
            get => _communicator.IsClosed;
        }

        private int counter;
        private double heightCache;
        private double widthCache;

        (double height, double width) GetValues()
        {
            return (Height, Width);
        }

        /// <summary>
        /// Initializes the MainWindow
        /// </summary>
        /// <param name="communicator">The ConsoleCommunicator used to communicate with console</param>
        public MainWindow(IConsoleCommunicatorModel communicator)
        {
            _communicator = communicator;
            try
            {
                InitializeComponent();
                heightCache = Height;
                widthCache = Width;
                _communicator.AddToPrint("Started successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "EXCEPTION", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
