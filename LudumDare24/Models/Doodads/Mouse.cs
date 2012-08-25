using System;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace LudumDare24.Models.Doodads
{
    public class Mouse : IDoodad
    {
        private const float Size = 50f / Constants.PixelsPerMeter;

        private readonly Body body;

        public Mouse(World world, Vector2 position)
        {
            this.body = BodyFactory.CreateBody(world, position, this);
            this.body.BodyType = BodyType.Dynamic;
            PolygonShape shape = new PolygonShape(0.2f);
            shape.SetAsBox(Mouse.Size, Mouse.Size);
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