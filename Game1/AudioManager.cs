using Game1.Item;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Game1
{
    class AudioManager
    {
        //TODO: Refactor mutex setting and resetting to something more functional
        private static bool mutex = false;

        private static float volumeMaster = 1.0f;
        private static float volumeMusic = 1.0f;
        private static float volumeSound = 1.0f;

        //used for delays and queuing
        private static List<SoundEffectInstance> soundQueue = new List<SoundEffectInstance>();
        private static List<double> delays = new List<double>();

        //used for looking up sounds
        private static Dictionary<string, SoundEffect> musicMap = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, SoundEffect> soundMap = new Dictionary<string, SoundEffect>();

        //used for muting an entire subset of sounds instantaneously - Consider deleting
        private static List<SoundEffectInstance> activeMusicList = new List<SoundEffectInstance>();
        private static List<SoundEffectInstance> activeSoundList = new List<SoundEffectInstance>();


        public static void LoadContent(ContentManager content)
        {
            //looped sounds
            musicMap.Add("dungeon", content.Load<SoundEffect>("audio/music/dungeon"));
            musicMap.Add("dungeon2", content.Load<SoundEffect>("audio/music/dungeonBass"));
            musicMap.Add("title", content.Load<SoundEffect>("audio/music/title"));
            musicMap.Add("title2", content.Load<SoundEffect>("audio/music/titleBass"));
            musicMap.Add("gameOver", content.Load<SoundEffect>("audio/music/gameOver"));
            musicMap.Add("gameOver2", content.Load<SoundEffect>("audio/music/gameOverBlasted"));
            soundMap.Add("aquamentusScream", content.Load<SoundEffect>("audio/sounds02/AquamentusScream"));
            soundMap.Add("boomerang", content.Load<SoundEffect>("audio/sounds/Boomerang"));
            soundMap.Add("lowHealth", content.Load<SoundEffect>("audio/sounds02/HealthLow"));

            //fire-forget sounds
            soundMap.Add("death", content.Load<SoundEffect>("audio/sounds/death"));
            soundMap.Add("linkPop", content.Load<SoundEffect>("audio/sounds02/LinkPop"));
            soundMap.Add("shield", content.Load<SoundEffect>("audio/sounds02/Shield"));
            soundMap.Add("sword", content.Load<SoundEffect>("audio/sounds02/Sword"));
            soundMap.Add("swordBeam", content.Load<SoundEffect>("audio/sounds02/SwordBeam"));
            soundMap.Add("aquamentusHurt", content.Load<SoundEffect>("audio/sounds02/AquamentusHurt"));
            soundMap.Add("bombExplode", content.Load<SoundEffect>("audio/sounds02/BombExplode"));
            soundMap.Add("bombPlace", content.Load<SoundEffect>("audio/sounds02/BombPlace"));
            soundMap.Add("chest", content.Load<SoundEffect>("audio/sounds02/Chest"));
            soundMap.Add("enemyDeath", content.Load<SoundEffect>("audio/sounds02/EnemyDeath"));
            soundMap.Add("powerPickUp", content.Load<SoundEffect>("audio/sounds02/FairyAppear"));
            soundMap.Add("ocarina", content.Load<SoundEffect>("audio/sounds02/Flute"));
            soundMap.Add("reveal", content.Load<SoundEffect>("audio/sounds02/Hole"));
            soundMap.Add("itemPickUp", content.Load<SoundEffect>("audio/sounds02/ItemPickup1"));
            soundMap.Add("key", content.Load<SoundEffect>("audio/sounds02/KeyAppear"));
            soundMap.Add("rupeePickUp", content.Load<SoundEffect>("audio/sounds02/Rupee"));
            soundMap.Add("stairs", content.Load<SoundEffect>("audio/sounds02/Stairs"));
            soundMap.Add("linkHurt", content.Load<SoundEffect>("audio/sounds02/PlayerHurt"));
            soundMap.Add("enemyHurt", content.Load<SoundEffect>("audio/sounds02/EnemyHurt"));
            soundMap.Add("triforce", content.Load<SoundEffect>("audio/sounds/triforceTheme"));
            soundMap.Add("doorLock", content.Load<SoundEffect>("audio/sounds02/LockedDoor"));
        }

        //Note that the volume parameter here is only for internal balancing between the volumes of each sound file
        public static SoundEffectInstance PlayFireForget(string sound, float timeDelay = 0.0f, float vol = 1.0f)
        {
            mutex = false;
            SoundEffectInstance reference;
            SoundEffect toPlay;
            if (soundMap.TryGetValue(sound, out toPlay))
            {
                SoundEffectInstance instanceToPlay = toPlay.CreateInstance();
                instanceToPlay.Volume = volumeSound * volumeMaster * vol;
                reference = instanceToPlay;
                soundQueue.Add(instanceToPlay);
                delays.Add(timeDelay);
                activeSoundList.Add(instanceToPlay);
            }
            else if (musicMap.TryGetValue(sound, out toPlay))
            {
                SoundEffectInstance instanceToPlay = toPlay.CreateInstance();
                instanceToPlay.Volume = volumeMusic * volumeMaster * vol;
                reference = instanceToPlay;
                soundQueue.Add(instanceToPlay);
                delays.Add(timeDelay);
                activeMusicList.Add(instanceToPlay);
            }
            else
            {
                throw new NotImplementedException(sound + " is not a supported name.");
            }
            return reference;
        }

        //Note that the volume parameter here is only for internal balancing between the volumes of each sound file
        public static SoundEffectInstance PlayLooped(string sound, float timeDelay = 0.0f, float vol = 1.0f)
        {
            mutex = false;
            SoundEffectInstance reference = null;
            SoundEffect toPlay;
            if (soundMap.TryGetValue(sound, out toPlay))
            {
                SoundEffectInstance instanceToPlay = toPlay.CreateInstance();
                instanceToPlay.Volume = volumeSound * volumeMaster * vol;
                instanceToPlay.IsLooped = true;
                reference = instanceToPlay;
                soundQueue.Add(instanceToPlay);
                delays.Add(timeDelay);
                activeSoundList.Add(instanceToPlay);
            }
            else if (musicMap.TryGetValue(sound, out toPlay))
            {
                SoundEffectInstance instanceToPlay = toPlay.CreateInstance();
                instanceToPlay.Volume = volumeMusic * volumeMaster * vol;
                instanceToPlay.IsLooped = true;
                reference = instanceToPlay;
                soundQueue.Add(instanceToPlay);
                delays.Add(timeDelay);
                activeMusicList.Add(instanceToPlay);
            }
            else
            {
                throw new NotImplementedException(sound + " is not a supported name.");
            }
            return reference;
        }

        public static void PlayMutex(string sound, float timeDelay = 0.0f, float vol = 1.0f)
        {
            if(!mutex)
            {
                PlayFireForget(sound, timeDelay, vol);
                mutex = true;
            }
        }

        //Works once only. Use with caution
        public static void StopAllMusic()
        {
            foreach (SoundEffectInstance music in activeMusicList)
            {
                if (music.State.HasFlag(SoundState.Playing))
                {
                    music.Stop();
                }
            }
        }

        public static void StopMusic(SoundEffectInstance musicRef)
        {
            musicRef.Stop();
            //deliberately left unchecked
            activeMusicList.Remove(musicRef);
        }

        public static void SetVolumeMusic(float vol)
        {
            volumeMusic = vol;
            foreach (SoundEffectInstance music in activeMusicList)
            {
                music.Volume = volumeMusic;
            }
        }

        public static void StopAllSound()
        {
            foreach (SoundEffectInstance sound in activeSoundList)
            {
                if (sound.State.HasFlag(SoundState.Playing))
                {
                    sound.Stop();
                }
            }
        }

        public static void StopSound(SoundEffectInstance soundRef)
        {
            soundRef.Stop();
            //deliberately left unchecked
            activeSoundList.Remove(soundRef);
        }

        public static void SetVolumeSound(float vol)
        {
            volumeSound = vol;
        }

        public static void SetVolumeMaster(float vol)
        {
            volumeMaster = vol;
        }

        public static void StopAudio()
        {
            StopAllMusic();
            StopAllSound();
            soundQueue = new List<SoundEffectInstance>();
            delays = new List<double>();
        }
        private static void garbageCollection()
        {
            //disabled for testing purposes
            return;
            List<SoundEffectInstance> replacement = new List<SoundEffectInstance>();
            foreach(SoundEffectInstance sound in activeMusicList)
            {
                if(!sound.State.HasFlag(SoundState.Stopped))
                {
                    replacement.Add(sound);
                }
            }
            activeSoundList = replacement;
            replacement.Clear();
            foreach(SoundEffectInstance music in activeMusicList)
            {
                if(!music.State.HasFlag(SoundState.Stopped))
                {
                    replacement.Add(music);
                }
            }
            activeMusicList = replacement;
        }

        public static void PlayItemSound(IItem item)
        {
            switch (item.GetType().Name)
            {
                case "RupeeBlue":
                case "RupeeYellow":
                    AudioManager.PlayFireForget("rupeePickUp");
                    break;
                case "Bomb":
                case "Compass":
                case "HeartContainer":
                case "Fairy":
                case "Clock":
                case "ArrowItem":
                case "ItemBoomerang":
                case "Map":
                    AudioManager.PlayFireForget("powerPickUp");
                    break;
                case "Bow":
                    AudioManager.StopAllMusic();
                    //AudioManager.StopMusic(AudioManager.musicMain);
                    AudioManager.PlayFireForget("powerPickUp");
                    AudioManager.PlayFireForget("chest");
                    AudioManager.PlayLooped("dungeon", 2.0f);
                    break;
                case "Triforce":
                    AudioManager.StopAllMusic();
                    //AudioManager.StopMusic(AudioManager.musicMain);
                    AudioManager.PlayFireForget("powerPickUp");
                    AudioManager.PlayFireForget("triforce");
                    AudioManager.PlayLooped("dungeon", 8.0f);
                    break;
                default:
                    AudioManager.PlayFireForget("itemPickUp");
                    break;
            }
        }

        public static void Update(GameTime gameTime)
        {
            //updates delay queue and sound queue
            List<double> newDelays = new List<double>();
            List<SoundEffectInstance> newSounds = new List<SoundEffectInstance>();
            int limit = soundQueue.Count;
            for(int i = 0; i < limit; i++)
            {
                double newDelay = delays.ElementAt(i) - gameTime.ElapsedGameTime.TotalSeconds;
                if (newDelay > 0.0) {
                    newDelays.Add(newDelay);
                    newSounds.Add(soundQueue.ElementAt(i));
                } else
                {
                    soundQueue.ElementAt(i).Play();
                }
            }
            delays = newDelays;
            soundQueue = newSounds;
        }
    }
}
