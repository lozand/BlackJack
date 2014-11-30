using System;
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
        /*
        // TODO: 
         *  - Finish writing cases for ClearBet()
         *  - Write cases for ClearHand()
         *  - Write cases for PushBet()
         *  - WRite cases for AwardBet()
         *  - Should a case be written for ShowPlayerCards()?
         *  - What about ShowPlayerCash()
         *  - What about NumberOfHands?
         */
    }
}
