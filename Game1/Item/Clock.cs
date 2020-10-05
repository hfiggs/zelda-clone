using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Item
{

	public class Clock : IItem
	{
        private ISprite sprite;
        private Vector2 position;

		public Clock(Vector2 position)
		{
			sprite = ItemSpriteFactory.Instance.CreateClockSprite();
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            //do nothing
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, position, color);
        }
    }
}