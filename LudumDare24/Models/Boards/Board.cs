using System.Collections.ObjectModel;
using LudumDare24.Models.Doodads;

namespace LudumDare24.Models.Boards
{
    public class Board : IBoard
    {
        private readonly ObservableCollection<IDoodad> doodads;

        public Board(int columns, int rows)
        {
            this.NumberOfColumns = columns;
            this.NumberOfRows = rows;
            this.doodads = new ObservableCollection<IDoodad>();
        }

        public int NumberOfColumns { get; private set; }

        public int NumberOfRows { get; private set; }

        public ObservableCollection<IDoodad> Doodads
        {
            get { return this.doodads; }
        }
    }
}