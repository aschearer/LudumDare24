namespace LudumDare24.Models.Tiles
{
    public interface ITile
    {
        int Column { get; }
        int Row { get; }
        Team Team { get; }
    }
}