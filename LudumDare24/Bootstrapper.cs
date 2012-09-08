using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using GalaSoft.MvvmLight.Ioc;
using LudumDare24.Models;
using LudumDare24.Models.Boards;
using LudumDare24.Models.Doodads;
using LudumDare24.Models.Levels;
using LudumDare24.Models.Sessions;
using LudumDare24.ViewModels;
using LudumDare24.ViewModels.States;
using LudumDare24.Views;
using LudumDare24.Views.Doodads;
using LudumDare24.Views.Input;
using LudumDare24.Views.States;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare24
{
    public class Bootstrapper
    {
        public Bootstrapper(ContentManager content, SpriteBatch spriteBatch)
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Framework
            SimpleIoc.Default.Register(() => content);
            SimpleIoc.Default.Register(() => spriteBatch);
            SimpleIoc.Default.Register(() => new Random());

            // Models
            SimpleIoc.Default.Register<World>(() => new World(new Vector2(0, 60f/ 3)));
            SimpleIoc.Default.Register<IBoard>(() => new Board(Constants.NumberOfColumns, Constants.NumberOfRows));
            SimpleIoc.Default.Register<BoardPacker>(() => new BoardPacker(Constants.NumberOfColumns, Constants.NumberOfRows));
            SimpleIoc.Default.Register<DoodadFactory>();
            SimpleIoc.Default.Register<LevelFactory>();
            SimpleIoc.Default.Register<Session>();
            SimpleIoc.Default.Register<SessionManager>();

            // View Models
            SimpleIoc.Default.Register<IConductorViewModel, ConductorViewModel>();
            SimpleIoc.Default.Register<PlayingViewModel>();

            // Views
            SimpleIoc.Default.Register<IInputManager, MouseInputManager>();
            SimpleIoc.Default.Register<MouseInputManager>(() => (MouseInputManager)this.GetInstance<IInputManager>());
            SimpleIoc.Default.Register<ConductorView>();
            SimpleIoc.Default.Register<PlayingView>();
            SimpleIoc.Default.Register<DoodadView>();

            List<IScreenView> screenViews = new List<IScreenView>();
            screenViews.Add(this.GetInstance<PlayingView>());
            SimpleIoc.Default.Register<IEnumerable<IScreenView>>(() => screenViews);
        }

        public T GetInstance<T>()
        {
            return (T)SimpleIoc.Default.GetInstance(typeof(T));
        }
    }
}