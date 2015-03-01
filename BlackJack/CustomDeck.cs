using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackJack.Interfaces;
using System.Xml;

namespace BlackJack
{
    public class CustomDeck
    {
        //public CustomDeck()
        //{
        //    List<ICard> deck = new List<ICard>();
        //}

        internal static List<ICard> GetXml()
        {
            List<ICard> newDeck = new List<ICard>();
            XmlDocument xml = new XmlDocument();
            xml.Load("D:\\Libraries\\Documents\\Projects\\BlackJack\\BlackJack\\CustomDeck.xml");
            XmlNodeList xmlCards = xml.SelectNodes("/cards/card");
            foreach (XmlNode card in xmlCards)
            {
                ICard thisCard = new Card(card.InnerText.ToString(), Suit.Clubs);

                newDeck.Add(thisCard);
            }

            return newDeck;
        }
    }
}
