using System.Diagnostics;
using System.Timers;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double heightCache;
        private double widthCache;

        public MainWindow()
        {
            InitializeComponent();
            SizeChanged += OnResize;
            heightCache = Height;
            widthCache = Width;
        }



        private void OnResize(object sender, SizeChangedEventArgs e)
        {

        }

        private void Counter()
        {

        }

        private void TimerFinished(object? sender, ElapsedEventArgs e)
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
