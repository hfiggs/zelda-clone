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

        // how far spike trap can travel in a particular direction (vertically and horizontally)
        private int verticalRadius;
        private int horizontalRadius;

        Game1 game;

        public SpikeTrap(Game1 game, Vector2 homePosition, SpriteBatch spriteBatch, int verticalRadius, int horizontalRadius)
        {
            this.game = game;

            stateMachine = new EnemyStateMachine(spriteBatch, new SpikeTrapStateHome(stateMachine, homePosition));

            this.verticalRadius = verticalRadius;
            this.horizontalRadius = horizontalRadius;
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
