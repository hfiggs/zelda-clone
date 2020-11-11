using Game1.Audio;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.AudioManagement
{
    class AmbientSound
    {
        private string sound;
        private float timeDelay;
        private float vol;
        private bool looped;

        private SoundEffectInstance soundRef = null;

        public AmbientSound(string sound, float timeDelay = 0.0f, float vol = 1.0f, bool looped = true)
        {
            this.sound = sound;
            this.timeDelay = timeDelay;
            this.vol = vol;
            this.looped = looped;
        }

        public void Play()
        {
            if (looped)
            {
                soundRef = AudioManager.PlayLooped(sound, timeDelay, vol);
            }
            else
            {
                soundRef = AudioManager.PlayFireForget(sound, timeDelay, vol);
            }
        }

        public void Stop()
        {
            if (soundRef != null)
            {
                AudioManager.StopSound(soundRef);
            }
        }
    }
}
