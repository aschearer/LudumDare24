namespace LudumDare24.Models.Doodads
{
    public class Balloon : Doodad
    {
        public Balloon(int column, int row)
            : base(column, row)
        {
            this.FallingState = FallingState.Up;
        }
    }
}