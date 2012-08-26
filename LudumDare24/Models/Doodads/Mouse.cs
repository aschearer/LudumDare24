using System;
using System.Collections.Generic;
using System.Linq;

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

        public override void Update(IEnumerable<IDoodad> doodads)
        {
            var neighbors = this.GetNeighbors(doodads);
            if (neighbors.Any(neighbor => neighbor is Cheese))
            {
                this.GotTheCheese = true;
            }
            else if (neighbors.Any(neighbor => neighbor is Cat))
            {
                this.CaughtByCat = true;
            }
        }

        private IEnumerable<IDoodad> GetNeighbors(IEnumerable<IDoodad> doodads)
        {
            return from doodad in doodads
                   where Math.Abs(doodad.Column - this.Column) + Math.Abs(doodad.Row - this.Row) == 1
                   select doodad;
        }
    }
}