using System;

namespace LudumDare24.Models.Units
{
    public class Marker : IUnit
    {
        private readonly int column;
        private readonly int row;
        private readonly Team team;

        public Marker(int column, int row, Team team)
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