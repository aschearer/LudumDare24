﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LudumDare24.Win8
{
    public sealed partial class MainPage : SwapChainBackgroundPanel
    {
        private readonly GameTimer gameTimer;
        private LudumDareMain game;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += this.OnLoad;

            this.gameTimer = new GameTimer();
            this.gameTimer.Draw += this.OnDraw;
            this.gameTimer.Update += this.OnUpdate;
            this.gameTimer.UpdateInterval = TimeSpan.Zero;

            var services = new AppServiceProvider();
            services.AddService(typeof(IGraphicsDeviceService), SharedGraphicsDeviceManager.Current);

            this.game = new LudumDareMain(SharedGraphicsDeviceManager.Current, new ContentManager(services, "Assets"));
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            var deviceManager = SharedGraphicsDeviceManager.Current;
            deviceManager.PreferredBackBufferWidth = (int)this.ActualWidth;
            deviceManager.PreferredBackBufferHeight = (int)this.ActualHeight;
            deviceManager.SwapChainPanel = this;
            deviceManager.ApplyChanges();

            this.game.Initialize();

            this.gameTimer.Start();

            Window.Current.SizeChanged += this.OnWindowSizeChanged;
        }

        private void OnUpdate(object sender, GameTimerEventArgs e)
        {
            GameTime gameTime = new GameTime(e.TotalTime, e.ElapsedTime);
            this.game.Update(gameTime);
        }

        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.Black);
            GameTime gameTime = new GameTime(e.TotalTime, e.ElapsedTime);
            this.game.Draw(gameTime);
        }

        public void OnExit()
        {
            this.game.OnExiting();
        }

        private void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.PreferredBackBufferWidth = (int)e.Size.Width;
            SharedGraphicsDeviceManager.Current.PreferredBackBufferHeight = (int)e.Size.Height;
            SharedGraphicsDeviceManager.Current.ApplyChanges();

            switch (ApplicationView.Value)
            {
                case ApplicationViewState.FullScreenLandscape:
                    break;
                case ApplicationViewState.Filled:
                    break;
                case ApplicationViewState.Snapped:
                    break;
                case ApplicationViewState.FullScreenPortrait:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
