using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using LudumDare24.Models;
using LudumDare24.Models.Boards;
using LudumDare24.Models.Doodads;
using Microsoft.Xna.Framework;

namespace LudumDare24.ViewModels.States
{
    public class PlayingViewModel
    {
        private readonly IBoard board;
        private readonly DoodadFactory doodadFactory;
        private float targetRotation;
        private bool rotateClockwise;
        private Vector2 gravity;

        public PlayingViewModel(IBoard board, DoodadFactory doodadFactory)
        {
            this.board = board;
            this.doodadFactory = doodadFactory;
            this.StartNewGameCommand = new RelayCommand<IEnumerable<DoodadPlacement>>(this.StartNewGame);
        }

        public ICommand StartNewGameCommand { get; private set; }

        public float Rotation { get; private set; }

        public ObservableCollection<IDoodad> Doodads
        {
            get { return this.board.Doodads; }
        }

        public void Update(GameTime gameTime)
        {
            foreach (IDoodad doodad in this.Doodads)
            {
                doodad.Update(gameTime);
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
                    this.gravity = adjustment * Constants.Gravity;
                }
            }
        }

        private void StartNewGame(IEnumerable<DoodadPlacement> doodadPlacements)
        {
            foreach (DoodadPlacement placement in doodadPlacements)
            {
                Type type = Type.GetType(placement.DoodadType);
                var doodad = this.doodadFactory.CreateDoodad(
                    type,
                    placement.Column,
                    placement.Row);
                this.board.Doodads.Add(doodad);
            }
        }

        public void Rotate(bool clockwise)
        {
            this.rotateClockwise = clockwise;
            this.targetRotation += clockwise ? MathHelper.PiOver2 : -MathHelper.PiOver2;
        }
    }
}