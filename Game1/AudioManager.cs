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
            //looped sounds
            musicMap.Add("dungeon", formatLooped(content.Load<SoundEffect>("audio/music/dungeon")));
            musicMap.Add("dungeon2", formatLooped(content.Load<SoundEffect>("audio/music/dungeonBass")));
            musicMap.Add("title", formatLooped(content.Load<SoundEffect>("audio/music/title")));
            musicMap.Add("title2", formatLooped(content.Load<SoundEffect>("audio/music/titleBass")));
            musicMap.Add("gameOver", formatLooped(content.Load<SoundEffect>("audio/music/gameOver")));
            musicMap.Add("gameOver2", formatLooped(content.Load<SoundEffect>("audio/music/gameOverBlasted")));
            soundMap.Add("aquamentusScream", formatLooped(content.Load<SoundEffect>("audio/sounds02/AquamentusScream")));
            soundMap.Add("boomerang", formatLooped(content.Load<SoundEffect>("audio/sounds/Boomerang")));
            soundMap.Add("lowHealth", formatLooped(content.Load<SoundEffect>("audio/sounds02/HealthLow")));

            //fire-forget sounds
            soundMap.Add("death", formatFireForget(content.Load<SoundEffect>("audio/sounds/death")));
            soundMap.Add("linkPop", formatFireForget(content.Load<SoundEffect>("audio/sounds02/LinkPop")));
            soundMap.Add("shield", formatFireForget(content.Load<SoundEffect>("audio/sounds02/Shield")));
            soundMap.Add("sword", formatFireForget(content.Load<SoundEffect>("audio/sounds02/Sword")));
            soundMap.Add("swordBeam", formatFireForget(content.Load<SoundEffect>("audio/sounds02/SwordBeam")));
            soundMap.Add("aquamentusScreamFF", formatFireForget(content.Load<SoundEffect>("audio/sounds02/AquamentusScream")));
            soundMap.Add("aquamentusHurt", formatFireForget(content.Load<SoundEffect>("audio/sounds02/AquamentusHurt")));
            soundMap.Add("bombExplode", formatFireForget(content.Load<SoundEffect>("audio/sounds02/BombExplode")));
            soundMap.Add("bombPlace", formatFireForget(content.Load<SoundEffect>("audio/sounds02/BombPlace")));
            soundMap.Add("chest", formatFireForget(content.Load<SoundEffect>("audio/sounds02/Chest")));
            soundMap.Add("deathEnemy", formatFireForget(content.Load<SoundEffect>("audio/sounds02/EnemyDeath")));
            soundMap.Add("powerPickUp", formatFireForget(content.Load<SoundEffect>("audio/sounds02/FairyAppear")));
            soundMap.Add("ocarina", formatFireForget(content.Load<SoundEffect>("audio/sounds02/Flute")));
            soundMap.Add("reveal", formatFireForget(content.Load<SoundEffect>("audio/sounds02/Hole")));
            soundMap.Add("heartPickUp", formatFireForget(content.Load<SoundEffect>("audio/sounds02/ItemPickup1")));
            soundMap.Add("key", formatFireForget(content.Load<SoundEffect>("audio/sounds02/KeyAppear")));
            soundMap.Add("rupeePickUp", formatFireForget(content.Load<SoundEffect>("audio/sounds02/Rupee")));
            soundMap.Add("stairs", formatFireForget(content.Load<SoundEffect>("audio/sounds02/Stairs")));
        }

        private static SoundEffectInstance formatLooped(SoundEffect original)
        {
            SoundEffectInstance formatted = original.CreateInstance();
            formatted.IsLooped = true;
            return formatted;
        }

        private static SoundEffectInstance formatFireForget(SoundEffect original)
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
