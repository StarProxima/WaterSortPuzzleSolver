using System;
using System.Collections;

namespace WaterSortPuzzleSolver
{

    class Flasks
    {
       int maxColors = 4;

        List<List<int>> list;

        List<(int, int)> path;

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