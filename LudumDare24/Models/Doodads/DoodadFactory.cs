using System;
using System.Linq;

namespace LudumDare24.Models.Doodads
{
    public class DoodadFactory
    {
        public IDoodad CreateDoodad(Type doodadType, int column, int row)
        {
            return (IDoodad)doodadType.GetConstructors().First().Invoke(new object[] { column, row });
        }
    }
}