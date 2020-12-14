using Game1.Audio;
using Game1.ResolutionManager;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.GameState
{
    class GameStateStartToSpawn : IGameState
    {
        private readonly Game1 game;
        private const float startSoundDelay = 750.0f; //ms
        private const float soundVolume = 0.5f;
        private bool playedSound = false;
        private float timer = 0;

        private readonly Room newRoom;
        private readonly (char, int) newRoomKey = ('G', 2);

        public GameStateStartToSpawn(Game1 game)
        {
            this.game = game;
            game.Screen.RoomsDict.TryGetValue(newRoomKey, out newRoom);
            AudioManager.StopAllMusic();
            AudioManager.ResetAudioManager();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager) { }

        public void Update(GameTime gameTime)
        {
            if ((timer += (float)game.TargetElapsedTime.TotalMilliseconds) > startSoundDelay)
            {
                AudioManager.StopAllMusic();

                newRoom.RoomMeta.PlayMusic();

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
