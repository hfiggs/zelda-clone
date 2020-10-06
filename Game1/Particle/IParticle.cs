using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Particle
{
    public interface IParticle
    {
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch, Color color);

        bool ShouldDelete();
    }
}
