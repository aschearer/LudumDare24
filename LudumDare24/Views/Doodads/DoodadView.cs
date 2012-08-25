using System;
using LudumDare24.Models;
using LudumDare24.Models.Doodads;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare24.Views.Doodads
{
    public class DoodadView : IRetainedControl
    {
        private readonly IDoodad doodad;
        private Texture2D texture;
        private Vector2 origin;
        private Vector2 position;
        private TimeSpan movementTimer;
        private float targetRunTime;

        public DoodadView(IDoodad doodad)
        {
            this.doodad = doodad;
            this.position = new Vector2(
                Constants.TileSize * doodad.Column + Constants.TileHalfSize,
                Constants.TileSize * doodad.Row + Constants.TileHalfSize);
        }

        public void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("Images/Doodads/" + this.doodad.GetType().Name);
            this.origin = new Vector2(this.texture.Width / 2f, this.texture.Height / 2f);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 targetPosition = new Vector2(
                Constants.TileSize * doodad.Column + Constants.TileHalfSize,
                Constants.TileSize * doodad.Row + Constants.TileHalfSize);

            Vector2 currentPosition = this.position;
            if (targetPosition != this.position && this.movementTimer == TimeSpan.Zero)
            {
                this.targetRunTime = ((targetPosition - this.position).Length() / Constants.TileSize) * 0.1f;
                this.movementTimer = TimeSpan.FromSeconds(this.targetRunTime);
            }

            if (this.movementTimer > TimeSpan.Zero)
            {
                if (this.movementTimer > gameTime.ElapsedGameTime)
                {
                    this.movementTimer -= gameTime.ElapsedGameTime;
                    currentPosition = Vector2.Lerp(
                        this.position,
                        targetPosition,
                        (float)(1 - (this.movementTimer.TotalSeconds / this.targetRunTime)));
                }
                else
                {
                    this.movementTimer = TimeSpan.Zero;
                    this.position = targetPosition;
                    currentPosition = targetPosition;
                }
            }

            spriteBatch.Draw(
                this.texture,
                currentPosition,
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