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
            Dealer = new Player("Dealer", 0);
            Deck = deck;
            Players = players;
            TableStatus = TableStatus.Open;
            ShuffleIfNecessary();
            SetBets();
            DealInitialHand();
            Play();
            EndGame();
            Results();
            ClearHand();
        }
        public List<Player> Players { get; set; }
        public Player Dealer { get; set; }
        public Deck Deck { get; set; }
        public TableStatus TableStatus { get; set; }

        private const int DealerStand = 17;
        private const int CardsNecessaryToPlay = 20;
        private Display show = new Display();

        #region Public Methods
        public void SetBets()
        {
            Deck.ProbabilityOfGettingBlackJack();
            show.SkipLine();
            foreach (Player player in Players)
            {
                double oldBet = player.Bet;
                player.ClearBet();
                show.PlayerBet(player.Name);
                string input = show.Read();
                if (input == "0")
                {
                    player.Status = Status.NotPlaying;
                }
                else if (input != "rebet")
                {
                    player.BetCash(Int32.Parse(input));
                }
                else
                {
                    player.BetCash(oldBet);
                }
            }
        }

        /// <summary>
        /// Deals this initial hand for all players. This consists of 2 cards for each player and 2 for the dealer.
        /// Notes: One of the dealer's cards should be face down (the second one)
        ///         Also, is there a cleaner way of doing this instead of calling DealCard twice? Is a loop really necessary here?
        /// </summary>
        public void DealInitialHand()
        {
            TableStatus = TableStatus.Dealing;
            DealCard(false);
            DealCard(false);
            CheckForBlackJack();
            if (TableStatus != TableStatus.Finished)
            {
                ShowPlayerCards();
                ShowDealerCard();
            }
        }

        public void Play()
        {
            TableStatus = TableStatus.InPlay;
            foreach (Player player in Players)
            {
                if (player.Status != Status.Won && player.Status != Status.BlackJack && player.Status != Status.Lost && player.Status != Status.NotPlaying)
                {
                    PlayerPlay(player);
                }
            }

            ShowDealerCard(false);

            if (!Players.All(p => p.Status == Status.Lost || p.Status == Status.Bust || p.Status == Status.NotPlaying))
            {
                while (Dealer.Total() < DealerStand)
                {
                    Hit(Dealer);
                }
            }
        }

        public void EndGame()
        {
            // If the dealer busts, then everyone who didn't bust wins.
            if (Dealer.Status == Status.Lost || Dealer.Status == Status.Bust)
            {
                foreach (Player player in Players)
                {
                    if (player.Status != Status.Lost && player.Status != Status.Bust)
                    {
                        player.Status = Status.Won;
                        player.AwardBet();
                    }
                }
            }
            else // If the dealer didn't bust, then only those who beat the dealer win.
            {
                foreach (Player player in Players.Where(p=>p.Status != Status.Won && p.Status != Status.Lost && p.Status != Status.Bust && p.Status != Status.BlackJack))
                {
                    int dealerTotal = Dealer.Total();
                    int playerTotal = player.Total();
                    if (playerTotal > dealerTotal)
                    {
                        player.Status = Status.Won;
                        player.AwardBet();
                    }
                    else if (playerTotal == dealerTotal)
                    {
                        player.Status = Status.Push;
                        player.PushBet();
                    }
                    else
                    {
                        player.Status = Status.Lost;
                    }
                }
            }
        }

        public void Results()
        {
            UpdateTable(false);
            if (Dealer.Status == Status.Lost)
            {
                show.PlayerResult(Dealer.Name, "Lost");
            }
        }

        public void ClearHand()
        {
            foreach (Player player in Players)
            {
                player.Hand = new List<Card>();
                player.Status = Status.InPlay;
            }
        }
        #endregion

        #region Private Methods
        private string GetResultString(Status status)
        {            
            Console.ResetColor();
            string result = "";
            switch(status)
                {
                    case Status.Lost:
                        result = "Lost";
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case Status.BlackJack:
                    case Status.Won:
                        result = "Won";
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case Status.Bust:
                        result = "Bust";
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case Status.Push:
                        result = "Tied";
                        break;
                    default:
                        result = "N/A";
                        break;
                }

            return result;
        }

        /// <summary>
        /// Deals one card to each player and then one to the dealer.
        /// This should only be called in the initial card deal.
        /// </summary>
        private void DealCard(bool ShowDealerCard)
        {
            foreach (Player player in Players)
            {
                if (player.Status != Status.NotPlaying)
                {
                    Card card = Deck.DealNextCard();
                    player.Hand.Add(card);
                    UpdateTable(true, true);
                    show.Wait();
                }
            }
            Card dealerCard = Deck.DealNextCard();
            Dealer.Hand.Add(dealerCard);
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
                if (player.Status != Status.NotPlaying)
                {
                    List<Card> hand = player.Hand;
                    if (hand.Any(c => c.IsAce()) && hand.Any(c => c.IsTenCard()))
                    {
                        player.Status = Status.BlackJack;
                        player.AwardBet(true);
                    }
                    else
                    {
                        player.Status = Status.InPlay;
                    }
                }
            }
            List<Card> dealerHand = Dealer.Hand;
            if (dealerHand.Any(c => c.IsAce()) && dealerHand.Any(c => c.IsTenCard()))
            {
                Dealer.Status = Status.BlackJack;
                SetPlayersToLose();
            }

        }

        /// <summary>
        /// Sets all players to lose unless they have won. Then they push.
        /// </summary>
        private void SetPlayersToLose()
        {
            foreach (Player player in Players)
            {
                if (player.Status != Status.NotPlaying)
                {
                    if (player.Status != Status.BlackJack)
                    {
                        player.Status = Status.Lost;
                    }
                    else
                    {
                        player.Status = Status.Push;
                    }
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
            int total = player.Total();

            string option = "Begin";
            string prob = "";
            string probOption = "";
            while (total < 21 && option != "Stand" && option != "Double" && player.Status == Status.InPlay)
            {
                UpdateTable(true, true);
                if (prob != "") { show.ProbabilityOfCards(Deck.ProbabilityOfCard(prob)); prob = ""; }
                show.PlayerOptions(player.Name);
                option = show.Read();
                if (option.IndexOf(' ') != -1) { probOption = option; option = option.Substring(0, option.IndexOf(' ')); }
                show.Clear();
                switch (option.ToLower())
                {
                    case "hit":
                        Hit(player);
                        break;
                    case "double":
                        // If the double was not successful, then you should go through the loop again
                        // DAL: TODO: Should find a way to relay a "fail" to the user since UpdateTable() 
                        // clears all messages. 
                        if (!Double(player)) { option = "Failed"; } 
                        break;
                    case "split":
                        Split(player);
                        break;
                    case "prob":
                        prob = CheckProbability(probOption);
                        break;
                    case "stand":
                    default:
                        option = "Stand";
                        break;
                }
            }
        }

        private void ShowPlayerCards()
        {
            foreach (Player player in Players)
            {
                player.ShowPlayerCards();
            }
        }

        private void ShowDealerCard(bool hideOneCard = true)
        {
            Console.WriteLine("Dealer has:");
            Dealer.ShowPlayerCards(hideOneCard);
        }

        private void ShuffleIfNecessary()
        {
            if (Deck.GetCardsRemaining() < CardsNecessaryToPlay)
            {
                Deck.Reset(Deck.NumberOfDecks);
            }
        }

        /// <summary>
        /// Deals the next card and assigns it out.
        /// </summary>
        /// <returns></returns>
        private Card NextCard()
        {
            return Deck.DealNextCard();
        }

        private void Hit(Player player)
        {
            Card card = NextCard();
            player.Hand.Add(card);
            UpdateTable();
            if (player.Total() > 21)
            {
                show.PlayerBust();
                player.Status = Status.Bust;
            }
        }

        private void Split(Player player)
        {
        }

        private bool Double(Player player)
        {
            bool doubleSuccess = false;
            if (player.Cash > player.Bet)
            {
                player.BetCash(player.Bet);
                Hit(player);
                doubleSuccess = true;
            }
            return doubleSuccess;
        }

        private string CheckProbability(string data)
        {
            return data.Substring(data.IndexOf(' '));
        }

        private void UpdateTable(bool hideDealerCards = true, bool showProbability = false)
        {
            show.Clear();

            foreach(Player player in Players)
            {
                player.ShowPlayerCards();
            }
            Dealer.ShowPlayerCards(hideDealerCards);
            if (showProbability)
            {
                show.SkipLine();
                Deck.ProbabilityOfHighCard();
            }
        }
        #endregion
    }
}
