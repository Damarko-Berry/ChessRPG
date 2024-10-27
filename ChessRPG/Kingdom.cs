﻿using System;
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
        public void Grow()
        {
            for (int i = 0; i < Villages.Count; i++)
            {
                Villages[i].population += (int)KingdomStatistics.AverageAbundance;
            }
        }
        public void Add(Village item)
        {
            Team Guard = new Team(race,KingdomStatistics);
            item.team = Guard;
            Villages.Add(item);
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
        public readonly Abundance AverageAbundance{
            get
            {
                Stat RTS = new Stat();
                RTS.Add(Food);
                RTS.Add(Wood);
                RTS.Add(Metals);
                RTS.Add(Fresh_Water);
                return (Abundance)RTS.Mean;
            }
        }

        public Statistics(Village[] villages)
        {

            Population = 16;
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