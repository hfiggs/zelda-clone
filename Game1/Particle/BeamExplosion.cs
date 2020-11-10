using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Particle
{
    class BeamExplosion : IParticle
    {
        private ISprite spriteNW;
        private ISprite spriteNE;
        private ISprite spriteSE;
        private ISprite spriteSW;

        private Vector2 position;
        private Vector2 positionMod = new Vector2(5.0f, 6.0f);
        private Vector2 positionModStep = new Vector2(1.0f, 1.0f);

        private float timeUntilNextFrame; // ms
        private const float animationTime = 30f; // ms per frame

        private float timeCounter = 0; // ms
        private const float existTime = 300f; // ms

        public BeamExplosion(Vector2 position)
        {
            spriteNW = ParticleSpriteFactory.Instance.CreateBeamExplosionNWSprite();
            spriteNE = ParticleSpriteFactory.Instance.CreateBeamExplosionNESprite();
            spriteSE = ParticleSpriteFactory.Instance.CreateBeamExplosionSESprite();
            spriteSW = ParticleSpriteFactory.Instance.CreateBeamExplosionSWSprite();

            this.position = position;

            timeUntilNextFrame = animationTime;
        }

        public void Update(GameTime gameTime)
        {
            timeUntilNextFrame -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeUntilNextFrame <= 0)
            {
                spriteNW.Update();
                spriteNE.Update();
                spriteSE.Update();
                spriteSW.Update();
                timeUntilNextFrame += animationTime;
            }

            timeCounter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            positionMod += positionModStep;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (!ShouldDelete())
            {
                spriteNW.Draw(spriteBatch, position - positionMod, color);
                spriteNE.Draw(spriteBatch, position + new Vector2(positionMod.X, -positionMod.Y), color);
                spriteSE.Draw(spriteBatch, position + new Vector2(-positionMod.X, positionMod.Y), color);
                spriteSW.Draw(spriteBatch, position + positionMod, color);
            }
        }

        public bool ShouldDelete()
        {
            return timeCounter >= existTime;
        }
    }
}
