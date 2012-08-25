using System;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace LudumDare24.Models.Doodads
{
    public class Crate : IDoodad
    {
        public const float Size = 100f / Constants.PixelsPerMeter;
        public const float HalfSize = Crate.Size / 2f;

        private readonly Body body;

        public Crate(World world, Vector2 position)
        {
            this.body = BodyFactory.CreateBody(world, position, this);
            this.body.BodyType = BodyType.Dynamic;
            PolygonShape shape = new PolygonShape(0.2f);
            shape.SetAsBox(Crate.HalfSize, Crate.HalfSize);
            Fixture fixture = this.body.CreateFixture(shape);
        }

        public Vector2 Position
        {
            get { return this.body.Position; }
        }

        public float Rotation
        {
            get { return this.body.Rotation; }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}