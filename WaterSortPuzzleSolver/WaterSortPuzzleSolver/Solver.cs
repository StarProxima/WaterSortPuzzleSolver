using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WaterSortPuzzleSolver
{

	class Rezult
	{
		private List<(int, int)> path;
		public List<(int, int)> Path
		{
			get { if (error == null) return path; else return null; }
		}
		private int iterationCounter;
		public int IterationCounter
		{
			get { if (error == null) return iterationCounter; else return -1; }
		}

		private int minStepCount;
		public int MinStepCount
		{
			get { if (error == null) return minStepCount; else return -1; }
		}


		private Exception error;
		public Exception Error
		{
			get { return error; }
		}
		private double time;
		public double Time
		{
			get { if (error == null) return time; else return -1; }
		}

		public Rezult(int iiterationCounter, double ttime, List<(int, int)> ppath)
		{
			iterationCounter = iiterationCounter;
			time = ttime;
			path = ppath;
			minStepCount = ppath.Count;
			error = null;
		}
		public Rezult(Exception e)
		{
			error = e;
		}
	}

	class Solver
	{
		int heuristic(FlasksStand stand)
        {
			int heuristic = 0;
			Dictionary<int, int> bottomColorsCount = new Dictionary<int, int>();

			for (int i = 0; i < stand.flasksState.Count; i++)
			{
				var flask = stand.flasksState[i];

				if (flask.Count == 0)
					continue;

				heuristic += stand.flasksState.ColorTowers(i) - 1;

				if (bottomColorsCount.ContainsKey(flask[0]))
					heuristic++;
				else
					bottomColorsCount[flask[0]] = 0;
			}
			return heuristic;
        }

		int iterationCounter = 0;

		double timeToSolve = 0;

		public List<(int, int)> path;

		public HashtableFlask hashtable;


		int lastMinDistance = 0;
		
		public Rezult Solve(FlasksStand initialState)
		{
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();

			this.hashtable.Check(ref initialState);

			int minDistance = (this.heuristic(initialState));
			
			while (true) 
			{
				if(minDistance != lastMinDistance)
                {
					lastMinDistance = minDistance;
					Console.WriteLine("heuristic val:" + lastMinDistance);
                }
				
				minDistance = this.iterate(initialState, minDistance);
				if (minDistance == -1) 
				{
					stopWatch.Stop();
					TimeSpan ts = stopWatch.Elapsed;
					return new Rezult(iterationCounter, ts.TotalMilliseconds, this.path);
				}
				if (minDistance == Int32.MaxValue) 
				{
					stopWatch.Stop();
					return new Rezult(new Exception("Решения не существует"));
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
            //for(int i = 0; i < stand.flasksState.Count; i++)
            //         {
            //	if(stand.flasksState[i].Count == 1 && stand.flasksState[i][0] == 5 )
            //		stand.Print();
            //}

            stand.Print();
            Console.WriteLine(newDistance);
            Console.WriteLine("---------------");
            int newMinDistance = Int32.MaxValue;
			int from = 0; int to = 0;
			bool b = false;
			while (true)
			{
				FlasksStand newStand;
				
				(newStand, from, to) = stand.ReachNextStand(from, to);
				
				if (from == -1)
					break;
				//if (stand.flasksState[from].Count == 3 && stand.flasksState.ColorTowers(from) == 3 && stand.flasksState[to].Count == 1 && stand.flasksState.ColorTowers(from) == 1)
				//{
				//	Console.WriteLine("-------------");
				//	Console.WriteLine(from + " " + to);
				//	stand.Print();

				//}
				//if (b)
				//{

				//	newStand.Print();
				//	Console.WriteLine("-------------");
				//}
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
