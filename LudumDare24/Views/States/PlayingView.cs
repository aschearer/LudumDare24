using System.Collections.Generic;
using LudumDare24.Models.Tiles;
using LudumDare24.ViewModels.States;
using LudumDare24.Views.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare24.Views.States
{
    public class PlayingView : IScreenView
    {
        private readonly SpriteBatch spriteBatch;
        private readonly ContentManager content;
        private readonly TileView tileView;
        private readonly PlayingViewModel viewModel;
        private bool isContentLoaded;

        public PlayingView(
            SpriteBatch spriteBatch, 
            ContentManager content, 
            TileView tileView,
            PlayingViewModel viewModel)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.tileView = tileView;
            this.viewModel = viewModel;
        }

        public void NavigateTo()
        {
            this.LoadContent();
        }

        public void NavigateFrom()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            this.DrawTiles(gameTime, this.viewModel.Tiles);
        }

        private void DrawTiles(GameTime gameTime, IEnumerable<ITile> tiles)
        {
            foreach (ITile tile in tiles)
            {
                this.tileView.Draw(gameTime, spriteBatch, tile);
            }
        }

        private void LoadContent()
        {
            if (this.isContentLoaded)
            {
                return;
            }

            this.tileView.LoadContent(this.content);
            this.isContentLoaded = true;
        }
    }
}