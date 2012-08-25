using System;
using System.Collections.Generic;
using System.Linq;
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
        private Cage cage;
        private float targetRotation;
        private bool rotateClockwise;

        public PlayingViewModel(IBoard board, DoodadFactory doodadFactory, World world)
        {
            this.board = board;
            this.doodadFactory = doodadFactory;
            this.world = world;
            this.StartNewGameCommand = new RelayCommand<IEnumerable<DoodadPlacement>>(this.StartNewGame);
        }

        public ICommand StartNewGameCommand { get; private set; }

        public float Rotation { get; private set; }

        public IEnumerable<IDoodad> Tiles
        {
            get { return this.board.Tiles; }
        }

        public IEnumerable<IUnit> Units
        {
            get { return this.board.Units; }
        }

        public IEnumerable<IDoodad> Doodads
        {
            get { return this.world.BodyList.Where(body => body.UserData != null).Select(body => (IDoodad)body.UserData); }
        }

        public void Update(GameTime gameTime)
        {
            this.world.Step(PlayingViewModel.StepTime);

            foreach (Body body in this.world.BodyList)
            {
                if (body.UserData != null)
                {
                    IDoodad doodad = (IDoodad)body.UserData;
                    doodad.Update(gameTime);
                }
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
                    this.world.Gravity = adjustment * 20;
                    foreach (Body body in this.world.BodyList)
                    {
                        body.Awake = true;
                    }
                }
            }

            //this.world.Gravity = new Vector2((float)Math.Sin(this.Rotation), (float)Math.Cos(this.Rotation));
        }

        private void StartNewGame(IEnumerable<DoodadPlacement> doodadPlacements)
        {
            this.cage = (Cage)this.doodadFactory.CreateDoodad(typeof(Cage), new Vector2(Cage.HalfSize, Cage.HalfSize));

            foreach (DoodadPlacement placement in doodadPlacements)
            {
                Type type = Type.GetType(placement.DoodadType);
                this.doodadFactory.CreateDoodad(
                    type,
                    new Vector2(Crate.HalfSize + placement.Column * Crate.Size, Crate.HalfSize + placement.Row * Crate.Size));
            }
        }

        public void Rotate(bool clockwise)
        {
            this.rotateClockwise = clockwise;
            this.targetRotation += clockwise ? MathHelper.PiOver2 : -MathHelper.PiOver2;
        }
    }
}