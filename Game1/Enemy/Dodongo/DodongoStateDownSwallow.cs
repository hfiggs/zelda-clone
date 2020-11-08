using Game1.Sprite;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class DodongoStateDownSwallow : IEnemyState
    {
        private IEnemy dodongo;

        private Vector2 position;

        public ISprite Sprite { get; private set; }

        private float timeUntilNewDirection;
        private const float moveTime = 1000f; // ms

        public DodongoStateDownSwallow(IEnemy dodongo, Vector2 position)
        {
            Sprite = EnemySpriteFactory.Instance.CreateDodongoDownDeadSprite();

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
            return new Vector2(0, 1);
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
                // State updating
                timeUntilNewDirection -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

                if (timeUntilNewDirection <= 0)
                {
                    dodongo.SetState(new DodongoStateDown(dodongo, position));
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
