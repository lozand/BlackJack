using BlackJack.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Card : ICard
    {
        public Card(string name, Suit suit)
        {
            Initialize(name, suit, new Display());
        }

        public Card(string name, Suit suit, IDisplay display)
        {
            Initialize(name, suit, display);
        }

        //public string Name()
        //{
        //    return cardName;
        //}
        
        public string Name { get; set; }
        public Suit Suit { get; set; }
        public char SuitSymbol { get; set; }
        public int NumberValue { get; set; }

        IDisplay show;

        /// <summary>
        /// Display function. Shows card in format "2 - Hearts" or "X - Clubs"
        /// </summary>
        public void DisplayCard()
        {
            show.Card(Name, Suit.ToString(), SuitSymbol.ToString());
        }

        /// <summary>
        /// Determines whether or not this card is an Ace
        /// </summary>
        /// <returns></returns>
        public bool IsAce()
        {
            return Name == "A";
        }

        /// <summary>
        /// Determines whether or not this cards is worth 10
        /// </summary>
        /// <returns></returns>
        public bool IsTenCard()
        {
            return NumberValue == 10;
        }

        private void Initialize(string name, Suit suit, IDisplay display)
        {
            this.Name = name;
            this.Suit = suit;
            this.NumberValue = SetNumberValue(name);
            this.SuitSymbol = SetSuitSymbol(suit);
            this.show = display;
        }


        /// <summary>
        /// Sets a number value to each card. 2-10 are set to the value on their card. J, Q, K are equal to 10 and A = 1 or 11. We may need to put rules to this, but not likely at the card level.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int SetNumberValue(string name)
        {
            int value = 0;
            switch(name)
            {
                case "X":
                case "J":
                case "Q":
                case "K":
                    value = 10;
                    break;
                case "A":
                    value = 11;
                    break;
                default:
                    value = Int32.Parse(name);
                    break;
            }
            return value;
        }

        /// <summary>
        /// Returns the ascii symbol for whichever suit is passed in. Will return 'N' for anything not a suit.
        /// </summary>
        /// <param name="suit"></param>
        /// <returns></returns>
        private char SetSuitSymbol(Suit suit)
        {
            switch (suit)
            {
                case Suit.Hearts:
                    return '\x03';
                case Suit.Clubs:
                    return '\x05';
                case Suit.Diamonds:
                    return '\x04';
                case Suit.Spades:
                    return '\x06';
                default:
                    return 'N';
            }
        }
    }
}
