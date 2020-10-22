﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class DodongoStateLeft : IEnemyState
    {
        private IEnemy dodongo;

        private Vector2 position;

        private bool isDead;

        public ISprite Sprite { get; private set; }

        private float timeUntilNextFrame; // ms
        private const float animationTime = 150f; // ms per frame

        private float timeUntilNewDirection;
        private const float moveTime = 1000f; // ms

        private const int moveSpeed = 1;

        public DodongoStateLeft(IEnemy dodongo, Vector2 position)
        {
            Sprite = EnemySpriteFactory.Instance.CreateDodongoLeftSprite();

            this.dodongo = dodongo;

            this.position = position;

            isDead = false;

            timeUntilNextFrame = animationTime;

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

        public void ReceiveDamage()
        {
            // Cannot receive damage
        }

        public Vector2 GetDirection()
        {
            return new Vector2(-1,0);
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            if (!isDead)
            {
                position.X -= moveSpeed;

                // Sprite updating
                timeUntilNextFrame -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

                if (timeUntilNextFrame <= 0)
                {
                    Sprite.Update();
                    timeUntilNextFrame += animationTime;
                }

                // State updating
                timeUntilNewDirection -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

                if (timeUntilNewDirection <= 0)
                {
                    switch((new Random()).Next(4))
                    {
                        case 0:
                            dodongo.SetState(new DodongoStateUp(dodongo, position));
                            break;
                        case 1:
                            dodongo.SetState(new DodongoStateDown(dodongo, position));
                            break;
                        case 2:
                            timeUntilNewDirection += moveTime;
                            break;
                        case 3:
                            dodongo.SetState(new DodongoStateRight(dodongo, position));
                            break;
                    }
                }

                // TODO: Determine if dodongo will swallow a 2nd bomb, if so set isDead = true
            }
            else
            {
                Sprite = EnemySpriteFactory.Instance.CreateDodongoLeftDeadSprite();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, position, Color.White);
        }
    }
}
