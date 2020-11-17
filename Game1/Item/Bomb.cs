using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Item
{
	public class Bomb : IItem
	{
        private ISprite sprite;

        public Vector2 Position { get; set; }

        public Bomb(Vector2 position)
		{
            sprite = ItemSpriteFactory.Instance.CreateBombSprite();

            Position = position;
		}

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, Position, color);
        }

        public void Update(GameTime gameTime)
        {
            //Do Nothing
        }

        public Rectangle GetHitbox()
        {
            const int xAndYDiff = 10, widthAndHeight = 20;
            return new Rectangle((int)Position.X + xAndYDiff, (int)Position.Y + xAndYDiff, widthAndHeight, widthAndHeight);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}