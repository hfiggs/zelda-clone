using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Game1
{
    class AudioManager
    {
        private static Dictionary<string, SoundEffectInstance> musicMap = new Dictionary<string, SoundEffectInstance>();
        private static Dictionary<string, SoundEffectInstance> soundMap = new Dictionary<string, SoundEffectInstance>();

        private static List<SoundEffectInstance> musicList = new List<SoundEffectInstance>();
        private static List<SoundEffectInstance> soundList = new List<SoundEffectInstance>();
        public static void LoadContent(ContentManager content)
        {
            musicMap.Add("dungeon", formatMusic(content.Load<SoundEffect>("audio/music/dungeon")));
            musicMap.Add("dungeon2", formatMusic(content.Load<SoundEffect>("audio/music/dungeonBass")));
            musicMap.Add("title", formatMusic(content.Load<SoundEffect>("audio/music/title")));
            musicMap.Add("title2", formatMusic(content.Load<SoundEffect>("audio/music/titleBass")));
            musicMap.Add("gameOver", formatMusic(content.Load<SoundEffect>("audio/music/gameOver")));
            musicMap.Add("gameOver2", formatMusic(content.Load<SoundEffect>("audio/music/gameOverBlasted")));
            
            soundMap.Add("death", formatSound(content.Load<SoundEffect>("audio/sounds/death")));
        }

        private static SoundEffectInstance formatMusic(SoundEffect original)
        {
            SoundEffectInstance formatted = original.CreateInstance();
            formatted.IsLooped = true;
            return formatted;
        }

        private static SoundEffectInstance formatSound(SoundEffect original)
        {
            SoundEffectInstance formatted = original.CreateInstance();
            return formatted;
        }

        public static void PlayMusic(string music)
        {
            SoundEffectInstance toPlay;
            if (musicMap.TryGetValue(music, out toPlay))
            {
                musicList.Add(toPlay);
                toPlay.Play();
            }
            else
            {
                throw new NotImplementedException(music + " is not a supported music name.");
            }
        }

        public static void PlaySound(string sound)
        {
            SoundEffectInstance toPlay;
            if(soundMap.TryGetValue(sound, out toPlay))
            {
                soundList.Add(toPlay);
                toPlay.Play();
            } 
            else
            {
                throw new NotImplementedException(sound + " is not a supported sound name.");
            }
        }

        public static void stopMusic()
        {
            foreach(SoundEffectInstance music in musicList)
            {
                music.Stop();
            }
        }

        public static void volumeMusic(float vol)
        {
            foreach(SoundEffectInstance music in musicList)
            {
                music.Volume = vol;
            }
        }

        public static void stopSound()
        {
            foreach(SoundEffectInstance sound in soundList)
            {
                sound.Stop();
            }
        }

        public static void volumeSound(float vol)
        {
            foreach (SoundEffectInstance sound in soundList)
            {
                sound.Volume = vol;
            }
        }
    }
}
