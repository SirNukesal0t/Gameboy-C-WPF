using System.Windows;

namespace Gameboy
{
    public partial class StartMenu : Window
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void BtnTicTacToe_Click(object sender, RoutedEventArgs e)
        {
            TicTacToe ticTacToeWindow = new TicTacToe();
            ticTacToeWindow.Show();
            this.Close();
        }

        private void BtnMinesweeper_Click(object sender, RoutedEventArgs e)
        {
            Minesweeper minesweeperWindow = new Minesweeper();
            minesweeperWindow.Show();
            this.Close();
        }

        private void BtnBlackjack_Click(object sender, RoutedEventArgs e)
        {
            Blackjack blackjackWindow = new Blackjack();
            blackjackWindow.Show();
            this.Close();
        }
    }
}
