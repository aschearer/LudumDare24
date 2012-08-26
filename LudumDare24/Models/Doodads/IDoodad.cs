using System.Collections.Generic;
using LudumDare24.Models.Boards;
using Microsoft.Xna.Framework;

namespace LudumDare24.Models.Doodads
{
    public interface IDoodad
    {
        int Column { get; set; }
        int Row { get; set; }
        FallingState FallingState { get; }

        void Update(IEnumerable<IDoodad> doodads);
    }
}