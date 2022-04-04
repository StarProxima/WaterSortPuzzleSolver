using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzleSolver
{

	

    class Solver
    {
		class Rezult
		{
			private List<int,int> path;
			public double Path
			{
				get { if (error == null) return path; else return null; }
			}
			private int iterationCounter;
			public double IterationCounter
			{
				get { if (error == null) return IterationCounter; else return -1; }
			}

			private int minStepCount;
			public double MinStepCount
			{
				get { if (error == null) return minStepCount; else return -1; }
			}
			
			
			private Exception error = null;
			public double Error
			{
				get { return error; }
			}
			private double time;
			public double Time
			{
				get { if (error == null) return time; else return -1; }
			}

			Rezult(int iiterationCounter, double ttime, List<int, int> ppath, int mminStepCount)
            {
				iterationCounter = iiterationCounter;
				time = ttime;
				path = ppath;
				minStepCount = mminStepCount;
            }
			Rezult(Exception e)
            {
				error = e;
            }
		}

		int heuristic(FlasksStand *State)
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

			pair<int, int> minDistanceAndSteps = make_pair(this.heuristic(initialState),0);
			while(true) 
			{
				minDistanceAndSteps = this.iterate(initialState, minDistanceAndSteps.first);
				if (minDistanceAndSteps.second != 0) 
				{
					return Rezult(iterationCounter, timeToSolve, path, initialState.stepToReach, minDistanceAndSteps.second);
				}
				if (minDistance == Int32.MaxValue) 
				{
					return Rezult(new Exception("Решения не существует"));
				}
			}
		}


		pair<int, int> iterate(FlasksStand state, int minDistance ) 
		{
			this.iterationCounter++;
			int newDistance = this.path.Length + this.heuristic(state);
			if (newDistance > minDistance) {
				return make_pair(newDistance, 0);
			}

			if (state.IsTerminal()) {
				return make_pair(0, state.stepToReach);
			}

			int newMinDistance = Int32.MaxValue;
			foreach (FlasksStand newState in state.Reachable()) {
				if (this.hashtable.Check(newState)) {
					continue;
				}
				this.path.Add(newState.lastTransfer);

				pair<int, int> newReachableDistanceAndSteps = this.iterate(newState, minDistance);
				if (newReachableDistanceAndSteps.second != 0) {
					return make_pair(0, newReachableDistanceAndSteps.second);
				}

				if (newReachableDistanceAndSteps.first < newMinDistance) {
					newMinDistance = newReachableDistanceAndSteps.first;
				}

				this.hashtable.Delete(newState);
				this.path.RemoveAt(this.path.Count - 1);
			}
			return make_pair(newMinDistance, 0);
		}
		
    }
}
