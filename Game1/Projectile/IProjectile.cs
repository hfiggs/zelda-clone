using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Projectile
{
    public interface IProjectile
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Color color);
    }
}
