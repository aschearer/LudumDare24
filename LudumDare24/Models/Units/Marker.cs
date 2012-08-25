namespace LudumDare24.Models.Units
{
    public class Marker : IUnit
    {
        private readonly int column;
        private readonly int row;

        public Marker(int column, int row)
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