using System;
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
        {////!!!!  не паралелится потому что обращаются к i and j вместе короче кринге
            for (int i = 0; i < t.Count; i++)
            //Parallel.For(0, t.Count, i =>
            {
                Add(new Collection<int>());
                for (int j = 0; j < t[i].Count; j++)
                //Parallel.For(0, t[i].Count, j =>
                {
                     this[i].Add(t[i][j]);
                }
                //);
            }
            //);
        }
        public int ColorTowers(int index)
        {
	        if (this[index].Count == 0) {
                return 0;
	        }
            int count = 1;
            ////!!!!
            //for (int i = 1; i < this[index].Count; i++)
            Parallel.For(1, this[index].Count, i =>
            {
                if (this[index][i] != this[index][i - 1])
                {
                    count++;
                }
            });
            return count;
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
        public (int, int) lastTransfer = (0,0);

        public bool Transfer(int from, int to)
        {
            if (flasksState[from].Count > 0 && flasksState[to].Count < maxColors)
            {
                int t = flasksState[from].Last();
                flasksState[to].Add(t);
                flasksState[from].RemoveAt(flasksState[from].Count - 1);
                stepToReach++;
                lastTransfer = (from,to);
                return true;
            }
            return false;
        }

        public bool IsTerminal()
        {
            for (int i = 0; i < this.flasksState.Count; i ++)
            //Parallel.For(0, this.flasksState.Count, i => 
            //{
                for (int j = 1; j < this.flasksState[i].Count; j++)
                    if (this.flasksState[i][j] != this.flasksState[i][j - 1])
                        return false;
            //});
            for (int i = 0; i < this.flasksState.Count; i++)
                if (flasksState[i].Count == 0)
                    continue;
                else
                    for (int j = 0; j < this.flasksState.Count; j++)
                        if (i != j  && flasksState[j].Count != 0 && flasksState[i][0] == flasksState[j][0])
                            return false;
            return true;
        }
        ////!!!!////!!!!
        public (FlasksStand, int, int) ReachNextStand(int startfrom, int startto)
        {
            FlasksStand newStand = new FlasksStand(this);
            for (; startfrom < this.flasksState.Count; startfrom++)
            {
                for (; startto < this.flasksState.Count; startto++)
                {
                    if (startfrom == startto)
                        continue;
                    if (newStand.Transfer(startfrom, startto))
                    {
                        return (newStand, startfrom, startto + 1);
                    }
                }
                startto = 0;
            }
            return (null, -1, -1);
        }
        public void InitializationRandom()
        {
            var random = new Random();

            int flasksCount = random.Next(2, 50);
            //Parallel.For(0, flasksCount, i =>
            for (int i = 0; i < flasksCount; i++)
            {

                flasksState.Add(new Collection<int>());

                int flaskCount = random.Next(0, maxColors + 1);
                //Parallel.For(0, flasksCount, i =>
                for (int j = 0; j < flaskCount; j++)
                {
                    flasksState[i].Add(random.Next(1, 50));
                }
                // );
            }
            //);

            //flasksState = new Flasks() { new Collection<int>() { 3,3,1,4}, new Collection<int>() { 4 }, new Collection<int>() { 4 }, new Collection<int>() { 4,3,1 }, new Collection<int>() { 4 } };
        }
        ////!!!!
        public void Print()
        {   //принт паралелить .....
            //Parallel.For(0, flasksState.Count, i =>
            for (int i = 0; i < flasksState.Count; i++)
            {
                //Parallel.For(0, flasksState[i].Count, j =>
               for (int j = 0; j < flasksState[i].Count; j++)
                {
                    Console.Write(flasksState[i][j] + " ");
                }
               // );
                Console.WriteLine();
            }
            //);
        }

        public FlasksStand(int maxColors)
        {
            this.maxColors = maxColors;
            flasksState = new Flasks();
            stepToReach = 0;
        }
        ////!!!!////!!!!
        public FlasksStand(FlasksStand flasksStand)
        {
            this.maxColors = flasksStand.maxColors;
            flasksState = new Flasks();
            for (int i = 0; i < flasksStand.flasksState.Count; i++)
            //Parallel.For(0, flasksStand.flasksState.Count, i =>
            {
                  flasksState.Add(new Collection<int>());
                  for (int j = 0; j < flasksStand.flasksState[i].Count; j++)
                  //Parallel.For(0, flasksStand.flasksState[i].Count, j =>
                  {
                       flasksState[i].Add(flasksStand.flasksState[i][j]);
                  }
                  //);
            }
           // );
            stepToReach = flasksStand.stepToReach;
        }
    }
}
