using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    public interface IEnvironment
    {
        void BehaviorUpdate(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch, Color color);
    }
}
