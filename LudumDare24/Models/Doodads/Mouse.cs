using System;
using System.Collections.Generic;
using System.Linq;
using LudumDare24.Models.Boards;
using Microsoft.Xna.Framework;

namespace LudumDare24.Models.Doodads
{
    public class Mouse : Doodad
    {
        public Mouse(int column, int row)
            : base(column, row)
        {
        }

        public bool GotTheCheese { get; private set; }
        public bool CaughtByCat { get; private set; }

        public override void Update(GameTime gameTime, IBoard board)
        {
            var neighbors = this.GetNeighbors(board);
            if (neighbors.Any(neighbor => neighbor is Cheese))
            {
                this.GotTheCheese = true;
            }
            else if (neighbors.Any(neighbor => neighbor is Cat))
            {
                this.CaughtByCat = true;
            }
        }

        private IEnumerable<IDoodad> GetNeighbors(IBoard board)
        {
            return from doodad in board.Doodads
                   where Math.Abs(doodad.Column - this.Column) + Math.Abs(doodad.Row - this.Row) == 1
                   select doodad;
        }
    }
}