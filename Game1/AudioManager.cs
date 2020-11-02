using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Game1
{
    class AudioManager
    {
        private static float volumeMaster = 1.0f;
        private static float volumeMusic = 1.0f;
        private static float volumeSound = 1.0f;

        private static Game1 game;
        private static List<SoundEffectInstance> soundQueue = new List<SoundEffectInstance>();
        private static List<double> delays = new List<double>();

        private static Dictionary<string, SoundEffect> musicMap = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, SoundEffect> soundMap = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, SoundEffect> testMap = new Dictionary<string, SoundEffect>();

        private static List<SoundEffectInstance> activeMusicList = new List<SoundEffectInstance>();
        private static List<SoundEffectInstance> activeSoundList = new List<SoundEffectInstance>();

        public AudioManager(Game1 gamep)
        {
            game = gamep;
        }

        public static void LoadContent(ContentManager content)
        {
            //Test
            testMap.Add("sword", content.Load<SoundEffect>("audio/sounds02/Sword"));

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
            soundMap.Add("aquamentusScreamFF", content.Load<SoundEffect>("audio/sounds02/AquamentusScream"));
            soundMap.Add("aquamentusHurt", content.Load<SoundEffect>("audio/sounds02/AquamentusHurt"));
            soundMap.Add("bombExplode", content.Load<SoundEffect>("audio/sounds02/BombExplode"));
            soundMap.Add("bombPlace", content.Load<SoundEffect>("audio/sounds02/BombPlace"));
            soundMap.Add("chest", content.Load<SoundEffect>("audio/sounds02/Chest"));
            soundMap.Add("deathEnemy", content.Load<SoundEffect>("audio/sounds02/EnemyDeath"));
            soundMap.Add("powerPickUp", content.Load<SoundEffect>("audio/sounds02/FairyAppear"));
            soundMap.Add("ocarina", content.Load<SoundEffect>("audio/sounds02/Flute"));
            soundMap.Add("reveal", content.Load<SoundEffect>("audio/sounds02/Hole"));
            soundMap.Add("heartPickUp", content.Load<SoundEffect>("audio/sounds02/ItemPickup1"));
            soundMap.Add("key", content.Load<SoundEffect>("audio/sounds02/KeyAppear"));
            soundMap.Add("rupeePickUp", content.Load<SoundEffect>("audio/sounds02/Rupee"));
            soundMap.Add("stairs", content.Load<SoundEffect>("audio/sounds02/Stairs"));
            soundMap.Add("linkHurt", content.Load<SoundEffect>("audio/sounds02/PlayerHurt"));
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
                activeSoundList.Add(instanceToPlay);
                reference = instanceToPlay;
                soundQueue.Add(instanceToPlay);
                delays.Add(timeDelay);
            }
            else if (musicMap.TryGetValue(sound, out toPlay))
            {
                SoundEffectInstance instanceToPlay = toPlay.CreateInstance();
                instanceToPlay.Volume = volumeMusic * volumeMaster * vol;
                activeMusicList.Add(instanceToPlay);
                reference = instanceToPlay;
                soundQueue.Add(instanceToPlay);
                delays.Add(timeDelay);
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
            SoundEffectInstance reference = null;
            SoundEffect toPlay;
            if (soundMap.TryGetValue(sound, out toPlay))
            {
                SoundEffectInstance instanceToPlay = toPlay.CreateInstance();
                instanceToPlay.Volume = volumeSound * volumeMaster * vol;
                instanceToPlay.IsLooped = true;
                activeSoundList.Add(instanceToPlay);
                reference = instanceToPlay;
                soundQueue.Add(instanceToPlay);
                delays.Add(timeDelay);
            }
            else if (musicMap.TryGetValue(sound, out toPlay))
            {
                SoundEffectInstance instanceToPlay = toPlay.CreateInstance();
                instanceToPlay.Volume = volumeMusic * volumeMaster * vol;
                instanceToPlay.IsLooped = true;
                activeMusicList.Add(instanceToPlay);
                reference = instanceToPlay;
                soundQueue.Add(instanceToPlay);
                delays.Add(timeDelay);
            }
            else
            {
                throw new NotImplementedException(sound + " is not a supported name.");
            }
            return reference;
        }

        public static void stopAllMusic()
        {
            foreach(SoundEffectInstance music in activeMusicList)
            {
                music.Stop(true);
            }
        }

        public static void stopMusic(SoundEffectInstance musicRef)
        {
            musicRef.Stop();
            //deliberately left unchecked
            activeMusicList.Remove(musicRef);
        }

        public static void SetVolumeMusic(float vol)
        {
            volumeMusic = vol;
        }

        public static void stopAllSound()
        {
            foreach(SoundEffectInstance sound in activeSoundList)
            {
                sound.Stop(true);
            }
        }

        public static void stopSound(SoundEffectInstance soundRef)
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

        private static void garbageCollection()
        {
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
                if(music.State.HasFlag(SoundState.Stopped))
                {
                    replacement.Add(music);
                }
            }
            activeMusicList = replacement;
        }

        public static void Update(GameTime gameTime)
        {
            //updates delay queue
            List<double> newDelays = new List<double>();
            for(int i = 0; i < delays.Count; i++)
            {
                double newDelay = delays.ElementAt<double>(i) - gameTime.ElapsedGameTime.TotalSeconds;
                if (newDelay > 0.0) {
                    newDelays.Add(newDelay);
                } else
                {
                    soundQueue.ElementAt<SoundEffectInstance>(i).Play();
                    soundQueue.RemoveAt(i);
                }
            }
            delays = newDelays;

            garbageCollection();
        }
    }
}
