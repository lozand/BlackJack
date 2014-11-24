using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackJack;

namespace UnitTests
{
    [TestClass]
    public class DeckTest
    {
        private int cardsInADeck = 52;


        [TestMethod]
        public void Constructor_SingleDeck_FourOfEachCardExists()
        {
            Deck deck = new Deck();

            var numberOfAces = deck.RemainingCards.FindAll(c => c.Name == "A").Count;

            int expectedNumberOfAces = 4;
            int actualNumberOfAces = numberOfAces;

            Assert.AreEqual(expectedNumberOfAces, actualNumberOfAces);
        }

        [TestMethod]
        public void Constructor_DoubleDeck_EightOfEachCardExists()
        {
            Deck deck = new Deck(2);

            var numberOfAces = deck.RemainingCards.FindAll(c => c.Name == "A").Count;

            int expectedNumberOfAces = 8;
            int actualNumberOfAces = numberOfAces;

            Assert.AreEqual(expectedNumberOfAces, actualNumberOfAces);
        }

        [TestMethod]
        public void GetCardsRemaining_SingleDeck_AllCardsRemaining()
        {
            Deck deck = new Deck();

            int actual = deck.GetCardsRemaining();
            int expected = cardsInADeck;

            Assert.AreEqual(actual, expected);
            
        }

        [TestMethod]
        public void GetCardsRemaining_SingleDeck_AllButOneCardRemaining()
        {
            Deck deck = new Deck();

            deck.DealNextCard();

            int actual = deck.GetCardsRemaining();
            int expected = cardsInADeck - 1;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void GetCardsRemaining_DoubleDeck_AllCardsRemaining()
        {
            Deck deck = new Deck(2);

            int actual = deck.GetCardsRemaining();
            int expected = cardsInADeck * 2;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void GetCardsRemaining_DoubleDeck_AllButOneCardRemaining()
        {
            Deck deck = new Deck(2);

            deck.DealNextCard();

            int actual = deck.GetCardsRemaining();
            int expected = (cardsInADeck * 2) - 1;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void PropTotalCards_SingleDeck_AllCardsRemain()
        {
            Deck deck = new Deck();

            PrivateObject obj = new PrivateObject(new Deck());

            int actual = (int)obj.GetFieldOrProperty("TOTAL_CARDS");

            int expected = cardsInADeck;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void PropTotalCards_DoubleDeck_AllCardsRemain()
        {
            Deck deck = new Deck(2);

            PrivateObject obj = new PrivateObject(new Deck(2));

            int actual = (int)obj.GetFieldOrProperty("TOTAL_CARDS");

            int expected = cardsInADeck * 2;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void PropTotalCards_SixDecks_AllCardsRemain()
        {
            int decks = 6;

            Deck deck = new Deck(decks);

            PrivateObject obj = new PrivateObject(new Deck(decks));

            int actual = (int)obj.GetFieldOrProperty("TOTAL_CARDS");

            int expected = cardsInADeck * decks;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Reset_DeckResetsToFullDeck_CardsReset()
        {
            int cardsToDeal = 15;
            Deck deck = new Deck();
            deck.DealCards(cardsToDeal, false);
            PrivateObject obj = new PrivateObject(deck);

            int expectedCards = (deck.TOTAL_CARDS * deck.NumberOfDecks) - cardsToDeal;
            int actualCards = deck.GetCardsRemaining();

            Assert.AreEqual(expectedCards, actualCards);

            deck.Reset(1);

            int expected = cardsInADeck;
            int actual = deck.GetCardsRemaining();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DealNextCard_PlayTopCard_CardNotAvaialableAfterDeal()
        {
            Deck deck = new Deck();

            Card card = deck.DealNextCard();

            var dealtCardAvailable = deck.RemainingCards.Find(c => c.Name == card.Name && c.Suit == card.Suit);

            Assert.IsNull(dealtCardAvailable);
        }

        [TestMethod]
        public void DealNextCard_PlayTopCardInDoubleDeck_OnlyOneCardOfThatTypeAvailalbeAfterDeal()
        {
            Deck deck = new Deck(2);

            Card card = deck.DealNextCard();

            var dealtCardAvailable = deck.RemainingCards.FindAll(c => c.Name == card.Name && c.Suit == card.Suit);

            int actualCountRemainingOfCardDealt = dealtCardAvailable.Count;
            int expectedCountRemainingOfCardDealt = 1;

            Assert.AreEqual(expectedCountRemainingOfCardDealt, actualCountRemainingOfCardDealt);
            
        }
    }
}
