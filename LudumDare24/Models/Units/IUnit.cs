using System;

namespace LudumDare24.Models.Units
{
    public interface IUnit
    {
        int Column { get; }
        int Row { get; }
        Team Team { get; }
    }
}