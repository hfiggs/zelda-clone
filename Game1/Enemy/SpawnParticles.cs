using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Enemy
{
    class SpawnParticles : IEnemy
    {
        ISprite sprite;

        Vector2 position;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 150f; // ms per frame

        public SpawnParticles(Vector2 position)
        {
            sprite = ParticleSpriteFactory.Instance.CreateCloudSprite();

            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public void ReceiveDamage(float amount, Vector2 direction)
        {
            // Cannot receive damage
        }

        public void Update(GameTime gameTime, Rectangle drawingLimits5)
        {
            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                sprite.Update();
                timeUntilNextFrame += animationTime;
            }
        }

        public void editPosition(Vector2 amount)
        {
            //Does nothing!
        }

        public bool shouldRemove()
        {
            return false;
        }

        public void SetState(IEnemyState state)
        {
            //Do Nothing
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X, (int)position.Y, 16, 16);
        }

        public void ReceiveDamage()
        {
            //throw new System.NotImplementedException();
        }

        public void SpawnAnimation()
        {
            throw new System.NotImplementedException();
        }
    }
}
