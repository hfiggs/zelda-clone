using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Audio
{
    public class AmbientSound
    {
        private string sound;
        private float timeDelay;
        private float vol;
        private bool looped;
        private bool intro = false;

        private SoundEffectInstance soundRef = null;
        private SoundEffectInstance soundSecondaryRef = null;

        private Tuple<string, float> introLoopedTuple;        

        public AmbientSound(string sound, float timeDelay = 0.0f, float vol = 1.0f, bool looped = true)
        {
            this.sound = sound;
            if (AudioManager.musicWithIntros.TryGetValue(sound, out introLoopedTuple))
                intro = true;
            this.timeDelay = timeDelay;
            this.vol = vol;
            this.looped = looped;
        }

        public void Play(float runtimeVolume = 1.0f, float runtimeDelay = 0.0f)
        {
            if (intro)
            {
                soundRef = AudioManager.PlayFireForget(sound, timeDelay + runtimeDelay, vol * runtimeVolume);
                if (intro)
                    soundSecondaryRef = AudioManager.PlayLooped(introLoopedTuple.Item1, introLoopedTuple.Item2 + runtimeDelay, vol * runtimeVolume);
            }
            else
            {
                if (looped)
                {
                    soundRef = AudioManager.PlayLooped(sound, timeDelay + runtimeDelay, vol * runtimeVolume);
                }
                else
                {
                    soundRef = AudioManager.PlayFireForget(sound, timeDelay + runtimeDelay, vol * runtimeVolume);
                }
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
