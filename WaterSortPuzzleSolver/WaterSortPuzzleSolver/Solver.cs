using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzleSolver
{



	class Solver
	{
		public class Rezult
		{
			private List<(int,int)> path;
			public List<(int, int)> Path
			{
				get { if (error == null) return path; else return null; }
			}
			private int iterationCounter;
			public int IterationCounter
			{
				get { if (error == null) return IterationCounter; else return -1; }
			}

			private int minStepCount;
			public int MinStepCount
			{
				get { if (error == null) return minStepCount; else return -1; }
			}
			
			
			private Exception error = null;
			public Exception Error
			{
				get { return error; }
			}
			private double time;
			public double Time
			{
				get { if (error == null) return time; else return -1; }
			}

			public Rezult(int iiterationCounter, double ttime, List<(int, int)> ppath, int mminStepCount)
            {
				iterationCounter = iiterationCounter;
				time = ttime;
				path = ppath;
				minStepCount = mminStepCount;
            }
			public Rezult(Exception e)
            {
				error = e;
            }
		}

		int heuristic(FlasksStand stand)
        {
			int heuristic = 0;
			Dictionary<int, int> bottomColorsCount = new Dictionary<int, int>();
			for (int i = 0; i< stand.flasksState.Count; i++) {
				var flask = stand.flasksState[i];

				if (flask.Count == 0) {
					continue;
				}

				heuristic += stand.flasksState.ColorTowers(i) - 1;
				if (bottomColorsCount.ContainsKey(flask[0]))
					bottomColorsCount[flask[0]]++;
				else
					bottomColorsCount[flask[0]] = 1;
			}

			foreach(int bottomColorCnt in bottomColorsCount.Values) 
			{
				heuristic += bottomColorCnt - 1;
			}
			return heuristic;
        }

		int iterationCounter = 0;

		double timeToSolve = 0;

		public List<(int, int)> path;

		public HashtableFlask hashtable;
		
		public Rezult Solve(FlasksStand initialState)
		{		
			this.hashtable.Check(ref initialState);

			(int, int) minDistanceAndSteps = (this.heuristic(initialState), -1);
			while(true) 
			{
				minDistanceAndSteps = this.iterate(initialState, minDistanceAndSteps.Item1);
				if (minDistanceAndSteps.Item2 != -1) 
				{
					return new Rezult(iterationCounter, timeToSolve, this.path, minDistanceAndSteps.Item2);
				}
				if (minDistanceAndSteps.Item1 == Int32.MaxValue) 
				{
					return new Rezult(new Exception("Решения не существует"));
				}
			}
		}

		(int, int) iterate(FlasksStand stand, int minDistance ) 
		{
			this.iterationCounter++;
			int newDistance = this.path.Count + this.heuristic(stand);
			if (newDistance > minDistance) {
				return (newDistance, -1);
			}
			
			if (stand.IsTerminal()) 
			{
				return (0, stand.stepToReach);
			}

			int newMinDistance = Int32.MaxValue;
			int from = 0; int to = 0;
			while (true)
			{
				FlasksStand newStand;
				(newStand, from, to) = stand.ReachNextStand(from, to);
				if (from == -1)
					break;
				if (this.hashtable.Check(ref newStand)) {
					continue;
				}
				this.path.Add(newStand.lastTransfer);

				(int, int) newReachableDistanceAndSteps = this.iterate(newStand, minDistance);
				if (newReachableDistanceAndSteps.Item2 != -1) {
					return (0, newReachableDistanceAndSteps.Item2);
				}

				if (newReachableDistanceAndSteps.Item1 < newMinDistance) {
					newMinDistance = newReachableDistanceAndSteps.Item1;
				}

				this.hashtable.RemoveStand(ref newStand);
				this.path.RemoveAt(this.path.Count - 1);
			}
			
			return (newMinDistance, -1);
		}
		

		public Solver(HashtableFlask hhashtable)
        {
			hashtable = hhashtable;
			path = new List<(int, int)>();
		}
    }
}
