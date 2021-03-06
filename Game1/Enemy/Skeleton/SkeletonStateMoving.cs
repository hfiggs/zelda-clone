﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Game1.Item;
using System.Collections.Generic;
using Game1.RoomLoading;
using Game1.Util;

namespace Game1.Enemy
{
    class SkeletonStateMoving : IEnemyState
    {
        public ISprite Sprite { get; private set; }
        private IEnemy skeleton;
        private Vector2 position;
        private Vector2 direction;
        private const float moveSpeed = .5f;
        private double totalElapsedSeconds = 0;
        private double MovementChangeTimeSeconds;
        private IItem item;
        private const int xDiff = 8, yDiff = 5;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 200f; // ms per frame

        public SkeletonStateMoving(Vector2 position, IEnemy skeleton)
        {
            this.Sprite = EnemySpriteFactory.Instance.CreateSkeletonSprite();

            this.position = position;
            this.direction = GetRandomDirection();

            this.MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
            this.timeUntilNextFrame = animationTime;

            this.skeleton = skeleton;
        }

        public SkeletonStateMoving(Room room, Vector2 position, IItem item, IEnemy skeleton)
        {
            Sprite = EnemySpriteFactory.Instance.CreateSkeletonSprite();

            room.SpawnItem(item);
            this.item = item;

            this.position = position;
            this. direction = GetRandomDirection();

            this.MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
            this.timeUntilNextFrame = animationTime;

            this.skeleton = skeleton;
        }

        public void Attack()
        {

        }

        public void Update(GameTime gameTime, Rectangle drawingLimits)
        {
            drawingLimits = new Rectangle(32, 32, 192, 112);
            if (skeleton.StunnedTimer == 0)
            {
                totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

                if (totalElapsedSeconds >= MovementChangeTimeSeconds)
                {
                    totalElapsedSeconds -= MovementChangeTimeSeconds;
                    direction = GetRandomDirection();
                    MovementChangeTimeSeconds = GetRandomDirectionMovementChangeTimeSeconds();
                }
                    
                position += direction;
                if (item != null)
                {
                    item.Position = new Vector2(position.X - xDiff, position.Y - yDiff);
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
            const double minimumTime = 0.3;
            Random random = new Random();
            return (float)(random.NextDouble() * 1.0 + minimumTime);
        }

        private Vector2 GetRandomDirection()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            const int randomNumberMax = 4;
            var randomDirection = (CompassDirection)random.Next(randomNumberMax);

            return Vector2.Multiply(new Vector2(moveSpeed, moveSpeed), CompassDirectionUtil.GetDirectionVector(randomDirection));
        }

        public void editPosition(Vector2 amount)
        {
           
            position = Vector2.Add(position, amount);
            if (item != null)
            {
                item.Position = new Vector2(position.X - xDiff, position.Y - yDiff);
            }
        }
    }
}
