using System;
using System.Collections.Generic;
using LudumDare24.Models;
using LudumDare24.Models.Doodads;
using LudumDare24.Models.Units;
using LudumDare24.ViewModels.States;
using LudumDare24.Views.Doodads;
using LudumDare24.Views.Farseer;
using LudumDare24.Views.Input;
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
        private readonly IInputManager inputManager;
        private readonly DoodadView doodadView;
        private readonly DebugViewXNA debugView;
        private readonly PlayingViewModel viewModel;
        private bool isContentLoaded;
        private DoodadPlacement[] placements;

        public PlayingView(
            SpriteBatch spriteBatch, 
            ContentManager content, 
            IInputManager inputManager,
            DoodadView doodadView,
            DebugViewXNA debugView,
            PlayingViewModel viewModel)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.inputManager = inputManager;
            this.doodadView = doodadView;
            this.debugView = debugView;
            this.viewModel = viewModel;
        }

        public void NavigateTo()
        {
            this.LoadContent();
            this.viewModel.StartNewGameCommand.Execute(this.placements);
            this.inputManager.Click += this.OnClick;
        }

        public void NavigateFrom()
        {
            this.inputManager.Click -= this.OnClick;
        }

        public void Update(GameTime gameTime)
        {
            this.viewModel.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            Matrix rotationMatrix =
                Matrix.CreateTranslation(-Cage.HalfSize * Constants.PixelsPerMeter, -Cage.HalfSize * Constants.PixelsPerMeter, 0) *
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
            this.spriteBatch.End();

            var matrix = Matrix.CreateOrthographicOffCenter(
                            0,
                            Constants.ScreenWidth / Constants.PixelsPerMeter,
                            Constants.ScreenHeight / Constants.PixelsPerMeter,
                            0f,
                            0f,
                            1f);
            //matrix * Matrix.CreateRotationZ(this.viewModel.Rotation);

            //this.debugView.RenderDebugData(ref matrix);
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

            this.doodadView.LoadContent(this.content);
            this.debugView.LoadContent(this.spriteBatch.GraphicsDevice, this.content);
            this.isContentLoaded = true;

            this.placements = new DoodadPlacement[5];
            placements[0] = new DoodadPlacement() { Column = 1, Row = 0, DoodadType = typeof(Crate).FullName };
            placements[1] = new DoodadPlacement() { Column = 2, Row = 2, DoodadType = typeof(Crate).FullName };
            placements[2] = new DoodadPlacement() { Column = 1, Row = 1, DoodadType = typeof(Crate).FullName };
            placements[3] = new DoodadPlacement() { Column = 4, Row = 0, DoodadType = typeof(Mouse).FullName };
            placements[4] = new DoodadPlacement() { Column = 0, Row = 0, DoodadType = typeof(Cheese).FullName };
        }

        private void OnClick(object sender, InputEventArgs e)
        {
            this.viewModel.RotateCage();
        }
    }
}