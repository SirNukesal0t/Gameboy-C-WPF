using System;
using System.Collections.Generic;
using System.Windows;

namespace Gameboy
{
    public partial class Blackjack : Window
    {
        private List<int> playerHand = new List<int>();
        private List<int> dealerHand = new List<int>();
        private Random rand = new Random();

        public Blackjack()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            playerHand.Add(DrawCard());
            UpdateHands();
            if (GetHandValue(playerHand) > 21)
            {
                txtResult.Text = "Player busts! Dealer wins!";
                EndGame();
            }
        }

        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            while (GetHandValue(dealerHand) < 17)
            {
                dealerHand.Add(DrawCard());
            }
            UpdateHands();
            int playerValue = GetHandValue(playerHand);
            int dealerValue = GetHandValue(dealerHand);
            if (dealerValue > 21 || playerValue > dealerValue)
            {
                txtResult.Text = "Player wins!";
            }
            else if (playerValue < dealerValue)
            {
                txtResult.Text = "Dealer wins!";
            }
            else
            {
                txtResult.Text = "It's a tie!";
            }
            EndGame();
        }

        private void InitializeGame()
        {
            playerHand.Clear();
            dealerHand.Clear();
            txtResult.Text = "";
            playerHand.Add(DrawCard());
            playerHand.Add(DrawCard());
            dealerHand.Add(DrawCard());
            dealerHand.Add(DrawCard());
            UpdateHands();
        }

        private int DrawCard()
        {
            return rand.Next(1, 12); // Kartenwerte von 1 bis 11 (Ass kann 1 oder 11 sein)
        }

        private void UpdateHands()
        {
            txtPlayerHand.Text = "Player Hand: " + string.Join(", ", playerHand) + " (Value: " + GetHandValue(playerHand) + ")";
            txtDealerHand.Text = "Dealer Hand: " + string.Join(", ", dealerHand) + " (Value: " + GetHandValue(dealerHand) + ")";
        }

        private int GetHandValue(List<int> hand)
        {
            int value = 0;
            int aceCount = 0;
            foreach (int card in hand)
            {
                if (card == 1)
                {
                    aceCount++;
                    value += 11;
                }
                else
                {
                    value += card;
                }
            }
            while (value > 21 && aceCount > 0)
            {
                value -= 10;
                aceCount--;
            }
            return value;
        }

        private void EndGame()
        {
            btnHit.IsEnabled = false;
            btnStand.IsEnabled = false;
        }
    }
}
