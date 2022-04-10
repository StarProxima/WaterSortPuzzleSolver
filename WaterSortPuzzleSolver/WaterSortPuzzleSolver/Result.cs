using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzleSolver
{
	class Result
	{
		private List<(int, int)> path;
		public List<(int, int)> Path
		{
			get { if (error == null) return path; throw error; }
		}


		private int iterationCounter;
		public int IterationCounter
		{
			get { if (error == null) return iterationCounter; throw error; }
		}


		private int minStepCount;
		public int MinStepCount
		{
			get { if (error == null) return minStepCount; throw error; }
		}


		private Exception? error;
		public Exception? Error
		{
			get { return error; }
		}


		private double time;
		public double Time
		{
			get { if (error == null) return time; throw error; }
		}

		public Result(int iterationCounter, double time, List<(int, int)> path)
		{
			this.iterationCounter = iterationCounter;
			this.time = time;
			this.path = path;
			minStepCount = path.Count;
			error = null;
		}
		public Result(Exception e)
		{
			error = e;
		}
	}
}
