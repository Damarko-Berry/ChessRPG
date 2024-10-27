using System;

namespace ChessRPG
{
    [Serializable]
    public struct Upgrades
    {
        public int addedstr;
        public int addedspd;
        public int addedDef;
        public int addedHP;
        public Upgrades(Statistics stats)
        {
            addedHP = (int)stats.Foodlvl;
            addedstr = (int)stats.Metalslvl;
            addedspd = (int)stats.Fresh_Waterlvl;
            addedDef = (int)stats.Woodlvl;
        }
    }
}
