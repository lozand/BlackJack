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

        /// <summary>
        /// Represents the cards in the hand. Duh.
        /// </summary>
        public List<Card> Cards { get; set; }

        /// <summary>
        /// Each hand has a status, this takes care of that.
        /// </summary>
        public HandStatus Status { get; set; }

        // TODO: This "method" should really be a property.
        public int Total()
        {
            int total = 0;
            Cards.ForEach(c =>
            {
                total += c.NumberValue;
            });
            if (total > 21 && !AcesReduced)
            {
                foreach (Card card in Cards)
                {
                    if (card.Name == "A" && card.NumberValue == 11 && !AcesReduced)
                    {
                        card.NumberValue -= 10;
                        AcesReduced = true;
                    }
                }
            }
            return total;
        }
    }
}
