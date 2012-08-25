using System.Collections.Generic;
using LudumDare24.Models.Doodads;

namespace LudumDare24.Models.Boards
{
    public class Board : IBoard
    {
        private readonly List<IDoodad> doodads;

        public Board()
        {
            this.doodads = new List<IDoodad>();
        }

        public IEnumerable<IDoodad> Doodads
        {
            get { return this.doodads; }
        }

        public void AddDoodad(IDoodad doodad)
        {
            this.doodads.Add(doodad);
        }
    }
}