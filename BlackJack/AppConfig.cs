using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class AppConfig
    {
        public static int DealerStand = Int16.Parse(System.Configuration.ConfigurationManager.AppSettings["DealerStand"]);
        public static int CardsNecessaryToPlay = Int16.Parse(System.Configuration.ConfigurationManager.AppSettings["CardsNecessaryToPlay"]);
        public static double Payout = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["Payout"]);
        public static double BlackJackPayout = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["BlackJackPayout"]);
    }
}
