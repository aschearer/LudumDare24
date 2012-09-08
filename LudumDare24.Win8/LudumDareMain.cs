using LudumDare24.Models.Sessions;
using LudumDare24.Models.Sounds;
using LudumDare24.ViewModels;
using LudumDare24.ViewModels.States;
using LudumDare24.Views;
using LudumDare24.Views.Input;
using LudumDare24.Views.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumDare24
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class LudumDareMain
    {
        private readonly IGraphicsDeviceService graphics;
        private readonly ContentManager content;
        private SpriteBatch spriteBatch;
        private ConductorView conductorView;
        private MouseInputManager inputManager;
        private SessionManager sessionManager;
        private ISoundManager soundManager;
        private SoundManagerView soundManagerView;

        public LudumDareMain(IGraphicsDeviceService graphics, ContentManager content)
        {
            this.graphics = graphics;
            this.content = content;
        }

        public void Initialize()
        {
            this.LoadContent();

            Bootstrapper bootstrapper = new Bootstrapper(this.content, this.spriteBatch);

            this.conductorView = bootstrapper.GetInstance<ConductorView>();
            this.inputManager = bootstrapper.GetInstance<MouseInputManager>();
            this.sessionManager = bootstrapper.GetInstance<SessionManager>();
            this.sessionManager.ReadSession();
            this.soundManager = bootstrapper.GetInstance<ISoundManager>();
            this.soundManagerView = bootstrapper.GetInstance<SoundManagerView>();

            this.soundManagerView.LoadContent(this.content);
            this.soundManagerView.Activate();
            this.soundManager.PlayMusic();

            IConductorViewModel conductorViewModel = bootstrapper.GetInstance<IConductorViewModel>();
            conductorViewModel.Push(typeof(PlayingViewModel));
        }

        private void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.graphics.GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        public void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            this.inputManager.Update(state.LeftButton, new Point(state.X, state.Y));

            this.conductorView.Update(gameTime);
        }

        public void OnExiting()
        {
            this.sessionManager.WriteSession();
        }

        public void Draw(GameTime gameTime)
        {
            this.graphics.GraphicsDevice.Clear(Color.White);

            this.conductorView.Draw(gameTime);
        }
    }
}
