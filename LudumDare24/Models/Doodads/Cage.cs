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
        private readonly RevoluteJoint joint;
        private float targetRotation;
        private bool isRotating;

        public Cage(World world, Vector2 position)
        {
            this.body = BodyFactory.CreateBody(world, position, this);
            this.body.BodyType = BodyType.Static;
            this.CreateWall(world, new Vector2(-Cage.Width - Cage.Height, 0), new Vector2(Cage.Height, Cage.Width));
            this.CreateWall(world, new Vector2(Cage.Width + Cage.Height, 0), new Vector2(Cage.Height, Cage.Width));
            this.CreateWall(world, new Vector2(0, -Cage.Width - Cage.Height), new Vector2(Cage.Width, Cage.Height));
            this.CreateWall(world, new Vector2(0, Cage.Width + Cage.Height), new Vector2(Cage.Width, Cage.Height));

            //Body jointBody = BodyFactory.CreateBody(world, position);
            //this.joint = JointFactory.CreateRevoluteJoint(world, this.body, jointBody, Vector2.Zero);
            //this.joint.MaxMotorTorque = 10000;
            //this.joint.MotorSpeed = 4f;
        }

        public void Rotate(bool clockwise)
        {
            //this.body.BodyType = BodyType.Dynamic;
            //this.joint.MotorEnabled = true;
            //this.targetRotation += clockwise ? -MathHelper.PiOver2 : MathHelper.PiOver2;
            //this.targetRotation = MathHelper.WrapAngle(this.targetRotation);
            //this.isRotating = true;
        }

        public float Rotation
        {
            get { return this.body.Rotation; }
        }

        public void Update(GameTime gameTime)
        {
            if (this.isRotating && Math.Abs(MathHelper.WrapAngle(this.body.Rotation) - this.targetRotation) < 0.05f)
            {
                this.joint.MotorEnabled = false;
                this.body.BodyType = BodyType.Static;
                this.body.Rotation = this.targetRotation;
                this.isRotating = false;
            }
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