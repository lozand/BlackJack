using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackJack;

namespace UnitTests
{
    [TestClass]
    public class HandTest
    {
        [TestMethod]
        public void Total_BlackJack_21()
        {
            Hand hand = new Hand();
            hand.Cards.Add(new Card("A", Suit.Clubs));
            hand.Cards.Add(new Card("J", Suit.Hearts));

            int expectedTotal = 21;
            int actualTotal = hand.Total();

            Assert.AreEqual(expectedTotal, actualTotal);
        }

        [TestMethod]
        public void Total_KingAndJack_20()
        {
            Hand hand = new Hand();
            hand.Cards.Add(new Card("K", Suit.Clubs));
            hand.Cards.Add(new Card("10", Suit.Hearts));

            int expectedTotal = 20;
            int actualTotal = hand.Total();

            Assert.AreEqual(expectedTotal, actualTotal);            
        }

        [TestMethod]
        public void Total_QueenAndJack_20()
        {
            Hand hand = new Hand();
            hand.Cards.Add(new Card("Q", Suit.Clubs));
            hand.Cards.Add(new Card("J", Suit.Hearts));

            int expectedTotal = 20;
            int actualTotal = hand.Total();

            Assert.AreEqual(expectedTotal, actualTotal);
        }

        [TestMethod]
        public void Total_2And3_5()
        {
            Hand hand = new Hand();
            hand.Cards.Add(new Card("2", Suit.Clubs));
            hand.Cards.Add(new Card("3", Suit.Hearts));

            int expectedTotal = 5;
            int actualTotal = hand.Total();

            Assert.AreEqual(expectedTotal, actualTotal);
        }

        [TestMethod]
        public void Total_7And8_15()
        {
            Hand hand = new Hand();
            hand.Cards.Add(new Card("7", Suit.Clubs));
            hand.Cards.Add(new Card("8", Suit.Hearts));

            int expectedTotal = 15;
            int actualTotal = hand.Total();

            Assert.AreEqual(expectedTotal, actualTotal);
        }

        [TestMethod]
        public void Total_2And3AndTenAnd5_20()
        {
            Hand hand = new Hand();
            hand.Cards.Add(new Card("2", Suit.Clubs));
            hand.Cards.Add(new Card("3", Suit.Hearts));
            hand.Cards.Add(new Card("10", Suit.Clubs));
            hand.Cards.Add(new Card("5", Suit.Diamonds));

            int expectedTotal = 20;
            int actualTotal = hand.Total();

            Assert.AreEqual(expectedTotal, actualTotal);
        }

        [TestMethod]
        public void Total_10And6AndJack_26()
        {
            Hand hand = new Hand();
            hand.Cards.Add(new Card("10", Suit.Clubs));
            hand.Cards.Add(new Card("6", Suit.Spades));
            hand.Cards.Add(new Card("J", Suit.Hearts));
            
            int expectedTotal = 26;
            int actualTotal = hand.Total();

            Assert.AreEqual(expectedTotal, actualTotal);
        }

        [TestMethod]
        public void Total_AceAnd10_AceEqualToEleven()
        {
            Hand hand = new Hand();
            hand.Cards.Add(new Card("10", Suit.Clubs));
            hand.Cards.Add(new Card("A", Suit.Spades));

            int expectedAceTotal = 11;
            int actualAceTotal = hand.Cards.Find(a => a.Name == "A").NumberValue;

            Assert.AreEqual(expectedAceTotal, actualAceTotal);
        }

        [TestMethod]
        public void Total_AceAnd7_AceEqualToEleven()
        {
            Hand hand = new Hand();
            hand.Cards.Add(new Card("7", Suit.Clubs));
            hand.Cards.Add(new Card("A", Suit.Spades));

            int expectedAceTotal = 11;
            int actualAceTotal = hand.Cards.Find(a => a.Name == "A").NumberValue;

            Assert.AreEqual(expectedAceTotal, actualAceTotal);
        }

        [TestMethod]
        public void Total_AceAnd7And10_AceEqualToOne()
        {
            Hand hand = new Hand();
            hand.Cards.Add(new Card("7", Suit.Clubs));
            hand.Cards.Add(new Card("A", Suit.Spades));
            hand.Cards.Add(new Card("K", Suit.Clubs));

            int expectedAceTotal = 1;
            int actualAceTotal = hand.Cards.Find(a => a.Name == "A").NumberValue;

            Assert.AreEqual(expectedAceTotal, actualAceTotal);
        }

        [TestMethod]
        public void Total_AceAnd7And10And7_AceEqualToOne()
        {
            Hand hand = new Hand();
            hand.Cards.Add(new Card("7", Suit.Clubs));
            hand.Cards.Add(new Card("A", Suit.Spades));
            hand.Cards.Add(new Card("K", Suit.Clubs));
            hand.Cards.Add(new Card("7", Suit.Hearts));

            int expectedAceTotal = 1;
            int actualAceTotal = hand.Cards.Find(a => a.Name == "A").NumberValue;

            Assert.AreEqual(expectedAceTotal, actualAceTotal);
        }

    }
}
