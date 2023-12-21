using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace UI;

partial class MainWindow
{
    readonly Dictionary<string, char> pieceDictionary =
        JsonSerializer.Deserialize<Dictionary<string, char>>(
        new MemoryStream(Properties.Resources.pieceDictionary));

    Dictionary<char, byte[]> pieceStreams = new(12);

    private Dictionary<char, byte[]> InitPieceDictionary()
    {
        var resMan = Properties.Resources.ResourceManager;
        var resCul = Properties.Resources.Culture ?? CultureInfo.CurrentUICulture;

        using var resSet = resMan.GetResourceSet(resCul, true, true)!;

        var resEnum = resSet.GetEnumerator();
        Dictionary<char, byte[]> pieces = [];
        while (resEnum.MoveNext())
        {
            string key = (string)resEnum.Key;
            if (key.StartsWith("Schach_"))
            {
                string keymod = key.Replace("Schach_", "");
                _ = pieceDictionary.TryGetValue(keymod, out char p);

                byte[] text = (byte[])resSet.GetObject(key, true)!;

                pieces.Add(p, text);
            }
        }
        
        return pieces;
    }

    bool piecesInitialized = false;
    void DrawPieces()
    {
        if (piecesInitialized) ClearPieces();
        
        pieceStreams = InitPieceDictionary();
        piecesInitialized = true;

        string fenString = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        ReadOnlySpan<char> fen = fenString;
        int counter = 0;

        for (int i = 0; i < 8; i++)
        {
            while (true)
            {
                if (counter >= 8 || fen[..1] == "/")
                {
                    fen = fen[1..];
                    counter = 0;
                    break;
                }

                if (int.TryParse(fen[..1], out int number))
                {
                    for (int j = 0; j < number; j++)
                        counter++;

                    fen = fen[1..];
                    continue;
                }

                PrintPiece(fen[..1], counter, i);
                counter++;
                fen = fen[1..];
            }
        }
    }

    private void ClearPieces()
    {
        for (int i = 0; i < ChessGrid.Children.Count; i++)
        {
            if (ChessGrid.Children[0] is not FrameworkElement fe)
            {
                return;
            }

            if (fe.Name.StartsWith('s'))
            {
                ChessGrid.Children.Remove(fe);
            }
        }
    }

    static char RankIntToChar(int rank)
    {
        return rank switch
        {
            0 => 'a',
            1 => 'b',
            2 => 'c',
            3 => 'd',
            4 => 'e',
            5 => 'f',
            6 => 'g',
            7 => 'h',
            _ => throw new UnreachableException("rank was " + rank.ToString())
        };
    }

    private void PrintPiece(ReadOnlySpan<char> piece, int rank, int file)
    {
        char pieceChar = piece[0];

        string pieceName = 's' + RankIntToChar(rank).ToString() + pieceChar;
        pieceStreams.TryGetValue(piece[0], out byte[]? pieceStream);

        BitmapImage bmi = new();
        bmi.BeginInit();
        bmi.StreamSource = new MemoryStream(pieceStream ?? throw new NullReferenceException("stream fix"));
        bmi.EndInit();

        Rectangle rect = new()
        {
            Name = pieceName,
            Fill = new ImageBrush(bmi),
            Visibility = Visibility.Visible,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };

        ChessGrid.Children.Add(rect);
        Grid.SetColumn(rect, rank + 2);
        Grid.SetRow(rect, file + 2);
    }


    static char MatchPieceDescription(string piece)
    {
        return piece switch
        {
            "König" => 'K',
            "König_Schwarz" => 'k',
            "Dame" => 'Q',
            "Dame_Schwarz" => 'q',
            "Turm" => 'R',
            "Turm_Schwarz" => 'r',
            "Läufer" => 'B',
            "Läufer_Schwarz" => 'b',
            "Springer" => 'N',
            "Springer_Schwarz" => 'n',
            "Bauer" => 'P',
            "Bauer_Schwarz" => 'p',
            _ => throw new UnreachableException("piece was " + piece)
        };
    }
}
