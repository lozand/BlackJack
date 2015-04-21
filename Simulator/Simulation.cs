using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack;
using BlackJack.Interfaces;

namespace Simulator
{
    public class Simulation
    {
        public Simulation()
        {
            IDisplay display = new Display(100);
            List<Player> players = new List<Player>();
            players.Add(new Player("Daniel", 500, true));
            players.Add(new Player("Matt", 500, true));
            players.Add(new Player("Ruben", 500, true));
            Table table = new Table(new Deck(6, display), players);
        }
    }
}
