using BlackJack.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Input : IInput
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }

        public double GetMoneyInput()
        {
            return 0.00;
        }

        public int GetNumberInput()
        {
            return 0;
        }
    }
}
