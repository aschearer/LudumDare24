using System.Collections.Generic;
using LudumDare24.Models.Boards;
using LudumDare24.Models.Tiles;
using LudumDare24.Models.Units;

namespace LudumDare24.ViewModels.States
{
    public class PlayingViewModel
    {
        private readonly IBoard board;

        public PlayingViewModel(IBoard board)
        {
            this.board = board;
        }

        public IEnumerable<ITile> Tiles
        {
            get { return this.board.Tiles; }
        }

        public IEnumerable<IUnit> Units
        {
            get { return this.board.Units; }
        }
    }
}