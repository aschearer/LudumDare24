using System.Collections.Generic;
using LudumDare24.Models.Doodads;

namespace LudumDare24.Models.Boards
{
    public interface IBoard
    {
        IEnumerable<IDoodad> Doodads { get; }

        void AddDoodad(IDoodad doodad);
    }
}