﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WaterSortPuzzleSolver
{
    public class Flasks : Collection<Collection<int>>
    {
        public Flasks(Flasks t) : base()
        {
            for (int i = 0; i < t.Count; i++)
            {
                Add(new Collection<int>());
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
        public int stepToReach;
        public pair<int, int> lastTransfer = make_pair(0,0);

        public bool Transfer(int from, int to)
        {
            if (flasksState[from].Count > 0 && flasksState[to].Count < maxColors)
            {
                int t = flasksState[from].Last();
                flasksState[to].Add(t);
                flasksState[from].RemoveAt(flasksState[from].Count - 1);
                stepToReach++;
                lastTransfer = make_pair(from, to);
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

            //    flasksState.Add(new Collection<int>());

            //    int flaskCount = random.Next(0, maxColors + 1);
            //    for (int j = 0; j < flaskCount; j++)
            //    {
            //        flasksState[i].Add(random.Next(1,8));
            //    }
            //}

            flasksState = new Flasks() { new Collection<int>() { 1, 2 }, new Collection<int>() { 4, 2 }, new Collection<int>() { 1 }, new Collection<int>() { 1 } };
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
            stepToReach = 0;
        }

        public FlasksStand(FlasksStand flasksStand)
        {
            this.maxColors = flasksStand.maxColors;
            flasksState = new Flasks();
            for (int i = 0; i < flasksStand.flasksState.Count; i++)
            {
                flasksState.Add(new Collection<int>());
                for (int j = 0; j < flasksStand.flasksState[i].Count; j++)
                {
                    flasksState[i].Add(flasksStand.flasksState[i][j]);
                }

            }
            stepToReach = flasksStand.stepToReach;
        }
    }
}
