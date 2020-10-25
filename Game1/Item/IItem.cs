using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Item
{
    public interface IItem
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Color color);
        Rectangle GetHitbox();
        bool ShouldDelete { get; set; }
    }
}
