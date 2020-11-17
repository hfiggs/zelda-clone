using Game1.Item;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Game1.Audio
{
    class AudioManager
    {
        //TODO: Refactor mutex setting and resetting to something more functional after room transitioning
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

        //used for muting an entire subset of sounds instantaneously
        private static List<SoundEffectInstance> activeMusicList = new List<SoundEffectInstance>();
        private static List<SoundEffectInstance> activeSoundList = new List<SoundEffectInstance>();

        private static readonly float chestSoundLength = 2.0f;

        private static bool stopSoundInit = false;
        private static bool paused = false;

        // Map Keys for audio
        const String overworld = "overworld", dungeon = "dungeon", dungeon2 = "dungeon2", title = "title", title2 = "title2", gameOver = "gameOver", gameOver2 = "gameOver2";
        const String aquamentusScream = "aquamentusScream", boomerang = "boomerang", lowHealth = "lowHealth", death = "death", linkPop = "linkPop", shield = "shield", sword = "sword";
        const String swordBeam = "swordBeam", aquamentusHurt = "aquamentusHurt", bombExplode = "bombExplode", bombPlace = "bombPlace", chest = "chest", enemyDeath = "enemyDeath";
        const String powerPickUp = "powerPickUp", ocarina = "ocarina", reveal = "reveal", itemPickUp = "itemPickUp", key = "key", rupeePickUp = "rupeePickUp", rupeeAddShort = "rupeeAddShort";
        const String rupeeAddLong = "rupeeAddLong", stairs = "stairs", linkHurt = "linkHurt", enemyHurt = "enemyHurt", triforce = "triforce", doorLock = "doorLock";

        // Map values for audio
        const String overworldPath = "audio/music/overworld", dungeonPath = "audio/music/dungeon", dungeon2Path = "audio/music/dungeonBass", titlePath = "audio/music/title", title2Path = "audio/music/titleBass", gameOverPath = "audio/music/gameOver", gameOver2Path = "audio/music/gameOverBlasted";
        const String aquamentusScreamPath = "audio/sounds/AquamentusScream", boomerangPath = "audio/sounds/Boomerang", lowHealthPath = "audio/sounds/HealthLow", deathPath = "audio/sounds/death", linkPopPath = "audio/sounds/LinkPop", shieldPath = "audio/sounds/Shield", swordPath = "audio/sounds/Sword";
        const String swordBeamPath = "audio/sounds/SwordBeam", aquamentusHurtPath = "audio/sounds/AquamentusHurt", bombExplodePath = "audio/sounds/BombExplode", bombPlacePath = "audio/sounds/BombPlace", chestPath = "audio/sounds/Chest", enemyDeathPath = "audio/sounds/EnemyDeath";
        const String powerPickUpPath = "audio/sounds/FairyAppear", ocarinaPath = "audio/sounds/Flute", revealPath = "audio/sounds/Hole", itemPickUpPath = "audio/sounds/ItemPickUp1", keyPath = "audio/sounds/KeyAppear", rupeePickUpPath = "audio/sounds/Rupee", rupeeAddShortPath = "audio/sounds/RupeeAddShort";
        const String rupeeAddLongPath = "audio/sounds/RupeeAddLong", stairsPath = "audio/sounds/Stairs", linkHurtPath = "audio/sounds/PlayerHurt", enemyHurtPath = "audio/sounds/EnemyHurt", triforcePath = "audio/sounds/triforceTheme", doorLockPath = "audio/sounds/LockedDoor";

        const String errorMessage = " is not a supported name.";
        public static void LoadContent(ContentManager content)
        {
            //looped sounds
            musicMap.Add(overworld, content.Load<SoundEffect>(overworldPath));
            musicMap.Add(dungeon, content.Load<SoundEffect>(dungeonPath));
            musicMap.Add(dungeon2, content.Load<SoundEffect>(dungeon2Path));
            musicMap.Add(title, content.Load<SoundEffect>(titlePath));
            musicMap.Add(title2, content.Load<SoundEffect>(title2Path));
            musicMap.Add(gameOver, content.Load<SoundEffect>(gameOverPath));
            musicMap.Add(gameOver2, content.Load<SoundEffect>(gameOver2Path));
            soundMap.Add(aquamentusScream, content.Load<SoundEffect>(aquamentusScreamPath));
            soundMap.Add(boomerang, content.Load<SoundEffect>(boomerangPath));
            soundMap.Add(lowHealth, content.Load<SoundEffect>(lowHealthPath));

            //fire-forget sounds
            soundMap.Add(death, content.Load<SoundEffect>(deathPath));
            soundMap.Add(linkPop, content.Load<SoundEffect>(linkPopPath));
            soundMap.Add(shield, content.Load<SoundEffect>(shieldPath));
            soundMap.Add(sword, content.Load<SoundEffect>(swordPath));
            soundMap.Add(swordBeam, content.Load<SoundEffect>(swordBeamPath));
            soundMap.Add(aquamentusHurt, content.Load<SoundEffect>(aquamentusHurtPath));
            soundMap.Add(bombExplode, content.Load<SoundEffect>(bombExplodePath));
            soundMap.Add(bombPlace, content.Load<SoundEffect>(bombPlacePath));
            soundMap.Add(chest, content.Load<SoundEffect>(chestPath));
            soundMap.Add(enemyDeath, content.Load<SoundEffect>(enemyDeathPath));
            soundMap.Add(powerPickUp, content.Load<SoundEffect>(powerPickUpPath));
            soundMap.Add(ocarina, content.Load<SoundEffect>(ocarinaPath));
            soundMap.Add(reveal, content.Load<SoundEffect>(revealPath));
            soundMap.Add(itemPickUp, content.Load<SoundEffect>(itemPickUpPath));
            soundMap.Add(key, content.Load<SoundEffect>(keyPath));
            soundMap.Add(rupeePickUp, content.Load<SoundEffect>(rupeePickUpPath));
            soundMap.Add(rupeeAddShort, content.Load<SoundEffect>(rupeeAddShortPath));
            soundMap.Add(rupeeAddLong, content.Load<SoundEffect>(rupeeAddLongPath));
            soundMap.Add(stairs, content.Load<SoundEffect>(stairsPath));
            soundMap.Add(linkHurt, content.Load<SoundEffect>(linkHurtPath));
            soundMap.Add(enemyHurt, content.Load<SoundEffect>(enemyHurtPath));
            soundMap.Add(triforce, content.Load<SoundEffect>(triforcePath));
            soundMap.Add(doorLock, content.Load<SoundEffect>(doorLockPath));

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
                throw new NotImplementedException(sound + errorMessage);
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
                throw new NotImplementedException(sound + errorMessage);
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

        public static void PauseAllAudio()
        {
            foreach (SoundEffectInstance music in activeMusicList)
            {
                if (music.State.HasFlag(SoundState.Playing))
                {
                    music.Pause();
                }
            }
            foreach (SoundEffectInstance sound in activeSoundList)
            {
                if (sound.State.HasFlag(SoundState.Playing))
                {
                    sound.Pause();
                }
            }
            paused = true;
        }

        public static void UnpauseAllAudio()
        {
            stopSoundInit = false;
            foreach (SoundEffectInstance music in activeMusicList)
            {
                if (music.State.HasFlag(SoundState.Playing))
                {
                    music.Resume();
                }
            }
            foreach (SoundEffectInstance sound in activeSoundList)
            {
                if (sound.State.HasFlag(SoundState.Playing))
                {
                    sound.Resume();
                }
            }
            paused = false;
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

            Properties.Settings.Default.VolumeMusic = vol;
            Properties.Settings.Default.Save();
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

            Properties.Settings.Default.VolumeSound = vol;
            Properties.Settings.Default.Save();
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

            Properties.Settings.Default.VolumeMaster = vol;
            Properties.Settings.Default.Save();
        }

        public static void PlayItemSound(IItem item)
        {
            // Switch Case Item Names
            const String RupeeBlue = "RupeeBlue", RupeeYellow = "RupeeYellow", Bomb = "Bomb", Compass = "Compass", HeartContainer = "HeartContainer", Fairy = "Fairy", Clock = "Clock", ArrowItem = "ArrowItem", Map = "Map", ItemBoomerang = "ItemBoomerang", Bow = "Bow", Triforce = "Triforce";

            switch (item.GetType().Name)
            {
                case RupeeBlue:
                    PlayFireForget(rupeeAddLong);
                    PlayFireForget(rupeePickUp);
                    break;
                case RupeeYellow:
                    PlayFireForget(rupeeAddShort);
                    PlayFireForget(rupeePickUp);
                    break;
                case Bomb:
                case Compass:
                case HeartContainer:
                case Fairy:
                case Clock:
                case ArrowItem:
                case Map:
                    PlayFireForget(powerPickUp);
                    break;
                case ItemBoomerang:
                case Bow:
                    StopAllMusic();
                    StopAllSound();
                    PlayFireForget(powerPickUp);
                    PlayFireForget(chest);
                    PlayLooped(dungeon, chestSoundLength);
                    break;
                case Triforce:
                    StopAllMusic();
                    StopAllSound();
                    PlayFireForget(powerPickUp);
                    PlayFireForget(triforce);
                    break;
                default:
                    PlayFireForget(itemPickUp);
                    break;
            }
        }

        public static void Update(GameTime gameTime)
        {
            if (!paused)
            {
                //updates delay queue and sound queue
                List<double> newDelays = new List<double>();
                List<SoundEffectInstance> newSounds = new List<SoundEffectInstance>();
                int limit = soundQueue.Count;
                for (int i = 0; i < limit; i++)
                {
                    double newDelay = delays.ElementAt(i) - gameTime.ElapsedGameTime.TotalSeconds;
                    if (newDelay > 0.0)
                    {
                        newDelays.Add(newDelay);
                        newSounds.Add(soundQueue.ElementAt(i));
                    }
                    else
                    {
                        soundQueue.ElementAt(i).Play();
                    }
                }
                delays = newDelays;
                soundQueue = newSounds;

                if (!stopSoundInit)
                {
                    StopAllSound();
                    stopSoundInit = true;
                }
            }
        }

        private static void ReadSoundSettings()
        {
            volumeMaster = Properties.Settings.Default.VolumeMaster;
            volumeMusic = Properties.Settings.Default.VolumeMusic;
            volumeSound = Properties.Settings.Default.VolumeSound;
        }

        public static float GetVolumeMaster()
        {
            return volumeMaster;
        }

        public static void ResetAudioManager()
        {
            stopSoundInit = false;
        }
    }
}
