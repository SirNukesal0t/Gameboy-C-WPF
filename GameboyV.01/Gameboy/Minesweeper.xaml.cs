using System;
using System.Windows;
using System.Windows.Controls;

namespace Gameboy
{
    public partial class Minesweeper : Window
    {
        // Konstanten für die Anzahl der Zeilen, Spalten und Minen
        private const int Rows = 10;
        private const int Columns = 10;
        private const int Mines = 10;

        // Arrays zur Speicherung der Buttons und Minenpositionen
        private Button[,] buttons = new Button[Rows, Columns];
        private bool[,] mines = new bool[Rows, Columns];

        public Minesweeper()
        {
            InitializeComponent();
            InitializeGame(); // Initialisiert das Spiel beim Start
        }

        // Ereignishandler für den "Neues Spiel"-Button
        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame(); // Startet ein neues Spiel
        }

        // Initialisiert das Spielfeld und platziert die Minen
        private void InitializeGame()
        {
            // Löscht alle vorhandenen Kinder und Definitionen im Grid
            gameGrid.Children.Clear();
            gameGrid.RowDefinitions.Clear();
            gameGrid.ColumnDefinitions.Clear();

            // Fügt Zeilen- und Spaltendefinitionen zum Grid hinzu
            for (int i = 0; i < Rows; i++)
            {
                gameGrid.RowDefinitions.Add(new RowDefinition());
                gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Initialisiert das Minen-Array und platziert zufällig Minen
            Random rand = new Random();
            mines = new bool[Rows, Columns];
            for (int i = 0; i < Mines; i++)
            {
                int row, col;
                do
                {
                    row = rand.Next(Rows);
                    col = rand.Next(Columns);
                } while (mines[row, col]); // Verhindert doppelte Platzierung von Minen
                mines[row, col] = true;
            }

            // Erstellt Buttons für jedes Feld im Grid
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Button button = new Button();
                    button.Click += Button_Click; // Fügt den Click-Ereignishandler hinzu
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    gameGrid.Children.Add(button);
                    buttons[i, j] = button; // Speichert den Button im Array
                }
            }
        }

        // Ereignishandler für Klicks auf die Spielfeld-Buttons
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (mines[row, col])
            {
                // Wenn das Feld eine Mine enthält, wird "M" angezeigt und das Spiel neu gestartet
                button.Content = "M";
                MessageBox.Show("Game Over!");
                InitializeGame();
            }
            else
            {
                // Wenn das Feld keine Mine enthält, wird die Anzahl der benachbarten Minen angezeigt
                int mineCount = CountMines(row, col);
                button.Content = mineCount.ToString();
                button.IsEnabled = false; // Deaktiviert den Button
            }
        }

        // Zählt die Minen in den benachbarten Feldern
        private int CountMines(int row, int col)
        {
            int count = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    // Überprüft, ob die benachbarten Felder innerhalb der Grenzen liegen und eine Mine enthalten
                    if (i >= 0 && i < Rows && j >= 0 && j < Columns && mines[i, j])
                    {
                        count++;
                    }
                }
            }
            return count; // Gibt die Anzahl der benachbarten Minen zurück
        }
    }
}
