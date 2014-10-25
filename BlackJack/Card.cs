using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Card
    {
        public Card(string name, Suite suite)
        {
            this.Name = name;
            this.Suite = suite;
            this.NumberValue = SetNumberValue(name);
        }
        
        public string Name { get; set; }
        public Suite Suite { get; set; }
        public int NumberValue { get; set; }

        /// <summary>
        /// Display function. Shows card in format "2 - Hearts" or "X - Clubs"
        /// </summary>
        public void DisplayCard()
        {
            Console.WriteLine("{0} - {1}", Name, Suite.ToString());
        }

        public bool IsAce()
        {
            return Name == "A";
        }

        public bool IsTenCard()
        {
            return Name == "X" || Name == "J" || Name == "Q" || Name == "K";
        }

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
    }
}
