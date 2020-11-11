using Game1.ResolutionManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.GameState
{
    class GameStateStart : IGameState
    {
        private readonly Game1 game;

        public GameStateStart(Game1 game)
        {
            this.game = game;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IResolutionManager resolutionManager)
        {
            // do nothing for now
        }

        public void Update(GameTime gameTime)
        {
            game.SetState(new GameStateStartToSpawn(game));
        }
    }
}
