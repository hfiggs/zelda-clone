using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Environment
{
    public interface IEnvironment
    {
        void BehaviorUpdate();

        void Draw(SpriteBatch spriteBatch, Color color);
    }
}
