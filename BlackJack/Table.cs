using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Table
    {
        public Table(Deck deck, List<Player> players)
        {
            Dealer = new Player("Dealer");
            Deck = deck;
            Players = players;
        }
        public List<Player> Players { get; set; }
        public Player Dealer { get; set; }
        public Deck Deck { get; set; }

        public void DealInitialHand()
        {
            DealCard();
            DealCard();
            CheckForBlackJack();
        }

        public void Play()
        {

        }

        private void DealCard()
        {
            foreach (Player player in Players)
            {
                player.Hand.Add(Deck.DealNextCard());
            }
            Dealer.Hand.Add(Deck.DealNextCard());
        }

        private void CheckForBlackJack()
        {
            foreach (Player player in Players)
            {
                List<Card> hand = player.Hand;
                if (hand.Any(c => c.IsAce()) && hand.Any(c => c.IsTenCard()))
                {
                    player.Status = Status.Won;
                    Console.WriteLine("Woo! Player {0} wins!!", player.Name);
                }
            }
            List<Card> dealerHand = Dealer.Hand;
            if (dealerHand.Any(c => c.IsAce()) && dealerHand.Any(c => c.IsTenCard()))
            {
                Dealer.Status = Status.Won;
                SetPlayersToLose();
                Console.WriteLine("Oh no! Dealer wins!!", Dealer.Name);
            }

        }

        private void SetPlayersToLose()
        {
            foreach (Player player in Players)
            {
                if (player.Status != Status.Won)
                {
                    player.Status = Status.Lost;
                }
                else
                {
                    player.Status = Status.Push;
                }
            }
        }

        private void PlayerPlay(Player player)
        {
            
        }

        private Card NextCard()
        {
            return Deck.DealNextCard();
        }

    }
}
