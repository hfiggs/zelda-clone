using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class Dodongo : IEnemy
    {
        private EnemyStateMachine stateMachine;
        private float health;

        public Dodongo(Game1 game, Vector2 position)
        {
            stateMachine = new EnemyStateMachine(game);

            switch ((new Random()).Next(4))
            {
                case 0:
                    stateMachine.SetState(new DodongoStateUp(stateMachine, position));
                    break;
                case 1:
                    stateMachine.SetState(new DodongoStateDown(stateMachine, position));
                    break;
                case 2:
                    stateMachine.SetState(new DodongoStateLeft(stateMachine, position));
                    break;
                case 3:
                    stateMachine.SetState(new DodongoStateRight(stateMachine, position));
                    break;
            }
            health = 8f;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            stateMachine.Draw(spriteBatch, color);
        }

        public void editPosition(Vector2 amount)
        {
            stateMachine.editPosition(amount);
        }

        public void ReceiveDamage(float amount, Vector2 direction) 
        {

            health -= amount;
            EnemyDamageDecorator decorator = new EnemyDamageDecorator(this, stateMachine, direction);
            stateMachine.swapInList(this, decorator);

        }

        public bool shouldRemove()
        {
            return health <= 0;
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            stateMachine.Update(gameTime, drawingLimits);
        }

        public Rectangle GetHitbox()
        {
            return stateMachine.GetHitbox();
        }
    }
}
