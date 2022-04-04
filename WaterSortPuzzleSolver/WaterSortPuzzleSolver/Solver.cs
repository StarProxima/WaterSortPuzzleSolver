using System.Text;

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

		int heuristic(FlasksStand state)
        {
			//
			return 0;
        }
		int iterationCounter = 0;
		double timeToSolve = 0;
		public List<(int, int)> path;
		public HashtableFlask hashtable;
		
		public Rezult Solve(FlasksStand initialState)
		{
			this.path = new List<(int, int)>();
			
			this.hashtable.Check(ref initialState);

			(int, int) minDistanceAndSteps = (this.heuristic(initialState),0);
			while(true) 
			{
				minDistanceAndSteps = this.iterate(initialState, minDistanceAndSteps.Item1);
				if (minDistanceAndSteps.Item2 != 0) 
				{
					return new Rezult(iterationCounter, timeToSolve, this.path, minDistanceAndSteps.Item2);
				}
				if (minDistanceAndSteps.Item1 == Int32.MaxValue) 
				{
					return new Rezult(new Exception("Решения не существует"));
				}
			}
		}


		(int, int) iterate(FlasksStand state, int minDistance ) 
		{
			this.iterationCounter++;
			int newDistance = this.path.Count + this.heuristic( state);
			if (newDistance > minDistance) {
				return (newDistance, 0);
			}
			/*
			if (state.IsTerminal()) {
				return (0, state.stepToReach);
			}

			int newMinDistance = Int32.MaxValue;
			FlasksStand[] ReachableStands = state.Reachable();
			for (int i = 0; i < ReachableStands.Length; i++) {
				if (this.hashtable.Check(ref ReachableStands[i])) {
					continue;
				}
				this.path.Add(ReachableStands[i].lastTransfer);

				(int, int) newReachableDistanceAndSteps = this.iterate(ReachableStands[i], minDistance);
				if (newReachableDistanceAndSteps.Item2 != 0) {
					return (0, newReachableDistanceAndSteps.Item2);
				}

				if (newReachableDistanceAndSteps.Item1 < newMinDistance) {
					newMinDistance = newReachableDistanceAndSteps.Item1;
				}

				this.hashtable.Delete(ReachableStands[i]);
				this.path.RemoveAt(this.path.Count - 1);
			}
			*/
			return (newMinDistance, 0);
		}
		
    }
}
