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
        private SoundEffectInstance backgroundMusic;
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

            this.backgroundMusic = content.Load<SoundEffect>("Sounds/BackgroundMusic").CreateInstance();
            this.backgroundMusic.IsLooped = true;
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
                this.backgroundMusic.Resume();
            }
            else
            {
                this.backgroundMusic.Play();
                this.isPlaying = true;
            }
        }

        private void OnMusicStopped(object sender, EventArgs e)
        {
            this.backgroundMusic.Pause();
        }
    }
}
