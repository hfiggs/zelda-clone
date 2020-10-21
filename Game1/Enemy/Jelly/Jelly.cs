using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Enemy
{
    class Jelly : IEnemy
    {
        private EnemyStateMachine stateMachine;
        private float health;
        public Jelly(Game1 game, Vector2 spawnPosition)
        {
            stateMachine = new EnemyStateMachine(game);
            stateMachine.SetState(new JellyStateMoving(stateMachine, spawnPosition));
            health = .5f;
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            health -= amount;
            EnemyDamageDecorator decorator = new EnemyDamageDecorator(this, stateMachine, direction);
            stateMachine.swapInList(this, decorator);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            stateMachine.Draw(spriteBatch,color);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            stateMachine.Update(gameTime, drawingLimits);
        }

        public void editPosition(Vector2 amount)
        {
            stateMachine.editPosition(amount);
        }

        public bool shouldRemove()
        {
            return health <= 0;
        }
    }
}
