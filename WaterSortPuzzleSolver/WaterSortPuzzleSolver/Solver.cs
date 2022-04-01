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
				error = e;
            }
		}


		public stepCounter = 0;

       public Rezult Solve(FlasksStand State) 
		{
			this.path = []State{initialState}
			this.pathVertices[initialState.String()] = struct{}{}

			minDistance := this.heuristic(initialState)
			var found bool
			for {
				minDistance, found = this.iterate(initialState, minDistance)
				if found {
					return this.composePath(), nil
				}
				if minDistance == math.MaxInt {
					return nil, ErrNotExist
				}
			}
		}


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
    }
}
