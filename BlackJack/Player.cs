using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Player
    {
        public Player(string name, double cash)
        {
            Name = name;
            Hand = new List<Card>();
            AcesReduced = false;
            Cash = cash;
        }
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public Status Status { get; set; }
        public double Bet { get; set; }
        public double Cash { get; set; }

        bool AcesReduced = false;
        private Display log = new Display();
        double payout = 2;
        double blackJackPayout = 2.5;

        public void BetCash(double money)
        {
            Bet += money;
            Cash -= money;
        }

        public void ClearBet()
        {
            Bet = 0;
        }

        public void PushBet()
        {
            Cash += Bet;
        }

        public void AwardBet(bool isBlackJack = false)
        {
            if (isBlackJack)
            {
                Cash += Bet * blackJackPayout;
            }
            else
            {
                Cash += Bet * payout;
            }
        }

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
                    log.PlayerHasBust(Name, Bet);
                }
                else if (Status == Status.BlackJack)
                {
                    log.PlayerGotBlackJack(Name, Bet);
                }
                else if (Status == Status.Won)
                {
                    log.PlayerHasWon(Name, Bet);
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

        public void ShowPlayerCash()
        {
            log.PlayerCash(Name, Cash);
        }
    }
}
