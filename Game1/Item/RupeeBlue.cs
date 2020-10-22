using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Item
{
	public class RupeeBlue : IItem
	{
        private ISprite sprite;

        private Vector2 position;

        public RupeeBlue(Vector2 position)
		{
            sprite = ItemSpriteFactory.Instance.CreateBlueRupeeSprite();

            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            //Do Nothing
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)position.X + 10, (int)position.Y + 10, 20, 20);
        }
    }
}