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
        private bool animateLevelComplete;
        private bool animateLevelStart;
        private TimeSpan spinTimer;
        private TimeSpan spinInTimer;
        private bool levelCleared;

        public PlayingViewModel(
            IBoard board, 
            BoardPacker boardPacker,
            LevelFactory levelFactory)
        {
            this.board = board;
            this.boardPacker = boardPacker;
            this.levelFactory = levelFactory;
            this.StartNewGameCommand = new RelayCommand(this.StartNewGame);
            this.AdvanceLevelCommand = new RelayCommand(this.AdvanceToNewLevel);
            this.StartLevelCommand = new RelayCommand(this.StartLevel);
        }

        public ICommand StartNewGameCommand { get; private set; }

        public ICommand AdvanceLevelCommand { get; private set; }

        public ICommand StartLevelCommand { get; private set; }

        public float Rotation { get; private set; }

        public bool IsLevelComplete { get; set; }

        public ObservableCollection<IDoodad> Doodads
        {
            get { return this.board.Doodads; }
        }

        public void Update(GameTime gameTime)
        {
            if (this.animateLevelStart)
            {
                this.spinInTimer += gameTime.ElapsedGameTime;
                float theta = (float)(2 * MathHelper.Pi * (this.spinInTimer.TotalSeconds / 0.75f));
                this.Rotation = this.rotateClockwise ? theta : -theta;

                return;
            }

            if (this.animateLevelComplete)
            {
                float thetaSpeed = MathHelper.Pi / 25;
                this.Rotation += thetaSpeed * (this.rotateClockwise ? 1 : -1);
                this.spinTimer += gameTime.ElapsedGameTime;
                if (this.spinTimer > TimeSpan.FromSeconds(0.5f))
                {
                    this.IsLevelComplete = true;
                }

                return;
            }

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
                    this.levelCleared = true;
                    this.animateLevelComplete = true;
                    this.spinTimer = TimeSpan.Zero;
                }
                else if (mouse.CaughtByCat)
                {
                    this.levelCleared = false;
                    this.animateLevelComplete = true;
                    this.spinTimer = TimeSpan.Zero;
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

        private void AdvanceToNewLevel()
        {
            if (this.levelCleared)
            {
                this.levelFactory.AdvanceToNextLevel();
            }
            else
            {
                this.levelFactory.LoadLevel();
            }

            this.spinInTimer = TimeSpan.Zero;
            this.Rotation = 0;
            this.animateLevelStart = true;
            this.animateLevelComplete = false;
        }

        private void StartLevel()
        {
            this.cooldownTimer = TimeSpan.Zero;
            this.Rotation = 0;
            this.targetRotation = 0;
            this.IsLevelComplete = false;
            this.animateLevelStart = false;
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