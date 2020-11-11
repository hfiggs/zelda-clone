using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Particle
{
    class WaterfallSpray : IParticle
    {
        private ISprite sprite;
        private Vector2 position;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 50f; // ms per frame

        public WaterfallSpray(Vector2 position)
        {
            sprite = ParticleSpriteFactory.Instance.CreateWaterfallSpray();

            this.position = position;

            timeUntilNextFrame = animationTime;
        }

        public void Update(GameTime gameTime)
        {
            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                sprite.Update();
                timeUntilNextFrame += animationTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public bool ShouldDelete()
        {
            return false;
        }
    }
}
