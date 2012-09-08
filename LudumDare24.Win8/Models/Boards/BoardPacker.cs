using System;
using System.Collections.Generic;
using System.Linq;
using LudumDare24.Models.Doodads;

namespace LudumDare24.Models.Boards
{
    public class BoardPacker
    {
        private readonly int numberOfColumns;
        private readonly int numberOfRows;

        public BoardPacker(int numberOfColumns, int numberOfRows)
        {
            this.numberOfColumns = numberOfColumns;
            this.numberOfRows = numberOfRows;
        }

        public void Pack(IEnumerable<IDoodad> doodads, float rotation)
        {
            float cos = (float)Math.Cos(rotation);
            float sin = (float)Math.Sin(rotation);
            int acrossStart;
            int acrossEnd;
            int acrossDelta;
            int downStart;
            int downEnd;
            int downDelta;
            Func<int, int, IDoodad> doodadFetcher;
            Action<IDoodad, int> doodadMover;

            if (Math.Abs(cos) == 1)
            {
                doodadFetcher =
                    (column, row) =>
                    doodads.FirstOrDefault(doodad => doodad.Column == column && doodad.Row == row);
                if (Math.Sign(cos) > 0)
                {
                    acrossStart = 0;
                    acrossEnd = this.numberOfColumns - 1;
                    acrossDelta = 1;
                    downStart = this.numberOfRows - 1;
                    downEnd = 0;
                    downDelta = -1;
                    doodadMover = (doodad, delta) => doodad.Row += delta;
                }
                else
                {
                    acrossStart = this.numberOfColumns - 1;
                    acrossEnd = 0;
                    acrossDelta = -1;
                    downStart = 0;
                    downEnd = this.numberOfRows - 1;
                    downDelta = 1;
                    doodadMover = (doodad, delta) => doodad.Row -= delta;
                }
            }
            else
            {
                doodadFetcher =
                    (column, row) =>
                    doodads.FirstOrDefault(doodad => doodad.Column == row && doodad.Row == column);
                if (Math.Sign(sin) > 0)
                {
                    acrossStart = 0;
                    acrossEnd = this.numberOfRows - 1;
                    acrossDelta = 1;
                    downStart = this.numberOfColumns - 1;
                    downEnd = 0;
                    downDelta = -1;
                    doodadMover = (doodad, delta) => doodad.Column += delta;
                }
                else
                {
                    acrossStart = this.numberOfRows - 1;
                    acrossEnd = 0;
                    acrossDelta = -1;
                    downStart = 0;
                    downEnd = this.numberOfColumns - 1;
                    downDelta = 1;
                    doodadMover = (doodad, delta) => doodad.Column -= delta;
                }
            }

            this.PushDown(
                acrossStart, 
                acrossEnd, 
                acrossDelta, 
                downStart, 
                downEnd, 
                downDelta, 
                doodadFetcher, 
                doodadMover);

            this.PullDown(
                acrossStart,
                acrossEnd,
                acrossDelta,
                downEnd,
                downStart,
                -downDelta,
                doodadFetcher,
                doodadMover);
        }

        private void PullDown(
            int acrossStart,
            int acrossEnd,
            int acrossDelta,
            int downStart,
            int downEnd,
            int downDelta,
            Func<int, int, IDoodad> doodadFetcher,
            Action<IDoodad, int> doodadMover)
        {
            for (int column = acrossStart; column != (acrossEnd + acrossDelta); column += acrossDelta)
            {
                int depth = 0;
                for (int row = downStart; row != (downEnd + downDelta); row += downDelta)
                {
                    IDoodad doodad = doodadFetcher(column, row);
                    if (doodad == null)
                    {
                        depth++;
                    }
                    else
                    {
                        if (depth > 0 && doodad.FallingState == FallingState.Up)
                        {
                            doodadMover(doodad, -depth);
                        }
                        else
                        {
                            depth = 0;
                        }
                    }
                }
            }
        }

        private void PushDown(
            int acrossStart, 
            int acrossEnd, 
            int acrossDelta, 
            int downStart, 
            int downEnd, 
            int downDelta, 
            Func<int, int, IDoodad> doodadFetcher, 
            Action<IDoodad, int> doodadMover)
        {
            for (int column = acrossStart; column != (acrossEnd + acrossDelta); column += acrossDelta)
            {
                int depth = 0;
                for (int row = downStart; row != (downEnd + downDelta); row += downDelta)
                {
                    IDoodad doodad = doodadFetcher(column, row);
                    if (doodad == null)
                    {
                        depth++;
                    }
                    else
                    {
                        if (depth > 0 && doodad.FallingState == FallingState.Down)
                        {
                            doodadMover(doodad, depth);
                        }
                        else
                        {
                            depth = 0;
                        }
                    }
                }
            }
        }
    }
}