using System;

namespace LudumDare24.Models.Sounds
{
    public class SoundEventArgs : EventArgs
    {
        public SoundEventArgs(string soundName)
        {
            this.SoundName = soundName;
        }

        public string SoundName { get; private set; }
    }
}