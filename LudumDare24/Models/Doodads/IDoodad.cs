using Microsoft.Xna.Framework;

namespace LudumDare24.Models.Doodads
{
    public interface IDoodad
    {
        int Column { get; }
        int Row { get; }

        void Update(GameTime gameTime);
    }
}