using System;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace LudumDare24.Models.Doodads
{
    public class Cage : IDoodad
    {
        private const float Width = 250f / Constants.PixelsPerMeter;
        private const float Height = 10f / Constants.PixelsPerMeter;

        private readonly Body body;

        public Cage(World world, Vector2 position)
        {
            this.body = BodyFactory.CreateBody(world, position);
            this.body.BodyType = BodyType.Dynamic;
            this.CreateWall(world, new Vector2(-Cage.Width, 0), new Vector2(Cage.Height, Cage.Width));
            this.CreateWall(world, new Vector2(Cage.Width, 0), new Vector2(Cage.Height, Cage.Width));
            this.CreateWall(world, new Vector2(0, -Cage.Width), new Vector2(Cage.Width, Cage.Height));
            this.CreateWall(world, new Vector2(0, Cage.Width), new Vector2(Cage.Width, Cage.Height));

            Body jointBody = BodyFactory.CreateBody(world, position);
            RevoluteJoint joint = JointFactory.CreateRevoluteJoint(world, this.body, jointBody, Vector2.Zero);
            joint.MotorEnabled = true;
            joint.MaxMotorTorque = 5000;
            joint.MotorSpeed = 1f;
        }

        public Vector2 Position
        {
            get { return this.body.Position; }
        }

        private void CreateWall(World world, Vector2 position, Vector2 dimensions)
        {
            PolygonShape shape = new PolygonShape(1);
            shape.SetAsBox(dimensions.X, dimensions.Y, position, 0);
            Fixture fixture = this.body.CreateFixture(shape);
        }
    }
}