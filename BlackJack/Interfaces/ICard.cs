using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.Interfaces
{
    public interface ICard
    {
        string Name { get; set; }
        Suit Suit { get; set; }
        char SuitSymbol { get; set; }
        int NumberValue { get; set; }

        void DisplayCard();

        bool IsAce();

        bool IsTenCard();
    }
}
