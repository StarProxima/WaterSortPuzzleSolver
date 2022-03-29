using System;
using System.Collections;

namespace WaterSortPuzzleSolver
{
    public class Hashtable
    {
        System.Collections.Hashtable hashtable;

        public Hashtable()
        {
            hashtable = new System.Collections.Hashtable();
        }

    }
    public class Flasks
    {
        int maxColors = 4;

        public List<List<int>> flasks;

        public List<(int, int)> path;

        public void transfer(int from, int to)
        {
            if (flasks[from].Count > 0 && flasks[to].Count < maxColors)
            {
                int t = flasks[from].Last();
                flasks[to].Add(t);
                flasks[from].Remove(t);
                path.Add((from, to));
            }
            else Console.WriteLine("invalid transfer");
        }

        public Flasks(int maxColors)
        {
            this.maxColors = maxColors;
            flasks = new List<List<int>>();
            path = new List<(int, int)>();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}