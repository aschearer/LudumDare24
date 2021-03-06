using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using GalaSoft.MvvmLight.Command;
using LudumDare24.Models;
using LudumDare24.Models.Doodads;
using LudumDare24.Models.Sounds;
using LudumDare24.ViewModels.States;
using LudumDare24.Views.Doodads;
using LudumDare24.Views.Input;
using LudumDare24.Views.Tweens;
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
        private readonly ITween translateOutTween;
        private readonly ITween translateInTween;
        private readonly ITween textTween;
        private bool isContentLoaded;
        private Texture2D boardTexture;
        private Texture2D stencilTexture;
        private SpriteFont levelHeaderFont;
        private readonly ButtonView spottedZebraButton;
        private Texture2D headerTexture;
        private Texture2D anExperimentByTexture;

        public PlayingView(
            SpriteBatch spriteBatch, 
            ContentManager content, 
            IInputManager inputManager,
            PlayingViewModel viewModel,
            ISoundManager soundManager)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.viewModel = viewModel;
            this.doodadViews = new List<DoodadView>();
            this.rotateClockwiseButton = new ButtonView(
                inputManager,
                "Images/Playing/RotateClockwise",
                new Vector2(Constants.ScreenWidth - 127, 428),
                soundManager);
            this.rotateClockwiseButton.Command = new RelayCommand(() => this.viewModel.Rotate(true));

            this.rotateCounterClockwiseButton = new ButtonView(
                inputManager,
                "Images/Playing/RotateCounterClockwise",
                new Vector2(127, 428),
                soundManager);
            this.rotateCounterClockwiseButton.Command = new RelayCommand(() => this.viewModel.Rotate(false));

            this.translateOutTween = TweenFactory.Tween(0, Constants.ScreenHeight, TimeSpan.FromSeconds(0.75f));
            this.translateOutTween.IsPaused = true;

            this.translateInTween = TweenFactory.Tween(Constants.ScreenHeight, 0, TimeSpan.FromSeconds(0.75f));
            this.translateInTween.IsPaused = true;

            this.textTween = TweenFactory.Tween(0, 1, TimeSpan.FromSeconds(0.3f));
            this.textTween.Reverse();
            this.textTween.IsPaused = true;

            this.spottedZebraButton = new ButtonView(
                inputManager,
                "Images/Playing/SpottedZebraLogo",
                new Vector2(Constants.ScreenWidth - 78, 36),
                soundManager);
            this.spottedZebraButton.Command = this.viewModel.OpenCompanyUrlCommand;
        }

        public void NavigateTo()
        {
            this.rotateClockwiseButton.Activate();
            this.rotateCounterClockwiseButton.Activate();
            this.spottedZebraButton.Activate();
            this.viewModel.Doodads.CollectionChanged += this.OnDoodadsChanged;
            this.LoadContent();

            this.viewModel.StartNewGameCommand.Execute(null);
        }

        public void NavigateFrom()
        {
            this.rotateClockwiseButton.Deactivate();
            this.rotateCounterClockwiseButton.Deactivate();
            this.spottedZebraButton.Deactivate();
            this.viewModel.Doodads.CollectionChanged -= this.OnDoodadsChanged;
        }

        public void Update(GameTime gameTime)
        {
            this.viewModel.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            //DepthStencilState stencilState = new DepthStencilState();
            //stencilState.StencilEnable = true;
            //stencilState.DepthBufferEnable = false;
            //stencilState.StencilFunction = CompareFunction.GreaterEqual;
            //stencilState.StencilPass = StencilOperation.Replace;
            //stencilState.ReferenceStencil = 1;

            //this.spriteBatch.Begin(
            //    SpriteSortMode.Deferred,
            //    null,
            //    null,
            //    null, // stencilState,
            //    null);
            //this.spriteBatch.Draw(this.stencilTexture, new Vector2(300, 300), Color.White);
            //this.spriteBatch.End();

            //DepthStencilState spotlightStencil = new DepthStencilState();
            //spotlightStencil.StencilEnable = true;
            //spotlightStencil.StencilFunction = CompareFunction.LessEqual;
            //spotlightStencil.StencilPass = StencilOperation.Keep;
            //spotlightStencil.ReferenceStencil = 1;

            float yOffset = 428;
            if (this.viewModel.IsLevelComplete)
            {
                if (!this.translateInTween.IsRunning && (this.translateOutTween.IsPaused || this.translateOutTween.IsFinished))
                {
                    this.translateOutTween.Restart();
                    this.textTween.Reverse();
                    this.textTween.Restart();
                }

                this.textTween.Update(gameTime);

                if (!this.translateOutTween.IsFinished)
                {
                    this.translateOutTween.Update(gameTime);
                    yOffset = 428 - this.translateOutTween.Value;
                    if (this.translateOutTween.IsFinished)
                    {
                        this.translateInTween.Restart();
                        this.textTween.Reverse();
                        this.textTween.Restart();
                        this.viewModel.AdvanceLevelCommand.Execute(null);
                    }
                }
                else
                {
                    this.translateInTween.Update(gameTime);
                    yOffset = 428 + this.translateInTween.Value;
                    if (this.translateInTween.IsFinished)
                    {
                        this.viewModel.StartLevelCommand.Execute(null);
                    }
                }
            }

            Matrix rotationMatrix =
                Matrix.CreateTranslation(-Constants.GameAreaHalfSize, -Constants.GameAreaHalfSize, 0) *
                Matrix.CreateRotationZ(this.viewModel.Rotation) *
                Matrix.CreateTranslation(Constants.ScreenWidth / 2f, yOffset, 0);

            this.spriteBatch.Begin(
                SpriteSortMode.Deferred,
                null,
                null,
                null, //spotlightStencil,
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

            this.spriteBatch.Draw(
                this.headerTexture,
                new Vector2(50, 20),
                Color.White);

            this.spriteBatch.Draw(
                this.anExperimentByTexture,
                new Vector2(Constants.ScreenWidth - 210, 48),
                Color.White);

            this.spriteBatch.DrawString(
                this.levelHeaderFont,
                "Level " + this.viewModel.CurrentLevel,
                new Vector2(262 - 100 * this.textTween.Value, 135),
                Color.Black * (1 - this.textTween.Value));

            string moveStringFormat = this.viewModel.CurrentMove == 1 ? "{0} Turn" : "{0} Turns";
            string moveString = string.Format(moveStringFormat, this.viewModel.CurrentMove);
            Vector2 textDimensions = this.levelHeaderFont.MeasureString(moveString);
            this.spriteBatch.DrawString(
                this.levelHeaderFont,
                moveString,
                new Vector2(760 + 100 * this.textTween.Value, 135),
                Color.Black * (1 - this.textTween.Value),
                0,
                new Vector2(textDimensions.X, 0), 
                1,
                SpriteEffects.None,
                0);

            this.spottedZebraButton.Draw(gameTime, this.spriteBatch);
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

            this.headerTexture = this.content.Load<Texture2D>("Images/Playing/Header");
            this.anExperimentByTexture = this.content.Load<Texture2D>("Images/Playing/AnExperimentBy");
            this.boardTexture = this.content.Load<Texture2D>("Images/Doodads/Cage");
            this.stencilTexture = this.content.Load<Texture2D>("Images/Playing/Mask");
            this.levelHeaderFont = this.content.Load<SpriteFont>("Fonts/ComicSans24");
            this.spottedZebraButton.LoadContent(this.content);
            this.rotateClockwiseButton.LoadContent(this.content);
            this.rotateCounterClockwiseButton.LoadContent(this.content);
            this.isContentLoaded = true;
        }
    }
}