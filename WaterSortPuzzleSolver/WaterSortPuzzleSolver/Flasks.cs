﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzleSolver
{
    public class Flasks : List<List<int>>
    {
        public Flasks(Flasks t) : base()
        {
            for (int i = 0; i < t.Count; i++)
            {
                Add(new List<int>());
                for (int j = 0; j < t[i].Count; j++)
                {
                    this[i].Add(t[i][j]);
                }
            }
        }
        public Flasks() : base()
        {

        }
    }



    public class FlasksStand
    {
        int maxColors;

        public Flasks flasksState;

        public List<(int, int)> path;

        public bool Transfer(int from, int to)
        {
            if (flasksState[from].Count > 0 && flasksState[to].Count < maxColors)
            {
                int t = flasksState[from].Last();
                flasksState[to].Add(t);
                flasksState[from].RemoveAt(flasksState[from].Count - 1);
                path.Add((from, to));
                return true;
            }
            return false;
        }


        public void InitializationRandom()
        {
            //var random = new Random();

            //int flasksCount = random.Next(2, 8);
            //for (int i = 0; i < flasksCount; i++)
            //{

            //    flasksState.Add(new List<int>());

            //    int flaskCount = random.Next(0, maxColors + 1);
            //    for (int j = 0; j < flaskCount; j++)
            //    {
            //        flasksState[i].Add(random.Next(1,8));
            //    }
            //}

            flasksState = new Flasks() { new List<int>() { 1, 2 }, new List<int>() { 4, 2 }, new List<int>() { 1 }, new List<int>() { 1 } };
        }

        public void Print()
        {
            for (int i = 0; i < flasksState.Count; i++)
            {
                for (int j = 0; j < flasksState[i].Count; j++)
                {
                    Console.Write(flasksState[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        public FlasksStand(int maxColors)
        {
            this.maxColors = maxColors;
            flasksState = new Flasks();
            path = new List<(int, int)>();
        }

        public FlasksStand(FlasksStand flasks)
        {
            this.maxColors = flasks.maxColors;
            flasksState = new Flasks();
            for (int i = 0; i < flasks.flasksState.Count; i++)
            {
                flasksState.Add(new List<int>());
                for (int j = 0; j < flasks.flasksState[i].Count; j++)
                {
                    flasksState[i].Add(flasks.flasksState[i][j]);
                }

            }
            path = new List<(int, int)>();
            for (int i = 0; i < flasks.path.Count; i++)
            {
                path.Add(flasks.path[i]);
            }
        }
    }
}
