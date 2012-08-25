using System.Collections.ObjectModel;
using LudumDare24.Models.Doodads;

namespace LudumDare24.Models.Boards
{
    public interface IBoard
    {
        ObservableCollection<IDoodad> Doodads { get; }
    }
}