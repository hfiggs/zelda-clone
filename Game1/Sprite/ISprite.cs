using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Sprite
{
    public interface ISprite
    {
        void Update();
        void Draw(SpriteBatch spriteBatch, Vector2 position, Color color);
    }
}
