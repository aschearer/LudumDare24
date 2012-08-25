using FarseerPhysics.Dynamics;

namespace LudumDare24.Models.Levels
{
    public class Level
    {
        private readonly World world;

        public Level(World world)
        {
            this.world = world;
        }
    }
}