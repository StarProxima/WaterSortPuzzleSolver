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
        
        public int ColorTowers(int index)
        {
	        if (this[index].Count == 0) {
                return 0;
	        }
            int count = 0;
            
            for(int i = 1; i < this[index].Count ; i++) 
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
            for (int j = 1; j < maxColorSize; j++)
                if (this[i][j] != this[i][j - 1])
                    return false;
            
            return true;
        }

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

        public bool GoFullPath(List<(int, int)> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                if (!TransferTower(path[i].Item1, path[i].Item2))
                    return false;
                //Print();
            }
                
            return true;


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
                
                //if (flasksState[to].Count + colorInTower > maxFlaskSize)
                //    for (; colorInTower > 1; colorInTower--)
                //    {
                //        if (colorInTower == 0) return false;
                //        if (flasksState[to].Count + colorInTower < maxFlaskSize) break;
                //    }
                
                if (flasksState[to].Count + colorInTower > maxFlaskSize)
                    return false;
                
                for (int i = 0; i < colorInTower; i++)
                {
                    flasksState[to].Add(t);
                    flasksState[from].RemoveAt(flasksState[from].Count - 1);
                    //if (flasksState[from].Count == 0)
                    //{
                    //    Console.WriteLine("colorInTower" + flasksState[from].Count);
                    //    Print();
                    //}

                }
                //if (flasksState[to].Count == maxFlaskSize)
                //{
                //    Console.WriteLine("colorInTower" + flasksState[from].Count);
                //    Print();
                //}
                stepToReach++;
                lastTransfer = (from, to);
                return true;
            }
            
            return false;
        }

        public bool IsTerminal()
        {
            for (int i = 0; i < this.flasksState.Count; i++)
                if (!flasksState.FlaskIsTerminal(i))
                    return false;

            for (int i = 0; i < this.flasksState.Count; i++)
            {
                if (flasksState[i].Count == 0)
                {
                    continue;
                }
                else
                {
                    for (int j = 0; j < this.flasksState.Count; j++)
                        if (i != j && flasksState[j].Count != 0 && flasksState[i][0] == flasksState[j][0])
                            return false;
                }
            }

            return true;
        }

        public (FlasksStand?, int, int) ReachNextStand(int startfrom, int startto)
        {
            FlasksStand newStand = new FlasksStand(this);
            for (; startfrom < this.flasksState.Count; startfrom++)
            {
                for (; startto < this.flasksState.Count; startto++)
                {
                    if (startfrom == startto 
                        || (flasksState.FlaskIsTerminal(startto) && flasksState[startto].Count != 0)
                        || (flasksState.FlaskIsTerminal(startfrom) && flasksState[startfrom].Count != 0 ))
                        continue;
                    
                    if (newStand.TransferTower(startfrom, startto))
                    {
                        //Console.WriteLine("k" + startfrom + " " + startto);
                        return (newStand, startfrom, startto + 1);
                    }
                }
                startto = 0;
            }
            
            return (null, -1, -1);
        }
        public void InitializationRandom(int flasksAmount, int flaskSize, int emptyFlasks = 0)
        {
            
            flasksState = new Flasks();
            flasksState.maxColorSize = flaskSize;
            
            var random = new Random();

            for (int i = 0; i < flasksAmount; i++)
            {
                flasksState.Add(new Collection<int>());
                for (int j = 0; j < flaskSize; j++)
                {
                    flasksState[i].Add(j);
                }

            }
           
            Print();

            for (int i = 0; i < emptyFlasks; i++)
                flasksState.Add(new Collection<int>());
            for (int i = 0; i < 1000;)
            {
                if (this.Transfer(random.Next(0, this.flasksState.Count), random.Next(0, this.flasksState.Count)))
                {
                    //Console.WriteLine("d");
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
                Console.WriteLine(" ");
            }
            Console.WriteLine();
        }

        public FlasksStand(int maxFlaskSize)
        {
            this.maxFlaskSize = maxFlaskSize;
            
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
            maxFlaskSize = flasksStand.maxFlaskSize;
            stepToReach = flasksStand.stepToReach;
        }
    }
}
