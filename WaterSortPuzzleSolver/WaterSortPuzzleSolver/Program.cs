using System;
using System.Collections;

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
    public class HashtableFlask
    {
        

        public Dictionary<Flasks, FlasksStand> hashtable = new Dictionary<Flasks, FlasksStand>();
        
        public bool Check(FlasksStand newFlasks)
        {
            if (hashtable.ContainsKey(newFlasks.flasksState))
            {
                
                if (hashtable[newFlasks.flasksState].path.Count > newFlasks.path.Count)
                {
                    hashtable[newFlasks.flasksState] = new FlasksStand(newFlasks);
                    return false;
                }
                return true;
            }
            else
            {
                Flasks t = new Flasks (newFlasks.flasksState);
                hashtable.Add(t, newFlasks);
            }
            return false;
        }

        public HashtableFlask()
        {
            hashtable = new Dictionary<Flasks, FlasksStand>();
        }
    }
    public class FlasksStand
    {
        int maxColors;

        public Flasks flasksState;

        public List<(int, int)> path;

        public static bool operator ==(FlasksStand fl1, FlasksStand fl2)
        {
            if (fl1.flasksState.Count != fl2.flasksState.Count) return false;
            for (int i = 0; i < fl1.flasksState.Count; i++)
            {
                if (!Enumerable.SequenceEqual(fl1.flasksState[i], fl2.flasksState[i])) return false;
            }
            return true;
        }

        public static bool operator !=(FlasksStand fl1, FlasksStand fl2)
        {
            if (fl1.flasksState.Count != fl2.flasksState.Count) return true;
            for (int i = 0; i < fl1.flasksState.Count; i++)
            {
                if (!Enumerable.SequenceEqual(fl1.flasksState[i], fl2.flasksState[i])) return true;
            }
            return false;
        }

        public bool Transfer(int from, int to)
        {
            if (flasksState[from].Count > 0 && flasksState[to].Count < maxColors)
            {
                int t = flasksState[from].Last();
                flasksState[to].Add(t);
                flasksState[from].RemoveAt(flasksState[from].Count -1);
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

            flasksState = new Flasks() {  new List<int>() {1,2 } , new List<int>() {4,2},new List<int>() { 1 }, new List<int>() { 1 } };
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

    internal class Program
    {
        static void Main(string[] args)
        {
            HashtableFlask hashtable = new HashtableFlask();
            FlasksStand flasks = new FlasksStand(5);
            flasks.InitializationRandom();
             
            flasks.Print();

            var rand = new Random();
            for (int i = 0; i < 10000;)
            {
                if (flasks.Transfer(rand.Next(0, flasks.flasksState.Count), rand.Next(0, flasks.flasksState.Count)))
                {
                    i++;
                    hashtable.Check(flasks);
                }
            }

            Console.WriteLine();
            flasks.Print();
            Console.WriteLine();
            Console.WriteLine(hashtable.hashtable.Count);
        }
    }
}