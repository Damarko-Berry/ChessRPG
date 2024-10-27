using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRPG
{
    public struct Stat : ICollection<double>
    {
        private double[] Data;
        public double Range => Data.Max() - Data.Min();
        public double Mean => Data.Average();
        public double Median
        {
            get
            {
                Array.Sort(Data);
                int size = Count;

                if (size % 2 == 0)
                {
                    int mid1 = size / 2;
                    int mid2 = mid1 - 1;
                    return (Data[mid1] + Data[mid2]) / 2.0;
                }
                else
                {
                    int mid = size / 2;
                    return Data[mid];
                }
            }
        }
        public double Mode => Data.GroupBy(n => n)
                    .OrderByDescending(g => g.Count())
                    .ThenBy(g => g.Key)
                    .Select(g => g.Key)
                    .FirstOrDefault();
        public double Total => Data.Sum();
        public int Count => Data.Length;
        public double Least => Data.Min();
        public double this[int index] => Data[index];
        public bool IsReadOnly => ((ICollection<double>)Data).IsReadOnly;

        public void Add(double item)
        {
            ((ICollection<double>)Data).Add(item);
        }

        public void Clear()
        {
            ((ICollection<double>)Data).Clear();
        }

        public bool Contains(double item)
        {
            return ((ICollection<double>)Data).Contains(item);
        }

        public void CopyTo(double[] array, int arrayIndex)
        {
            ((ICollection<double>)Data).CopyTo(array, arrayIndex);
        }

        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)Data).GetEnumerator();
        }

        public bool Remove(double item)
        {
            return ((ICollection<double>)Data).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }
}
