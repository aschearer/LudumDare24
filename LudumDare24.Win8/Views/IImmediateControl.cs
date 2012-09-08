using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare24.Views
{
    public interface IImmediateControl<in T>
    {
        void LoadContent(ContentManager content);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch, T dataContext);
    }
}