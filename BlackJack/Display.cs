using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BlackJack
{
    public class Display
    {
        public Display()
        {
        }

        // DAL: TODO: This class should be interface'd.

        const string good = "good",
            bad = "bad",
            info = "info",
            blackCard = "blackcard",
            redCard = "redcard",
            question = "question";

        public void PlayerResult(string name, string result)
        {
            string punctuation = ".";
            if (name == "Dealer")
            {
                if (result == "bust") { Set(good); } else { Set(info); }
                Console.WriteLine("Dealer {0}", result);
            }
            else
            {
                if (result.ToLower() == "won")
                {
                    Set(good);
                    punctuation = "!";
                }
                else if (result.ToLower() == "lost")
                {
                    Set(bad);
                }
                else
                {
                    Set(info);
                }
                Console.WriteLine("Player {0} {1}{2}", name, result, punctuation);
            }
            Reset();
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void PlayerBust()
        {
            Set(bad);
            Console.WriteLine("Bust!!");
            Reset();
        }

        public void PlayerHasBust(string name, double bet)
        {
            Set(bad);
            Console.WriteLine("Player {0} bust and lost ${1}", name, bet);
            Reset();
        }

        public void PlayerHasWon(string name, double bet)
        {
            Set(good);
            Console.WriteLine("Player {0} won ${1} with these cards:", name, bet);
            Reset();
        }

        public void PlayerHasTied(string name)
        {
            Set(info);
            Console.WriteLine("Player {0} tied", name);
            Reset();
        }

        public void PlayerGotBlackJack(string name, double bet)
        {
            Set(good);
            Console.WriteLine("Player {0} got Black Jack and won ${1}!!!", name, bet);
            Reset();
        }

        public void PlayerCash(string name, double cash)
        {
            Set(info);
            Console.WriteLine("Player {0} has ${1} now.", name, cash);
            Reset();
        }

        public void DealerWins()
        {
            Set(bad);
            Console.WriteLine("Oh no! Dealer wins!!");
            Reset();
        }

        public void PlayerWins(string name)
        {
            Set("good");
            Console.WriteLine("Woo! Player {0} wins!!", name);
            Reset();
        }

        public void PlayerCards(string name)
        {
            Set("info");
            Console.WriteLine("Player {0} has these cards", name);
            Reset();
        }

        public void PlayerCardsInPlay(string name)
        {
            Set("info");
            Console.WriteLine("Player {0} has these cards     <-------", name);
            Reset();
        }

        public void DealerCard(string name)
        {
            Set("info");
            Console.WriteLine("{0} has these cards", name);
            Reset();
        }

        public void PlayerOptions(string name)
        {
            Set("info");
            Console.WriteLine("Player {0} to move. You may Hit, Stand or Double", name);
            Reset();
        }

        public void PlayerBet(string name)
        {
            Set(question);
            Console.WriteLine("How much does Player {0} want to bet?", name);
            Reset();
        }

        public void Card(string name, string suit, string suitSymbol)
        {
            if (suit == "Hearts" || suit == "Diamonds")
            {
                Set("redcard");
            }
            else
            {
                Set("blackcard");
            }
            Console.WriteLine("{0} - {1}", name, suitSymbol);
            Reset();
        }

        public void Total(int total)
        {
            Set("blackcard");
            Console.WriteLine("Total: {0}", total.ToString());
            Reset();
        }

        public void CardsRemaining(int count)
        {
            Set("info");
            Console.WriteLine("{0} cards remaining in this deck.", count);
            Reset();
        }

        public void AskDecksToCreate()
        {
            Set("question");
            Console.WriteLine("How many decks to Create?");
            Reset();
        }

        public void ProbabilityOfCards(double probability)
        {
            Set(info);
            Console.WriteLine("Probability of getting these cards is {0}%.", probability);
            Reset();
        }

        public void SkipLine()
        {
            Console.WriteLine("");
        }

        public string Read()
        {
            SkipLine();
            return Console.ReadLine();
        }

        public void Wait()
        {
            Thread.Sleep(500);
        }

        private void Set(string state = "")
        {
            switch (state.ToLower())
            {
                case good:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case bad:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case question:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case redCard:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case blackCard:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        private void Reset()
        {
            Console.ResetColor();
        }
    }
}
