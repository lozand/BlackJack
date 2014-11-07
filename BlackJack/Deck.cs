using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Deck
    {
        /// <summary>
        /// Initializes a new deck and shuffles
        /// </summary>
        public Deck(int numberOfDecks = 1)
        {
            NumberOfDecks = numberOfDecks;
            TOTAL_CARDS = cardsInADeck * numberOfDecks;
            Initialize(numberOfDecks);
            // DAL: Do we need to shuffle more than once? I wonder if there is some theory behind shuffling and random numbers.
            Shuffle(3);
        }

        #region Properties
        public List<Card> RemainingCards = new List<Card>();
        public List<Card> PlayedCards = new List<Card>();
        public int TOTAL_CARDS = 0;
        public int cardsInADeck = 52;
        public int NumberOfDecks;
        private Display show = new Display();
        #endregion

        #region Public/General Methods
        public int GetCardsRemaining()
        {
            return RemainingCards.Count;
        }

        /// <summary>
        /// Resets and shuffles the deck
        /// </summary>
        /// <param name="numberOfDecks"></param>
        public void Reset(int numberOfDecks)
        {
            RemainingCards.Clear();
            PlayedCards.Clear();
            // Since the below steps are completed in the constructor maybe it's worth bringing these methods out into their own method.
            TOTAL_CARDS = cardsInADeck * numberOfDecks;
            Initialize(numberOfDecks);
            Shuffle(3);
        }

        /// <summary>
        /// Deals the next card and returns it.
        /// </summary>
        /// <returns></returns>
        public Card DealNextCard()
        {
            var nextCard = RemainingCards.FirstOrDefault();
            RemainingCards.Remove(nextCard);
            PlayedCards.Add(nextCard);

            return nextCard;
        }
        #endregion

        #region Probability Methods
        /// <summary>
        /// Returns the probability of being dealt an A, K, Q, J, or 10
        /// </summary>
        public void ProbabilityOfHighCard()
        {
            double prob;

            int totalCardsLeft = RemainingCards.Count;
            int totalHighCards = RemainingCards.Where(c => c.Name == "A" || c.Name == "K" || c.Name == "J" || c.Name == "Q" || c.Name == "X").Count();

            prob = ((double)totalHighCards / (double)totalCardsLeft) * 100;

            Console.WriteLine("Probability of drawing 10, J, Q, K, or A is: {0:0.00}%", prob);
        }

        /// <summary>
        /// Returns the probability of getting a card in the parameter. Expecting a string formatted like "JQKA" and would return the probability of getting a Jack OR a Queen OR a King OR an Ace.
        /// </summary>
        /// <param name="cardNames"></param>
        /// <returns></returns>
        public double ProbabilityOfCard(string cardNames)
        {
            int totalOfTheseCards = 0;
            int totalCardsLeft = RemainingCards.Count;

            foreach (char cardName in cardNames)
            {
                totalOfTheseCards += RemainingCards.Where(c => c.Name == cardName.ToString()).Count();
            }

            return ((double)totalOfTheseCards / (double)totalCardsLeft) * 100;
        }


        /// <summary>
        /// Calculates the probability of the player getting black jack.
        /// Assumes no other players except player and dealer
        /// </summary>
        public void ProbabilityOfGettingBlackJack()
        {
            // DAL: TODO: Write
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates new cards for the deck. X = 10, 
        /// </summary>
        private void Initialize(int numberOfDecks)
        {
            List<Suit> suites = new List<Suit>();
            suites.Add(Suit.Hearts);
            suites.Add(Suit.Clubs);
            suites.Add(Suit.Diamonds);
            suites.Add(Suit.Spades);

            string[] names = { "2", "3", "4", "5", "6", "7", "8", "9", "X", "J", "Q", "K", "A" };

            for (int i = 0; i < suites.Count; i++)
            {
                for (int j = 0; j < names.Count(); j++)
                {
                    for (int k = 0; k < numberOfDecks; k++)
                    {
                        RemainingCards.Add(new Card(names[j], suites[i]));
                    }
                }
            }
            
        }

        /// <summary>
        /// Shuffles "RemainingCards," i times.
        /// </summary>
        private void Shuffle(int timesToShulffle)
        {
            for (int i = 0; i < timesToShulffle; i++)
            {
                Random rand = new Random();
                List<Card> temp = new List<Card>();
                int remaining = TOTAL_CARDS;
                while (remaining > 0)
                {
                    Card card = RemainingCards[rand.Next(0, remaining)];
                    temp.Add(card);
                    RemainingCards.Remove(card);
                    remaining--;
                }
                RemainingCards = temp;
            }
        }
        #endregion

        #region Utility Methods
        /// <summary>
        /// I haven't found a use for this yet, but I will probably need it when calculating probabilities. I wonder if there is already a method for this in the Math class.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private int Factorial(int num)
        {
            if (num == 1)
            {
                return 1;
            }
            return num * Factorial(num - 1);
        }
        #endregion

        #region Depricated/Non-game Methods
        /// <summary>
        /// Plays the next card and displays it if it needs to
        /// </summary>
        /// <param name="displayCards"></param>
        public void PlayNextCard(bool displayCards)
        {
            var nextCard = RemainingCards.FirstOrDefault();
            RemainingCards.Remove(nextCard);
            PlayedCards.Add(nextCard);

            if (displayCards)
            {
                nextCard.DisplayCard();
            }
        }

        /// <summary>
        /// Display function to show which cards are left in the deck
        /// </summary>
        public void ShowCardsRemaining()
        {
            foreach (Card card in RemainingCards)
            {
                card.DisplayCard();
            }
            DisplayNumberOfCardsRemaining();
        }

        /// <summary>
        /// Removes the top x cards in the deck and displays their values
        /// </summary>
        /// <param name="cardsToDeal"></param>
        public void DealCards(int cardsToDeal, bool showCards)
        {
            for (int i = 0; i < cardsToDeal; i++)
            {
                PlayNextCard(showCards);
            }
        }

        /// <summary>
        /// "Plays" a card. Removes the "card" from the RemainingCards and Adds to the PlayedCards prop. Expects "card" in string[2] like 2H for 2 of hearts and XC for 10 of clubs.
        /// </summary>
        /// <param name="card"></param>
        public void PlayCard(string card)
        {
            // DAL: TODO: Add some error checking here
            string name = card[0].ToString();
            char suiteInitial = card[1];

            Suit suite;

            switch (suiteInitial)
            {
                case 'H':
                    suite = Suit.Hearts;
                    break;
                case 'D':
                    suite = Suit.Diamonds;
                    break;
                case 'C':
                    suite = Suit.Clubs;
                    break;
                case 'S':
                    suite = Suit.Spades;
                    break;
                default:
                    suite = Suit.Hearts; // ?
                    break;
            }
            var removeMe = RemainingCards.Where(c => c.Name == name && c.Suit == suite);
            if (removeMe.FirstOrDefault() != null)
            {
                RemainingCards.Remove(removeMe.FirstOrDefault());
                PlayedCards.Add(new Card(name, suite));
            }
            else
            {
                Console.WriteLine("This card is not in the deck");
            }
        }

        /// <summary>
        /// Function for displaying the number of cards remaining in the deck.
        /// </summary>
        private void DisplayNumberOfCardsRemaining()
        {
            show.CardsRemaining(RemainingCards.Count);
        }
        #endregion
    }
}
