using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace LudumDare24.Models.Doodads
{
    public class Doodad : IDoodad
    {
        private readonly Body body;

        public Doodad(World world, Vector2 position)
        {
            this.body = BodyFactory.CreateBody(world, position, this);
        }

        public Vector2 Position
        {
            get { return this.body.Position; }
        }
    }
}