using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class Goriya : IEnemy
    {
        private EnemyStateMachine stateMachine;
        private Vector2 oldDirection;
        public Goriya(Game1 game, SpriteBatch spriteBatch, Vector2 spawnPosition)
        {
            stateMachine = new EnemyStateMachine(game, spriteBatch);
            stateMachine.SetState(new GoriyaStateMovingRight(stateMachine, spawnPosition));
            oldDirection = stateMachine.GetDirection();
        }

        public void Attack()
        {
            stateMachine.Attack();
        }

        public void ReceiveDamage()
        {
            stateMachine.ReceiveDamage();
        }

        public void Draw()
        {
            stateMachine.Draw(Color.White);
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            Vector2 newDirection = stateMachine.GetDirection();
            if (newDirection.X != oldDirection.X || newDirection.Y != oldDirection.Y) {
                Console.WriteLine("Here");
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

