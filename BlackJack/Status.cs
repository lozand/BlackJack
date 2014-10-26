using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public enum Status
    {
        Betting = 0,
        InPlay = 1,
        Won = 2,
        Lost = 3,
        Bust = 4,
        Push = 5,
        BlackJack = 6,
        NotPlaying = 7
    }
}
