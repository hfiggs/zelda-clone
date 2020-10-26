using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Item
{
    public interface IItem
    {
        Vector2 Position {get; set;}
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Color color);
        Rectangle GetHitbox();
        bool ShouldDelete { get; set; }
    }
}
