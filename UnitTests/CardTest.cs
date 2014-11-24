using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlackJack;

namespace UnitTests
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void IsAce_AceIsChosen_ReturnsTrue()
        {
            Card card = new Card("A", Suit.Clubs);

            bool actualIsAce = card.IsAce();
            bool expectedIsAce = true;

            Assert.AreEqual(expectedIsAce, actualIsAce);
        }

        [TestMethod]
        public void IsAce_ThreeIsChosen_ReturnsFalse()
        {
            Card card = new Card("3", Suit.Clubs);

            bool actualIsAce = card.IsAce();
            bool expectedIsAce = false;

            Assert.AreEqual(expectedIsAce, actualIsAce);
        }

        [TestMethod]
        public void IsAce_KingChosen_ReturnsFalse()
        {
            Card card = new Card("K", Suit.Hearts);

            bool actualIsAce = card.IsAce();
            bool expectedIsAce = false;

            Assert.AreEqual(expectedIsAce, actualIsAce);
        }

        [TestMethod]
        public void IsAce_AceIsChosenAgain_ReturnsTrue()
        {
            Card card = new Card("A", Suit.Diamonds);

            bool actualIsAce = card.IsAce();
            bool expectedIsAce = true;

            Assert.AreEqual(expectedIsAce, actualIsAce);
        }

        [TestMethod]
        public void IsTenCard_AceChosen_ReturnsFalse()
        {
            Card card = new Card("A", Suit.Hearts);

            bool expectedIsTenCard = false;
            bool actualIsTenCard = card.IsTenCard();

            Assert.AreEqual(expectedIsTenCard, actualIsTenCard);
        }

        [TestMethod]
        public void IsTenCard_KingChosen_ReturnsTrue()
        {
            Card card = new Card("K", Suit.Spades);

            bool expectedIsTenCard = true;
            bool actualIsTenCard = card.IsTenCard();

            Assert.AreEqual(expectedIsTenCard, actualIsTenCard);
        }

        [TestMethod]
        public void IsTenCard_QueenChosen_ReturnsTrue()
        {
            Card card = new Card("Q", Suit.Diamonds);

            bool expectedIsTenCard = true;
            bool actualIsTenCard = card.IsTenCard();

            Assert.AreEqual(expectedIsTenCard, actualIsTenCard);
        }

        [TestMethod]
        public void IsTenCard_JackChosen_ReturnsTrue()
        {
            Card card = new Card("J", Suit.Clubs);

            bool expectedIsTenCard = true;
            bool actualIsTenCard = card.IsTenCard();

            Assert.AreEqual(expectedIsTenCard, actualIsTenCard);
        }

        [TestMethod]
        public void IsTenCard_ThreeChosen_ReturnFalse()
        {
            Card card = new Card("3", Suit.Hearts);

            bool expectedIsTenCard = false;
            bool actualIsTenCard = card.IsTenCard();

            Assert.AreEqual(expectedIsTenCard, actualIsTenCard);
        }

        [TestMethod]
        public void PropNumberValue_InitialAce_IsEleven()
        {
            Card card = new Card("A", Suit.Clubs);

            int expectedValue = 11;
            int actualValue = card.NumberValue;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void PropNumberValue_Ace_IsEleven()
        {
            Card card = new Card("A", Suit.Clubs);

            int expectedValue = 11;
            int actualValue = card.NumberValue;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void PropNumbervalue_Ten_IsTen()
        {
            Card card = new Card("10", Suit.Diamonds);

            int expectedValue = 10;
            int actualValue = card.NumberValue;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void PropNubmerValue_Seven_IsSeven()
        {
            Card card = new Card("7", Suit.Diamonds);

            int expectedValue = 7;
            int actualValue = card.NumberValue;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void PropNumberValue_King_IsTen()
        {
            Card card = new Card("K", Suit.Diamonds);

            int expectedValue = 10;
            int actualValue = card.NumberValue;

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
