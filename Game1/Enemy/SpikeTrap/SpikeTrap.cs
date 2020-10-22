/* Author: Hunter Figgs */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class SpikeTrap : IEnemy
    {
        private IEnemyState state;

        public SpikeTrap(Game1 game, Vector2 homePosition, int verticalRange, int horizontalRange)
        {
            state = new SpikeTrapStateHome(game, this, homePosition, verticalRange, horizontalRange);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            state.Draw(spriteBatch,color);
        }

        public void ReceiveDamage()
        {
            // Cannot receive damage
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
