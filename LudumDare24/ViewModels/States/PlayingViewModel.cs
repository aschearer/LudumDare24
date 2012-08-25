using System.Collections.Generic;
using System.Windows.Input;
using FarseerPhysics.Dynamics;
using GalaSoft.MvvmLight.Command;
using LudumDare24.Models;
using LudumDare24.Models.Boards;
using LudumDare24.Models.Doodads;
using LudumDare24.Models.Units;
using Microsoft.Xna.Framework;

namespace LudumDare24.ViewModels.States
{
    public class PlayingViewModel
    {
        private const float StepTime = 1 / 60f;

        private readonly IBoard board;
        private readonly DoodadFactory doodadFactory;
        private readonly World world;

        public PlayingViewModel(IBoard board, DoodadFactory doodadFactory, World world)
        {
            this.board = board;
            this.doodadFactory = doodadFactory;
            this.world = world;
            this.StartNewGameCommand = new RelayCommand(this.StartNewGame);
        }

        public ICommand StartNewGameCommand { get; private set; }

        public IEnumerable<IDoodad> Tiles
        {
            get { return this.board.Tiles; }
        }

        public IEnumerable<IUnit> Units
        {
            get { return this.board.Units; }
        }

        public void Update(GameTime gameTime)
        {
            this.world.Step(PlayingViewModel.StepTime);
        }

        private void StartNewGame()
        {
            Vector2 position = new Vector2(Constants.ScreenWidth / 2f, Constants.ScreenHeight / 2f);
            var cage = this.doodadFactory.CreateDoodad<Cage>(position / Constants.PixelsPerMeter);

            this.doodadFactory.CreateDoodad<Crate>(new Vector2(11, 12));
            this.doodadFactory.CreateDoodad<Crate>(new Vector2(13.5f, 9));
            this.doodadFactory.CreateDoodad<Crate>(new Vector2(12.5f, 7));

            //float size = 80 / 30f;
            //for (int row = 0; row < 4; row++)
            //{
            //    for (int column = 0; column < 4; column++)
            //    {
            //        this.board.AddTile(this.doodadFactory.CreateDoodad<Doodad>(new Vector2(2 + size * column, 2 + size * row)));
            //    }
            //}
        }
    }
}