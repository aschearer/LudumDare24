using LudumDare24.Models.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare24.Views.Units
{
    public class UnitView : IImmediateControl<IUnit>
    {
        private readonly TeamView teamView;
        private Texture2D tileTexture;

        public UnitView(TeamView teamView)
        {
            this.teamView = teamView;
        }

        public void LoadContent(ContentManager content)
        {
            this.tileTexture = content.Load<Texture2D>("Images/Units/Marker");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IUnit dataContext)
        {
            spriteBatch.Draw(
                this.tileTexture,
                ScreenHelper.CoordinatesToPixels(dataContext.Column, dataContext.Row),
                this.teamView.GetColorForTeam(dataContext.Team));
        }
    }
}