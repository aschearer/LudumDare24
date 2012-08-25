using System;
using LudumDare24.Models;
using Microsoft.Xna.Framework;

namespace LudumDare24.Views
{
    public class TeamView
    {
        public Color GetColorForTeam(Team team)
        {
            switch (team)
            {
                case Team.None:
                    return Color.White;
                case Team.One:
                    return Color.Red;
                case Team.Two:
                    return Color.Blue;
                default:
                    throw new ArgumentOutOfRangeException("team");
            }
        }
    }
}