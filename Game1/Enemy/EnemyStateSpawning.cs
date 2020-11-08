/* Author: Hunter Figgs */

using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1.Enemy
{
    class EnemyStateSpawning : IEnemyState
    {
        private Vector2 position;
        private IEnemy enemy;
        private IEnemyState nextState;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 150f; // ms per frame

        private float timeUntilNextState;
        private const float spawnTime = 450f; // ms

        public ISprite Sprite { get; private set; }

        public EnemyStateSpawning(Vector2 spawnPosition, IEnemy enemy, IEnemyState nextState)
        {
            position = spawnPosition;

            this.nextState = nextState;
            this.enemy = enemy;

            Sprite = ParticleSpriteFactory.Instance.CreateCloudSprite();

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
            const int heightAndWidth = 16;
            hitboxList.Add(new Rectangle((int)position.X, (int)position.Y, heightAndWidth, heightAndWidth));
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
                enemy.SetState(nextState);
            }
        }
    }
}
