using System.Collections.Generic;
using LudumDare24.Models.Doodads;
using LudumDare24.Models.Units;

namespace LudumDare24.Models.Boards
{
    public interface IBoard
    {
        IEnumerable<IDoodad> Tiles { get; }
        IEnumerable<IUnit> Units { get; }

        void AddTile(IDoodad doodad);
        void AddUnit(IUnit unit);
    }
}