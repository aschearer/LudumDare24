using System;
using Microsoft.Xna.Framework;

namespace LudumDare24.Models.Doodads
{
    public class Doodad : IDoodad
    {
        public Doodad(int column, int row)
        {
            this.Column = column;
            this.Row = row;
            this.FallingState = FallingState.Down;
        }

        public int Column { get; set; }

        public int Row { get; set; }

        public FallingState FallingState { get; protected set; }

        public void Update(GameTime gameTime)
        {
        }
    }
}