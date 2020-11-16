using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class GoriyaStateMovingLeft : IEnemyState
    {
        public ISprite Sprite { get; private set; }
        private Game1 game;
        private IEnemy goriya;

        private Vector2 direction;
        private Vector2 position;
        private const float moveSpeed = .5f;
        private const int zero = 0;
        private const int negative = -1;
        private double totalElapsedSeconds = 0;
        private double MovementChangeTimeSeconds;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 200f; // ms per frame

        public GoriyaStateMovingLeft(Game1 game, IEnemy goriya, Vector2 position)
        {
            this.game = game;
            this.goriya = goriya;
            this.position = position;
            this.direction = new Vector2(negative * moveSpeed, zero);
            this.MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
            Sprite = EnemySpriteFactory.Instance.CreateGoriyaLeftSprite();

            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {
            goriya.SetState(new GoriyaStateAttackingLeft(game, position));
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

            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= zero)
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
            const int spriteWidth = 13;
            const int spriteHeight = 16;
            const int xAdjustment = 8;
            const int yAdjustment = 7;

            List<Rectangle> hitboxList = new List<Rectangle>();
            hitboxList.Add(new Rectangle((int)position.X + xAdjustment, (int)position.Y + yAdjustment, spriteWidth, spriteHeight));
            return hitboxList;
        }

        private float GetRandomDirectionMovementChangeTimeSeconds()
        {
            const double one = 1.0;
            const double minimumTime = 0.3;
            Random random = new Random();
            return (float) (random.NextDouble() * one + minimumTime);
        }

        private Vector2 GetRandomDirection()
        {
            const int numberOfDirections = 4;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int randomDirection = random.Next(numberOfDirections);

            const int goLeft = 0, goRight = 1, goUp = 2;
            switch (randomDirection)
            {
                case goLeft:
                    return new Vector2(negative * moveSpeed, zero);
                case goRight:
                    return new Vector2(moveSpeed, zero);
                case goUp:
                    return new Vector2(zero, negative * moveSpeed);
                default:
                    return new Vector2(zero, moveSpeed);
            }
        }

        public void editPosition(Vector2 amount)
        {
            position = Vector2.Add(amount, position);
        }
    }
}
