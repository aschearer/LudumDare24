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

        public void Pack()
        {
            for (int column = 0; column < this.board.NumberOfColumns; column++)
            {
                int depth = 0;
                for (int row = this.board.NumberOfRows - 1; row >= 0; row--)
                {
                    IDoodad doodad = this.GetDoodadAt(column, row);
                    if (doodad == null)
                    {
                        depth++;
                    }
                    else
                    {
                        if (depth > 0)
                        {
                            doodad.Row += depth;
                        }
                        else
                        {
                            depth = 0;
                        }
                    }
                }
            }
        }

        private IDoodad GetDoodadAt(int column, int row)
        {
            return this.board.Doodads.FirstOrDefault(doodad => doodad.Column == column && doodad.Row == row);
        }
    }
}