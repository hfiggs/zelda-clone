﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Item
{

	public class ArrowItem : IItem
	{
        private ISprite sprite;
        public Vector2 Position { get; set; }

        public ArrowItem(Vector2 Position)
		{
			sprite = ItemSpriteFactory.Instance.CreateArrowItemSprite();
            this.Position = Position;
        }

        public void Update(GameTime gameTime)
        {
            //do nothing
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            sprite.Draw(spriteBatch, Position, color);
        }

        public Rectangle GetHitbox()
        {
            const int xOffset = 17, yOffset = 11, width = 7, height = 18;
            return new Rectangle((int)Position.X + xOffset, (int)Position.Y + yOffset, width, height);
        }

        public bool ShouldDelete { get; set; } = false;
    }
}