using System;
using System.Collections;

namespace WaterSortPuzzleSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashtableFlask hashtable = new HashtableFlask();
            FlasksStand flasks = new FlasksStand(5);
            flasks.InitializationRandom();
             
            flasks.Print();

            var rand = new Random();

            var r = new Solver(hashtable);
            r.Solve(flasks);
            for (int i = 0; i < 100000;)
            {
                if (flasks.Transfer(rand.Next(0, flasks.flasksState.Count), rand.Next(0, flasks.flasksState.Count)))
                {
                    i++;
                    hashtable.Check(ref flasks);
                    hashtable.RemoveStand(ref flasks);
                }
            }

            Console.WriteLine();
            flasks.Print();
            Console.WriteLine();
            Console.WriteLine(flasks.stepToReach);
        }
    }
}