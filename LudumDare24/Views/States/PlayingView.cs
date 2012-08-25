using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using LudumDare24.Models;
using LudumDare24.Models.Doodads;
using LudumDare24.ViewModels.States;
using LudumDare24.Views.Doodads;
using LudumDare24.Views.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare24.Views.States
{
    public class PlayingView : IScreenView
    {
        private readonly SpriteBatch spriteBatch;
        private readonly ContentManager content;
        private readonly DoodadView doodadView;
        private readonly PlayingViewModel viewModel;
        private readonly ButtonView rotateClockwiseButton;
        private readonly ButtonView rotateCounterClockwiseButton;
        private bool isContentLoaded;
        private DoodadPlacement[] placements;
        private Texture2D boardTexture;

        public PlayingView(
            SpriteBatch spriteBatch, 
            ContentManager content, 
            IInputManager inputManager,
            DoodadView doodadView,
            PlayingViewModel viewModel)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.doodadView = doodadView;
            this.viewModel = viewModel;
            this.rotateClockwiseButton = new ButtonView(
                inputManager,
                "Images/Playing/RotateClockwise",
                new Vector2(Constants.ScreenWidth - 100, Constants.ScreenHeight / 2));
            this.rotateClockwiseButton.Command = new RelayCommand(() => this.viewModel.Rotate(true));

            this.rotateCounterClockwiseButton = new ButtonView(
                inputManager,
                "Images/Playing/RotateCounterClockwise",
                new Vector2(100, Constants.ScreenHeight / 2));
            this.rotateCounterClockwiseButton.Command = new RelayCommand(() => this.viewModel.Rotate(false));
        }

        public void NavigateTo()
        {
            this.LoadContent();
            this.viewModel.StartNewGameCommand.Execute(this.placements);
            this.rotateClockwiseButton.Activate();
            this.rotateCounterClockwiseButton.Activate();
        }

        public void NavigateFrom()
        {
            this.rotateClockwiseButton.Deactivate();
            this.rotateCounterClockwiseButton.Deactivate();
        }

        public void Update(GameTime gameTime)
        {
            this.viewModel.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            Matrix rotationMatrix =
                Matrix.CreateTranslation(-Constants.BoardHalfSize, -Constants.BoardHalfSize, 0) *
                Matrix.CreateRotationZ(this.viewModel.Rotation) *
                Matrix.CreateTranslation(Constants.ScreenWidth / 2f, Constants.ScreenHeight / 2f, 0);
            this.spriteBatch.Begin(
                SpriteSortMode.Deferred,
                null,
                null,
                null,
                null,
                null,
                rotationMatrix);
            this.DrawDoodads(gameTime, this.viewModel.Doodads);
            this.spriteBatch.Draw(
                this.boardTexture,
                Vector2.Zero,
                Color.White);
            this.spriteBatch.End();

            this.spriteBatch.Begin();
            this.rotateClockwiseButton.Draw(gameTime, this.spriteBatch);
            this.rotateCounterClockwiseButton.Draw(gameTime, this.spriteBatch);
            this.spriteBatch.End();
        }

        private void DrawDoodads(GameTime gameTime, IEnumerable<IDoodad> doodads)
        {
            foreach (IDoodad doodad in doodads)
            {
                this.doodadView.Draw(gameTime, this.spriteBatch, doodad);
            }
        }

        private void LoadContent()
        {
            if (this.isContentLoaded)
            {
                return;
            }

            this.boardTexture = this.content.Load<Texture2D>("Images/Doodads/Cage");
            this.doodadView.LoadContent(this.content);
            this.rotateClockwiseButton.LoadContent(this.content);
            this.rotateCounterClockwiseButton.LoadContent(this.content);
            this.isContentLoaded = true;

            this.placements = new DoodadPlacement[6];
            placements[0] = new DoodadPlacement() { Column = 1, Row = 0, DoodadType = typeof(Crate).FullName };
            placements[1] = new DoodadPlacement() { Column = 1, Row = 2, DoodadType = typeof(Crate).FullName };
            placements[2] = new DoodadPlacement() { Column = 1, Row = 1, DoodadType = typeof(Crate).FullName };
            placements[3] = new DoodadPlacement() { Column = 4, Row = 0, DoodadType = typeof(Mouse).FullName };
            placements[4] = new DoodadPlacement() { Column = 0, Row = 0, DoodadType = typeof(Cheese).FullName };
            placements[5] = new DoodadPlacement() { Column = 3, Row = 1, DoodadType = typeof(Peg).FullName };
        }
    }
}