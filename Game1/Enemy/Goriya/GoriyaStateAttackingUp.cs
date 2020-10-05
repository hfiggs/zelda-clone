using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;

namespace Game1.Enemy
{
    class GoriyaStateAttackingUp : IEnemyState
    {
        private EnemyStateMachine stateMachine;
        public ISprite Sprite { get; private set; }

        private Vector2 direction;
        private Vector2 position;
        private const int moveSpeed = 2;
        private IProjectile projectile;
        private double totalElapsedSeconds = 0;
        double MovementChangeTimeSeconds;

        public GoriyaStateAttackingUp(EnemyStateMachine stateMachine, Vector2 position)
        {
            this.stateMachine = stateMachine;
            this.position = position;
            this.direction = new Vector2(0, -1 * moveSpeed);
            this.MovementChangeTimeSeconds = 2;
            Sprite = EnemySpriteFactory.Instance.CreateGoriyaRightSprite();
            projectile = new EnemyBoomerang('N', position);
            stateMachine.spawnProjectile(projectile);

        }

        public void Attack()
        {

        }

        public void ReceiveDamage()
        {

        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (totalElapsedSeconds >= MovementChangeTimeSeconds)
            {
                totalElapsedSeconds -= MovementChangeTimeSeconds;
                this.direction = GetRandomDirection();
                this.MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
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
