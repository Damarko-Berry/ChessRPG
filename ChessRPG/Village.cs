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
            Abundance.Low => 8,
            Abundance.Medium => 16, 
            Abundance.High => 32,
        };
        public Village() { }
    }
}
