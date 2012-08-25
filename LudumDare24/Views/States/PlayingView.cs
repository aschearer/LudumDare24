using System.Collections.Generic;
using LudumDare24.Models.Tiles;
using LudumDare24.Models.Units;
using LudumDare24.ViewModels.States;
using LudumDare24.Views.Tiles;
using LudumDare24.Views.Units;
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
        private readonly UnitView unitView;
        private readonly PlayingViewModel viewModel;
        private bool isContentLoaded;

        public PlayingView(
            SpriteBatch spriteBatch, 
            ContentManager content, 
            TileView tileView,
            UnitView unitView,
            PlayingViewModel viewModel)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.tileView = tileView;
            this.unitView = unitView;
            this.viewModel = viewModel;
        }

        public void NavigateTo()
        {
            this.LoadContent();
            this.viewModel.StartNewGameCommand.Execute(null);
        }

        public void NavigateFrom()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            this.spriteBatch.Begin();
            this.DrawTiles(gameTime, this.viewModel.Tiles);
            this.DrawUnits(gameTime, this.viewModel.Units);
            this.spriteBatch.End();
        }

        private void DrawTiles(GameTime gameTime, IEnumerable<ITile> tiles)
        {
            foreach (ITile tile in tiles)
            {
                this.tileView.Draw(gameTime, spriteBatch, tile);
            }
        }

        private void DrawUnits(GameTime gameTime, IEnumerable<IUnit> units)
        {
            foreach (IUnit unit in units)
            {
                this.unitView.Draw(gameTime, spriteBatch, unit);
            }
        }

        private void LoadContent()
        {
            if (this.isContentLoaded)
            {
                return;
            }

            this.tileView.LoadContent(this.content);
            this.unitView.LoadContent(this.content);
            this.isContentLoaded = true;
        }
    }
}