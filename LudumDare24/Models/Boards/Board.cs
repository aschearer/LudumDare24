using System.Collections.Generic;
using LudumDare24.Models.Doodads;
using LudumDare24.Models.Units;

namespace LudumDare24.Models.Boards
{
    public class Board : IBoard
    {
        private readonly List<IDoodad> tiles;
        private readonly List<IUnit> units;

        public Board()
        {
            this.tiles = new List<IDoodad>();
            this.units = new List<IUnit>();
        }

        public IEnumerable<IDoodad> Tiles
        {
            get { return this.tiles; }
        }

        public IEnumerable<IUnit> Units
        {
            get { return this.units; }
        }

        public void AddTile(IDoodad doodad)
        {
            this.tiles.Add(doodad);
        }

        public void AddUnit(IUnit unit)
        {
            this.units.Add(unit);
        }
    }
}