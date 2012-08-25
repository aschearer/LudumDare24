using System;
using LudumDare24.Models.Doodads;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare24.Views.Doodads
{
    public class RetainedDoodadView : IRetainedControl
    {
        private readonly IDoodad doodad;
        private Texture2D texture;
        private Vector2 origin;
        private Vector2 position;

        public RetainedDoodadView(IDoodad doodad)
        {
            this.doodad = doodad;
            this.position = new Vector2(100 * doodad.Column, 100 * doodad.Row);
        }

        public void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("Images/Doodads/" + this.doodad.GetType().Name);
            this.origin = new Vector2(this.texture.Width / 2f, this.texture.Height / 2f);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.texture,
                this.position,
                null,
                Color.White,
                0,
                this.origin,
                1,
                SpriteEffects.None,
                0);
        }

        public void Dispose()
        {
        }
    }
}