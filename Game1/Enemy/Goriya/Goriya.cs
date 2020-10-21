using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class Goriya : IEnemy
    {
        private EnemyStateMachine stateMachine;
        private Vector2 oldDirection;
        private double totalElapsedSeconds;
        private double attackCooldown = 5;
        private float health;

        public Goriya(Game1 game, Vector2 spawnPosition)
        {
            stateMachine = new EnemyStateMachine(game);
            stateMachine.SetState(new GoriyaStateMovingRight(stateMachine, spawnPosition));
            oldDirection = stateMachine.GetDirection();
            health = 3f;
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
            Vector2 newDirection = stateMachine.GetDirection();
            totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (totalElapsedSeconds >= attackCooldown)
            {
                stateMachine.Attack();
                totalElapsedSeconds -= attackCooldown;
            }

            else if (newDirection.X != oldDirection.X || newDirection.Y != oldDirection.Y) {
                if (stateMachine.GetDirection().X < 0)
                {
                    stateMachine.SetState(new GoriyaStateMovingLeft(stateMachine, stateMachine.GetPosition()));
                }
                if (stateMachine.GetDirection().X > 0)
                {
                    stateMachine.SetState(new GoriyaStateMovingRight(stateMachine, stateMachine.GetPosition()));
                }
                if (stateMachine.GetDirection().Y < 0)
                {
                    stateMachine.SetState(new GoriyaStateMovingUp(stateMachine, stateMachine.GetPosition()));
                }
                if (stateMachine.GetDirection().Y > 0)
                {
                    stateMachine.SetState(new GoriyaStateMovingDown(stateMachine, stateMachine.GetPosition()));
                }
                oldDirection = stateMachine.GetDirection();
            }

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

