using System;

namespace LudumDare24.Models.Sounds
{
    public interface ISoundManager
    {
        event EventHandler<SoundEventArgs> SoundPlayed;
        event EventHandler<EventArgs> MusicStarted;
        event EventHandler<EventArgs> MusicStopped;

        void PlayMusic();
        void PauseMusic();
        void PlaySound(string soundName);
    }

    public class SoundManager : ISoundManager
    {
        public event EventHandler<SoundEventArgs> SoundPlayed;
        public event EventHandler<EventArgs> MusicStarted;
        public event EventHandler<EventArgs> MusicStopped;

        private bool isMusicPlaying;

        public void PlayMusic()
        {
            if (!this.isMusicPlaying && this.MusicStarted != null)
            {
                this.MusicStarted(this, new EventArgs());
                this.isMusicPlaying = true;
            }
        }

        public void PauseMusic()
        {
            if (this.MusicStarted != null)
            {
                this.MusicStopped(this, new EventArgs());
                this.isMusicPlaying = false;
            }
        }

        public void PlaySound(string soundName)
        {
            if (this.SoundPlayed != null)
            {
                this.SoundPlayed(this, new SoundEventArgs(soundName));
            }
        }
    }
}
