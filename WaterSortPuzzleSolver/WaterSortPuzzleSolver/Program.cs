using System;
using System.Collections;

namespace WaterSortPuzzleSolver
{
    public class Hashtable
    {
        System.Collections.Hashtable hashtable;

        public bool Check(ref Flasks newFlasks)
        {
            if (hashtable.ContainsKey(newFlasks.flasks))
            {
                if ((hashtable[newFlasks.flasks] as Flasks).flasks.Count > newFlasks.flasks.Count)
                {
                    hashtable[newFlasks.flasks] = newFlasks;
                    return false;
                }

                return true;
            }
            else
            {
                hashtable.Add(newFlasks.flasks, newFlasks);
            }

            return false;
        }

        //public Flasks Replacement(Flasks newFlasks)
        //{
        //    return hashtable[newFlasks.flasks] as Flasks;
        //}

        public Hashtable()
        {
            hashtable = new System.Collections.Hashtable();
        }

    }
    public class Flasks
    {
        int maxColors;

        public List<List<int>> flasks;

        public List<(int, int)> path;

        public void Transfer(int from, int to)
        {
            if (flasks[from].Count > 0 && flasks[to].Count < maxColors)
            {
                int t = flasks[from].Last();
                flasks[to].Add(t);
                flasks[from].Remove(t);
                path.Add((from, to));
            }
            else Console.WriteLine("invalid transfer");
        }

        public void InitializationRandom()
        {
            var random = new Random();

            int flasksCount = random.Next(2, 10);
            for (int i = 0; i < flasksCount; i++)
            {
                
                flasks.Add(new List<int>());

                int flaskCount = random.Next(2, maxColors + 1);
                for (int j = 0; j < flaskCount; j++)
                {
                    flasks[i].Add(random.Next(1,8));
                }
            }
        }

        public void Print()
        {
            for (int i = 0; i < flasks.Count; i++)
            {
                for (int j = 0; j < flasks[i].Count; j++)
                {
                    Console.Write(flasks[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        public Flasks(int maxColors)
        {
            this.maxColors = maxColors;
            flasks = new List<List<int>>();
            path = new List<(int, int)>();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Flasks flasks = new Flasks(4);
            flasks.InitializationRandom();
            flasks.Print();

            Console.WriteLine("Hello World!");
        }
    }
}