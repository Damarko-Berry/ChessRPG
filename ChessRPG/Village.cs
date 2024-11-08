using System;
using System.Collections.Generic;
using System.Text;

namespace ChessRPG
{
    [Serializable]
    public class Village
    {
        public int id;
        public Team? team = null;
        public int population;
        public Resource resourceAvailible;
        public Abundance AmountAvailible;
        public int Availible => AmountAvailible switch
        {
            Abundance.Low => 32,
            Abundance.Medium => 64, 
            Abundance.High => 128,
        };
        public Village() { }
    }
}
