using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI;

partial class MainWindow
{
    void SetUpRotate()
    {
        RotateButton.MouseLeftButtonDown += RotateBoard;
        RotateButtonText.MouseLeftButtonDown += RotateBoard;
    }

    private void RotateBoard(object sender, MouseButtonEventArgs e)
    {
        _communicator.AddToPrint(sender.GetType());
        
        for (int i = 0; i < ChessGrid.Children.Count; i++)
        {
            if (ChessGrid.Children[i] is not FrameworkElement item || item.Name.StartsWith("Schach_"))
            {
                continue;
            }

            int row = Grid.GetRow(item);
            int newRow = 7 - (row - 2);

            Grid.SetRow(item, newRow + 2);
        }
    }
}
