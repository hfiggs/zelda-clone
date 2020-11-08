using Game1.Item;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using SharpDX.Direct2D1;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;

namespace Game1
{
    class AudioManager
    {
        //TODO: Refactor mutex setting and resetting to something more functional after room transitioning
        private static bool mutex = false;

        private static XmlDocument config;

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

        private static readonly float chestSoundLength = 2.0f;
        private static readonly float triforceSoundLength = 8.0f;


        public static void LoadContent(ContentManager content)
        {
            //looped sounds
            musicMap.Add("dungeon", content.Load<SoundEffect>("audio/music/dungeon"));
            musicMap.Add("dungeon2", content.Load<SoundEffect>("audio/music/dungeonBass"));
            musicMap.Add("title", content.Load<SoundEffect>("audio/music/title"));
            musicMap.Add("title2", content.Load<SoundEffect>("audio/music/titleBass"));
            musicMap.Add("gameOver", content.Load<SoundEffect>("audio/music/gameOver"));
            musicMap.Add("gameOver2", content.Load<SoundEffect>("audio/music/gameOverBlasted"));
            soundMap.Add("aquamentusScream", content.Load<SoundEffect>("audio/sounds/AquamentusScream"));
            soundMap.Add("boomerang", content.Load<SoundEffect>("audio/sounds/Boomerang"));
            soundMap.Add("lowHealth", content.Load<SoundEffect>("audio/sounds/HealthLow"));

            //fire-forget sounds
            soundMap.Add("death", content.Load<SoundEffect>("audio/sounds/death"));
            soundMap.Add("linkPop", content.Load<SoundEffect>("audio/sounds/LinkPop"));
            soundMap.Add("shield", content.Load<SoundEffect>("audio/sounds/Shield"));
            soundMap.Add("sword", content.Load<SoundEffect>("audio/sounds/Sword"));
            soundMap.Add("swordBeam", content.Load<SoundEffect>("audio/sounds/SwordBeam"));
            soundMap.Add("aquamentusHurt", content.Load<SoundEffect>("audio/sounds/AquamentusHurt"));
            soundMap.Add("bombExplode", content.Load<SoundEffect>("audio/sounds/BombExplode"));
            soundMap.Add("bombPlace", content.Load<SoundEffect>("audio/sounds/BombPlace"));
            soundMap.Add("chest", content.Load<SoundEffect>("audio/sounds/Chest"));
            soundMap.Add("enemyDeath", content.Load<SoundEffect>("audio/sounds/EnemyDeath"));
            soundMap.Add("powerPickUp", content.Load<SoundEffect>("audio/sounds/FairyAppear"));
            soundMap.Add("ocarina", content.Load<SoundEffect>("audio/sounds/Flute"));
            soundMap.Add("reveal", content.Load<SoundEffect>("audio/sounds/Hole"));
            soundMap.Add("itemPickUp", content.Load<SoundEffect>("audio/sounds/ItemPickup1"));
            soundMap.Add("key", content.Load<SoundEffect>("audio/sounds/KeyAppear"));
            soundMap.Add("rupeePickUp", content.Load<SoundEffect>("audio/sounds/Rupee"));
            soundMap.Add("stairs", content.Load<SoundEffect>("audio/sounds/Stairs"));
            soundMap.Add("linkHurt", content.Load<SoundEffect>("audio/sounds/PlayerHurt"));
            soundMap.Add("enemyHurt", content.Load<SoundEffect>("audio/sounds/EnemyHurt"));
            soundMap.Add("triforce", content.Load<SoundEffect>("audio/sounds/triforceTheme"));
            soundMap.Add("doorLock", content.Load<SoundEffect>("audio/sounds/LockedDoor"));

            ReadSoundSettings();
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

        //Only used for stair sounds. Should be changed once room transitioning is complete. Minimum viable product
        public static void PlayMutex(string sound, float timeDelay = 0.0f, float vol = 1.0f)
        {
            if(!mutex)
            {
                PlayFireForget(sound, timeDelay, vol);
                mutex = true;
            }
        }

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

        public static void StopMusic(SoundEffectInstance musicRef)
        {
            musicRef.Stop();
            //deliberately left unchecked
            activeMusicList.Remove(musicRef);
        }

        public static void StopSound(SoundEffectInstance soundRef)
        {
            soundRef.Stop();
            //deliberately left unchecked
            activeSoundList.Remove(soundRef);
        }

        public static void SetVolumeMusic(float vol)
        {
            foreach (SoundEffectInstance music in activeMusicList)
            {
                if (volumeMusic != 0)
                {
                    music.Volume = music.Volume / volumeMusic * vol;
                } else
                {
                    music.Volume = vol * volumeMaster;
                }
            }
            volumeMusic = vol;
            XmlNode settingsNode = config.GetElementsByTagName("Settings")[0]["SoundSettings"];
            settingsNode["Music"].InnerText = vol.ToString();
            config.Save("../../../../app.config");
        }

        public static void SetVolumeSound(float vol)
        {
            foreach (SoundEffectInstance sound in activeSoundList)
            {
                if (volumeSound != 0)
                {
                    sound.Volume = sound.Volume / volumeSound * vol;
                } else
                {
                    sound.Volume = vol * volumeMaster;
                }
            }
            volumeSound = vol;
            XmlNode settingsNode = config.GetElementsByTagName("Settings")[0]["SoundSettings"];
            settingsNode["Sound"].InnerText = vol.ToString();
            config.Save("../../../../app.config");
        }

        public static void SetVolumeMaster(float vol)
        {
            foreach (SoundEffectInstance music in activeMusicList)
            {
                if (volumeMaster != 0)
                {
                    music.Volume = music.Volume / volumeMaster * vol;
                }
                else
                {
                    music.Volume = vol * volumeMusic;
                }
            }
            foreach (SoundEffectInstance sound in activeSoundList)
            {
                if (volumeMaster != 0)
                {
                    sound.Volume = sound.Volume / volumeMaster * vol;
                }
                else
                {
                    sound.Volume = vol * volumeSound;
                }
            }
            volumeMaster = vol;
            XmlNode settingsNode = config.GetElementsByTagName("Settings")[0]["SoundSettings"];
            settingsNode["Volume"].InnerText = vol.ToString();
            config.Save("../../../../app.config");
        }

        public static void PlayItemSound(IItem item)
        {
            switch (item.GetType().Name)
            {
                case "RupeeBlue":
                case "RupeeYellow":
                    PlayFireForget("rupeePickUp");
                    break;
                case "Bomb":
                case "Compass":
                case "HeartContainer":
                case "Fairy":
                case "Clock":
                case "ArrowItem":
                case "ItemBoomerang":
                case "Map":
                    PlayFireForget("powerPickUp");
                    break;
                case "Bow":
                    StopAllMusic();
                    PlayFireForget("powerPickUp");
                    PlayFireForget("chest");
                    PlayLooped("dungeon", chestSoundLength);
                    break;
                case "Triforce":
                    StopAllMusic();
                    PlayFireForget("powerPickUp");
                    PlayFireForget("triforce");
                    PlayLooped("dungeon", triforceSoundLength);
                    break;
                default:
                    PlayFireForget("itemPickUp");
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

        private static void ReadSoundSettings()
        {
            config = new XmlDocument();
            config.Load("../../../../App.config");
            XmlNode settingsNode = config.GetElementsByTagName("Settings")[0]["SoundSettings"];
            volumeMaster = float.Parse(settingsNode["Volume"].InnerText);
            volumeMusic = float.Parse(settingsNode["Music"].InnerText);
            volumeSound = float.Parse(settingsNode["Sound"].InnerText);
        }
    }
}
