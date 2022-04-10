using System;
using System.Collections;


namespace WaterSortPuzzleSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashtableFlask hashtable = new HashtableFlask();
            FlasksStand flasks = new FlasksStand();
            flasks.InitializationRandom(8, 5, 4, 1);

            flasks.Print();

            var rand = new Random();


            Solver s = new Solver(hashtable);
            Rezult rez = s.Solve(flasks);

            Console.WriteLine("-----------------");

            foreach (var x in rez.Path)
            {
                Console.Write(x.Item1);
                Console.Write("  ");
                Console.WriteLine(x.Item2);
            }

            Console.WriteLine("Время в милисекундах - {0}", rez.Time);
            Console.WriteLine("Время в секундах - {0}", rez.Time / 1000.0);
            Console.WriteLine("Время в минутах - {0}", rez.Time / 1000.0 / 60.0);
            Console.WriteLine("Время в часах - {0}", rez.Time / 1000.0 / 60.0 / 60.0);
            Console.WriteLine("Количество итераций - {0}", rez.IterationCounter);
            Console.ReadKey();
        }
    }
}
