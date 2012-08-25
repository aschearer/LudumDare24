using LudumDare24.Models.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare24.Views.Units
{
    public class UnitView : IImmediateControl<IUnit>
    {
        private Texture2D tileTexture;

        public void LoadContent(ContentManager content)
        {
            this.tileTexture = content.Load<Texture2D>("Images/Units/Marker");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IUnit dataContext)
        {
            spriteBatch.Draw(
                this.tileTexture,
                ScreenHelper.CoordinatesToPixels(dataContext.Column, dataContext.Row),
                Color.Black);
        }
    }
}