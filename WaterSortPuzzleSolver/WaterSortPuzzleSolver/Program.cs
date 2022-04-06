﻿using System;
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


            Solver s = new Solver(hashtable);
            Rezult rez = s.Solve(flasks);

            Console.WriteLine();////!!!!
            foreach (var x in rez.Path)
            {
                Console.Write(x.Item1);
                Console.Write("  ");
                Console.WriteLine(x.Item2);
            }
            Console.WriteLine("Время в милисекундах - {0}", rez.Time);
            Console.WriteLine("Количество итераций - {0}", rez.IterationCounter);
        }
    }
}

/*
            for (int i = 0; i < 100000;)
            {
                if (flasks.Transfer(rand.Next(0, flasks.flasksState.Count), rand.Next(0, flasks.flasksState.Count)))
                {
                    i++;
                    hashtable.Check(ref flasks);
                    hashtable.RemoveStand(ref flasks);
                }
            }
*/