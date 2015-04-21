using BlackJack.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BlackJack
{
    public class Program
    {
        public static bool customMode = false;
        static void Main(string[] args)
        {
            int displaySpeed = 500;
            Display show = new Display(displaySpeed);
            Deck myDeck = new Deck(3, show);
            Deck originalDeck = myDeck;
            //myDeck.ShowCardsRemaining();
            const string Exit = "9",
                Infinite = "2",
                CustomDeck = "5",
                PlayGame = "1";

            DisplayOptions();

            var choice = Console.ReadLine();

            while (choice.ToLower() != Exit.ToLower())
            {
                switch (choice.ToLower())
                {
                    //case Deal:
                    //    Program.DealCard(myDeck);
                    //    break;
                    //case ProbabilityOfHighCard:
                    //    Program.ProbabilityOfHighCard(myDeck);
                    //    break;
                    //case PlayCard:
                    //    Program.PlayCard(myDeck);
                    //    break;
                    case CustomDeck:
                        Program.SetCustomDeck(out myDeck, originalDeck, show);
                        break;
                    case Infinite:
                        Display simDisp = new Display(100, true);
                        Program.Infinite(new Deck(3, simDisp), simDisp);
                        break;
                    case PlayGame:
                        Program.PlayTheGame(myDeck, show);
                        break;
                    default:
                        //Console.WriteLine("You must enter one of the following keywords: {0}, {1}, {2}, {3}, {4}, or {5}.", Deal, ProbabilityOfHighCard, PlayCard, Reset, ShowCards, Exit);
                        break;
                }
                DisplayOptions();
                choice = Console.ReadLine();
            }            
        }

        private static void PlayCard(Deck deck)
        {
            string card = Console.ReadLine();
            deck.PlayCard(card);
        }

        private static void ShowCards(Deck deck)
        {
            deck.ShowCardsRemaining();
        }

        private static void Reset(Deck deck, Display show) 
        {
            show.AskDecksToCreate();
            string decks = Console.ReadLine();
            deck.Reset(Int16.Parse(decks));
        }

        private static void DealCard(Deck deck)
        {
            Console.WriteLine("How many cards to Deal?");
            string cardsToDeal = Console.ReadLine();
            deck.DealCards(Int16.Parse(cardsToDeal), true);
        }

        private static void ProbabilityOfHighCard(Deck deck)
        {
            deck.ProbabilityOfHighCard();
        }

        private static void PlayTheGame(Deck deck, Display display)
        {
            string playAgain = "y";
            Console.WriteLine("");
            List<Player> players = new List<Player>();
            players.Add(new Player("Daniel",500, display, false));
            players.Add(new Player("Matt", 500, display, true));
            players.Add(new Player("John", 500, display, true));
            players.Add(new Player("Ruben", 500, display, true));
            Table table = new Table(deck, players);
            while (playAgain == "y")
            {
                table.StartGame();
                
                display.SkipLine();
                foreach (Player player in players)
                {
                    player.ShowPlayerCash();
                }
                display.SkipLine();
                Console.Write("Play Again? y/n  ");
                playAgain = Console.ReadLine();
                display.Clear();
            }
        }

        private static void Infinite(IDeck deck, Display display)
        {
            bool playAgain = true;
            int timesPlayed = 0;
            Console.WriteLine("");
            List<Player> players = new List<Player>();
            players.Add(new Player("Daniel", 500, display, true));
            players.Add(new Player("Matt", 500, display, true));
            players.Add(new Player("John", 500, display, true));
            players.Add(new Player("Ruben", 500, display, true));
            Table table = new Table(deck, players, display);
            while (playAgain)
            {
                display.SkipLine();
                display.Clear();
                playAgain = false;
                table.StartGame();

                display.SkipLine();
                foreach (Player player in players)
                {
                    player.ShowPlayerCash();
                    if (player.Cash > 0)
                    {
                        playAgain = true;
                    }
                }
                timesPlayed += 1;
                Console.WriteLine("At least 1 player still has money. Playing again. Times played: {0}", timesPlayed);
                display.Wait();
                display.Wait();
            }
            display.Wait();
            display.Wait();
            Console.ReadLine();
        }

        private static void SetCustomDeck(out Deck deck, Deck originalDeck, Display disp)
        {
            if (!customMode)
            {
                try
                {
                    List<ICard> customDeck = CustomDeck.GetXml();
                    deck = new Deck(customDeck, disp);
                    customMode = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Successfully loaded custom deck!!");
                }
                catch (Exception ex)
                {
                    customMode = false;
                    deck = originalDeck;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to load the custom deck!!");
                }
            }
            else
            {
                deck = originalDeck;
                customMode = false;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully loaded random deck!!");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(800);
            Console.Clear();
            Thread.Sleep(800);
        }

        private static void DisplayOptions()
        {
            Console.Clear();
            Console.WriteLine("Black Jack Game!");
            if (customMode)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Using custom deck");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine("Choose from the following options:");
            Console.WriteLine("1. Play Game");
            Console.WriteLine("2. Simulation");
            if (customMode)
            {
                Console.WriteLine("5. Revert to Random Deck");
            }
            else
            {
                Console.WriteLine("5. Use a Custom Deck");
            }
            Console.WriteLine("9. Exit...");
        }
    }
}
