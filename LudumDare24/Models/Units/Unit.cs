namespace LudumDare24.Models.Units
{
    public class Unit : IUnit
    {
        private readonly int column;
        private readonly int row;

        public Unit(int column, int row)
        {
            this.column = column;
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
    }
}