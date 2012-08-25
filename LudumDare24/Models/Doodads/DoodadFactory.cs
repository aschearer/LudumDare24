using System;
using System.Linq;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace LudumDare24.Models.Doodads
{
    public class DoodadFactory
    {
        private readonly World world;

        public DoodadFactory(World world)
        {
            this.world = world;
        }

        public IDoodad CreateDoodad(Type doodadType, Vector2 position)
        {
            return (IDoodad)doodadType.GetConstructors().First().Invoke(new object[] { this.world, position });
        }
    }
}