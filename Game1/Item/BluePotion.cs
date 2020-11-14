using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Item
{
    public class BluePotion : IItem
    {
        Color color = Color.White;
        private ISprite sprite;
        public Vector2 Position { get; set; }

        public BluePotion(Vector2 Position)
        {
            sprite = ItemSpriteFactory.Instance.CreateBluePotionSprite();

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
            const int xAndYDiff = 10, widthAndHeight = 20;
            return new Rectangle((int)Position.X + xAndYDiff, (int)Position.Y + xAndYDiff, widthAndHeight, widthAndHeight);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}

