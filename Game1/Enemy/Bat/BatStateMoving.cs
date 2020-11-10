using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class BatStateMoving : IEnemyState
    {
        public ISprite Sprite { get; private set; }
        private Vector2 position;
        private Vector2 direction;
        private const float moveSpeed = .5f;
        private double totalElapsedSeconds = 0;
        private double MovementChangeTimeSeconds;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 100f; // ms per frame

        public BatStateMoving(Vector2 position)
        {
            this.position = position;
            direction = GetRandomDirection();
            MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
            Sprite = EnemySpriteFactory.Instance.CreateBatSprite();

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
                direction = GetRandomDirection();
                MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
            }
            if(drawingLimits.Contains(position.X + direction.X, position.Y + direction.Y))
            {
                position += direction;
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

        public List<Rectangle> GetHitboxes()
        {
            const int widthAndHeight = 16;
            List<Rectangle> hitboxList = new List<Rectangle>();
            hitboxList.Add(new Rectangle((int)position.X, (int)position.Y, widthAndHeight, widthAndHeight));
            return hitboxList;
        }

        private float GetRandomDirectionMovementChangeTimeSeconds()
        {
            const double minimumTime = 0.3;
            Random random = new Random();
            return (float)(random.NextDouble() * 1.0 + minimumTime);
        }
        private Vector2 GetRandomDirection()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            const int randomNumberMax = 8;
            int randomDirection = random.Next(randomNumberMax);

            switch (randomDirection)
            {
                case 0:
                    return new Vector2(-1 * moveSpeed, 0);
                case 1:
                    return new Vector2(-1 * moveSpeed, -1 * moveSpeed);
                case 2:
                    return new Vector2(0, -1 * moveSpeed);
                case 3:
                    return new Vector2(moveSpeed, -1 * moveSpeed);
                case 4:
                    return new Vector2(moveSpeed, 0);
                case 5:
                    return new Vector2(moveSpeed, moveSpeed);
                case 6:
                    return new Vector2(0, moveSpeed);
                default:
                    return new Vector2(-1 * moveSpeed, moveSpeed);
            }
        }

        public void editPosition(Vector2 amount)
        {
            position = Vector2.Add(position, amount);
        }
    }
}
