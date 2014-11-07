using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public enum HandStatus
    {
        InPlay = 0,
        Push = 1,
        BlackJack = 2,
        Won = 3,
        Lost = 4,
        Bust = 5        
    }
}
