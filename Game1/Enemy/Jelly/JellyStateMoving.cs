using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class JellyStateMoving : IEnemyState
    {
        public ISprite Sprite { get; private set; }
        private Vector2 position;
        private Vector2 direction;
        private const int moveSpeed = 1;
        private double totalElapsedSeconds = 0;
        private double MovementChangeTimeSeconds;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 200f; // ms per frame

        public JellyStateMoving(Vector2 position)
        {
            this.position = position;
            this.direction = GetRandomDirection();
            this.MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
            Sprite = EnemySpriteFactory.Instance.CreateJellySprite();

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
            const int xDiff = 11;
            const int yDiff = 10;
            const int width = 8;
            const int height = 9;

            List<Rectangle> hitboxList = new List<Rectangle>();
            hitboxList.Add(new Rectangle((int)position.X + xDiff, (int)position.Y + yDiff, width, height));
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
            const int randomNumberMax = 4;
            int randomDirection = random.Next(randomNumberMax);

            const int goLeft = 0, goRight = 1, goUp = 2;
            switch (randomDirection)
            {
                case goLeft:
                    return new Vector2(-1 * moveSpeed, 0);
                case goRight:
                    return new Vector2(moveSpeed, 0);
                case goUp:
                    return new Vector2(0, -1 * moveSpeed);
                default:
                    return new Vector2(0, moveSpeed);
            }
        }

        public void editPosition(Vector2 amount)
        {
            position = Vector2.Add(position, amount);
        }
    }
}
