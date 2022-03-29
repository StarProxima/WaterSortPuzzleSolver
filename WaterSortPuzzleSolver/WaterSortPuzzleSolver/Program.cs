using System;
using System.Collections;

namespace WaterSortPuzzleSolver
{

    class Flasks
    {
       int maxColors = 4;

        List<List<int>> list;

        List<(int, int)> path;

        public void transfer(int from, int to)
        {
            if (list[from].Count > 0 && list[to].Count < maxColors)
            {
                int t = list[from].Last();
                list[to].Add(t);
                list[from].Remove(t);
                path.Add((from, to));
            }
            else Console.WriteLine("invalid transfer");
        }

        public Flasks(int maxColors)
        {
            this.maxColors = maxColors;
            list = new List<List<int>>();
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