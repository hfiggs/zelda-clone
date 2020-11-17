using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class DodongoStateRight : IEnemyState
    {
        private IEnemy dodongo;

        private Vector2 position;

        public ISprite Sprite { get; private set; }
        public bool bombToSwallow;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 150f; // ms per frame

        private float timeUntilNewDirection;
        private const float moveTime = 1000f; // ms

        private const float moveSpeed = 0.6f;

        public DodongoStateRight(IEnemy dodongo, Vector2 position)
        {
            this.dodongo = dodongo;

            Sprite = EnemySpriteFactory.Instance.CreateDodongoRightSprite();

            this.position = position;

            bombToSwallow = false;

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
            return new Vector2(1,0);
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
                position.X += moveSpeed;

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
                                dodongo.SetState(new DodongoStateDown(dodongo, position));
                                break;
                            case goLeft:
                                dodongo.SetState(new DodongoStateLeft(dodongo, position));
                                break;
                            case goRight:
                                timeUntilNewDirection += moveTime;
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
