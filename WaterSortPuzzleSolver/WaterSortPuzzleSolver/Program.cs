using System;
using System.Collections;

namespace WaterSortPuzzleSolver
{

    public class FlasksComparer : IEqualityComparer<Flasks>
    {
        public bool Equals(Flasks x, Flasks y)
        {
            if (x.Count != y.Count) return false;
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i].Count != y[i].Count) return false;

                for (int j = 0; j < x[i].Count; j++)
                {
                    if (!x[i].SequenceEqual(y[i])) return false;
                }
            }
            return true;
        }

        public int GetHashCode(Flasks obj)
        {
            int hashcode = 0;
            for (int i = 0; i < obj.Count; i++)
                for (int j = 0; j < obj[i].Count; j++)
                {
                    //Not tested for collisions
                    hashcode += obj[i][j].GetHashCode() ^ j.GetHashCode();
                }
            return hashcode;

        }
    }

    
    public class HashtableFlask 
    {
        

        public Dictionary<Flasks, FlasksStand> hashtable = new Dictionary<Flasks, FlasksStand>(new FlasksComparer());
        
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
            hashtable = new Dictionary<Flasks, FlasksStand>(new FlasksComparer());
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
            for (int i = 0; i < 100000;)
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