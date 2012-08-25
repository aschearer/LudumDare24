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
            this.textures[typeof(Crate)] = content.Load<Texture2D>("Images/Doodads/Crate");
            this.textures[typeof(Mouse)] = content.Load<Texture2D>("Images/Doodads/Mouse");
            this.textures[typeof(Cheese)] = content.Load<Texture2D>("Images/Doodads/Cheese");
            this.textures[typeof(Cat)] = content.Load<Texture2D>("Images/Doodads/Cat");
            this.textures[typeof(Peg)] = content.Load<Texture2D>("Images/Doodads/Peg");
            this.textures[typeof(Balloon)] = content.Load<Texture2D>("Images/Doodads/Balloon");

            this.origins = new Dictionary<Type, Vector2>();
            this.origins[typeof(Crate)] = new Vector2(this.textures[typeof(Crate)].Width / 2f, this.textures[typeof(Crate)].Height / 2f);
            this.origins[typeof(Mouse)] = new Vector2(this.textures[typeof(Mouse)].Width / 2f, this.textures[typeof(Mouse)].Height / 2f);
            this.origins[typeof(Cheese)] = new Vector2(this.textures[typeof(Cheese)].Width / 2f, this.textures[typeof(Cheese)].Height / 2f);
            this.origins[typeof(Cat)] = new Vector2(this.textures[typeof(Cat)].Width / 2f, this.textures[typeof(Cat)].Height / 2f);
            this.origins[typeof(Peg)] = new Vector2(this.textures[typeof(Peg)].Width / 2f, this.textures[typeof(Peg)].Height / 2f);
            this.origins[typeof(Peg)] = new Vector2(this.textures[typeof(Balloon)].Width / 2f, this.textures[typeof(Balloon)].Height / 2f);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IDoodad dataContext)
        {
            Texture2D texture = this.textures[dataContext.GetType()];
            Vector2 origin = this.origins[dataContext.GetType()];
            //spriteBatch.Draw(
            //    texture,
            //    dataContext.Position * Constants.PixelsPerMeter,
            //    null,
            //    Color.White,
            //    dataContext.Rotation,
            //    origin,
            //    1,
            //    SpriteEffects.None,
            //    0);
        }
    }
}