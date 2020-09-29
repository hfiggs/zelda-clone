using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Enemy
{
    class GoriyaStateMovingLeft : IEnemyState
    {
        private EnemyStateMachine stateMachine;
        public ISprite Sprite { get; private set; }

        private Vector2 direction;
        private Vector2 position;
        private const int moveSpeed = 2;
        private double totalElapsedSeconds = 0;
        double MovementChangeTimeSeconds;

        public GoriyaStateMovingLeft(EnemyStateMachine stateMachine, Vector2 position)
        {
            this.stateMachine = stateMachine;
            this.position = position;
            this.direction = new Vector2(-1 * moveSpeed, 0);
            this.MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
            Sprite = EnemySpriteFactory.Instance.CreateGoriyaLeftSprite();
        }

        public void Attack()
        {

        }

        public void ReceiveDamage()
        {

        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
                totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

                if (totalElapsedSeconds >= MovementChangeTimeSeconds)
                {
                    totalElapsedSeconds -= MovementChangeTimeSeconds;
                    this.direction = GetRandomDirection();
                    this.MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
                }
                if (drawingLimits.Contains(position.X + direction.X, position.Y + direction.Y))
                {
                  position += direction;
                }

            Sprite.Update();
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetDirection()
        {
            return direction;
        }
        private float GetRandomDirectionMovementChangeTimeSeconds()
        {
            Random random = new Random();
            return (float) (random.NextDouble() * (0.7 + 0.3) + 0.3);
        }
        private Vector2 GetRandomDirection()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int randomDirection = random.Next(4);

            switch (randomDirection)
            {
                case 0:
                    return new Vector2(-1 * moveSpeed, 0);
                case 1:
                    return new Vector2(moveSpeed, 0);
                case 2:
                    return new Vector2(0, -1 * moveSpeed);
                default:
                    return new Vector2(0, moveSpeed);
            }
        }
    }
}
