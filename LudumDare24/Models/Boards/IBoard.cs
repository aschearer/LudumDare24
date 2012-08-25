using System.Collections.ObjectModel;
using LudumDare24.Models.Doodads;

namespace LudumDare24.Models.Boards
{
    public interface IBoard
    {
        int NumberOfRows { get; }
        int NumberOfColumns { get; }
        ObservableCollection<IDoodad> Doodads { get; }
    }
}