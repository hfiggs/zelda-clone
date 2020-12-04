using Game1.Audio;
using Game1.Player;
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
        private float timer = 0;

        public GameStateStartToSpawn(Game1 game)
        {
            this.game = game;
            AudioManager.StopAllMusic();
            AudioManager.PlayFireForget("swordBeam");
            AudioManager.PlayFireForget("rupeePickUp");
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

                const string overworldMusic = "overworld";
                AudioManager.PlayLooped(overworldMusic);

                game.SetState(new GameStateRoom(game));
            }
        }
    }
}
