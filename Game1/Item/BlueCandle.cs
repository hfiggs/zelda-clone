using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Item
{
    public class BlueCandle : IItem
    {
        Color color = Color.White;
        private ISprite sprite;
        public Vector2 Position { get; set; }

        public BlueCandle(Vector2 Position)
        {
            sprite = ItemSpriteFactory.Instance.CreateBlueCandleSprite();

            this.Position = Position;
        }

        public void Draw(SpriteBatch spriteBatch, Color UNUSED)
        {
            sprite.Draw(spriteBatch, Position, color);
        }

        public void Update(GameTime gameTime)
        {
            // Do Nothing
        }

        public Rectangle GetHitbox()
        {
            const int xOffset = 16, yOffset = 11, width = 10, height = 18;
            return new Rectangle((int)Position.X + xOffset, (int)Position.Y + yOffset, width, height);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}
