using System;
using System.Collections.Generic;
using System.Text;

namespace ChessRPG
{
    [Serializable]
    public class Village
    {
        public int id;
        public Team team = null;
        public int population;
        public Resource resourceAvailible;
        public Abundance AmountAvailible;
        public int Availible => AmountAvailible switch
        {
            Abundance.Low => 24,
            Abundance.Medium => 32, 
            Abundance.High => 64,
        };
        public Village() { }
    }
}
