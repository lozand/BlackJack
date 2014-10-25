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
            TableStatus = TableStatus.Open;
        }
        public List<Player> Players { get; set; }
        public Player Dealer { get; set; }
        public Deck Deck { get; set; }
        public TableStatus TableStatus { get; set; }

        /// <summary>
        /// Deals this initial hand for all players. This consists of 2 cards for each player and 2 for the dealer.
        /// Notes: One of the dealer's cards should be face down (the second one)
        ///         Also, is there a cleaner way of doing this instead of calling DealCard twice? Is a loop really necessary here?
        /// </summary>
        public void DealInitialHand()
        {
            TableStatus = TableStatus.Dealing;
            DealCard(true);
            DealCard(false);
            CheckForBlackJack();
        }

        public void Play()
        {
            TableStatus = TableStatus.InPlay;
        }

        /// <summary>
        /// Deals one card to each player and then one to the dealer.
        /// This should only be called in the initial card deal.
        /// </summary>
        private void DealCard(bool ShowDealerCard)
        {
            foreach (Player player in Players)
            {
                Card card = Deck.DealNextCard();
                Console.WriteLine("Player {0} got", player.Name);
                card.DisplayCard();
                player.Hand.Add(card);
            }
            Card dealerCard = Deck.DealNextCard();
            Dealer.Hand.Add(dealerCard);
            if (ShowDealerCard)
            {
                Console.WriteLine("Dealer got");
                dealerCard.DisplayCard();
            }
            
        }

        /// <summary>
        /// After all cards have initially been dealt, we need to see who has blackjack.
        /// Notes: In the future, there would be an instant payout here for people who got blackjack.
        /// If the dealer gets blackjack, then all people who didn't instantly lose and those who won, now push.
        /// </summary>
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

        /// <summary>
        /// Sets all players to lose unless they have won. Then they push.
        /// </summary>
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
            TableStatus = TableStatus.Finished;
        }

        /// <summary>
        /// The player should now have the option of hitting, staying, splitting or doubling down. 
        /// This function should run through the motions of the actual game of blackjack
        /// </summary>
        /// <param name="player"></param>
        private void PlayerPlay(Player player)
        {
            
        }

        /// <summary>
        /// Deals the next card and assigns it out.
        /// </summary>
        /// <returns></returns>
        private Card NextCard()
        {
            return Deck.DealNextCard();
        }

    }
}
