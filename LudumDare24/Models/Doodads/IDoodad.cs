using Microsoft.Xna.Framework;

namespace LudumDare24.Models.Doodads
{
    public interface IDoodad
    {
        Vector2 Position { get; }
        float Rotation { get; }

        void Update(GameTime gameTime);
    }
}