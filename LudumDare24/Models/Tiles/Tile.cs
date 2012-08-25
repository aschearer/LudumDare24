using System;

namespace LudumDare24.Models.Tiles
{
    public class Tile : ITile
    {
        private readonly int column;
        private readonly int row;
        private readonly Team team;

        public Tile(int column, int row, Team team)
        {
            this.column = column;
            this.team = team;
            this.row = row;
        }

        public int Column
        {
            get { return this.column; }
        }

        public int Row
        {
            get { return this.row; }
        }

        public Team Team
        {
            get { return this.team; }
        }
    }
}