using Game1.RoomLoading;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Audio
{
    public class AudioFactory
    {
        // Map Keys for audio
        const string overworldIntro = "overworldIntro", overworldLoop = "overworldLoop", dungeon = "dungeon", dungeon2 = "dungeon2", title = "title", title2 = "title2", gameOver = "gameOver", gameOver2 = "gameOver2";
        const string aquamentusScream = "aquamentusScream", boomerang = "boomerang", lowHealth = "lowHealth", death = "death", linkPop = "linkPop", shield = "shield", sword = "sword";
        const string swordBeam = "swordBeam", aquamentusHurt = "aquamentusHurt", bombExplode = "bombExplode", bombPlace = "bombPlace", chest = "chest", enemyDeath = "enemyDeath";
        const string powerPickUp = "powerPickUp", ocarina = "ocarina", reveal = "reveal", itemPickUp = "itemPickUp", key = "key", rupeePickUp = "rupeePickUp", rupeeAddShort = "rupeeAddShort";
        const string rupeeAddLong = "rupeeAddLong", stairs = "stairs", linkHurt = "linkHurt", enemyHurt = "enemyHurt", triforce = "triforce", doorLock = "doorLock";

        // Map values for audio
        const string overworldIntroPath = "audio/music/overworldIntro", overworldLoopPath = "audio/music/overworldLoop", dungeonPath = "audio/music/dungeon", dungeon2Path = "audio/music/dungeonBass", titlePath = "audio/music/title", title2Path = "audio/music/titleBass", gameOverPath = "audio/music/gameOver", gameOver2Path = "audio/music/gameOverBlasted";
        const string aquamentusScreamPath = "audio/sounds/AquamentusScream", boomerangPath = "audio/sounds/Boomerang", lowHealthPath = "audio/sounds/HealthLow", deathPath = "audio/sounds/death", linkPopPath = "audio/sounds/LinkPop", shieldPath = "audio/sounds/Shield", swordPath = "audio/sounds/Sword";
        const string swordBeamPath = "audio/sounds/SwordBeam", aquamentusHurtPath = "audio/sounds/AquamentusHurt", bombExplodePath = "audio/sounds/BombExplode", bombPlacePath = "audio/sounds/BombPlace", chestPath = "audio/sounds/Chest", enemyDeathPath = "audio/sounds/EnemyDeath";
        const string powerPickUpPath = "audio/sounds/FairyAppear", ocarinaPath = "audio/sounds/Flute", revealPath = "audio/sounds/Hole", itemPickUpPath = "audio/sounds/ItemPickUp1", keyPath = "audio/sounds/KeyAppear", rupeePickUpPath = "audio/sounds/Rupee", rupeeAddShortPath = "audio/sounds/RupeeAddShort";
        const string rupeeAddLongPath = "audio/sounds/RupeeAddLong", stairsPath = "audio/sounds/Stairs", linkHurtPath = "audio/sounds/PlayerHurt", enemyHurtPath = "audio/sounds/EnemyHurt", triforcePath = "audio/sounds/triforceTheme", doorLockPath = "audio/sounds/LockedDoor";

        private static readonly float chestSoundLength = 2.0f;

        public static AudioFactory Instance = new AudioFactory();

        private AudioFactory() { }

        //NOTE: Requires musicMap and soundMap to be properly initialized by AudioManager
        public void LoadContent(ContentManager content, Dictionary<string, SoundEffect> musicMap, Dictionary<string, SoundEffect> soundMap)
        {
            //designated music
            musicMap.Add(overworldIntro, content.Load<SoundEffect>(overworldIntroPath));
            musicMap.Add(overworldLoop, content.Load<SoundEffect>(overworldLoopPath));
            musicMap.Add(dungeon, content.Load<SoundEffect>(dungeonPath));
            musicMap.Add(dungeon2, content.Load<SoundEffect>(dungeon2Path));
            musicMap.Add(title, content.Load<SoundEffect>(titlePath));
            musicMap.Add(title2, content.Load<SoundEffect>(title2Path));
            musicMap.Add(gameOver, content.Load<SoundEffect>(gameOverPath));
            musicMap.Add(gameOver2, content.Load<SoundEffect>(gameOver2Path));
            soundMap.Add(aquamentusScream, content.Load<SoundEffect>(aquamentusScreamPath));
            soundMap.Add(boomerang, content.Load<SoundEffect>(boomerangPath));
            soundMap.Add(lowHealth, content.Load<SoundEffect>(lowHealthPath));

            //designated sounds
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
        }

        public void SoundRupeeBlue()
        {
            AudioManager.PlayFireForget(rupeeAddLong);
            AudioManager.PlayFireForget(rupeePickUp);
        }

        public void SoundRupeeYellow()
        {
            AudioManager.PlayFireForget(rupeeAddShort);
            AudioManager.PlayFireForget(rupeePickUp);
        }

        public void SoundPowerPickup()
        {
            AudioManager.PlayFireForget(powerPickUp);
        }

        public void SoundBow(Room currentRoom)
        {
            AudioManager.PlayFireForget(powerPickUp);
            AudioManager.PlayFireForget(chest);
            currentRoom.PlayMusic(chestSoundLength);            
        }

        public void SoundTriforce()
        {
            AudioManager.PlayFireForget(powerPickUp);
            AudioManager.PlayFireForget(triforce);
        }

        public void SoundDefaultItem()
        {
            AudioManager.PlayFireForget(itemPickUp);
        }
    }
}
