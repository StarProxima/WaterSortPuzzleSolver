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
			private int stepCount;

			private int minStepCount;
			public double MinStepCount
			{
				get { if (error == null) return minStepCount; else return -1; }
			}
			public double StepCount
			{
				get { if (error == null) return stepCount; else return -1; }
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

			Rezult(int sstepCount, double ttime, FlasksStand fs, Exception e = null)
            {
				stepCount = sstepCount;
				time = ttime;
				path = fs.path;
				minStepCount = fs.path.Length;
				error = e;
            }
		}

		int heuristic(FlasksStand *State)
        {
			//
        }
		int iterationCounter = 0;
		public List<(int, int)> path;
		public HashtableFlask hashtable;
		
		public Rezult Solve(FlasksStand initialState)
		{
			this.path = new List<(int, int)>();
			this.hashtable.Check(ref initialState);

			int minDistance = this.heuristic(initialState);
			bool found = false;
			while(true) 
			{
				minDistance, found = this.iterate(initialState, minDistance)
				if (found) 
				{
					return this.composePath(), nil
				}
				if (minDistance == math.MaxInt) 
				{
					return nil, ErrNotExist
				}
			}
		}

		/*
		func (s *IDAStarSolver) iterate(state State, minDistance int) (newMinDistance int, found bool) {
			s.stats.Steps++
			newDistance := len(s.path) + s.heuristic(state)
			if newDistance > minDistance {
				return newDistance, false
			}

			if state.IsTerminal() {
				return 0, true
			}

			newMinDistance = math.MaxInt
			for _, newState := range state.ReachableStates() {
				newStateStr := newState.String()
				if _, ok := s.pathVertices[newStateStr]; ok {
					continue
				}
				s.pathVertices[newStateStr] = struct{}{}
				s.path = append(s.path, newState)

				newReachableDistance, found := s.iterate(newState, minDistance)
				if found {
					return 0, true
				}

				if newReachableDistance < newMinDistance {
					newMinDistance = newReachableDistance
				}

				delete(s.pathVertices, newStateStr)
				s.path = s.path[:len(s.path)-1]
			}
			return newMinDistance, false
		}
		*/
    }
}
