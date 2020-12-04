using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class DodongoStateDown : IEnemyState
    {
        private IEnemy dodongo;

        private Vector2 position;

        public ISprite Sprite { get; private set; }

        private float timeUntilNextFrame; // ms
        private const float animationTime = 150f; // ms per frame

        private float timeUntilNewDirection;
        private const float moveTime = 1500f; // ms

        private const float moveSpeed = 0.6f;

        public DodongoStateDown(IEnemy dodongo, Vector2 position)
        {
            Sprite = EnemySpriteFactory.Instance.CreateDodongoDownSprite();

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
            return new Vector2(0,1);
        }

        public List<Rectangle> GetHitboxes()
        {
            const int xDiff = 9;
            const int yDiffHead = 32;
            const int yDiffBody = 8;
            const int width = 15;
            const int headHeight = 4;
            const int bodyHeight = 24;

            List<Rectangle> hitboxList = new List<Rectangle>();
            hitboxList.Add(new Rectangle((int)position.X + xDiff, (int)position.Y + yDiffBody, width, bodyHeight));
            hitboxList.Add(new Rectangle((int)position.X + xDiff, (int)position.Y + yDiffHead, width, headHeight));
            return hitboxList;
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            if (!dodongo.ShouldRemove())
            {
                position.Y += moveSpeed;

                // Sprite updating
                timeUntilNextFrame -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

                if (timeUntilNextFrame <= 0)
                {
                    Sprite.Update();
                    timeUntilNextFrame += animationTime;
                }

                if (dodongo.StunnedTimer == 0)
                {
                    // State updating
                    timeUntilNewDirection -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

                    if (timeUntilNewDirection <= 0)
                    {
                        const int randomNumberMax = 4, goUp = 0, goDown = 1, goLeft = 2, goRight = 3; ;
                        switch ((new Random()).Next(randomNumberMax))
                        {
                            case goUp:
                                dodongo.SetState(new DodongoStateUp(dodongo, position));
                                break;
                            case goDown:
                                timeUntilNewDirection += moveTime;
                                break;
                            case goLeft:
                                dodongo.SetState(new DodongoStateLeft(dodongo, position));
                                break;
                            case goRight:
                                dodongo.SetState(new DodongoStateRight(dodongo, position));
                                break;
                        }
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
