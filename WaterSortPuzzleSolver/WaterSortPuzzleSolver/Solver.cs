using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace WaterSortPuzzleSolver
{
	class Solver
	{
		int iterationCounter = 0;

		double timeToSolve = 0;

		public List<(int, int)> path;

		public HashtableFlask hashtable;

		int heuristic(FlasksStand stand)
		{
			int heuristic = 0;
			Dictionary<int, int> bottomColorsCount = new Dictionary<int, int>();

			for (int i = 0; i < stand.flasksState.Count; i++)
			{
				var flask = stand.flasksState[i];

				if (flask.Count == 0)
				{
					continue;
				}


				heuristic += stand.flasksState.ColorTowers(i) - 1;


                if (bottomColorsCount.ContainsKey(flask[0]))
                    bottomColorsCount[flask[0]]++;
                else
                    bottomColorsCount[flask[0]] = 0;

            }

			foreach(var bottomColorCnt in bottomColorsCount.Values) 
			{
				
				heuristic += bottomColorCnt ;
			}
			
				//Console.WriteLine("heuristic" + heuristic);


			return heuristic;
		}

		public Result Solve(FlasksStand initialState)
		{
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();

			this.hashtable.Check(ref initialState);

			int minDistance = (this.heuristic(initialState));
			//Console.WriteLine(1);
			while (true) 
			{
				//Console.WriteLine(2);
				minDistance = this.iterate(initialState, minDistance);
				
				if (minDistance == -1) 
				{
					
					stopWatch.Stop();
					TimeSpan ts = stopWatch.Elapsed;
					return new Result(iterationCounter, ts.TotalMilliseconds, this.path);
				}
				if (minDistance == Int32.MaxValue) 
				{
					stopWatch.Stop();
					
					return new Result(new Exception("Решения не существует"));
				}
				
			}
		}


		int iterate(FlasksStand stand, int minDistance ) 
		{
			this.iterationCounter++;

			int newDistance = this.path.Count + this.heuristic(stand);

			if (newDistance > minDistance) {
				return newDistance;
			}
			
			if (stand.IsTerminal()) 
			{
				return -1;
			}
			
			int newMinDistance = Int32.MaxValue;
			int from = 0; int to = 0;
			

			while (true)
			{
				FlasksStand? newStand;
				(newStand, from, to) = stand.ReachNextStand(from, to);
				
				if (from == -1)
					break;
				if (this.hashtable.Check(ref newStand)) {
					continue;
				}
				this.path.Add(newStand.lastTransfer);

				int newReachableDistance = this.iterate(newStand, minDistance);
				if (newReachableDistance == -1) {
					return -1;
					
				}

				if (newReachableDistance < newMinDistance) {
					newMinDistance = newReachableDistance;
					//Console.WriteLine("newDistance" + newDistance);
				}

				this.hashtable.RemoveStand(ref newStand);
				this.path.RemoveAt(this.path.Count - 1);
				
			}
			
			return newMinDistance;
		}

		public Solver(HashtableFlask hhashtable)
        {
			hashtable = hhashtable;
			path = new List<(int, int)>();
		}
    }
}
