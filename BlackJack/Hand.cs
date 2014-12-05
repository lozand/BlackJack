using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Hand
    {
        public Hand()
        {
            AcesReduced = false;
            Status = HandStatus.Waiting;
            Cards = new List<Card>();
        }

        /// <summary>
        /// This property determines whether or not a hand needs to count an Ace as a one or eleven.
        /// </summary>
        bool AcesReduced = false;

        private List<Card> cards { get; set; }

        /// <summary>
        /// Represents the cards in the hand. Duh.
        /// </summary>
        public List<Card> Cards
        {
            get
            {
                if (Total > 21 && !AcesReduced)
                {
                    foreach (Card card in cards)
                    {
                        if (card.Name == "A" && card.NumberValue == 11 && !AcesReduced)
                        {
                            card.NumberValue -= 10;
                            AcesReduced = true;
                        }
                    }
                }
                return cards;
            }
            set { cards = value; }
        }

        /// <summary>
        /// Each hand has a status, this takes care of that.
        /// </summary>
        public HandStatus Status { get; set; }

        public int Total { get {
            int total = 0;

            cards.ForEach(c =>
            {
                total += c.NumberValue;
            });

            return total;
        } }
    }
}
