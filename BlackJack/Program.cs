using BlackJack.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Program
    {
        static void Main(string[] args)
        {
            Display show = new Display();
            Deck myDeck = new Deck(3, show);
            //myDeck.ShowCardsRemaining();
            const string Exit = "9",
                Deal = "deal",
                ProbabilityOfHighCard = "highcard",
                PlayCard = "playcard",
                Reset = "reset",
                ShowCards = "showcards",
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
                    //case Reset:
                    //    Program.Reset(myDeck, show);
                    //    break;
                    //case ShowCards:
                    //    Program.ShowCards(myDeck);
                    //    break;
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

        private static void PlayTheGame(IDeck deck, Display display)
        {
            string playAgain = "y";
            
            Console.WriteLine("");
            List<Player> players = new List<Player>();
            players.Add(new Player("Daniel",500, display));
            players.Add(new Player("Matt", 500, display));
            players.Add(new Player("John", 500, display));
            players.Add(new Player("Ruben", 500, display));
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

        private static void DisplayOptions()
        {
            Console.Clear();
            Console.WriteLine("Black Jack Game!");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Choose from the following options:");
            Console.WriteLine("1. Play Game");
            Console.WriteLine("9. Exit...");
        }
    }
}
