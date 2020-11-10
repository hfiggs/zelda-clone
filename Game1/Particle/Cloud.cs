using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;

namespace Game1.Particle
{
    class Cloud : IParticle
    {
        private ISprite sprite;
        private Vector2 position;

        private bool flash;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 200f; // ms per frame

        private float timeCounter = 0; // ms
        private const float existTime = 600f; // ms

        public Cloud(Vector2 position, bool flash = true)
        {
            sprite = ParticleSpriteFactory.Instance.CreateCloudSprite();

            this.position = position;

            timeUntilNextFrame = animationTime;
            this.flash = flash;
        }

        public void Update(GameTime gameTime)
        {
            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                sprite.Update();
                timeUntilNextFrame += animationTime;
            }

            timeCounter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (flash && !ShouldDelete())
            {
                sprite.Draw(spriteBatch, position, color);
            }
            flash = !flash;
        }

        public bool ShouldDelete()
        {
            return timeCounter >= existTime;
        }
    }
}
