using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
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
            this.StartNewGameCommand = new RelayCommand(this.StartNewGame);
        }

        public ICommand StartNewGameCommand { get; private set; }

        public IEnumerable<ITile> Tiles
        {
            get { return this.board.Tiles; }
        }

        public IEnumerable<IUnit> Units
        {
            get { return this.board.Units; }
        }

        private void StartNewGame()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    this.board.AddTile(new Tile(column, row));
                }
            }
        }
    }
}