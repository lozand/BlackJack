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
            ReshuffleIfNecessary();
            SetInitialBets();
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

        private const string hit = "hit", stand = "stand", dbl = "double", split = "split";

        private Display show = new Display();

        #region Public Methods
        public void SetInitialBets()
        {
            Deck.ProbabilityOfGettingBlackJack();
            show.SkipLine();
            foreach (Player player in Players)
            {
                bool betOk = false;
                while (!betOk)
                {
                    betOk = false;
                    double oldBet = player.Bet;
                    player.ClearBet();
                    show.PlayerBet(player.Name);
                    string input = show.Read();
                    if (input == "0")
                    {
                        player.Status = PlayerStatus.NotPlaying;
                        betOk = true;
                    }
                    else if (input != "rebet")
                    {
                        int bet = Int32.Parse(input);
                        if (bet <= player.Cash)
                        {
                            player.BetCash(bet);
                            player.Status = PlayerStatus.InPlay;
                            betOk = true;
                        }
                        else
                        {
                            show.PlayerNotEnoughCash(player.Name, bet, player.Cash);
                            betOk = false;
                        }
                    }
                    else
                    {
                        player.BetCash(oldBet);
                        betOk = true;
                    }
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
            DealCard();
            DealCard();
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
                if (player.Status != PlayerStatus.Done && player.Status != PlayerStatus.NotPlaying)
                {
                    PlayerPlay(player, player.Hands.First());
                }
                player.Status = PlayerStatus.Done;
            }

            ShowDealerCard(false);

            //if (!Players.All(p => p.Status == PlayerStatus.Lost || p.Status == PlayerStatus.Bust || p.Status == PlayerStatus.NotPlaying))
            if(!Players.All(p => p.Hands.All(h => h.Status == HandStatus.Bust) || p.Status == PlayerStatus.NotPlaying))
            {
                while (Dealer.Hands.First().Total() < DealerStand)
                {
                    Hit(Dealer.Hands.First());
                }
            }
        }

        public void EndGame()
        {
            // If the dealer busts, then everyone who didn't bust wins.
            if (Dealer.Hands.First().Status == HandStatus.Lost || Dealer.Hands.First().Status == HandStatus.Bust)
            {
                foreach (Player player in Players)
                {
                    foreach (Hand hand in player.Hands)
                    {
                        if (hand.Status != HandStatus.Bust)
                        {
                            hand.Status = HandStatus.Won;
                            // DAL: TODO: We'll need to refactor how we award bets. These should probably also be done at the hand level -.-
                            player.AwardBet();
                        }
                    }
                }
            }
            else // If the dealer didn't bust, then only those who beat the dealer win.
            {
                List<Player> eligiblePlayers = Players.Where(p => p.Status != PlayerStatus.NotPlaying).ToList();
                foreach (Player player in eligiblePlayers)
                {
                    foreach (Hand hand in player.Hands)
                    {
                        if (hand.Status == HandStatus.Played)
                        {
                            int dealerTotal = Dealer.Hands.First().Total();
                            int playerTotal = hand.Total();
                            if (playerTotal > dealerTotal)
                            {
                                hand.Status = HandStatus.Won;
                                player.AwardBet();
                            }
                            else if (playerTotal == dealerTotal)
                            {
                                hand.Status = HandStatus.Push;
                                player.PushBet();
                            }
                            else
                            {
                                hand.Status = HandStatus.Lost;
                            }
                        }
                    }
                }
            }
        }

        public void Results()
        {
            UpdateTable(false);
        }

        public void ClearHand()
        {
            foreach (Player player in Players)
            {
                player.ClearHand();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Deals one card to each player and then one to the dealer.
        /// This should only be called in the initial card deal.
        /// </summary>
        private void DealCard()
        {
            foreach (Player player in Players)
            {
                if (player.Status != PlayerStatus.NotPlaying)
                {
                    Card card = Deck.DealNextCard();
                    player.Hands.First().Cards.Add(card);
                    UpdateTable(true, true);
                    show.Wait();
                }
            }
            Card dealerCard = Deck.DealNextCard();
            Dealer.Hands.First().Cards.Add(dealerCard);
            UpdateTable(true, true);
            show.Wait();
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
                if (player.Status != PlayerStatus.NotPlaying)
                {
                    List<Hand> hand = player.Hands;
                    if (hand.First().Cards.Any(c => c.IsAce()) && hand.First().Cards.Any(c => c.IsTenCard()))
                    {
                        player.Hands.First().Status = HandStatus.BlackJack;
                        player.Status = PlayerStatus.Done;
                        player.AwardBet(true);
                    }
                    else
                    {
                        player.Status = PlayerStatus.InPlay;
                    }
                }
            }
            List<Hand> dealerHand = Dealer.Hands;
            if (dealerHand.First().Cards.Any(c => c.IsAce()) && dealerHand.First().Cards.Any(c => c.IsTenCard()))
            {
                Dealer.Hands.First().Status = HandStatus.BlackJack;
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
                if (player.Status != PlayerStatus.NotPlaying)
                {
                    foreach (Hand hand in player.Hands)
                    {
                        if (hand.Status != HandStatus.BlackJack)
                        {
                            hand.Status = HandStatus.Lost;
                        }
                        else
                        {
                            hand.Status = HandStatus.Push;
                        }
                    }
                    player.Status = PlayerStatus.Done;
                }
            }
            TableStatus = TableStatus.Finished;
        }

        /// <summary>
        /// The player should now have the option of hitting, staying, splitting or doubling down. 
        /// This function should run through the motions of the actual game of blackjack
        /// </summary>
        /// <param name="player"></param>
        private void PlayerPlay(Player player, Hand thisHand)
        {
            
            int total = thisHand.Total();
            thisHand.Status = HandStatus.InPlay;
            string option = "Begin";
            string prob = "";
            string probOption = "";
            while (total < 21 && (option.ToLower() == hit || option.ToLower() == "begin") && thisHand.Status != HandStatus.Bust)
            {
                UpdateTable(true, true);
                if (prob != "") { show.ProbabilityOfCards(Deck.ProbabilityOfCard(prob)); prob = ""; }
                show.PlayerOptions(player.Name);
                option = show.Read();
                if (option.IndexOf(' ') != -1) { probOption = option; option = option.Substring(0, option.IndexOf(' ')); }
                show.Clear();

                string lowerOption = option.ToLower();

                if(lowerOption == hit)
                {
                    Hit(thisHand);
                }
                else if(lowerOption == dbl)
                {
                    if (!Double(player, thisHand))
                    {
                        var message = "You do not have enough money to double";
                        show.AddToMessage(message);
                        option = "begin";
                    }
                    else if (thisHand.Status != HandStatus.Bust)
                    {
                        thisHand.Status = HandStatus.Played;
                    }
                }
                else if(lowerOption == split)
                {
                    if (thisHand.Cards[0].Name == thisHand.Cards[1].Name)
                    {
                        // We also need to verify that the player has enough money to split.
                        Split(ref player);
                        option = stand;
                        thisHand.Status = HandStatus.Played;
                    }
                    else
                    {
                        var message = "Split is not valid in this situation.";
                        show.AddToMessage(message);
                        option = "Begin";
                    }
                }
                else if(lowerOption == stand)
                {
                    prob = CheckProbability(probOption);
                }
                else
                {
                    option = stand;
                    thisHand.Status = HandStatus.Played;
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
            //Console.WriteLine("Dealer has:");
            Dealer.ShowPlayerCards(hideOneCard);
        }

        private void ReshuffleIfNecessary()
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

        private void Hit(Hand hand)
        {
            Card card = NextCard();
            hand.Cards.Add(card);
            UpdateTable();
            if (hand.Total() > 21)
            {
                show.PlayerBust();
                hand.Status = HandStatus.Bust;
            }
        }

        private void Split(ref Player player)
        {
            Hand firstHand = player.Hands[player.NumberOfHands];
            player.Hands.Add(new Hand());
            Hand secondHand = player.Hands[player.NumberOfHands];
            secondHand.Cards.Add(firstHand.Cards[1]);
            firstHand.Cards.Remove(firstHand.Cards[1]);

            firstHand.Cards.Add(NextCard());
            PlayerPlay(player, firstHand);

            secondHand.Cards.Add(NextCard());
            PlayerPlay(player, secondHand);
        }

        private bool Double(Player player, Hand hand)
        {
            bool doubleSuccess = false;
            if (player.Bet > player.Cash)
            {
                doubleSuccess = false;
            }
            else
            {
                player.Bet *= 2;
                Hit(hand);
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

            show.DisplayMessage();
            show.ClearMessage();

        }
        #endregion
    }
}
