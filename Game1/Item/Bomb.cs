using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Item
{
	public class Bomb : IItem
	{
        private ISprite sprite;

        public Vector2 Position { get; set; }

		public Bomb(Vector2 Position)
		{
            sprite = ItemSpriteFactory.Instance.CreateBombSprite();

            this.Position = Position;
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
            return new Rectangle((int)Position.X + 10, (int)Position.Y + 10, 20, 20);
        }
    }
}