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
       
        int maxTowerColor;

        public Flasks flasksState;
        public int stepToReach;
        public (int, int) lastTransfer = (0,0);

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
            maxTowerColor = maxFlaskSize;
            flasksState.maxColorSize = flaskSize;
           
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

        public FlasksStand(int maxColors)
        {
            
            flasksState = new Flasks();
            stepToReach = 0;
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
            maxTowerColor = flasksStand.maxTowerColor;
            stepToReach = flasksStand.stepToReach;
        }
    }
}
