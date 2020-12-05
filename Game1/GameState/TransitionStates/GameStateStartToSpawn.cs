using Game1.Audio;
using Game1.Player;
using Game1.ResolutionManager;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.GameState
{
    class GameStateStartToSpawn : IGameState
    {
        private readonly Game1 game;
        private const float startSoundDelay = 750.0f; //ms
        private const float soundVolume = 0.5f;
        private bool playedSound = false;
        private float timer = 0;

        public GameStateStartToSpawn(Game1 game)
        {
            this.game = game;
            AudioManager.StopAllMusic();
            AudioManager.ResetAudioManager();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            // do nothing for now
        }

        public void Update(GameTime gameTime)
        {
            if ((timer += (float)game.TargetElapsedTime.TotalMilliseconds) > startSoundDelay)
            {
                AudioManager.StopAllMusic();

                const string overworldIntro = "overworldIntro";
                Tuple<string, float> overworldLoopedTuple;
                AudioManager.musicWithIntros.TryGetValue(overworldIntro, out overworldLoopedTuple);

                AudioManager.PlayFireForget(overworldIntro);
                AudioManager.PlayLooped(overworldLoopedTuple.Item1, overworldLoopedTuple.Item2);

                game.SetState(new GameStateRoom(game));
            }

            AudioManager.Update(gameTime);

            if (!playedSound)
            {
                AudioManager.PlayFireForget("swordBeam");
                AudioManager.PlayFireForget("powerPickUp", 0.0f, soundVolume);
                AudioManager.PlayFireForget("key", 0.0f, soundVolume);
                playedSound = true;
            }
        }
    }
}
