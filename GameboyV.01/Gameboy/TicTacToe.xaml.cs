using System;
using System.Windows;
using System.Windows.Controls;

namespace Gameboy
{
    public partial class TicTacToe : Window
    {
        private bool isSinglePlayer = true;
        private char[,] board = new char[3, 3];
        private char currentPlayer = 'X';

        public TicTacToe()
        {
            InitializeComponent();
            ResetBoard();
        }

        private void BtnSinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            isSinglePlayer = true;
            ResetBoard();
        }

        private void BtnMultiplayer_Click(object sender, RoutedEventArgs e)
        {
            isSinglePlayer = false;
            ResetBoard();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (board[row, col] == '\0')
            {
                board[row, col] = currentPlayer;
                button.Content = currentPlayer;

                if (CheckWin())
                {
                    MessageBox.Show($"{currentPlayer} gewinnt!");
                    ResetBoard();
                }
                else if (IsBoardFull())
                {
                    MessageBox.Show("Unentschieden!");
                    ResetBoard();
                }
                else
                {
                    currentPlayer = currentPlayer == 'X' ? 'O' : 'X';

                    if (isSinglePlayer && currentPlayer == 'O')
                    {
                        MakeAIMove();
                    }
                }
            }
        }

        private void MakeAIMove()
        {
            // Einfache KI-Logik: Wählt das erste freie Feld
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '\0')
                    {
                        board[i, j] = 'O';
                        foreach (Button button in gameGrid.Children)
                        {
                            if (Grid.GetRow(button) == i && Grid.GetColumn(button) == j)
                            {
                                button.Content = 'O';
                                break;
                            }
                        }

                        if (CheckWin())
                        {
                            MessageBox.Show("O gewinnt!");
                            ResetBoard();
                        }
                        else if (IsBoardFull())
                        {
                            MessageBox.Show("Unentschieden!");
                            ResetBoard();
                        }
                        else
                        {
                            currentPlayer = 'X';
                        }

                        return;
                    }
                }
            }
        }

        private bool CheckWin()
        {
            // Überprüft alle Gewinnbedingungen
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != '\0' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return true;
                if (board[0, i] != '\0' && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return true;
            }
            if (board[0, 0] != '\0' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return true;
            if (board[0, 2] != '\0' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (char cell in board)
            {
                if (cell == '\0')
                    return false;
            }
            return true;
        }

        private void ResetBoard()
        {
            board = new char[3, 3];
            currentPlayer = 'X';
            foreach (Button button in gameGrid.Children)
            {
                button.Content = null;
            }
        }
    }
}
