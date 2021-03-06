﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class DodongoStateRightSwallow : IEnemyState
    {
        private IEnemy dodongo;

        private Vector2 position;

        public ISprite Sprite { get; private set; }

        private float timeUntilNewDirection;
        private const float moveTime = 1500f; // ms

        public DodongoStateRightSwallow(IEnemy dodongo, Vector2 position)
        {
            Sprite = EnemySpriteFactory.Instance.CreateDodongoRightDeadSprite();

            this.dodongo = dodongo;

            this.position = position;

            timeUntilNewDirection = moveTime;
        }

        public void Attack()
        {
            // Cannot attack
        }

        public Vector2 GetPosition()
        {
            return position;
        }
        public Vector2 GetDirection()
        {
            return new Vector2(1, 0);
        }

        public List<Rectangle> GetHitboxes()
        {
            const int yDiff = 9;
            const int xDiffHead = 26;
            const int xDiffBody = 2;
            const int height = 15;
            const int headWidth = 4;
            const int bodyWidth = 24;

            List<Rectangle> hitboxList = new List<Rectangle>();
            hitboxList.Add(new Rectangle((int)position.X + xDiffBody, (int)position.Y + yDiff, bodyWidth, height));
            hitboxList.Add(new Rectangle((int)position.X + xDiffHead, (int)position.Y + yDiff, headWidth, height));
            return hitboxList;
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            if (!dodongo.ShouldRemove())
            {
                if (dodongo.StunnedTimer == 0)
                {
                    // State updating
                    timeUntilNewDirection -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

                    if (timeUntilNewDirection <= 0)
                    {
                        dodongo.SetState(new DodongoStateRight(dodongo, position));
                    }
                }
            }
        }

        public void editPosition(Vector2 amount)
        {
            position = Vector2.Add(position, amount);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, position, color);
        }
    }
}
