using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ChessRPG
{
    [Serializable]
    public class Kingdom
    {
        public Race race;
        public List<Village> Villages = new List<Village>();
        public Kingdom() { }
        public Statistics KingdomStatistics=> new Statistics(Villages.ToArray());
        public void Grow()
        {
            for (int i = 0; i < Villages.Count; i++)
            {
                Random random = new Random();
                switch (KingdomStatistics.SurvivalChances)
                {
                    case Abundance.Low:
                        Villages[i].population += random.Next(-8, 0);
                        break;
                    case Abundance.Medium:
                        Villages[i].population += random.Next(-4, 4);
                        break;
                    case Abundance.High:
                        Villages[i].population += random.Next(4, 8);
                        break;
                }
                Villages[i].population += (int)KingdomStatistics.SurvivalChances;
            }
        }
        public void Add(Village item)
        {
            Team Guard = new Team(race,KingdomStatistics);
            item.team = Guard;
            item.population = 16;
            Villages.Add(item);
        }
        public bool Remove(Village item)
        {
            return ((ICollection<Village>)Villages).Remove(item);
        }
    }

    public struct Statistics
    {
        public readonly int Population,Fresh_Water,
        Wood,
        Food,
        Metals;
        public readonly Abundance Fresh_Waterlvl
        {
            get
            {
                if(Fresh_Water> Population * 1.5)
                {
                    return Abundance.High;
                }else if(Fresh_Water>= Population)
                {
                    return Abundance.Medium;
                }
                    
                return Abundance.Low;
            }
        }
        public readonly Abundance Woodlvl
        {
            get
            {
                if (Wood > Population * 1.5)
                {
                    return Abundance.High;
                }
                else if (Wood >= Population)
                {
                    return Abundance.Medium;
                }

                return Abundance.Low;
            }
        }
        public readonly Abundance Foodlvl
        {
            get
            {
                if (Food > Population * 1.5)
                {
                    return Abundance.High;
                }
                else if (Food>= Population)
                {
                    return Abundance.Medium;
                }

                return Abundance.Low;
            }
        }
        public readonly Abundance Metalslvl
        {
            get
            {
                if (Metals > Population * 1.5)
                {
                    return Abundance.High;
                }
                else if (Metals >= Population)
                {
                    return Abundance.Medium;
                }

                return Abundance.Low;
            }
        }
        public readonly Abundance SurvivalChances=> (Abundance)new Stat
                {
                    (int)Foodlvl,
                    (int)Woodlvl,
                    (int)Fresh_Waterlvl
                }.Mean;

        public Statistics(Village[] villages)
        {

            Population = 0;
            Fresh_Water = 16;
            Wood = 16;
            Food = 16;
            Metals = 16;
            for (int i = 0; i < villages.Length; i++)
            {
                Population += villages[i].population;
                switch (villages[i].resourceAvailible)
                {
                    case Resource.Wood:
                        Wood += villages[i].Availible;
                        break;
                    case Resource.Fresh_Water:
                        Fresh_Water += villages[i].Availible;
                        break;
                    case Resource.Food:
                        Food += villages[i].Availible;
                        break;
                    case Resource.Metals:
                        Metals += villages[i].Availible;
                        break;
                }
            }
        }
    }
}
