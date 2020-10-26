﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Enemy
{
    class SnakeStateMoving : IEnemyState
    {
        private Vector2 position;

        private Random rand;

        private bool isFacingLeft;

        // N=0, S=1, E=2, W=3
        private int moveDirection;

        private float timeUntilNewDirection;
        private const float moveTime = 1000f; // ms

        private const int normalSpeed = 1;
        private const int fastSpeed = 4;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 150f; // ms per frame

        public ISprite Sprite { get; private set; }

        public SnakeStateMoving(Vector2 position)
        {
            rand = new Random();

            isFacingLeft = rand.Next(2) == 0;
            Sprite = isFacingLeft ? EnemySpriteFactory.Instance.CreateSnakeLeftSprite() : EnemySpriteFactory.Instance.CreateSnakeRightSprite();

            this.position = position;

            timeUntilNewDirection = moveTime;

            moveDirection = rand.Next(4);

            timeUntilNextFrame = animationTime;
        }

        public void Attack()
        {
            // Cannot attack
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, position, color);
        }

        public void editPosition(Vector2 amount)
        {
            position = Vector2.Add(position, amount);
        }

        public Vector2 GetDirection()
        {
            // N=0, S=1, E=2, W=3
            switch (moveDirection)
            {
                case 0:
                    return new Vector2(0, -1);
                case 1:
                    return new Vector2(0, 1);
                case 2:
                    return new Vector2(1, 0);
                case 3:
                default:
                    return new Vector2(-1, 0);
            }
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X, (int)position.Y, 15, 15);
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            timeUntilNewDirection -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNewDirection <= 0)
            {
                moveDirection = rand.Next(4);

                timeUntilNewDirection += moveTime;
            }

            if (isFacingLeft && moveDirection == 2)
            {
                isFacingLeft = false;
                Sprite = EnemySpriteFactory.Instance.CreateSnakeRightSprite();
            }
            else if (!isFacingLeft && moveDirection == 3)
            {
                isFacingLeft = true;
                Sprite = EnemySpriteFactory.Instance.CreateSnakeLeftSprite();
            }

            int speed = normalSpeed;

            switch (moveDirection)
            {
                case 0:
                    position.Y -= speed;
                    break;
                case 1:
                    position.Y += speed;
                    break;
                case 2:
                    position.X += speed;
                    break;
                case 3:
                    position.X -= speed;
                    break;
            }

            timeUntilNextFrame -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                Sprite.Update();
                timeUntilNextFrame += animationTime;
            }
        }
    }
}