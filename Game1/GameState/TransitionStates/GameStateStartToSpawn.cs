using Game1.Audio;
using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.GameState
{
    class GameStateStartToSpawn : IGameState
    {
        private readonly Game1 game;

        public GameStateStartToSpawn(Game1 game)
        {
            this.game = game;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            // do nothing for now
        }

        public void Update(GameTime gameTime)
        {
            AudioManager.StopAllMusic();

            const string overworldMusic = "overworld";
            AudioManager.PlayLooped(overworldMusic);

            game.SetState(new GameStateRoom(game));
        }
    }
}
