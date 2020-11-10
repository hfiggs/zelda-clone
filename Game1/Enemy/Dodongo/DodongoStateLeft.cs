using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class DodongoStateLeft : IEnemyState
    {
        private IEnemy dodongo;

        private Vector2 position;

        public ISprite Sprite { get; private set; }

        private float timeUntilNextFrame; // ms
        private const float animationTime = 150f; // ms per frame

        private float timeUntilNewDirection;
        private const float moveTime = 1000f; // ms

        private const float moveSpeed = 0.6f;

        public DodongoStateLeft(IEnemy dodongo, Vector2 position)
        {
            Sprite = EnemySpriteFactory.Instance.CreateDodongoLeftSprite();

            this.dodongo = dodongo;

            this.position = position;

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

        public Vector2 GetDirection()
        {
            return new Vector2(-1,0);
        }

        public List<Rectangle> GetHitboxes()
        {
            const int yDiff = 9;
            const int xDiffHead = 2;
            const int xDiffBody = 6;
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
                    const int randomNumberMax = 4;
                    switch ((new Random()).Next(randomNumberMax))
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
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite.Draw(spriteBatch, position, color);
        }

        public void editPosition(Vector2 amount)
        {
            position = Vector2.Add(amount, position);
        }
    }
}
