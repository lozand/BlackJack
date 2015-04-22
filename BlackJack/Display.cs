using BlackJack.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace BlackJack
{
    public class Display : IDisplay
    {
        public Display(int speed, bool writeToFile = false)
        {
            IsWriting = false;
            DisplaySpeed = speed;
            if (writeToFile)
            {
                RunningSimulation = true;
                Initialize();
            }
            else
            {
                RunningSimulation = false;
            }
        }

        public int DisplaySpeed { get; set; }
        public bool RunningSimulation { get; set; }
        public bool IsWriting { get; set; }

        const string good = "good",
            bad = "bad",
            info = "info",
            blackCard = "blackcard",
            redCard = "redcard",
            question = "question";

        string fileName = "D:\\Simulation.txt";

        private static string Message { get; set; }

        public void DisplayMessage()
        {
            Console.WriteLine(Message);
        }

        public void WriteToFile(string text, string level)
        {
            bool successfulWrite = false;
            while (!successfulWrite)
            {
                if (!IsWriting)
                {
                    IsWriting = true;
                    Set(level);
                    Console.WriteLine(text);
                    Reset();
                    using (StreamWriter sw = File.AppendText(fileName))
                    {
                        sw.Write(text);
                        sw.WriteLine("");
                    }
                    IsWriting = false;
                    successfulWrite = true;
                }
            }
        }

        public void Write(string text, string level)
        {
            Set(level);
            Console.WriteLine(text);
            Reset();
            if (RunningSimulation)
            {
                WriteToFile(text, level);
            }
        }

        public void Initialize()
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            using (FileStream fs = File.Create(fileName))
            {
            }
        }

        public void AddToMessage(string content)
        {
            Message += "\r\n";
            Message += content;
        }

        public void NewMessage(string content)
        {
            Message = content;
        }

        public void ClearMessage()
        {
            Message = "";
        }

        public void Error(string message)
        {
            Write(message, bad);
        }

        public void PlayerResult(string name, string result)
        {
            string punctuation = ".";
			string level = "";
            if (name == "Dealer")
            {
                if (result == "bust") { level = good; } else { level = info; }
                Write(String.Format("Dealer {0}", result), level);
            }
            else
            {
                if (result.ToLower() == "won")
                {
                    level = good;
                    punctuation = "!";
                }
                else if (result.ToLower() == "lost")
                {
                    level = bad;
                }
                else
                {
                    level = info;
                }
                Write(String.Format("Player {0} {1}{2}", name, result, punctuation), level);
            }
            Reset();
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void PlayerBet(string name, double bet)
        {
			Write(String.Format("Player {0} has bet {1}", name, bet.ToString()), info);
        }

		public void PlayerBust()
		{
			Write(String.Format("Bust!!"),bad);
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

        public void PlayerNotEnoughCash(string name, double bet, double cash)
        {
            Set(info);
            Console.WriteLine("You tried to bet ${0}, but you only have ${1}", bet, cash);
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

        public void PlayerToBet(string name, string amount)
        {
            Set(question);
            SkipLine();
            Console.WriteLine("How much does Player {0} want to bet? ({0} has ${1} left.)", name, amount);
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
            //Console.WriteLine("Probability of getting these cards is {0}%.", probability);
            string message = String.Format("Probability of getting these cards is {0}%.", probability);
            AddToMessage(message);
            Reset();
        }

        public void SkipLine()
        {
            Console.WriteLine("");
        }

        public void Wait()
        {
            Thread.Sleep(DisplaySpeed);
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
