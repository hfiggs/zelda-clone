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
        public Goriya(Game1 game, Vector2 spawnPosition)
        {
            stateMachine = new EnemyStateMachine(game);
            stateMachine.SetState(new GoriyaStateMovingRight(stateMachine, spawnPosition));
            oldDirection = stateMachine.GetDirection();
        }

        public void ReceiveDamage()
        {
            stateMachine.ReceiveDamage();
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

            if (newDirection.X != oldDirection.X || newDirection.Y != oldDirection.Y) {
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
            
    }
}

