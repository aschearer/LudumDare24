using Microsoft.Xna.Framework;

namespace LudumDare24.Views
{
    public static class ScreenHelper
    {
        public static Vector2 CoordinatesToPixels(int column, int row)
        {
            return new Vector2(column * 80, row * 80);
        }
    }
}