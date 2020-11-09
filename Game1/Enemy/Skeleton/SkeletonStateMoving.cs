using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Game1.Item;
using System.Collections.Generic;
using Game1.RoomLoading;

namespace Game1.Enemy
{
    class SkeletonStateMoving : IEnemyState
    {
        public ISprite Sprite { get; private set; }
        private Vector2 position;
        private Vector2 direction;
        private const float moveSpeed = .5f;
        private double totalElapsedSeconds = 0;
        private double MovementChangeTimeSeconds;
        private IItem item;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 200f; // ms per frame

        public SkeletonStateMoving(Vector2 position)
        {
            this.Sprite = EnemySpriteFactory.Instance.CreateSkeletonSprite();

            this.position = position;
            this.direction = GetRandomDirection();

            this.MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
            this.timeUntilNextFrame = animationTime;
        }

        public SkeletonStateMoving(Room room, Vector2 position, IItem item)
        {
            Sprite = EnemySpriteFactory.Instance.CreateSkeletonSprite();

            room.SpawnItem(item);
            this.item = item;

            this.position = position;
            this. direction = GetRandomDirection();

            this.MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
            this.timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {

        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
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
                if(item != null)
                {
                    item.Position = new Vector2(position.X-8, position.Y-4);
                }
               
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

        public ISprite GetSprite()
        {
            return this.Sprite;
        } 

        public Vector2 GetDirection()
        {
            return direction;
        }
        public Vector2 GetPosition()
        {
            return position;
        }

        public List<Rectangle> GetHitboxes()
        {
            const int xAndYDiff = 7;
            const int width = 15;
            const int height = 16;
            List<Rectangle> hitboxList = new List<Rectangle>();
            hitboxList.Add(new Rectangle((int)position.X + xAndYDiff, (int)position.Y + xAndYDiff, width, height));
            return hitboxList;
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
            position = Vector2.Add(position, amount);
        }
    }
}
