/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class EnemyStateDying : IEnemyState
    {
        private Vector2 position;
        private float timeUntilNextFrame; // ms
        private const float animationTime = 100f; // ms per frame

        private float timeUntilNextState;
        private const float spawnTime = 600f; // ms

        public bool dead = false;

        public ISprite Sprite { get; private set; }

        public EnemyStateDying(IEnemy enemy, Vector2 position)
        {
            enemy.StunnedTimer = 0;
            this.position = position;
            Sprite = EnemySpriteFactory.Instance.CreateEnemyDyingSprite();

            timeUntilNextFrame = animationTime;
            timeUntilNextState = spawnTime;
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
            // Cannot edit position (no collision)
        }

        public Vector2 GetDirection()
        {
            // Has no direction when spawning
            return new Vector2(0, 0);
        }

        public List<Rectangle> GetHitboxes()
        {
            List<Rectangle> hitboxList = new List<Rectangle>();
            hitboxList.Add(new Rectangle(0, 0, 0, 0));
            return hitboxList;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void Update(GameTime gametime, Rectangle drawingLimits)
        {
            timeUntilNextFrame -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                Sprite.Update();
                timeUntilNextFrame += animationTime;
            }

            timeUntilNextState -= (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextState <= 0)
            {
                dead = true;
            }

        }

        public bool ShouldRemove()
        {
            return dead;
        }
    }
}
