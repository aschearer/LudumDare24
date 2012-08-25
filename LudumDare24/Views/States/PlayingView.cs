using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private readonly PlayingViewModel viewModel;
        private readonly ButtonView rotateClockwiseButton;
        private readonly ButtonView rotateCounterClockwiseButton;
        private readonly List<DoodadView> doodadViews;
        private bool isContentLoaded;
        private Texture2D boardTexture;

        public PlayingView(
            SpriteBatch spriteBatch, 
            ContentManager content, 
            IInputManager inputManager,
            PlayingViewModel viewModel)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.viewModel = viewModel;
            this.doodadViews = new List<DoodadView>();
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
            this.rotateClockwiseButton.Activate();
            this.rotateCounterClockwiseButton.Activate();
            this.viewModel.Doodads.CollectionChanged += this.OnDoodadsChanged;
            this.LoadContent();

            this.viewModel.StartNewGameCommand.Execute(null);
        }

        public void NavigateFrom()
        {
            this.rotateClockwiseButton.Deactivate();
            this.rotateCounterClockwiseButton.Deactivate();
            this.viewModel.Doodads.CollectionChanged -= this.OnDoodadsChanged;
        }

        public void Update(GameTime gameTime)
        {
            this.viewModel.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            Matrix rotationMatrix =
                Matrix.CreateTranslation(-Constants.GameAreaHalfSize, -Constants.GameAreaHalfSize, 0) *
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

            foreach (DoodadView doodadView in this.doodadViews)
            {
                doodadView.Draw(gameTime, this.spriteBatch);
            }

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

        private void OnDoodadsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (IDoodad doodad in e.NewItems)
                    {
                        var view = new DoodadView(doodad);
                        view.LoadContent(this.content);
                        this.doodadViews.Add(view);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach (DoodadView view in this.doodadViews)
                    {
                        view.Dispose();
                    }

                    this.doodadViews.Clear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void LoadContent()
        {
            if (this.isContentLoaded)
            {
                return;
            }

            this.boardTexture = this.content.Load<Texture2D>("Images/Doodads/Cage");
            this.rotateClockwiseButton.LoadContent(this.content);
            this.rotateCounterClockwiseButton.LoadContent(this.content);
            this.isContentLoaded = true;
        }
    }
}