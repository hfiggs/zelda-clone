using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;

namespace Game1.Particle
{
    class BombOverlay : IParticle
    {
        private ISprite sprite;
        private Vector2 position = new Vector2(0.0f, 0.0f);

        private bool flash;

        private float timeCounter; // ms
        private const float existTime = 100f; // ms - max time flash will happen

        private const int flashNum = 2;
        private int flashes = 0;

        public BombOverlay(Color color, bool flash = true)
        {
            sprite = ParticleSpriteFactory.Instance.CreateFlashOverlay(color);

            timeCounter = 0;
            this.flash = flash;
        }

        public void Update(GameTime gameTime)
        {
            timeCounter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (flashes < flashNum)
            {
                if (flash && !ShouldDelete())
                {
                    sprite.Draw(spriteBatch, position, color);
                    flashes++;
                }
            }
            flash = !flash;
        }

        public bool ShouldDelete()
        {
            return timeCounter >= existTime;
        }
    }
}
