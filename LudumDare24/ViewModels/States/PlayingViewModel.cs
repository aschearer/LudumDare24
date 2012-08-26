using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using LudumDare24.Models.Boards;
using LudumDare24.Models.Doodads;
using LudumDare24.Models.Levels;
using Microsoft.Xna.Framework;
using Mouse = LudumDare24.Models.Doodads.Mouse;

namespace LudumDare24.ViewModels.States
{
    public class PlayingViewModel
    {
        private readonly IBoard board;
        private readonly BoardPacker boardPacker;
        private readonly LevelFactory levelFactory;
        private float targetRotation;
        private bool rotateClockwise;
        private TimeSpan cooldownTimer;

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
            this.boardPacker.Pack(this.board.Doodads, this.Rotation);

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

            if (this.cooldownTimer == TimeSpan.Zero)
            {
                foreach (IDoodad doodad in this.Doodads)
                {
                    doodad.Update(this.board.Doodads);
                }

                Mouse mouse = (Mouse)this.Doodads.First(doodad => doodad is Mouse);
                if (mouse.GotTheCheese)
                {
                    this.levelFactory.AdvanceToNextLevel();
                    this.levelFactory.LoadLevel();
                    this.Rotation = 0;
                    this.targetRotation = 0;
                }
                else if (mouse.CaughtByCat)
                {
                    this.levelFactory.LoadLevel();
                    this.Rotation = 0;
                    this.targetRotation = 0;
                }
            }
            else
            {
                if (this.cooldownTimer > gameTime.ElapsedGameTime)
                {
                    this.cooldownTimer -= gameTime.ElapsedGameTime;
                }
                else
                {
                    this.cooldownTimer = TimeSpan.Zero;
                }
            }
        }

        private void StartNewGame()
        {
            this.levelFactory.LoadLevel();
        }

        public void Rotate(bool clockwise)
        {
            if (this.cooldownTimer > TimeSpan.Zero)
            {
                return;
            }

            this.cooldownTimer = TimeSpan.FromSeconds(.75f);
            this.rotateClockwise = clockwise;
            this.targetRotation += clockwise ? MathHelper.PiOver2 : -MathHelper.PiOver2;
        }
    }
}