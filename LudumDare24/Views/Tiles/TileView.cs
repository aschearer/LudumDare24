using LudumDare24.Models.Doodads;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare24.Views.Tiles
{
    public class TileView : IImmediateControl<IDoodad>
    {
        private readonly TeamView teamView;
        private Texture2D tileTexture;

        public TileView(TeamView teamView)
        {
            this.teamView = teamView;
        }

        public void LoadContent(ContentManager content)
        {
            this.tileTexture = content.Load<Texture2D>("Images/Tiles/Normal");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IDoodad dataContext)
        {
            spriteBatch.Draw(
                this.tileTexture,
                ScreenHelper.MetersToPixels(dataContext.Position),
                Color.White);
        }
    }
}