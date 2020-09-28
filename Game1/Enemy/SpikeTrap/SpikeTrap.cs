/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy.SpikeTrap
{
    class SpikeTrap : IEnemy
    {
        private EnemyStateMachine stateMachine;

        public SpikeTrap(Game1 game, SpriteBatch spriteBatch, Vector2 homePosition, int verticalRange, int horizontalRange)
        {
            stateMachine = new EnemyStateMachine(game, spriteBatch);
            stateMachine.SetState(new SpikeTrapStateHome(stateMachine, homePosition, verticalRange, horizontalRange));
        }

        public void Attack()
        {
            stateMachine.Attack();
        }

        public void Draw()
        {
            stateMachine.Draw(Color.White);
        }

        public void ReceiveDamage()
        {
            stateMachine.ReceiveDamage();
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            stateMachine.Update(gameTime, drawingLimits);
        }
    }
}
