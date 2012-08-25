using LudumDare24.Models.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare24.Views.Tiles
{
    public class TileView : IImmediateControl<ITile>
    {
        private Texture2D tileTexture;

        public void LoadContent(ContentManager content)
        {
            this.tileTexture = content.Load<Texture2D>("Images/Tiles/Normal");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ITile dataContext)
        {
            spriteBatch.Draw(
                this.tileTexture,
                ScreenHelper.CoordinatesToPixels(dataContext.Column, dataContext.Row),
                Color.White);
        }
    }
}