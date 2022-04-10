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
        public int maxTowerColor;
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

        public bool FlaskIsTerminal(int i)
        {
            if (this[i].Count == 0)
                return true;
            if (this[i].Count != maxTowerColor)
                return false;
            for (int j = 1; j < this[i].Count; j++)
                if (this[i][j] != this[i][j - 1])
                    return false;
            return true;
        }
        public Flasks() : base()
        {

        }
    }



    public class FlasksStand
    {
        int maxColors;
        int maxTowerColor;

        public Flasks flasksState;
        public int stepToReach;
        public (int, int) lastTransfer = (0,0);

        public void BackTransfer(int from, int to)
        {
            int t = flasksState[from].Last();
            flasksState[to].Add(t);
            flasksState[from].RemoveAt(flasksState[from].Count - 1);
            stepToReach--;
            lastTransfer = (-1, -1);
        }
        public bool Transfer(int from, int to)
        {
            if (flasksState[from].Count > 0 && flasksState[to].Count < maxTowerColor)
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
        public bool TransferTower(int from, int to)
        {

            if (flasksState[from].Count > 0 && flasksState[to].Count < maxTowerColor)
            {
                int colorInTower = 1;
                int t = flasksState[from].Last();
                for (int i = flasksState[from].Count-2; i >= 0; i--)
                {
                    if (flasksState[from][i] == t)
                        colorInTower++;
                    else
                        break;
                }
                if (flasksState[to].Count + colorInTower > maxTowerColor)
                    return false;
                for (int i = 0; i < colorInTower; i++)
                {
                    flasksState[to].Add(t);
                    flasksState[from].RemoveAt(flasksState[from].Count - 1);
                }

                stepToReach++;
                lastTransfer = (from, to);
                return true;
            }
            return false;
        }

        public bool IsTerminal()
        {
            for (int i = 0; i < this.flasksState.Count; i++)
                //Parallel.For(0, this.flasksState.Count, i => 
                //{
                if (!(flasksState.FlaskIsTerminal(i)))
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
        public (int, int) ReachNextStand(int startfrom, int startto)
        {
            for (; startfrom < this.flasksState.Count; startfrom++)
            {
                for (; startto < this.flasksState.Count; startto++)
                {
                    if (startfrom == startto

                        || (flasksState.ColorTowers(startto) == 1 && flasksState[startfrom].Count == 0)
                        || (flasksState.ColorTowers(startfrom) == 1 && flasksState[startto].Count == 0))
                        continue;
                    if (this.Transfer(startfrom, startto))
                    {
                        return (startfrom, startto + 1);
                    }
                }
                startto = 0;
            }
            return (-1, -1);
        }
        public void InitializationRandom(int flasksCount, int emptyFlasks, int newMaxTowerColor)
        {
            maxTowerColor = newMaxTowerColor;
            maxColors = flasksCount;
            
            flasksState.maxTowerColor = maxTowerColor;
            var random = new Random();

            for (int i = 0; i < flasksCount; i++)
            {
                flasksState.Add(new Collection<int>());
                for (int j = 0; j < maxTowerColor; j++)
                {
                    flasksState[i].Add(i+1);
                }

            }

            for (int i = 0; i < emptyFlasks; i++)
                flasksState.Add(new Collection<int>());

            for (int i = 0; i < 20;)
            {
                if (this.Transfer(random.Next(0, this.flasksState.Count), random.Next(0, this.flasksState.Count)))
                {
                    i++;
                }
            }
            stepToReach = 0;
            

            //for (int i = 0; i < flasksCount; i++)
            //{

            //    flasksState.Add(new Collection<int>());

            //    int flaskCount = random.Next(0, maxColors);
            //    for (int j = 0; j < flaskCount; j++)
            //    {
            //        flasksState[i].Add(random.Next(1, maxColors + 1));
            //    }

            //}
            //);
            //flasksState.Add(new Collection<int>());
            /*
            flasksState = new Flasks() { 
                new Collection<int>() { 2,3},
                new Collection<int>() { 3,2 },
                new Collection<int>() { 4,1,1 },
                new Collection<int>() { 4,2,3,4 },
                new Collection<int>() {  1 },
                new Collection<int>() {  } 
            };
            */
            /*
            flasksState = new Flasks() {
                new Collection<int>() {4,5,3,1,5},
                new Collection<int>() {3,1,3,4,6},
                new Collection<int>() {4,3,4,5},
                new Collection<int>() {2,6,2,1,6},
                new Collection<int>() {1,1,6,6},
                new Collection<int>() {4,2,5,5,3},
                new Collection<int>() { 2,2 },
                new Collection<int>() {  }
            };
            */
            //
            //flasksState = new Flasks() {
            //    new Collection<int>() { 1,1,2,3},
            //    new Collection<int>() { 4,3,5,2 },
            //    new Collection<int>() { 6,1,7,8 },
            //    new Collection<int>() { 9,9,5,3 },
            //    new Collection<int>() { 7,4,4,5 },
            //    new Collection<int>() { 5,9,2,6},
            //    new Collection<int>() { 8,7,2,8},
            //    new Collection<int>() { 7,1,4,8},
            //    new Collection<int>() { 6,3,6,9 },
            //};
        }

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
            flasksState.maxTowerColor = flasksStand.maxTowerColor;
            maxTowerColor = flasksStand.maxTowerColor;
            stepToReach = flasksStand.stepToReach;
        }
    }
}
