using System;

namespace LudumDare24.Views.Input
{
    public interface IKeyboardInputManager
    {
        /// <summary>
        /// Fired when keyboard key is down.
        /// </summary>
        event EventHandler<KeyboardEventArgs> KeyDown;
    }
}