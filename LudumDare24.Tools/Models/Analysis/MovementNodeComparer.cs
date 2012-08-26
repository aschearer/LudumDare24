using System.Collections.Generic;

namespace LudumDare24.Tools.Models.Analysis
{
    public class MovementNodeComparer : IComparer<MovementNode>
    {
        public int Compare(MovementNode x, MovementNode y)
        {
            return x.Depth - y.Depth;
        }
    }
}