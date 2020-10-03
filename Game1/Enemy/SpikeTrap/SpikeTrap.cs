/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class SpikeTrap : IEnemy
    {
        private EnemyStateMachine stateMachine;

        public SpikeTrap(Game1 game, Vector2 homePosition, int verticalRange, int horizontalRange)
        {
            stateMachine = new EnemyStateMachine(game);
            stateMachine.SetState(new SpikeTrapStateHome(stateMachine, homePosition, verticalRange, horizontalRange));
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            stateMachine.Draw(spriteBatch,color);
        }

        public void ReceiveDamage()
        {
            // Cannot receive damage
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            stateMachine.Update(gameTime, drawingLimits);
        }
    }
}
