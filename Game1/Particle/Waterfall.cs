using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Particle
{
    class Waterfall : IParticle
    {
        private ISprite sprite;
        private Vector2 position;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 80f; // ms per frame

        public Waterfall(Vector2 position, int frameOffset = 0)
        {
            sprite = ParticleSpriteFactory.Instance.CreateWaterfall();

            this.position = position;

            timeUntilNextFrame = animationTime;

            for(int i = 0; i < frameOffset; i++)
            {
                sprite.Update();
            }
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
