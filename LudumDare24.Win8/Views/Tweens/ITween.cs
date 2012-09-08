using Microsoft.Xna.Framework;

namespace LudumDare24.Views.Tweens
{
    /// <summary>
    /// A utility class which makes it easy to interpolate between two values.
    /// </summary>
    /// <remarks>
    /// This class is used to create simple animations programatically. For instance
    /// to fade a screen into view or scale a button on click.
    /// </remarks>
    public interface ITween
    {
        float Value { get; }
        Repeat Repeats { get; set; }
        bool YoYos { get; set; }
        bool IsPaused { get; set; }
        bool IsFinished { get; }
        bool IsRunning { get; }
        bool IsReversed { get; }

        void Update(GameTime gameTime);
        void Restart();
        void Reverse();
    }
}