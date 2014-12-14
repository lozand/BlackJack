using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackJack;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class TableTest
    {
        [TestMethod]
        public void Table_DealerIsPresent_Success()
        {
            Table tbl = new Table(new Deck(3), new List<Player> { new Player("Tester", 500) });
            var actualPlayers = tbl.Players.Count;
            var expectedPlayers = 2;

            Assert.AreEqual(actualPlayers, expectedPlayers);
        }
    }
}
