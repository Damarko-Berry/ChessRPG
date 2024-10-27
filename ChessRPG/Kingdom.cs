using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ChessRPG
{
    public class Kingdom
    {
        public Race race;
        public List<Village> Villages;
        public Kingdom() { }
        public Statistics KingdomStatistics=> new Statistics(Villages.ToArray());

        public void Add(Village item)
        {
            Team Guard = new Team(race,KingdomStatistics);
            item.team = Guard;
            ((ICollection<Village>)Villages).Add(item);
        }

        public bool Contains(Village item)
        {
            return ((ICollection<Village>)Villages).Contains(item);
        }

        public bool Remove(Village item)
        {
            return ((ICollection<Village>)Villages).Remove(item);
        }
    }

    public readonly struct Statistics
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
        public readonly Abundance TotalResources => (Abundance)RS.Mean;
        public readonly Stat RS;
        public Statistics(Village[] villages)
        {
            RS = new Stat();
            Population = 16;
            Fresh_Water = 16;
            Wood = 16;
            Food = 16;
            Metals = 16;
            for (int i = 0; i < villages.Length; i++)
            {
                Population += villages[i].population;
                RS.Add((int)villages[i].AmountAvailible);
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
