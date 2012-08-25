using System;
using System.Linq;
using LudumDare24.Models.Doodads;

namespace LudumDare24.Models.Boards
{
    public class BoardPacker
    {
        private readonly IBoard board;

        public BoardPacker(IBoard board)
        {
            this.board = board;
        }

        public void Pack(float rotation)
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
                    this.board.Doodads.FirstOrDefault(doodad => doodad.Column == column && doodad.Row == row);
                if (Math.Sign(cos) > 0)
                {
                    acrossStart = 0;
                    acrossEnd = this.board.NumberOfColumns - 1;
                    acrossDelta = 1;
                    downStart = this.board.NumberOfRows - 1;
                    downEnd = 0;
                    downDelta = -1;
                    doodadMover = (doodad, delta) => doodad.Row += delta;
                }
                else
                {
                    acrossStart = this.board.NumberOfColumns - 1;
                    acrossEnd = 0;
                    acrossDelta = -1;
                    downStart = 0;
                    downEnd = this.board.NumberOfRows - 1;
                    downDelta = 1;
                    doodadMover = (doodad, delta) => doodad.Row -= delta;
                }
            }
            else
            {
                doodadFetcher =
                    (column, row) =>
                    this.board.Doodads.FirstOrDefault(doodad => doodad.Column == row && doodad.Row == column);
                if (Math.Sign(sin) > 0)
                {
                    acrossStart = 0;
                    acrossEnd = this.board.NumberOfRows - 1;
                    acrossDelta = 1;
                    downStart = this.board.NumberOfColumns - 1;
                    downEnd = 0;
                    downDelta = -1;
                    doodadMover = (doodad, delta) => doodad.Column += delta;
                }
                else
                {
                    acrossStart = this.board.NumberOfRows - 1;
                    acrossEnd = 0;
                    acrossDelta = -1;
                    downStart = 0;
                    downEnd = this.board.NumberOfColumns - 1;
                    downDelta = 1;
                    doodadMover = (doodad, delta) => doodad.Column -= delta;
                }
            }

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