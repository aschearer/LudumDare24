namespace LudumDare24.Models.Tiles
{
    public class Tile : ITile
    {
        private readonly int column;
        private readonly int row;

        public Tile(int column, int row)
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