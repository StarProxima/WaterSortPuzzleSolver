
using System.Text;


namespace WaterSortPuzzleSolver
{
    //Хранит все добавленные flasksState с наименьшим количеством шагов
    public class HashtableFlask : Dictionary<Flasks, FlasksStand>
    {
        private class FlasksComparer : IEqualityComparer<Flasks>
        {
            public bool Equals(Flasks x, Flasks y)
            {
                if (x.Count != y.Count) return false;
                ////!!!!
                //Parallel.For(0, x.Count, i =>
                // не параллелится потому что ретурны не могут возвращать паралельно хз кринж какой то
                for (int i = 0; i < x.Count; i++)
                {
                    if (x[i].Count != y[i].Count) return false;
                    ////!!!!
                    //Parallel.For(0, x[i].Count, j =>
                    for (int j = 0; j < x[i].Count; j++)
                    {
                    if (!x[i].SequenceEqual(y[i])) return false;
                    }
                    //);
                }
                //);
                return true;
            }

            public int GetHashCode(Flasks obj)
            {
                int hashcode = 0;
                ////!!!! не паралелится потому что дает ошибку в Check хз почему он вообще с ним не связан
                for (int i = 0; i < obj.Count; i++)
                //Parallel.For(0, obj.Count, i =>
                { 
                    //Parallel.For(0, obj[i].Count, j =>
                    for (int j = 0; j < obj[i].Count; j++)
                    {
                        //Not tested for collisions
                        hashcode += obj[i][j].GetHashCode() ^ j.GetHashCode();
                    }
                   // );
                 }
                //);
                return hashcode;
            }
        }



        public bool Check(ref FlasksStand flasksStand)
        {
            if (this.ContainsKey(flasksStand.flasksState))
            {

                if (this[flasksStand.flasksState].stepToReach > flasksStand.stepToReach)
                {
                    this[flasksStand.flasksState] = new FlasksStand(flasksStand);
                }
                else
                {
                    //Возможно, стоит не заменять текущий flasksStand на более выгодный по шагам, а возвращать новый FlasksStand, хз. 
                    //flasksStand = new FlasksStand(this[flasksStand.flasksState]);
                }
                return true;
            }
            else
            {
                this.Add(new Flasks(flasksStand.flasksState), new FlasksStand(flasksStand));
            }
            return false;
        }

        public bool RemoveStand(ref FlasksStand flasksStand)
        {
            if (this.ContainsKey(flasksStand.flasksState))
            {
                this.Remove(flasksStand.flasksState);
                return true;
            }
            return false;
        }

        public HashtableFlask() : base(new FlasksComparer())
        {
        }
    }
}
