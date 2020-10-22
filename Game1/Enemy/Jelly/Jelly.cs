using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class Jelly : IEnemy
    {
        private IEnemyState state;
        public Jelly(Game1 game, Vector2 spawnPosition)
        {
            state = new JellyStateMoving(spawnPosition);
        }

        public void ReceiveDamage()
        {
            state.ReceiveDamage();
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch,color);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            state.Update(gameTime, drawingLimits);
        }

        public void SetState(IEnemyState state)
        {
            this.state = state;
        }
    }
}
