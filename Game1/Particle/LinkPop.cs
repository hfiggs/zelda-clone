using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Particle
{
    class LinkPop : IParticle
    {
        private ISprite sprite;
        private Vector2 position;

        private float timeUntilNextFrame; // ms
        private const float animationTime = 80f; // ms per frame

        private const float maxAge = 240.0f;
        private float age = 0;

        private float delay;

        private bool remove = false;

        public LinkPop(Vector2 position, float delay)
        {
            sprite = ParticleSpriteFactory.Instance.CreateLinkPop();

            this.position = position;
            this.delay = delay;

            timeUntilNextFrame = animationTime;
        }

        public void Update(GameTime gameTime)
        {
            age += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (age > maxAge + delay)
            {
                remove = true;
            }
            else
            {
                if (age > delay)
                {
                    timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (timeUntilNextFrame <= 0)
                    {
                        sprite.Update();
                        timeUntilNextFrame += animationTime;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {            
            if(age > delay && age < maxAge + delay) {
                sprite.Draw(spriteBatch, position, color, Util.SpriteLayerUtil.topLayer);
            }
        }

        public bool ShouldDelete()
        {
            return remove;
        }
    }
}