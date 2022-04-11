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
        public int maxColorSize;
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

        public int ColorTowers(int index)
        {
	        if (this[index].Count == 0) {
                return 0;
	        }
            int count = 1;
           
            for (int i = 1; i < this[index].Count; i++)
            {
                if (this[index][i] != this[index][i - 1])
                {
                    count++;
                }
            }
            return count;
        }

        public bool FlaskIsTerminal(int i)
        {
            if (this[i].Count == 0)
                return true;
            if (this[i].Count != maxColorSize)
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
       
        int maxFlaskSize;

        public Flasks flasksState;
        public int stepToReach;
        public (int, int) lastTransfer = (0,0);

        public bool Transfer(int from, int to)
        {
            if (flasksState[from].Count > 0 && flasksState[to].Count < maxFlaskSize)
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

            if (flasksState[from].Count > 0 && flasksState[to].Count < maxFlaskSize)
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

                //for(; colorInTower > 1; colorInTower--)
                //{
                //    if (flasksState[to].Count + colorInTower <= maxFlaskSize)
                //        break;
                //}
                if (flasksState[to].Count + colorInTower > maxFlaskSize)
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
                if (!(flasksState.FlaskIsTerminal(i)))
                    return false;
            
            for (int i = 0; i < this.flasksState.Count; i++)
                if (flasksState[i].Count == 0)
                    continue;
                else
                    for (int j = 0; j < this.flasksState.Count; j++)
                        if (i != j  && flasksState[j].Count != 0 && flasksState[i][0] == flasksState[j][0])
                            return false;
            return true;
        }
        
        public (FlasksStand, int, int) ReachNextStand(int startfrom, int startto)
        {
            FlasksStand newStand = new FlasksStand(this);
            for (; startfrom < this.flasksState.Count; startfrom++)
            {
                for (; startto < this.flasksState.Count; startto++)
                {
                    if (startfrom == startto
                        || (flasksState.ColorTowers(startto) == 1 && flasksState[startfrom].Count == 0)
                        || (flasksState.ColorTowers(startfrom) == 1 && flasksState[startto].Count == 0))
                        continue;
                    if (newStand.TransferTower(startfrom, startto))
                    {
                        return (newStand, startfrom, startto + 1);
                    }
                }
                startto = 0;
            }
            return (null, -1, -1);
        }
        public void InitializationRandom(int flasksCount, int maxFlaskSize, int flaskSize, int emptyFlasks)
        {
            this.maxFlaskSize = maxFlaskSize;
            flasksState = new Flasks();
            
           
            for (int i = 0; i < flasksCount; i++)
            {
                flasksState.Add(new Collection<int>());
                for (int j = 0; j < flaskSize; j++)
                {
                    flasksState[i].Add(i+1);
                }
            }

            for (int i = 0; i < emptyFlasks; i++)
                flasksState.Add(new Collection<int>());

            var random = new Random();
            for (int i = 0; i < 1000000;)
            {
                if (this.Transfer(random.Next(0, this.flasksState.Count), random.Next(0, this.flasksState.Count)))
                {
                    i++;
                }
            }

            //flasksState = new Flasks() {
            //    new Collection<int>(){ 1,2,3,4},
            //    new Collection<int>(){ 5,6,7,8},
            //    new Collection<int>(){ 7,5,8,4},
            //    new Collection<int>(){ 9,1,10,10},
            //    new Collection<int>(){ 11,6,6,5},
            //    new Collection<int>(){ 11,1,4,3},
            //    new Collection<int>(){ 3,4,12,7},
            //    new Collection<int>(){ 5,7,2,12},
            //    new Collection<int>(){ 3,12,6,1},
            //    new Collection<int>(){ 11,8,9,10},
            //    new Collection<int>(){ 11,10,2,9},
            //    new Collection<int>(){ 9,12,2,8},
            //    new Collection<int>(),
            //    new Collection<int>()
            //};
            flasksState.maxColorSize = flaskSize;
            stepToReach = 0;
           
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

        public FlasksStand()
        {
        }
        
        public FlasksStand(FlasksStand flasksStand)
        {
            flasksState = new Flasks();

            for (int i = 0; i < flasksStand.flasksState.Count; i++)
            {
                  flasksState.Add(new Collection<int>());
                  for (int j = 0; j < flasksStand.flasksState[i].Count; j++)
                  {
                       flasksState[i].Add(flasksStand.flasksState[i][j]);
                  }
            }
           
            flasksState.maxColorSize = flasksStand.flasksState.maxColorSize;
            maxFlaskSize = flasksStand.maxFlaskSize;
            stepToReach = flasksStand.stepToReach;
        }
    }
}
