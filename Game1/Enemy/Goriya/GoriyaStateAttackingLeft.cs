using Game1.Projectile;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class GoriyaStateAttackingLeft : IEnemyState
    {
        public ISprite Sprite { get; private set; }

        private Vector2 direction;
        private Vector2 position;
        private const int moveSpeed = 1;
        private IProjectile projectile;
        private double totalElapsedSeconds = 0;
        private double MovementChangeTimeSeconds;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 200f; // ms per frame

        public GoriyaStateAttackingLeft(Game1 game, Vector2 position)
        {
            this.position = position;
            this.direction = new Vector2(-1 * moveSpeed, 0);
            this.MovementChangeTimeSeconds = 2;
            Sprite = EnemySpriteFactory.Instance.CreateGoriyaLeftSprite();
            projectile = new EnemyBoomerang('W', position);
            game.Screen.SpawnProjectile(projectile);

            timeUntilNextFrame = animationTime;
        }

        public void Attack()
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
                this.MovementChangeTimeSeconds = 0;
            }


            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                Sprite.Update();
                timeUntilNextFrame += animationTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, position, color);
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetDirection()
        {
            return direction;
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X + 8, (int)position.Y + 7, 13, 16);
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
        public void editPosition(Vector2 amount)
        {
            //Do Nothing, No knockback while Attacking
        }
    }
}
