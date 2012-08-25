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

        public T CreateDoodad<T>(Vector2 position)
        {
            return (T)typeof(T).GetConstructors().First().Invoke(new object[] { this.world, position });
        }
    }
}