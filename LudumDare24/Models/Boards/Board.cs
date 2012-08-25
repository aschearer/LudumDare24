using System.Collections.ObjectModel;
using LudumDare24.Models.Doodads;

namespace LudumDare24.Models.Boards
{
    public class Board : IBoard
    {
        private readonly ObservableCollection<IDoodad> doodads;

        public Board()
        {
            this.doodads = new ObservableCollection<IDoodad>();
        }

        public ObservableCollection<IDoodad> Doodads
        {
            get { return this.doodads; }
        }
    }
}