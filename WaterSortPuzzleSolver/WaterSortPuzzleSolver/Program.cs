using System;
using System.Collections;


namespace WaterSortPuzzleSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashtableFlask hashtable = new HashtableFlask();
            FlasksStand flasksStand = new FlasksStand(5);
            flasksStand.InitializationRandom(5, 4, 2);

            flasksStand.Print();

            var rand = new Random();

            Solver s = new Solver(hashtable);
            Result res = s.Solve(flasksStand);

            Console.WriteLine("-----------------");

            foreach (var x in res.Path)
            {
                Console.Write(x.Item1);
                Console.Write("  ");
                Console.WriteLine(x.Item2);
            }

            Console.WriteLine("-----------------");

            flasksStand.GoFullPath(res.Path);
            flasksStand.Print();
            Console.WriteLine("Время в милисекундах - {0}", res.Time);
            Console.WriteLine("Время в секундах - {0}", res.Time/1000.0);
            Console.WriteLine("Время в минутах - {0}", res.Time/1000.0/60.0);
            Console.WriteLine("Время в часах - {0}", res.Time / 1000.0 / 60.0 / 60.0);
            Console.WriteLine("Количество итераций - {0}", res.IterationCounter);
            Console.ReadKey();
        }
    }
}
