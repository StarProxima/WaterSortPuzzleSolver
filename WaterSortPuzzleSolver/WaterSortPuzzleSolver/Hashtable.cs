using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterSortPuzzleSolver
{
    public class HashtableFlask
    {
        private class FlasksComparer : IEqualityComparer<Flasks>
        {
            public bool Equals(Flasks x, Flasks y)
            {
                if (x.Count != y.Count) return false;
                for (int i = 0; i < x.Count; i++)
                {
                    if (x[i].Count != y[i].Count) return false;

                    for (int j = 0; j < x[i].Count; j++)
                    {
                        if (!x[i].SequenceEqual(y[i])) return false;
                    }
                }
                return true;
            }

            public int GetHashCode(Flasks obj)
            {
                int hashcode = 0;
                for (int i = 0; i < obj.Count; i++)
                    for (int j = 0; j < obj[i].Count; j++)
                    {
                        //Not tested for collisions
                        hashcode += obj[i][j].GetHashCode() ^ j.GetHashCode();
                    }
                return hashcode;

            }
        }

        public Dictionary<Flasks, FlasksStand> hashtable = new Dictionary<Flasks, FlasksStand>(new FlasksComparer());

        public bool Check(FlasksStand newFlasks)
        {
            if (hashtable.ContainsKey(newFlasks.flasksState))
            {

                if (hashtable[newFlasks.flasksState].path.Count > newFlasks.path.Count)
                {
                    hashtable[newFlasks.flasksState] = new FlasksStand(newFlasks);
                    return false;
                }
                return true;
            }
            else
            {
                Flasks t = new Flasks(newFlasks.flasksState);
                hashtable.Add(t, newFlasks);
            }
            return false;
        }

        public HashtableFlask()
        {
            hashtable = new Dictionary<Flasks, FlasksStand>(new FlasksComparer());
        }
    }
}
