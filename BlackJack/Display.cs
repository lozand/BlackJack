using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Display
    {
        public Display()
        {
        }

        // I'd like to move all or most of the methods that touch the Console class to be in here. 
        // DAL: TODO: This class should be interface'd.

        public void PlayerResult(string name, string result)
        {
            string punctuation = ".";
            if (result.ToLower() == "won")
            {
                Set("good");
                punctuation = "!";
            }
            else if (result.ToLower() == "lost")
            {
                Set("bad");
            }
            else
            {
                Set("info");
            }
            Console.WriteLine("Player {0} {1}{2}", name, result, punctuation);
            //SkipLine();
            Reset();
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void PlayerBust()
        {
            Set("bad");
            Console.WriteLine("Bust!!");
            Reset();
        }

        public void PlayerHasBust(string name)
        {
            Set("bad");
            Console.WriteLine("Player {0} bust", name);
            Reset();
        }

        public void PlayerHasWon(string name)
        {
            Set("good");
            Console.WriteLine("Player {0} won with these cards:", name);
            Reset();
        }

        public void PlayerHasTied(string name)
        {
            Set("info");
            Console.WriteLine("Player {0} tied", name);
            Reset();
        }

        public void PlayerGotBlackJack(string name)
        {
            Set("good");
            Console.WriteLine("Player {0} got Black Jack!!!", name);
            Reset();
        }

        public void DealerWins()
        {
            Set("bad");
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

        public void PlayerOptions(string name)
        {
            Set("info");
            Console.WriteLine("Player {0} to move. You may Hit or Stand", name);
            Reset();
        }

        public void Card(string name, string suit)
        {
            if (suit == "Hearts" || suit == "Diamonds")
            {
                Set("redcard");
            }
            else
            {
                Set("blackcard");
            }
            Console.WriteLine("{0} - {1}", name, suit);
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

        public void SkipLine()
        {
            Console.WriteLine("");
        }

        public string Read()
        {
            SkipLine();
            return Console.ReadLine();
        }

        private void Set(string state = "")
        {
            switch (state.ToLower())
            {
                case "good":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "bad":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "info":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "question":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "redcard":
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "blackcard":
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
