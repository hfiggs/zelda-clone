﻿using Game1.Item;
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
        private static float volumeMaster = 1.0f;
        private static float volumeMusic = 1.0f;
        private static float volumeSound = 1.0f;

        //used for delays and queuing
        private static List<SoundEffectInstance> soundQueue = new List<SoundEffectInstance>();
        private static List<double> delays = new List<double>();

        //used for looking up sounds
        private static Dictionary<string, SoundEffect> musicMap = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, SoundEffect> soundMap = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, Tuple<SoundEffectInstance, int>> singleSoundMap = new Dictionary<string, Tuple<SoundEffectInstance, int>>();

        public static Dictionary<string, Tuple<string, float>> musicWithIntros = new Dictionary<string, Tuple<string, float>>() { { "overworldIntro", new Tuple<string, float>("overworldLoop", 6.5f) } };

        //used for muting an entire subset of sounds instantaneously
        private static List<SoundEffectInstance> activeMusicList = new List<SoundEffectInstance>();
        private static List<SoundEffectInstance> activeSoundList = new List<SoundEffectInstance>();

        private static bool stopSoundInit = false;
        private static bool paused = false;

        const string errorMessage = " is not a supported name.";

        private static Game1 gameRef;

        public static void InitializeAudioManager(Game1 game)
        {
            gameRef = game;
        }

        public static void LoadContent(ContentManager content)
        {
            AudioFactory.Instance.LoadContent(content, musicMap, soundMap);

            ReadSoundSettings();
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

        //Note that the volume parameter here is only for internal balancing between the volumes of each sound file
        public static SoundEffectInstance PlayFireForget(string sound, float timeDelay = 0.0f, float vol = 1.0f)
        {
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

        public static void PlaySingleSoundLooped(string sound, float delay = 0.0f, float volume = 1.0f)
        {
            if(!singleSoundMap.ContainsKey(sound))
            {
                SoundEffectInstance reference = PlayLooped(sound, delay, volume);
                Tuple<SoundEffectInstance, int> singleEntry = new Tuple<SoundEffectInstance, int>(reference, 1);
                singleSoundMap.Add(sound, singleEntry);
            } else
            {
                Tuple<SoundEffectInstance, int> singleEntry;
                singleSoundMap.TryGetValue(sound, out singleEntry);
                singleSoundMap.Remove(sound);                
                Tuple<SoundEffectInstance, int> newSingleEntry = new Tuple<SoundEffectInstance, int>(singleEntry.Item1, singleEntry.Item2 + 1);
                singleSoundMap.Add(sound, newSingleEntry);
            }
        }

        public static void StopSingleSound(string sound)
        {
            Tuple<SoundEffectInstance, int> singleEntry;
            if (singleSoundMap.TryGetValue(sound, out singleEntry))
            {
                singleSoundMap.Remove(sound);
                Tuple<SoundEffectInstance, int> newSingleEntry = new Tuple<SoundEffectInstance, int>(singleEntry.Item1, singleEntry.Item2 - 1);                
                if (newSingleEntry.Item2 <= 0)
                {
                    newSingleEntry.Item1.Stop();
                }
                else
                {
                    singleSoundMap.Add(sound, newSingleEntry);
                }                
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
            activeMusicList.Clear();
        }

        public static void StopMusic(SoundEffectInstance musicRef)
        {
            musicRef.Stop();
            //deliberately left unchecked
            activeMusicList.Remove(musicRef);
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

        public static void PauseAllAudio()
        {
            List<SoundEffectInstance> musicToRemove = new List<SoundEffectInstance>();
            foreach (SoundEffectInstance music in activeMusicList)
            {
                if(music.State.HasFlag(SoundState.Stopped) && soundQueue.IndexOf(music) == -1)
                {
                    musicToRemove.Add(music);
                }
                else if (music.State.HasFlag(SoundState.Playing))
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

            foreach (SoundEffectInstance toRemove in musicToRemove)
            {
                activeMusicList.Remove(toRemove);
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

        public static float GetVolumeMaster()
        {
            return volumeMaster;
        }

        public static void PlayItemSound(IItem item)
        {
            // Switch Case Item Names
            const string RupeeBlue = "RupeeBlue", RupeeYellow = "RupeeYellow", Bomb = "Bomb", Compass = "Compass", HeartContainer = "HeartContainer", Fairy = "Fairy", Clock = "Clock", ArrowItem = "ArrowItem", Map = "Map", ItemBoomerang = "ItemBoomerang", Bow = "Bow", Triforce = "Triforce";

            switch (item.GetType().Name)
            {
                case RupeeBlue:
                    AudioFactory.Instance.SoundRupeeBlue();
                    break;
                case RupeeYellow:
                    AudioFactory.Instance.SoundRupeeYellow();
                    break;
                case Bomb:
                case Compass:
                case HeartContainer:
                case Fairy:
                case Clock:
                case ArrowItem:
                case Map:
                    AudioFactory.Instance.SoundPowerPickup();
                    break;
                case ItemBoomerang:
                case Bow:
                    ClearQueue();
                    StopAllMusic();
                    StopAllSound();
                    AudioFactory.Instance.SoundBow(gameRef.Screen.CurrentRoom);
                    break;
                case Triforce:
                    ClearQueue();
                    StopAllMusic();
                    StopAllSound();
                    AudioFactory.Instance.SoundTriforce();
                    break;
                default:
                    AudioFactory.Instance.SoundDefaultItem();
                    break;
            }
        }

        private static void ReadSoundSettings()
        {
            volumeMaster = Properties.Settings.Default.VolumeMaster;
            volumeMusic = Properties.Settings.Default.VolumeMusic;
            volumeSound = Properties.Settings.Default.VolumeSound;
        }

        public static void ResetAudioManager()
        {
            stopSoundInit = false;
        }

        public static void ClearQueue()
        {
            soundQueue.Clear();
            delays.Clear();
        }
    }
}
