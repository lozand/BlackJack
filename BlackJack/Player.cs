using BlackJack.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Player
    {
        public Player(string name, double cash, IDisplay disp)
        {
            Initialize(name, cash, disp);
        }
        //public Player(string name, double cash)
        //{
        //    Initialize(name, cash, new Display());
        //}
        public string Name { get; set; }
        public Input input { get; set; }
        public List<Hand> Hands { get; set; }
        public PlayerStatus Status { get; set; }
        public double Bet { get; set; }
        public double Cash { get; set; }
        public int NumberOfHands
        {
            get
            {
                return Hands.Count;
            }
        }


        private IDisplay log;
        double payout = AppConfig.Payout;
        double blackJackPayout = AppConfig.BlackJackPayout;

        public string GetInput()
        {
            return input.GetInput();
        }
        public void BetCash(double money)
        {
            if (money < 0)
            {
                money *= -1;
            }
            if (money == 0)
            {
                throw new ArgumentException("value needs to be non-zero");
            }
            Bet += money;
            Cash -= money;
            // Is throwing an exception here the best way to do this?
        }

        public void ClearBet()
        {
            Bet = 0;
        }

        public void ClearHand()
        {
            Hands = new List<Hand>();
            Hands.Add(new Hand());
            Status = PlayerStatus.InPlay;
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

        public void ShowPlayerCards(bool hideOneCard = false)
        {
            if (hideOneCard) // This should be set to true only if the player is the dealer AND all players have played.
            {
                log.DealerCard(Name);
                if (Hands.FirstOrDefault().Cards.FirstOrDefault() != null)
                {
                    Hands.First().Cards.First().DisplayCard();
                }
            }
            else
            {
                foreach (Hand hand in Hands)
                {
                    if (Name != "Dealer")
                    {
                        if (hand.Status == HandStatus.Bust)
                        {
                            log.PlayerHasBust(Name, Bet);
                        }
                        else if (hand.Status == HandStatus.BlackJack)
                        {
                            log.PlayerGotBlackJack(Name, Bet * blackJackPayout);
                        }
                        else if (hand.Status == HandStatus.Won)
                        {
                            log.PlayerHasWon(Name, Bet);
                        }
                        else if (hand.Status == HandStatus.Lost)
                        {
                            log.PlayerResult(Name, "lost");
                        }
                        else if (hand.Status == HandStatus.BlackJack)
                        {
                            log.PlayerHasWon(Name, Bet * 1.5);
                        }
                        else if (hand.Status == HandStatus.Push && Name != "Dealer")
                        {
                            log.PlayerHasTied(Name);
                        }
                        else if (hand.Status == HandStatus.InPlay)
                        {
                            log.PlayerCardsInPlay(Name);
                        }
                        else
                        {
                            log.PlayerCards(Name);
                        }
                    }
                    else
                    {
                        if (hand.Status == HandStatus.Bust)
                        {
                            log.PlayerResult(Name, "bust");
                        }
                        else
                        {
                            log.PlayerCards(Name);
                        }
                    }

                    foreach (Card card in hand.Cards)
                    {
                        card.DisplayCard();
                    }

                    log.Total(hand.Total);
                }
            }
            log.SkipLine();
        }

        public void ShowPlayerCash()
        {
            log.PlayerCash(Name, Cash);
        }

        private void Initialize(string name, double cash, IDisplay disp)
        {
            Name = name;
            Hands = new List<Hand>();
            Hands.Add(new Hand());
            Cash = cash;
            input = new Input();
            log = disp;
        }
    }
}
