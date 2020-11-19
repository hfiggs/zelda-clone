using Game1.Audio;
using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Item
{
    class Fairy : IItem
    {
        private ISprite sprite;
        public Vector2 Position { get; set; }
        private Vector2 direction;
        private const float moveSpeed = .5f;
        private double totalElapsedSeconds = 0;
        private double MovementChangeTimeSeconds;
        Rectangle RoomLimits = new Rectangle(0, 0, 200, 130);
        int timeTillSwap;
        bool frameChanged = true;
        const int timer = 250; //ms

        public Fairy(Vector2 Position)
        {
            sprite = ItemSpriteFactory.Instance.CreateFairySpriteOne();

            this.Position = Position;

            timeTillSwap = timer;
            MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
            const string pickUpAudio = "powerPickUp";
            AudioManager.PlayFireForget(pickUpAudio);
        }
        public void Update(GameTime gameTime)
        {
            timeTillSwap -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeTillSwap <= 0)
            {
                if (frameChanged)
                    sprite = ItemSpriteFactory.Instance.CreateFairySpriteTwo();
                else
                    sprite = ItemSpriteFactory.Instance.CreateFairySpriteOne();
                timeTillSwap = timer;
                frameChanged = !frameChanged;
            }

            //Position update
            totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (totalElapsedSeconds >= MovementChangeTimeSeconds)
            {
                totalElapsedSeconds -= MovementChangeTimeSeconds;
                direction = GetRandomDirection();
                MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
            }
            if (RoomLimits.Contains(Position.X + direction.X, Position.Y + direction.Y))
            {
                Position += direction;
            }
            else
            {
                direction = GetRandomDirection();
                MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
            }

        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, Position, color);
        }

        public Rectangle GetHitbox()
        {
            const int xOffset = 16, yOffset = 11, width = 10, height = 18;
            return new Rectangle((int)Position.X + xOffset, (int)Position.Y + yOffset, width, height);
        }

        public bool ShouldDelete { get; set; } = false;

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

        private float GetRandomDirectionMovementChangeTimeSeconds()
        {
            const double minimumTime = 0.3;
            Random random = new Random();
            return (float)(random.NextDouble() * 1.0 + minimumTime);
        }
    }
}
