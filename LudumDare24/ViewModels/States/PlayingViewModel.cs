using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using LudumDare24.Models.Boards;
using LudumDare24.Models.Doodads;
using LudumDare24.Models.Levels;
using Microsoft.Xna.Framework;

namespace LudumDare24.ViewModels.States
{
    public class PlayingViewModel
    {
        private readonly IBoard board;
        private readonly BoardPacker boardPacker;
        private readonly LevelFactory levelFactory;
        private float targetRotation;
        private bool rotateClockwise;

        public PlayingViewModel(
            IBoard board, 
            BoardPacker boardPacker,
            LevelFactory levelFactory)
        {
            this.board = board;
            this.boardPacker = boardPacker;
            this.levelFactory = levelFactory;
            this.StartNewGameCommand = new RelayCommand(this.StartNewGame);
        }

        public ICommand StartNewGameCommand { get; private set; }

        public float Rotation { get; private set; }

        public ObservableCollection<IDoodad> Doodads
        {
            get { return this.board.Doodads; }
        }

        public void Update(GameTime gameTime)
        {
            this.boardPacker.Pack(this.Rotation);
            foreach (IDoodad doodad in this.Doodads)
            {
                doodad.Update(gameTime, this.board);
            }

            if (Math.Abs(this.Rotation - this.targetRotation) > 0.01f)
            {
                float theta = MathHelper.Pi / 50;
                this.Rotation += this.rotateClockwise ? theta : -theta;
                if (Math.Abs(this.Rotation - this.targetRotation) <= 0.01f)
                {
                    this.Rotation = this.targetRotation;
                    Vector2 adjustment = new Vector2((float)Math.Sin(this.Rotation), (float)Math.Cos(this.Rotation));
                    adjustment.Normalize();
                }
            }
        }

        private void StartNewGame()
        {
            this.levelFactory.LoadLevel();
        }

        public void Rotate(bool clockwise)
        {
            this.rotateClockwise = clockwise;
            this.targetRotation += clockwise ? MathHelper.PiOver2 : -MathHelper.PiOver2;
        }
    }
}