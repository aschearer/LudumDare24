using Microsoft.Xna.Framework;

namespace LudumDare24.Views
{
    /// <summary>
    /// Responsible for rendering a single screen.
    /// </summary>
    public interface IScreenView
    {
        void NavigateTo();
        void NavigateFrom();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}