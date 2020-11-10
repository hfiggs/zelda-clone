using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Particle
{
    class Cloud : IParticle
    {
        private ISprite sprite;
        private Vector2 position;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 200f; // ms per frame

        private float timeUntilNextFlash; // ms
        private const float flashTime = 50f; // ms per frame

        private float timeCounter; // ms
        private const float existTime = 600f; // ms

        public Cloud(Vector2 position)
        {
            sprite = ParticleSpriteFactory.Instance.CreateCloudSprite();

            this.position = position;

            timeUntilNextFrame = animationTime;
            timeUntilNextFlash = flashTime;
            timeCounter = 0;
        }

        public void Update(GameTime gameTime)
        {
            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                sprite.Update();
                timeUntilNextFrame += animationTime;
            }

            timeUntilNextFlash -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                timeUntilNextFlash += flashTime;
            }

            timeCounter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            const float halfFlashTime = flashTime / 2;
            if ((timeUntilNextFlash <= halfFlashTime) && !ShouldDelete())
            {
                sprite.Draw(spriteBatch, position, color);
            }
        }

        public bool ShouldDelete()
        {
            return timeCounter >= existTime;
        }
    }
}
