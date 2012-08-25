using System.Collections.Generic;
using LudumDare24.Models.Tiles;
using LudumDare24.Models.Units;

namespace LudumDare24.Models.Boards
{
    public class Board : IBoard
    {
        private readonly List<ITile> tiles;
        private readonly List<IUnit> units;

        public Board()
        {
            this.tiles = new List<ITile>();
            this.units = new List<IUnit>();
        }

        public IEnumerable<ITile> Tiles
        {
            get { return this.tiles; }
        }

        public IEnumerable<IUnit> Units
        {
            get { return this.units; }
        }

        public void AddTile(ITile tile)
        {
            this.tiles.Add(tile);
        }

        public void AddUnit(IUnit unit)
        {
            this.units.Add(unit);
        }
    }
}