using LudumDare24.Models;
using LudumDare24.ViewModels;
using LudumDare24.ViewModels.States;
using LudumDare24.Views;
using LudumDare24.Views.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumDare24
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class LudumDareMain : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private ConductorView conductorView;
        private MouseInputManager inputManager;

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

            IConductorViewModel conductorViewModel = bootstrapper.GetInstance<IConductorViewModel>();
            conductorViewModel.Push(typeof(PlayingViewModel));
#if SILVERLIGHT
            this.AddFont("appleberry", "./Content/Fonts/appleberry_with_cryllic.ttf#appleberry");
#endif
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            this.conductorView.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
