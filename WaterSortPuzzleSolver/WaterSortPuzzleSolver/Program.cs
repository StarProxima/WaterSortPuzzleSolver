using System;
using System.Collections;

namespace WaterSortPuzzleSolver
{
    //public class Hashtable
    //{
    //    public System.Collections.Hashtable hashtable;

    //    public bool Check(Flasks newFlasks)
    //    {
    //        if (hashtable.ContainsKey(newFlasks.flasksState))
    //        {
    //            if ((hashtable[newFlasks.flasksState] as Flasks).path.Count > newFlasks.path.Count)
    //            {
    //                hashtable[newFlasks.flasksState] = new Flasks(newFlasks);
    //                return false;
    //            }
                
    //            return true;
    //        }
    //        else
    //        {
    //            hashtable.Add(newFlasks.flasksState, newFlasks);
    //        }
            
    //        return false;
    //    }

    //    //public Flasks Replacement(Flasks newFlasks)
    //    //{
    //    //    return hashtable[newFlasks.flasks] as Flasks;
    //    //}

    //    public Hashtable()
    //    {
    //        hashtable = new System.Collections.Hashtable();
    //    }

    //}
    public class Flasks
    {
        int maxColors;

        public List<List<int>> flasksState;

        public List<(int, int)> path;

        public bool Transfer(int from, int to)
        {
            if (flasksState[from].Count > 0 && flasksState[to].Count < maxColors)
            {
                int t = flasksState[from].Last();
                flasksState[to].Add(t);
                flasksState[from].Remove(t);
                path.Add((from, to));
                return true;
            }
            return false;
        }
            

        public void InitializationRandom()
        {
            var random = new Random();

            int flasksCount = random.Next(4, 6);
            for (int i = 0; i < flasksCount; i++)
            {

                flasksState.Add(new List<int>());

                int flaskCount = random.Next(2, maxColors + 1);
                for (int j = 0; j < flaskCount; j++)
                {
                    flasksState[i].Add(random.Next(1,8));
                }
            }
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

        public Flasks(int maxColors)
        {
            this.maxColors = maxColors;
            flasksState = new List<List<int>>();
            path = new List<(int, int)>();
        }

        public Flasks(Flasks flasks)
        {
            this.maxColors = flasks.maxColors;
            flasksState = new List<List<int>>();
            for (int i = 0; i < flasks.flasksState.Count; i++)
            {
                flasksState.Add(new List<int>());
                for (int j = 0; j < flasks.flasksState[i].Count; j++)
                {
                    flasksState[i][j] = flasks.flasksState[i][j];
                }
                
            }
            path = new List<(int, int)>();
            for (int i = 0; i < flasks.path.Count; i++)
            {
                path.Add(flasks.path[i]);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Hashtable hashtable = new Hashtable();
            Flasks flasks = new Flasks(4);
            flasks.InitializationRandom();
            //hashtable.Check(ref flasks);
             
            flasks.Print();

            var rand = new Random();
            for (int i = 0; i < 1000;)
            {
                if (flasks.Transfer(rand.Next(0, flasks.flasksState.Count), rand.Next(0, flasks.flasksState.Count)))
                {
                    i++;
                    //hashtable.Check(flasks);
                }
                    
                
            }

            Console.WriteLine();
            flasks.Print();
        }
    }
}