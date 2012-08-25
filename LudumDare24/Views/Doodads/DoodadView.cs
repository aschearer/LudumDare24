using System;
using System.Collections.Generic;
using LudumDare24.Models;
using LudumDare24.Models.Doodads;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare24.Views.Doodads
{
    public class DoodadView : IImmediateControl<IDoodad>
    {
        private Dictionary<Type, Texture2D> textures;
        private Dictionary<Type, Vector2> origins;

        public void LoadContent(ContentManager content)
        {
            this.textures = new Dictionary<Type, Texture2D>();
            this.textures[typeof(Cage)] = content.Load<Texture2D>("Images/Doodads/Cage");
            this.textures[typeof(Crate)] = content.Load<Texture2D>("Images/Doodads/Crate");

            this.origins = new Dictionary<Type, Vector2>();
            this.origins[typeof(Cage)] = new Vector2(this.textures[typeof(Cage)].Width / 2f, this.textures[typeof(Cage)].Height / 2f);
            this.origins[typeof(Crate)] = new Vector2(this.textures[typeof(Crate)].Width / 2f, this.textures[typeof(Crate)].Height / 2f);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IDoodad dataContext)
        {
            Texture2D texture = this.textures[dataContext.GetType()];
            Vector2 origin = this.origins[dataContext.GetType()];
            spriteBatch.Draw(
                texture,
                dataContext.Position * Constants.PixelsPerMeter,
                null,
                Color.White,
                dataContext.Rotation,
                origin,
                1,
                SpriteEffects.None,
                0);
        }
    }
}