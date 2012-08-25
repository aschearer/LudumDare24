using System;
using System.Collections.Generic;
using LudumDare24.Models.Tiles;
using LudumDare24.Models.Units;

namespace LudumDare24.Models.Boards
{
    public interface IBoard
    {
        IEnumerable<ITile> Tiles { get; }
        IEnumerable<IUnit> Units { get; }

        void AddTile(ITile tile);
    }
}