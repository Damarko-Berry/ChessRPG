using System;
using System.Collections.Generic;
using System.Text;

namespace ChessRPG
{
    public class Village
    {
        public Team team = null;
        public int population;
        public Resource resourceAvailible;
        public Abundance AmountAvailible;
        public int Availible => AmountAvailible switch
        {
            Abundance.Low => 100,
            Abundance.Medium => 300, 
            Abundance.High => 600,
        };
        public Village() { }
    }
}
