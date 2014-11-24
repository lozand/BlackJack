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

        bool AcesReduced = false;

        public List<Card> Cards { get; set; }
        public HandStatus Status { get; set; }

        //public void ResetAces()
        //{
        //    AcesReduced = false;
        //}

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
