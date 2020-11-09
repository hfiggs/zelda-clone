using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;

namespace Game1.Particle
{
    class ShieldDeflect : IParticle
    {
        private ISprite sprite;
        private Vector2 position;

        private const int existFrames = 2;
        private int frames = 0;
        private bool exist = true;

        public ShieldDeflect(Vector2 position)
        {
            sprite = ParticleSpriteFactory.Instance.CreateShieldDeflect();
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            //nothing to do here
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (frames < existFrames)
            {
                sprite.Draw(spriteBatch, position, color);
                frames++;
            }
            else
            {
                exist = false;
            }
        }

        public bool ShouldDelete()
        {
            return !exist;
        }
    }
}
