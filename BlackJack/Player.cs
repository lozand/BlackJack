using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
            AcesReduced = false;
        }
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public Status Status { get; set; }

        bool AcesReduced = false;
        private Display log = new Display();

        public int Total()
        {
            int total = 0;
            Hand.ForEach(c=>{
                total += c.NumberValue;
            });
            if(total > 21 && !AcesReduced)
            {
                foreach(Card card in Hand)
                {
                    if(card.Name == "A" && card.NumberValue == 11 && !AcesReduced)
                    {
                        card.NumberValue -= 10;
                        AcesReduced = true;
                    }
                }
            }
            return total;
        }

        public void ShowPlayerCards(bool hideOneCard = false)
        {
            if (hideOneCard)
            {
                Hand.First().DisplayCard();
            }
            else
            {
                if (Status == Status.Bust)
                {
                    log.PlayerHasBust(Name);
                }
                else if (Status == Status.BlackJack)
                {
                    log.PlayerGotBlackJack(Name);
                }
                else if (Status == Status.Won)
                {
                    log.PlayerHasWon(Name);
                }
                else if (Status == Status.Lost)
                {
                    log.PlayerResult(Name, "lost");
                }
                else if (Status == Status.Push && Name != "Dealer")
                {
                    log.PlayerHasTied(Name);
                }
                else
                {
                    log.PlayerCards(Name);
                }

                foreach (Card card in Hand)
                {
                    card.DisplayCard();
                }
                log.Total(Total());
            }
            log.SkipLine();
        }
    }
}
