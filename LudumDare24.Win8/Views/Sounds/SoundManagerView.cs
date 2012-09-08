using System;
using System.Collections.Generic;
using LudumDare24.Models.Sounds;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace LudumDare24.Views.Sounds
{
    public class SoundManagerView
    {
        private readonly ISoundManager soundManager;
        private readonly Random random;
        private readonly Dictionary<string, List<SoundEffectInstance>> sounds;
        private Song backgroundMusic;
        private bool isPlaying;

        public SoundManagerView(ISoundManager soundManager, Random random)
        {
            this.sounds = new Dictionary<string, List<SoundEffectInstance>>();
            this.soundManager = soundManager;
            this.random = random;
        }

        public void Activate()
        {
            this.soundManager.SoundPlayed += this.OnSoundPlayed;
            this.soundManager.MusicStarted += this.OnMusicStarted;
            this.soundManager.MusicStopped += this.OnMusicStopped;
        }

        public void Deactivate()
        {
            this.soundManager.SoundPlayed -= this.OnSoundPlayed;
            this.soundManager.MusicStarted -= this.OnMusicStarted;
            this.soundManager.MusicStopped -= this.OnMusicStopped;
        }

        public void LoadContent(ContentManager content)
        {
            this.sounds["Click"] = new List<SoundEffectInstance>();
            this.sounds["Click"].Add(content.Load<SoundEffect>("Sounds/Click").CreateInstance());

            this.backgroundMusic = content.Load<Song>("Sounds/BackgroundMusic");
        }

        private void OnSoundPlayed(object sender, SoundEventArgs e)
        {
            List<SoundEffectInstance> soundEffects = this.sounds[e.SoundName];
            soundEffects[this.random.Next(soundEffects.Count)].Play();
        }

        private void OnMusicStarted(object sender, EventArgs e)
        {
            if (this.isPlaying)
            {
               MediaPlayer.Resume();
            }
            else
            {
                MediaPlayer.Play(this.backgroundMusic);
                MediaPlayer.IsRepeating = true;
                this.isPlaying = true;
            }
        }

        private void OnMusicStopped(object sender, EventArgs e)
        {
            MediaPlayer.Pause();
        }
    }
}
