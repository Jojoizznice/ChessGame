using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UI;

partial class MainWindow
{
    void DrawBoard() 
    {
        SolidColorBrush dark = new(Color.FromArgb(0xFF, 0x5F, 0x45, 0x45));
        SolidColorBrush bright = new(Color.FromArgb(0xFF, 0xEE, 0xC8, 0x97));

        for (int i = 0; i < 64; i++)
        {     
            Rectangle rect = new()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Stroke = GetBrightDark(i) ? dark : bright,
                Fill = GetBrightDark(i) ? dark : bright,
                Visibility = Visibility.Visible,

            };

            ChessGrid.Children.Add(rect);
            Grid.SetColumn(rect, ( i / 8 ) + 2);
            Grid.SetRow(rect, ( i % 8 ) + 2);
        }
    }

    static bool GetBrightDark(int position)
    {
        int row = position / 8;
        int modifier = row % 2;
        int column = position % 8;

        return (column + modifier) % 2 == 0;
    }
}
