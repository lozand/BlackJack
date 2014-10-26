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
            Deck myDeck = new Deck(1);
            myDeck.ShowCardsRemaining();
            Console.WriteLine("");
            string whatsNext = "Go";
            const string Exit = "exit",
                Deal = "deal",
                ProbabilityOfHighCard = "highcard",
                PlayCard = "playcard",
                Reset = "reset",
                ShowCards = "showcards",
                PlayGame = "play";

            while (whatsNext.ToLower() != Exit.ToLower())
            {
                switch (whatsNext.ToLower())
                {
                    case Deal:
                        Program.DealCard(myDeck);
                        break;
                    case ProbabilityOfHighCard:
                        Program.ProbabilityOfHighCard(myDeck);
                        break;
                    case PlayCard:
                        Program.PlayCard(myDeck);
                        break;
                    case Reset:
                        Program.Reset(myDeck);
                        break;
                    case ShowCards:
                        Program.ShowCards(myDeck);
                        break;
                    case PlayGame:
                        Program.PlayTheGame(myDeck);
                        break;
                    default:
                        Console.WriteLine("You must enter one of the following keywords: {0}, {1}, {2}, {3}, {4}, or {5}.", Deal, ProbabilityOfHighCard, PlayCard, Reset, ShowCards, Exit);
                        break;
                }

                whatsNext = Console.ReadLine();
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

        private static void Reset(Deck deck)
        {
            Console.WriteLine("How many decks to Create?");
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

        private static void PlayTheGame(Deck deck)
        {
            List<Player> players = new List<Player>();
            players.Add(new Player("Daniel"));
            Table table = new Table(deck, players);
            table.DealInitialHand();
            table.Play();

            Console.WriteLine("Game is done");
        }
    }
}
