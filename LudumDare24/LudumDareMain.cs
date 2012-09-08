using LudumDare24.Models;
using LudumDare24.Models.Sessions;
using LudumDare24.Models.Sounds;
using LudumDare24.ViewModels;
using LudumDare24.ViewModels.States;
using LudumDare24.Views;
using LudumDare24.Views.Input;
using LudumDare24.Views.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LudumDare24
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class LudumDareMain : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ConductorView conductorView;
        private MouseInputManager inputManager;
        private SessionManager sessionManager;
        private ISoundManager soundManager;
        private SoundManagerView soundManagerView;

        public LudumDareMain()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            this.graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Bootstrapper bootstrapper = new Bootstrapper(this.Content, this.spriteBatch);

            this.conductorView = bootstrapper.GetInstance<ConductorView>();
            this.inputManager = bootstrapper.GetInstance<MouseInputManager>();
            this.sessionManager = bootstrapper.GetInstance<SessionManager>();
            this.sessionManager.ReadSession();
            this.soundManager = bootstrapper.GetInstance<ISoundManager>();
            this.soundManagerView = bootstrapper.GetInstance<SoundManagerView>();

            this.soundManagerView.LoadContent(this.Content);
            this.soundManagerView.Activate();
            this.soundManager.PlayMusic();

            IConductorViewModel conductorViewModel = bootstrapper.GetInstance<IConductorViewModel>();
            conductorViewModel.Push(typeof(PlayingViewModel));
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState state = Mouse.GetState();
            this.inputManager.Update(state.LeftButton, new Point(state.X, state.Y));

            this.conductorView.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void OnExiting(object sender, System.EventArgs args)
        {
            base.OnExiting(sender, args);
            this.sessionManager.WriteSession();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            this.conductorView.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
