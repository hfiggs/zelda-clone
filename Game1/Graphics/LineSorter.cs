using System.Collections.Generic;

namespace Game1.Graphics
{
    public class LineSorter : IComparer<Line>
    {
        public int Compare(Line l1, Line l2)
        {
            return l2.GetAngle().CompareTo(l1.GetAngle());
        }
    }
}
