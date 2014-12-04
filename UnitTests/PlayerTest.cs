using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackJack;

namespace UnitTests
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void BetCash_WholeNumber_Success()
        {
            Player myPlayer = new Player("Tester", 500);

            myPlayer.BetCash(200);

            double expectedRemainingAmount = 300;
            double actualRemainingAmount = myPlayer.Cash;

            double expectedBetAmount = 200;
            double actualBetAmount = myPlayer.Bet;

            Assert.AreEqual(expectedRemainingAmount, actualRemainingAmount);
            Assert.AreEqual(expectedBetAmount, actualBetAmount);
        }

        [TestMethod]
        public void BetCash_NegativeNumberUsesAbsoluteValue_Success()
        {
            Player player = new Player("Tester", 500);

            player.BetCash(-200);

            double expectedRemainingAmount = 300;
            double actualRemainingAmount = player.Cash;

            double expectedBetAmount = 200;
            double actualBetAmount = player.Bet;

            Assert.AreEqual(expectedRemainingAmount, actualRemainingAmount);
            Assert.AreEqual(expectedBetAmount, actualBetAmount);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BetCash_ZeroValue_ThrowsError()
        {
            Player player = new Player("Tester", 500);

            player.BetCash(0);
        }

        [TestMethod]
        public void ClearBet_BetNotEqualToZero_Success()
        {
            Player player = new Player("Tester", 500);

            player.Bet = 150;

            double expectedValue = 150;
            double actualValue = player.Bet;

            // This assert may not be necessary since it is tesed in another case
            Assert.AreEqual(expectedValue, actualValue);

            player.ClearBet();

            expectedValue = 0;
            actualValue = player.Bet;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void ClearBet_BetEualToZero_Success()
        {
            Player player = new Player("Tester", 500);

            player.Bet = 0;

            player.ClearBet();

            double expectedValue = 0;
            double actualValue = player.Bet;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void ClearHand_SingleHand2Cards_Success()
        {
            Player player = new Player("Tester", 500);

            player.Hands.First().Cards.Add(new Card("A", Suit.Spades));
            player.Hands.First().Cards.Add(new Card("J", Suit.Hearts));

            int numberOfHandsActual = player.NumberOfHands;
            int numberOfHandsExpected = 1;

            Assert.AreEqual(numberOfHandsActual, numberOfHandsExpected);

            int numberOfCardsInHandActual = player.Hands.First().Cards.Count;
            int numberOfCardsInHandExpected = 2;

            Assert.AreEqual(numberOfCardsInHandActual, numberOfCardsInHandExpected);

            player.ClearHand();

            numberOfCardsInHandActual = player.Hands.First().Cards.Count;
            numberOfCardsInHandExpected = 0;

            Assert.AreEqual(numberOfCardsInHandActual, numberOfCardsInHandExpected);
        }

        [TestMethod]
        public void ClearHand_2Hand_Success()
        {
            Player player = new Player("Tester", 500);

            player.Hands[0].Cards.Add(new Card("A", Suit.Spades));
            player.Hands[0].Cards.Add(new Card("J", Suit.Hearts));

            player.Hands.Add(new Hand());

            player.Hands[1].Cards.Add(new Card("7", Suit.Diamonds));
            player.Hands[1].Cards.Add(new Card("10", Suit.Hearts));

            int numberOfHandsActual = player.NumberOfHands;
            int numberOfHandsExpected = 2;

            Assert.AreEqual(numberOfHandsActual, numberOfHandsExpected);

            player.ClearHand();

            int numberOfCardsInHandActual = player.Hands.First().Cards.Count;
            int numberOfCardsInHandExpected = 0;

            Assert.AreEqual(numberOfCardsInHandActual, numberOfCardsInHandExpected);

            numberOfHandsActual = player.NumberOfHands;
            numberOfHandsExpected = 1;

            Assert.AreEqual(numberOfHandsActual, numberOfHandsExpected);
        }

        [TestMethod]
        public void ClearHand_3Hand_Success()
        {
            Player player = new Player("Tester", 500);

            player.Hands[0].Cards.Add(new Card("A", Suit.Spades));
            player.Hands[0].Cards.Add(new Card("J", Suit.Hearts));

            player.Hands.Add(new Hand());

            player.Hands[1].Cards.Add(new Card("7", Suit.Diamonds));
            player.Hands[1].Cards.Add(new Card("10", Suit.Hearts));

            player.Hands.Add(new Hand());

            player.Hands[2].Cards.Add(new Card("2", Suit.Clubs));
            player.Hands[2].Cards.Add(new Card("9", Suit.Spades));

            int numberOfHandsActual = player.NumberOfHands;
            int numberOfHandsExpected = 3;

            Assert.AreEqual(numberOfHandsActual, numberOfHandsExpected);

            player.ClearHand();

            int numberOfCardsInHandActual = player.Hands.First().Cards.Count;
            int numberOfCardsInHandExpected = 0;

            Assert.AreEqual(numberOfCardsInHandActual, numberOfCardsInHandExpected);

            numberOfHandsActual = player.NumberOfHands;
            numberOfHandsExpected = 1;

            Assert.AreEqual(numberOfHandsActual, numberOfHandsExpected);
        }

        /*
        // TODO: 
         *  - Write cases for PushBet()
         *  - WRite cases for AwardBet()
         *  - Should a case be written for ShowPlayerCards()?
         *  - What about ShowPlayerCash()
         *  - What about NumberOfHands?
         */
    }
}
